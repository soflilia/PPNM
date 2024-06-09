
using System;
using static System.Math;
using static System.Console;



class minimization{
    public static void Main(){

        vector start = new vector(2,3);
        MIN Rosen = new MIN(Rosenbrocks,start);
        Error.Write("Computing minima at start point = (2,3)...\n");
        Error.Write($"Rosenbrucks function: n = {Rosen.count} iterations, value = {Rosen.value_min}\n");


        MIN Himmel = new MIN(Himmelblau,start);
        Error.Write($"Himmelblau function: n = {Himmel.count} iterations, value = {Himmel.value_min}\n");

    }


    static double Rosenbrocks(vector x){
        double result = Pow(1-x[0],2)+100*Pow((x[1]-Pow(x[0],2)),2);
        return result;
    }

    static double Himmelblau(vector x){
        double result = Pow(Pow(x[0],2)+x[1]-11,2)+Pow(Pow(x[1],2)+x[0]-7,2);
        return result;
    }
}