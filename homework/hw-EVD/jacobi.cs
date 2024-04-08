using static vector;
using static matrix;
using System;


public class EVD{
    public vector w;
    public matrix V;
    public matrix D;
    public static void Jtimes(matrix A, int p, int q, double theta){
        int n = A.size1;
        double c=Math.Cos(theta),s=Math.Sin(theta);
        for(int i = 0; i<n-1; i++){
            double api = A[p,i];
            double aqi = A[q,i];
            A[p,i] = api*c + aqi*s;
            A[q,i] = -api*s+ api*c;
        }
    }
    public static void timesJ(matrix A, int p, int q, double theta){
        double c=Math.Cos(theta),s=Math.Sin(theta);
        for(int j = 0; j<A.size1; j++){
            double ajp = A[j,p];
            double ajq = A[j,q];
            A[j,p] = ajp*c - ajq*s;
            A[j,q] = ajp*s+ ajp*c;
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
    
    public EVD(matrix M){
	    matrix A = M.copy();
        w = new vector(M.size1);
	    V = matrix.id(M.size1);
        cyclic(A,V);
        matrix D = V.T * A * V;
        for(int i=0; i< D.size1; i++){
            w[i] = D[i,i];
                }
            }
	/* run Jacobi rotations on A and update V */
	/* make matrix D which is diagonal with eigenvalues and w which is the vectors that make up D*/
	}
