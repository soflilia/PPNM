using System;


public class qspline{
	public vector x,y,b,c;
	public qspline(vector xs,vector ys){
		this.x = xs.copy();
		this.y = ys.copy();

		//if time I could also make a backwards recursive and then average 
		double c1 = 0.0;
		double [] cs = recursive(c1,x,y,0,new double[x.size-1]);

		int length = cs.Length;
		double [] bs = new double[length];
		//Console.Error.Write($"cs length:{length}\n");
		//Console.Error.Write($"xs size:{x.size}\n");
		//Console.Error.Write($"y size:{y.size}\n");


		for(int j = 0; j<length; j++){
			bs[j] = (ys[j+1]-ys[j])/(xs[j+1]-xs[j])-cs[j]*(xs[j+1]-xs[j]);
			}
		this.c = new vector(cs);
		this.b = new vector(bs);
		}

    public static int binsearch(vector x, double z){
	    if( z<x[0] || z>x[x.size-1] ) {throw new Exception("z outside interval\n");}
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

	private double[] recursive(double c, vector xs, vector ys, int n, double[] result){
		if(n >= xs.size-2){
			return result;}
		// de burde altid give 1 med de x'er vi bruger
		double dx_n = xs[n+1]-xs[n];
		double dx_next = xs[n+2]-xs[n+1];

		double dy_n = ys[n+1]-ys[n];
		double dy_next = ys[n+2]-ys[n+1];

		double next_c = 1/(dx_next)*(dy_next/dx_next-dy_n/dx_n-c*dx_n);
		result[n]=c;
		recursive(next_c, xs, ys, n+1, result);
		return result;
		}

	public double evaluate(double z){
		int i = binsearch(this.x,z);
		return y[i]+b[i]*(z-x[i])+c[i]*Math.Pow((z-x[i]),2);
		}

	
	public double derivative(double z){
		// only returns derivative, not xs
		int i = binsearch(this.x,z);
		return b[i]+2*c[i]*(z-x[i]);

		/*
		double[] der = new double[c.size];
		for (int i=0; i<c.size; i++){
			der[i] = b[i]+2*c[i]*z-2*x[i]*c[i];
			Console.Write($"{der[i]}\n");
			}		
		*/

	}

	public double integral(double z){
		//Returns value of integral from 0 to z
		int i = binsearch(this.x,z);
        double integrand = 0.0;
        for (int j=0; j<i; j++){
			double dx = x[j+1]-x[j];
            integrand += y[j]*dx+b[j]/2*(Math.Pow(dx,2))+c[j]/3*Math.Pow(dx,3);
            }
		double last_dx = z-x[i];
		integrand += y[i]*last_dx+b[i]/2*(Math.Pow(last_dx,2))+c[i]/3*Math.Pow(last_dx,3);
		return integrand ;
	}

	}
