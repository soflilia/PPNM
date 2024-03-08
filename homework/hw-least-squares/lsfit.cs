using System.Collections.Generic;
using System;
using static matrix;
using static vector;
using static QRGS;

public class lsfit{
    public vector cks;
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
        (matrix Q, matrix R) = QRGS.decomp(A);
        cks = QRGS.solve(Q,R,b);
        }
    }
}