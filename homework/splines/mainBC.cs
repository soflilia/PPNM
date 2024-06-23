using System;
using static System.Console;
using static System.Math;

class Mainquad{
    public static void Main(){
        double [] xs = {0,1,2,3,4,5,6,7,8,9};
        double [] ys = new double [xs.Length];
        for(int i=0; i<xs.Length; i++){
            double tal = xs[i];
            ys[i]=Math.Sin(tal);
            //skrive min første output til xs , ys
		    WriteLine($"{xs[i]} {ys[i]}");
	        }
        //Laver mellemrum så gnuplot kan læse min outfil
	    Write("\n\n");

        qspline sine_func = new qspline(xs,ys);

        for(double z=xs[0]; z<=xs[xs.Length-1]; z+=1.0/64){
            WriteLine($"{z} {sine_func.evaluate(z)} {sine_func.integral(z)}");
            }
        Write("\n\n");

        // ........... CUBIC SPLINE ...................


        //OUTLIER FUNKTION : f(x) =  2/x pånær x = 2 hvor f(x) = 5
        double[] xs_outlier = {1, 1.3, 1.6, 1.8, 1.9, 2, 2.05,  2.1, 2.5,   3, 4};
        double[] ys_outlier = new double[xs_outlier.Length];
        for(int i=0; i<xs_outlier.Length; i++){
            double tal = xs_outlier[i];
            ys_outlier[i]=outlier_func(tal);
		    Write($"{xs_outlier[i]} {ys_outlier[i]}\n");
	        }
        Write($"\n\n");
        //Initialising cubic spline
        cubic outlier = new cubic(xs_outlier,ys_outlier);

        Write($"\n\n");
        for (double z =1.01; z<3.99; z+= 1.0/24 ){
            Write($"{z} {outlier.evaluate(z)} {outlier.derivative(z)} {outlier.integral(z)}\n");
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
        //Initialising cubic spline
        cubic disc = new cubic(xs_disc,ys_disc);
        Write($"\n\n");
        for (double z = -3.9 ; z<3.99; z+= 1.0/2 ){
            Error.Write("does this work?\n");
            Write($"{z} {disc.evaluate(z)} {disc.derivative(z)} {disc.integral(z)}\n");
        }
        Write($"\n\n");



        }//main 
        


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

} // EVD STOP