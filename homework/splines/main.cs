using static matrix;
using static QRGS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Mainspline{
    public static void Main(){
        double [] xs = {0,1,2,3,4,5,6,7,8,9};
        double [] ys = new double [xs.Length];
        for(int i=0; i<xs.Length; i++){
            double tal = xs[i];
            ys[i]=Math.Cos(tal);
            //skrive min første output til xs , ys
		    Console.WriteLine($"{xs[i]} {ys[i]}");
	        }
        //tests SKRIV ERROR FORAN
        Console.Error.Write($"checking that is does what I want: {ys[4]}\n");
        Console.Error.Write($"checking that is does what I want: {Math.Cos(4)}\n");

        //Laver mellemrum så gnuplot kan læse min outfil
	    Console.Write("\n\n");

        //Linear splines OPG A, både spline og integrale
	    for(double z=xs[0];z<=xs[xs.Length-1];z+=1.0/64){
        Console.WriteLine($"{z} {Splines.linterp(xs,ys,z)} {Splines.linterpInteg(xs,ys,z)}");

        // OPG B 
		}
        }//main stops

} // EVD STOPS
