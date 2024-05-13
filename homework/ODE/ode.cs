using static vector;
using static matrix;
using static qspline;
using System.Collections.Generic;
using System;
using static System.Math;

public class ODE{
    public static (vector,vector) rkstep12(
    	//inputs to rkstep12
		Func<double,vector,vector> f,/* the f from dy/dx=f(x,y) */
		double x,                    /* the current value of the variable */
		vector y,                    /* the current value y(x) of the sought function */
		double h                     /* the step to be taken */){   
	    	vector k0 = f(x,y);              /* embedded lower order formula (Euler) */
	    	vector k1 = f(x+h/2,y+k0*(h/2)); /* higher order formula (midpoint) */
	    	vector yh = y+k1*h;              /* y(x+h) estimate */
	    	vector e_y = (k1-k0)*h;           /* error estimate */
	    	return (yh,e_y);
        	}

    
	public static (List<double>, List<vector>) driver(
		Func<double,vector,vector> F,/* the f from dy/dx=f(x,y) */
		(double,double) interval,    /* (start-point,end-point) */
		vector ystart,               /* y(start-point) */
		double h=0.125,              /* initial step-size */
		double acc=0.01,             /* absolute accuracy goal */
		double eps=0.01              /* relative accuracy goal */){
    		var (a,b)=interval; double x=a; vector y=ystart.copy();
    		var xlist=new List<double>(); xlist.Add(x);
    		var ylist=new List<vector>(); ylist.Add(y);
    		do{
        		if(x>=b) return (xlist,ylist); /* job done */
        		if(x+h>b) h=b-x;               /* last step should end at b */
        		var (yh,e_y) = rkstep12(F,x,y,h);
        		double tol = (acc+eps*yh.norm()) * Sqrt(h/(b-a));
        		double err = e_y.norm();
        		if(err<=tol){ // accept step
					x+=h; y=yh;
					xlist.Add(x);
					ylist.Add(y);
				}
				h *= Min( Pow(tol/err,0.25)*0.95 , 2); // readjust stepsize
        	}while(true);
    	}//driver

	
	public static Func<double,vector> make_quad_interpolant(List<double> x,List<vector> y){
		//initialising stuff
		vector x1 = new vector(x.Count);
		for(int k =0; k<x.Count; k++){x1[k]=x[k];}
		Console.Error.Write($"x.count ={x.Count}\n");
		Console.Error.Write($"y[0].size = {y[0].size}\n");
		vector solutions = new vector(y[0].size);
		List<vector> ys = new List<vector>(y[0].size);
		List<qspline> instances = new List<qspline>();
		for (int i =0; i<y[0].size; i++){
			Console.Error.Write($"we are i deep into the for loop:{i}\n");
			vector yprime = new vector(x1.size);
			for(int j=0; j<x1.size; j++){
				Console.Error.Write($"going j deeper..:{j}\n");
				yprime[j]= y[j][i];
				}
			ys.Add(yprime);
			instances.Add(new qspline(x1,ys[i]));
			}

		Func<double,vector> interpolant = delegate(double z){
			Console.Error.WriteLine($"z is {z}");
			for(int i =0; i<y[0].size; i++){solutions[i]=instances[i].evaluate(z);}
			return solutions;
			};
		return interpolant;
		}//make_quad_interpolant


	public static Func<double,vector> make_ode_ivp_interpolant(Func<double,vector,vector> f,
	(double,double) interval,
	vector y,
	double acc=0.01,double eps=0.01,double hstart=0.01)
	{
	var (xlist,ylist) = driver(f,interval,y,acc,eps,hstart);
	Console.Error.WriteLine($"z must be between {xlist[0]} and {xlist[xlist.Count-1]}");
	return make_quad_interpolant(xlist,ylist);
	}

} //ODE stop