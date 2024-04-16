using static matrix;
using static QRGS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Mainspline{
    public static void Main(){
        double [] xs = {0,1,2,3,4,5,6,7,8,9};
        // Using Linq I make a list where ys that take xs into => ys =f(xs)
        double [] ys = xs.Select(x=> Math.Cos(x)).ToArray();
        //Console.Write($"checking that is does what I want: {ys[6]}\n");

        var points1 = make_spline(xs,ys);
        double [] points_x = points1.Item1;
        double [] points_s = points1.Item2;
        var point1 = make_splineintegration(xs,ys);
        double [] points_int = point1.Item2;
        string outputA = "output_xs.txt";
        string outputA2 = "output_splines.txt";
        string outputA3 = "output_splinesint.txt";

        try{
            using(StreamWriter writer = new StreamWriter(outputA2))
            {foreach(double val in points_s){writer.WriteLine($"{val}");}}

            using(StreamWriter writer_x = new StreamWriter(outputA))
            {foreach(double val in points_x){writer_x.WriteLine($"{val}");}}

            using(StreamWriter writer_int = new StreamWriter(outputA3))
            {foreach(double val in points_int){writer_int.WriteLine($"{val}");}}
        }
        catch (Exception ex){
            Console.Write($"oh no, something is wrong: {ex.Message}\n");
        }
        



    }

    public static (double[], double []) make_spline(double [] xs, double [] ys){
        // I want to make a function that gives a list of values linterp from the data
        // that you can plot
        double [] splines = new double[100];
        double [] bits = new double[100];
        for (int i=0; i<100; i++){
            double bite = (xs[xs.Length-1]-xs[0]) / 100;
            bits[i] = xs[0]+bite*i;
            Splines spline_of_bite = new Splines(xs,ys,bits[i]);
            splines[i]= spline_of_bite.linear_interpolation; 
            }
        return (bits,splines);
        }

    public static (double[], double []) make_splineintegration(double [] xs, double [] ys){
        // I want to make a function that gives a list of values linterp from the data
        // that you can plot
        double [] splines_int = new double[100];
        double [] bits = new double[100];
        for (int i=0; i<100; i++){
            double bite = (xs[xs.Length-1]-xs[0]) / 100;
            bits[i] = xs[0]+bite*i;
            Splines spline_of_bite = new Splines(xs,ys,bits[i]);
            splines_int[i] = spline_of_bite.linterp_integration;
            }
        return (bits,splines_int);
        }


} // EVD STOPS