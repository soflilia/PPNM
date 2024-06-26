using static matrix;
using System;
using static System.Math;
using static System.Console;


class main{
static void Main(){

        // initialiser en xs og ys hvor y(x) = cos(x)
        double [] xs = {0,1,2,3,4,5,6,7,8,9};
        double [] ys = new double [xs.Length];
        for(int i=0; i<xs.Length; i++){
            double tal = xs[i];
            ys[i]=Cos(tal);
		    Write($"{xs[i]} {ys[i]}\n");
	        }
	    Write("\n\n");
        //Initialising cubic subspline med cos(x)
        cubic_subspline cubic_init  = new cubic_subspline(xs,ys);
        vector p_init = cubic_init.p;
        //Mine derivative punkter i graf pi
        for(int i = 0; i<p_init.size; i++){
            Write($"{xs[i]} {p_init[i]}\n");
        }
        Write($"\n\n");
        // graf for cubic subspline 
        for (double z =0.01; z<9; z+= 1.0/12 ){
            Write($"{z} {cubic_init.evaluate(z)} {cubic_init.derivative(z)} {cubic_init.integral(z)}\n");
        }
        Write($"\n\n");

        //....................TEST PÅ 'VÆRRE' FUNKTIONER END COS(x).............. //

        //OUTLIER FUNKTION : f(x) =  2/x, pånær x = 2, hvor f(x) = 5
        double[] xs_outlier = {1, 1.3, 1.6, 1.8, 1.9, 2, 2.05,  2.1, 2.5,   3, 4};
        double[] ys_outlier = new double[xs_outlier.Length];
        for(int i=0; i<xs_outlier.Length; i++){
            double tal = xs_outlier[i];
            ys_outlier[i]=outlier_func(tal);
		    Write($"{xs_outlier[i]} {ys_outlier[i]}\n");
	        }
        Write($"\n\n");
        //Initialising cubic subspline
        cubic_subspline cubic_outlier  = new cubic_subspline(xs_outlier,ys_outlier);
        vector p_outlier= cubic_outlier.p;
        for(int i = 0; i<p_outlier.size; i++){
            Write($"{xs_outlier[i]} {p_outlier[i]}\n");
        }
        Write($"\n\n");

        for (double z =xs_outlier[0]+0.01; z< xs_outlier[xs_outlier.Length-1]-0.01; z+= (2-xs_outlier[0]+0.01)/24 ){
            Write($"{z} {cubic_outlier.evaluate(z)} {cubic_outlier.derivative(z)} {cubic_outlier.integral(z)}\n");
        }
        Write($"\n\n");

        //DISCONTINUOUS FUNCTION f(x) = -1 for x<0 og f(x) = 2 for x>0
        double[] xs_disc = {-4,-3,-2,-1, -0.5, 1, 2, 3, 4};
        double[] ys_disc = new double[xs_disc.Length];
        for(int i=0; i<xs_disc.Length; i++){
            double tal = xs_disc[i];
            ys_disc[i]=discontinuity_func(tal);
		    Write($"{xs_disc[i]} {ys_disc[i]}\n");
	        }
        Write($"\n\n");
        //Initialising cubic subspline
        cubic_subspline cubic_disc  = new cubic_subspline(xs_disc,ys_disc);
        vector p_disc= cubic_disc.p;
        for(int i = 0; i<p_disc.size; i++){
            Write($"{xs_disc[i]} {p_disc[i]}\n");
        }
        Write($"\n\n");

        for (double z =xs_disc[0]+0.01; z< xs_disc[xs_disc.Length-1]-0.01; z+= (2-xs_disc[0]+0.01)/24 ){
            Write($"{z} {cubic_disc.evaluate(z)} {cubic_disc.derivative(z)} {cubic_disc.integral(z)}\n");
        }
        Write($"\n\n");

}

    public static double outlier_func(double x){
        if (x == 2){ return 5 ;
        }
        return 2/x;
    }

    public static double discontinuity_func(double x){
        if (x < 0){ return -1 ;
        }
        return 2;
    }
}