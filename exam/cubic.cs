using static vector;
using static matrix;
using static qspline;
using static System.Math;
using static System.Console;
using static QRGS;
using System;

public class cubic_spline{
	public vector x,y,b,c,d,p;

	public cubic_spline(vector xs,vector ys){
        // initialiser derivatives fra quadspline
        this.x = xs.copy();
        this.y = ys.copy();

        quad_3_points(xs,ys);

        }
    
    public void quad_3_points(vector xs, vector ys){
        this.p = new vector(xs.size);
        for(int i=0; i<xs.size-2; i++){
            //Error.Write("MATRIX {i}\n");
            //ok herinde har du fat i et sæt med 3 points
            double[] points = {xs[i],xs[i+1],xs[i+2]};
            vector answers = new vector(new double[] {ys[i],ys[i+1],ys[i+2]});

            matrix A = new matrix(3,3);

            //Initialiserer min A-matrix vil gerne gøre det pænere
            A[0, 0] = Pow(points[0], 2);
            A[0, 1] = points[0];

            A[1, 0] = Pow(points[1], 2);
            A[1, 1] = points[1];

            A[2, 0] = Pow(points[2], 2);
            A[2, 1] = points[2];

            A[0, 2] = A[1, 2] = A[2,2] = 1;

            (matrix Q, matrix R) = QRGS.decomp(A);
            vector abc = QRGS.solve(Q,R,answers);

            // derivative of ax²+bx+c
            //sidste og første p-værdier
            if (i == 0) {p[0]=2*abc[0]*points[0]+abc[1];}
            if (i == xs.size-1) {p[i+2]=2*abc[0]*points[2]+abc[1];}
            p[i+1] = 2*abc[0]*points[1]+abc[1];

            // Display the matrix for debugging
            /*
            for (int row = 0; row < 3; row++) {
                for (int col = 0; col < 3; col++) {
                    Error.Write($"{A[row, col]}    ");
                }
                Error.Write("\n");
                */
            }
        }

    public vector spline(double x0,double x1,double y0,double y1,double p0,double p1){
        //her er x0 = xi og x1 = x(i+1) osv

        matrix A = new matrix(3,3);
        A[0,0] = x1-x0;
        A[0,1] = Pow(x1-x0,2);
        A[0,2] = Pow(x1-x0,3);

        A[1,0] = 1;
        A[1,1] = 0;
        A[1,2] = 0;

        A[2,0] = 1;
        A[2,1] = 2*(x1-x0);
        A[2,2] = 3*(Pow(x0,2)+Pow(x1,2)-2*x0*x1);

        vector answers = new vector(new double[] {y1-y0,p0,p1});

        (matrix Q1, matrix R1) = QRGS.decomp(A);
        //coeff består af : {bi,ci,di}
        vector coeff = QRGS.solve(Q1,R1,answers);

        return coeff;
        }

    public static int binsearch(vector x, double z){
	    if( z<x[0] || z>x[x.size-1] ){throw new Exception("z outside interval}\n");}
        else{
	        int i=0, j=x.size-1;
	        while(j-i>1){
		        int mid=(i+j)/2;
		        if(z>x[mid]) {i=mid;}
                else {j=mid;}
		    }
            return i;
        }
	}

    public double evaluate(double z){
        int i = binsearch(this.x,z);
        vector coffs = spline(x[i],x[i+1],y[i],y[i+1],p[i],p[i+1]);
        return y[i] + coffs[0]*(z-x[i])+ coffs[1]*Pow(z-x[i],2) + coffs[2]*(Pow(z-x[i],3));
    }


    }
