using System;
using static System.Math;
using static System.Console;
using static QRGS;

public class cubic{
	public vector p,x,y,b,c,d;
	public cubic(vector xs,vector ys){
		this.x = xs.copy();
		this.y = ys.copy();
        this.p = new vector(xs.size-1);
        this.c = new vector(xs.size-1);
        this.d = new vector(xs.size-1);

        init_coefficients(x,y);
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

    public void init_coefficients(vector xs, vector ys){
        // der er færre bn end xs
        int n = xs.size;
        matrix A = new matrix(n,n);

        A[0,0] = 2; 
        A[n-1,n-1] = 2;

        A[0,1] = 1;

        for(int i=0; i<n-2; i++){
            double h0 = x[i+1]-x[i];
            double h1 = x[i+2]-x[i+1];
            A[i+1,i+1] = 2 * h0/h1 +2;
            //Q værdi og et-taller
            A[i+1,i+2] = h0/h1;
            A[i+1,i] = 1;
        }

        vector B = new vector(n);
        for (int i=0; i<n-1; i++){
            this.p[i] = (y[i+1]-y[i])/(x[i+1]-x[i]);
        }
        B[0] = 3 * p[0];
        B[n-1] = 3 * p[n-2];
        for (int i=1; i<n-1; i++){
            double h0 = x[i]-x[i-1];
            double h1 = x[i+1]-x[i];
            B[i] = 3 * (p[i-1]+p[i]*h0/h1);
        }

        (matrix Q, matrix R) = QRGS.decomp(A);
        this.b = QRGS.solve(Q,R,B);

        // Display the matrix for debugging
            
        for (int row = 0; row < A.size1; row++) {
            for (int col = 0; col < A.size1 ; col++) {
                Error.Write($"{A[row, col]}    ");
            }
            Error.Write("\n");
        }

        for (int i=0; i<n-1; i++){
            double hi = x[i+1]-x[i];
            this.c[i] = (-2*b[i] - b[i+1] + 3*p[i])/hi;
            this.d[i] = (b[i]+b[i+1]-2*p[i])/Pow(hi,2);
        }
    }

    public double evaluate(double z){
        int i = binsearch(this.x,z);
        return y[i] + b[i]*(z-x[i])+ c[i]*Pow(z-x[i],2) + d[i]*(Pow(z-x[i],3));
    }

	public double derivative(double z){
		int i = binsearch(this.x,z);
		return b[i]+2*c[i]*(z-x[i])+3*d[i]*Pow(z-x[i],2);
	}


	public double integral(double z){
		int i = binsearch(this.x,z);
        double integrand = 0.0;
        for (int j =0; j<i; j++){
            double dx = x[j+1]-x[j];
            integrand += y[j]*dx+ b[j]/2*Pow(dx,2) + c[j]/3*Pow(dx,3) + d[j]/4*Pow(dx,4);
        }
        double last_dx = z-x[i];
        integrand += y[i]*last_dx + b[i]/2*Pow(last_dx,2) + c[i]/3*Pow(last_dx,3) + d[i]/4*Pow(last_dx,4);
        return integrand;

	}
}
