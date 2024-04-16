using static vector;
using static matrix;
using System;


public class Splines{
    public double linear_interpolation;
    public double linterp_integration;
    public double integration_bites;
    //ting du kan kalde

    public static int binsearch(double[] x, double z){
	    if( z<x[0] || z>x[x.Length-1] ) {throw new Exception("z outside interval\n");}
        else{
	        int i=0, j=x.Length-1;
	        while(j-i>1){
		        int mid=(i+j)/2;
		        if(z>x[mid]) {i=mid;}
                else {j=mid;}
		    }
            return i;
        }
	}

    public static double linterp(double[] x, double[] y, double z){
        int i = binsearch(x,z);
        double dx=x[i+1]-x[i]; 
        if(!(dx>0)) throw new Exception("xs not in order\n");
        double dy=y[i+1]-y[i];
        return y[i]+dy/dx*(z-x[i]);
        }

    public static double linterpInteg(double[] x, double[] y, double z){
        //integration fra x[0] til z, de skal alle lægges sammen
        int i = binsearch(x,z);
        double integrand = 0.0;
        for (int j=0; j<i; j++){
            double dx = x[j+1]-x[j];
            if(!(dx>0)){ throw new Exception("xs not in order \n");}
            double dy = y[j+1]-y[j];
            integrand += y[j]*(z-x[j])+dy/dx*(Math.Pow(z-x[j],2)/2);
            //integrand += y[j] * dx + 0.5 * dy / dx * (dx * dx);
            }
        //plus from x[i] to z
        double last_dx = z-x[i];
        double last_dy = (y[i+1]-y[i])/(x[i+1]-x[i]);
        //trekant plus stykke under
        integrand += y[i]*last_dx+0.5*last_dx*last_dy;
        return integrand;
        }

    //NÅET TIL OPG A.c 

    public Splines(double[] x, double[] y, double z){
        this.linear_interpolation = linterp(x,y,z);
        this.linterp_integration = linterpInteg(x,y,z);

        //nu lader vi som om vi skal have integrationen LIGE hvor z er ikke ved x0
        /*
        int j = binsearch(x,z);
        double [] local_x = {x[j],x[j+1]};
        double [] local_y = {y[j],y[j+1]};
        this.integration_bites = linterpInteg(local_x,y,z);
        */
    }
}