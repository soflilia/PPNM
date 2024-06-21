using static matrix;
using System;
using static System.Console;


class main{
static void Main(){
        // initialiser en xs og ys
        double [] xs = {0,1,2,3,4,5,6,7,8,9};
        double [] ys = new double [xs.Length];
        for(int i=0; i<xs.Length; i++){
            double tal = xs[i];
            ys[i]=Math.Cos(tal);
            //skrive min første output til xs , ys
		    Write($"{xs[i]} {ys[i]}\n");
	        }

        //Laver mellemrum så gnuplot kan læse min outfil
	    Write("\n\n");

        cubic_spline cubic_init  = new cubic_spline(xs,ys);
        vector p_init = cubic_init.p;
        Error.Write($"p size is {p_init.size} when xs is {xs.Length}");
        for(int i = 0; i<p_init.size; i++){
            Write($"{xs[i]} {p_init[i]}\n");
        }
        Write($"\n\n");
        //double eval = cubic_init.evaluate(1);
        //Write($"{eval}\n");

        for (double z =0; z<10; z+= 1.0/64 ){
            Write($"{z} {cubic_init.evaluate(z)} \n");
        }
        

        //Linear splines OPG A, både spline og integrale
	    /*
        for(double z=xs[0];z<=xs[xs.Length-1];z+=1.0/64){
        WriteLine($"{z} {Splines.linterp(xs,ys,z)} {Splines.linterpInteg(xs,ys,z)}");
		}
        */
}
}