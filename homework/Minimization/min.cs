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
            double dx = Max(Abs(x[i]),1)*Pow(2,-24);
            x[i]+= dx;
            double fx_plus_dx = f(x);
            if (double.IsNaN(fx_plus_dx)) {
                throw new Exception($"NaN detected in gradient calculation at index {i}, x = {x}");}
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
    double max_count = 1000, // sæt et nyt hvis du vil
    bool central = false
    ){
    count = 0;
    double λmin = 1/1024;
    vector x = x_start.copy();

    if(central==false){
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
            if (Double.IsNaN(fx)) {Error.Write($"is Nan inside minimize, iteration no: {count}");
            fx = 0;};
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
    }
    else{
        do{ /* Newton's iterations */
            (matrix H, vector f_grad) = hessian_central(f,x);
            count +=1;
            if(f_grad.norm() < acc){
                break;
                }; /* good step: accept */

            // decomposition of H(x) dx = d phi(x) i pdf
            (matrix Q,matrix R) = QRGS.decomp(H);   /* QR decomposition */
            vector dx = QRGS.solve(Q,R,-f_grad); /* Newton's step */

            double λ=1,fx=f(x);
            if (Double.IsNaN(fx)) {Error.Write($"is Nan inside minimize, iteration no: {count}");
            fx = 0;};
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
    }
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



//NY HEASSIAN OG NY GRADIENT

    public static (matrix,vector) hessian_central(Func<vector,double> f,vector x){
	matrix H = new matrix(x.size);
    //adopter notation med at f1 = first derivative og f2 = second derivative
	(vector f1, vector f2_samex) = gradient_central(f,x);
    vector x1 = x.copy();
	for(int j=0;j<x.size;j++){
		double dxj = Max(Abs(x[j]),1)*Pow(2,-13); /* for numerical gradient */
		x[j] += dxj;
        x1[j] -= dxj;
        H[j,j] = f2_samex[j];
		for(int k=0;k<x.size;k++) {
            if (j != k){
                // før _ betyder minus, efter _ betyder plus, så f_jk = f( x + dxj + dxk)
                double dxk = Max(Abs(x[k]),1)*Pow(2,-23);
                x[k] += dxk;
                x1[k] +=dxk;
                double f_jk = f(x);
                double fj_k = f(x1);
                x[k] -= 2*dxk;
                x1[k] -= 2*dxk;
                double fk_j = f(x);
                double fjk_ = f(x1);
                x[k] += dxk;
                x1[k] += dxk;
                H[j,k] = (f_jk - fj_k - fk_j + fjk_)/(4*dxj*dxk);
            }
        }
		x[j]-=dxj;
        x1[j] += dxj;
	}
	//return H;
	return ((H+H.T)/2, f1 ); // you think?
    }


    public static (vector, vector) gradient_central(Func<vector,double> f, vector x){
        vector grad = new vector(x.size);
        vector diff_grad_samex = new vector(x.size);
        double fx= f(x);
        for (int i=0; i< x.size; i++){
            double dx = Max(Abs(x[i]),1)*Pow(2,-24);
            x[i]+= dx;
            double f_plus_dx = f(x);
            x[i]-= 2*dx;
            double f_minus_dx = f(x);
            if (double.IsNaN(f_plus_dx)|| double.IsNaN(f_minus_dx)) {
                throw new Exception($"NaN detected in gradient calculation at index {i}, x = {x}");}
            grad[i] = (f_plus_dx - f_minus_dx)/(2*dx);
            diff_grad_samex[i] = (f_plus_dx - 2*fx + f_minus_dx) / Pow(dx,2);
            x[i]+= dx;
        }
        //grad.print("grad");
        return (grad, diff_grad_samex);
    }

}