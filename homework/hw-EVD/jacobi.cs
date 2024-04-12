using static vector;
using static matrix;
using System;


public class EVD{
    public vector w;
    public matrix V;
    public matrix D;
    // ground means lowest eigenvalue and corresponding eigenvector
    public vector ground_eigenvec;
    public double ground_value;
    public static void Jtimes(matrix A, int p, int q, double theta){
        int n = A.size1;
        double c=Math.Cos(theta),s=Math.Sin(theta);
        for(int i = 0; i<n; i++){
            double api = A[p,i];
            double aqi = A[q,i];
            A[p,i] = c*api+s*aqi;
            A[q,i] = -s*api+ c*aqi;
        }
    }
    public static void timesJ(matrix A, int p, int q, double theta){
        double c=Math.Cos(theta),s=Math.Sin(theta);
        for(int j = 0; j<A.size1; j++){
            double ajp = A[j,p];
            double ajq = A[j,q];
            A[j,p] = ajp*c - ajq*s;
            A[j,q] = ajp*s+ ajq*c;
        } 
    }
    public static (matrix,matrix) cyclic(matrix A, matrix V){
        bool changed;
        int n = A.size1;

        do{
            changed = false;
            for(int p=0;p<n-1; p++){
                for(int q=p+1; q<n; q++){
                    double apq = A[p,q], app= A[p,p], aqq=A[q,q];
                    double theta = 0.5*Math.Atan2(2*apq, aqq-app);
                    double c = Math.Cos(theta), s = Math.Sin(theta); 
		            double new_app=c*c*app-2*s*c*apq+s*s*aqq;
		            double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
                    double epsilon = 0.000000001;
                    if(Math.Abs(new_app-app) > epsilon || Math.Abs(new_aqq-aqq) > epsilon){
                        changed = true;
                        //V.print();
                        timesJ(A,p,q,theta);
                        Jtimes(A,p,q,-theta);
                        timesJ(V,p,q,theta); //hvad er V=?
                    }
                }
            }
        }
        while(changed);
        //A.print();
        return (A,V);
        }
    
    public EVD(matrix A){
        w = new vector(A.size1);
	    V = matrix.id(A.size1);
        cyclic(A,V);
        this.D = V.T * A * V;
        for(int i=0; i< D.size1; i++){
            this.w[i] = D[i,i];
            }
        //finding groundstate eigenvalue and eigenvector
        
        this.ground_value= this.w[0];
        this.ground_eigenvec = this.V[0];
        for(int i=1; i<this.w.size; i++){
            if(this.w[i]<this.ground_value){
                this.ground_value = this.w[i];
                this.ground_eigenvec = this.V[i];
                }
            }
        }
	/* run Jacobi rotations on A and update V */
	/* make matrix D which is diagonal with eigenvalues and w which is the vectors that make up D*/
	}

