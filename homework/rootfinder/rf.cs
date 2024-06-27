using static vector;
using static matrix;
using System;
using static System.Math;
using static System.Console;
using static QRGS;

public class RF{

    public static matrix jacobian(Func<vector,vector> f,vector x,vector fx=null,vector dx=null){
	//laver en dx fyldt med små dx værdier
    if(dx == null) dx = x.map(xi => Max(Abs(xi),1)*Pow(2,-26));
	if(fx == null) fx = f(x);
	matrix J = new matrix(x.size);
	for(int j=0;j < x.size; j++){
		x[j]+=dx[j];
		vector df = f(x)-fx;
		for(int i=0;i < x.size;i++) J[i,j]=df[i]/dx[j];
		x[j]-=dx[j];
		}
	return J;
    }

    public static vector newton(
	Func<vector,vector> f /* the function to find the root of */
	,vector start        /* the start point */
	,double acc=1e-2     /* accuracy goal: on exit ‖f(x)‖ should be <acc */
	,vector δx=null      /* optional δx-vector for calculation of jacobian */
	,bool quad_linesearch = false
	){
        vector x = start.copy();
        vector fx = f(x);
        vector z;
        vector fz;
        do{ 
		/* Newton's iterations */
    	if(fx.norm() < acc) break; /* job done */
	    matrix J=jacobian(f,x,fx,δx);
		//Løser J*x = -f(x)
	    (matrix Q,matrix R)= QRGS.decomp(J);
	    vector Dx = QRGS.solve(Q,R,-fx); /* Newton's step */
	    double λ=1;

		if(!quad_linesearch){
			do{ /* linesearch */
				z=x+λ*Dx;
				fz=f(z);
				double λmin = 1/64;
				if( fz.norm() < (1-λ/2)*fx.norm() ) break;
				if( λ < λmin ) break;
				λ/=2;
				}while(true);
			x=z; fx=fz;}
		if(quad_linesearch){
			x = linesearch_quad(f,λ,x,Dx);
			fx = f(x);
			}
		
		}while(true);
        return x;
    }

	public static vector linesearch_quad(Func<vector,vector> f, double λ, vector x, vector dx){
		//They said recursive, so I made a recursive function
		vector z = x+λ*dx;
		vector fz = f(x+λ*dx);
		vector fx = f(x);
		double λmin = 1/64;

		//vi skal stoppe engang
		if( fz.norm() < (1-λ/2)*fx.norm() ) return x+λ*dx;
		if( λ < λmin ) return x+λ*dx;

		double phi_o = 1/2*Pow(fx.norm(),2);
		double phi_o1 = - Pow(fx.norm(),2);
		double phi_trial = 1/2 * Pow(fz.norm(),2);

		double c = (phi_trial - phi_o - phi_o1*λ)/Pow(λ,2);
		double λ_next = - phi_o1/(2*c);
		return linesearch_quad(f,λ_next, x, dx);
	}

} //ODE stop