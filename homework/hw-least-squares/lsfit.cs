using System.Collections.Generic;
using System;
using static matrix;
using static vector;
using static QRGS;

public class lsfit{
    public vector cks;
    public matrix cov;
    public double c1_unc;
    public double c2_unc;
    public lsfit(Func<double,double>[] funcs , vector x , vector y , vector dy){
        int n = x.size;
        int m = funcs.Length;
        matrix A = new matrix(n,m);
        vector b = new vector(n);
        for(int i=0; i<n; i++){
            b[i] = y[i]/dy[i];
        } 
        for(int k=0; k<m; k++){
            for(int i=0; i<n; i++){
                A[i,k] = funcs[k](x[i]) / dy[i];
            }
        }
        (matrix Q, matrix R) = QRGS.decomp(A);
        cks = QRGS.solve(Q,R,b);
        

        // opg b
        matrix A_in = QRGS.inverse(Q,R);
        cov = A_in*A_in.T;
        c1_unc = Math.Sqrt(cov[0,0]);
        c2_unc = Math.Sqrt(cov[1,1]);
    }
}