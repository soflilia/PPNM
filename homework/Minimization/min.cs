using static vector;
using static matrix;
using System;
using static System.Math;
using static System.Console;
using static QRGS;

public class MIN{
    public double count;
    public double value_min;
    public vector min_point;



    public static vector gradient(Func<vector,double> f, vector x){
        vector grad = new vector(x.size);
        double fx= f(x);
        for (int i=0; i< x.size; i++){
            double dx = Max(Abs(x[i]),1)*Pow(2,-26);
            x[i]+= dx;
            grad[i] = (f(x)- fx)/dx;
            x[i]-= dx;
        }
        //grad.print("grad");
        return grad;
    }

    public MIN(
	Func<vector,double> f, /* objective function */
    vector x_start, /* starting point */
	double acc=1e-3,  /* accuracy goal, on exit |∇φ| should be < acc */
    double max_count = 1000 // sæt et nyt hvis du vil
    ){
    count = 0;
    double λmin = 1/1024;
    vector x = x_start.copy();

	do{ /* Newton's iterations */
		var f_grad= gradient(f,x);
        count +=1;
		if(f_grad.norm() < acc){
            //Error.Write("gradient nonexistent!\n");
            //Error.Write($"={f_grad.norm()}\n");
            break;
            }; /* good step: accept */
		var H = hessian(f,x);
        // decomposition of H(x) dx = d phi(x) i pdf
		(matrix Q,matrix R) = QRGS.decomp(H);   /* QR decomposition */
		vector dx = QRGS.solve(Q,R,-f_grad); /* Newton's step */

		double λ=1,fx=f(x);
		do{ /* linesearch */
			if( f(x+λ*dx) < fx ){
            //Error.Write("good step!\n");
            break;
            }; /* good step: accept */
			λ/=2;
		}while(λ > λmin);
		x+=λ*dx;
	}while(count<max_count);
    value_min = f(x);
	min_point = x;
    }//newton

    public static matrix hessian(Func<vector,double> f,vector x){
	matrix H = new matrix(x.size);
	vector f_grad=gradient(f,x);
	for(int j=0;j<x.size;j++){
		double dx = Max(Abs(x[j]),1)*Pow(2,-13); /* for numerical gradient */
		x[j]+=dx;
        // dette er diff af en diff = dobelt diff
		vector diff_grad = gradient(f,x)-f_grad;
		for(int i=0;i<x.size;i++) H[i,j] = diff_grad[i]/dx;
		x[j]-=dx;
	}
	//return H;
	return (H+H.T)/2; // you think?
    }


}