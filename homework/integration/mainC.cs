using static matrix;
using static QRGS;
using System;
using static System.Console;
using static System.Math;


class MainINTC{
    public static void Main(){
        //testing that the functions give me what it says in the homework
        Console.Error.Write($"\n \nOPG C: \n");

        int ncalls = 0;
        Func <double,double> counts_inf = z=> {ncalls++ ; return 1.0/(Pow(z,2));};
        // Vector of size 2 giving result and error
        vector inf = Int.integrate(counts_inf,1,Double.PositiveInfinity);


        int ncalls1 = 0;
        Func <double,double> counts_inf1 = z=> {ncalls1++ ; return 1.0/(Pow(z,2));};
        // Vector of size 2 giving result and error
        vector inf1 = Int.integrate(counts_inf1,Double.NegativeInfinity,-1);


        int ncalls2 = 0;
        Func <double,double> counts_inf2 = z=> {ncalls2++ ; return Exp(-Pow(z,2));};
        // Vector of size 2 giving result and error
        vector inf2 = Int.integrate(counts_inf2,Double.NegativeInfinity,0);


        int ncalls3 = 0;
        Func <double,double> counts_inf3 = z=> {ncalls3++ ; return Exp(-Pow(z,2));};
        // Vector of size 2 giving result and error
        vector inf3 = Int.integrate(counts_inf3,Double.NegativeInfinity,Double.PositiveInfinity);


        Console.Error.Write($"Infinite integration of 1/x² on [1, inf] evaluates {ncalls} times and its value is {inf[0]} with error {inf[1]} \n");
        Console.Error.Write("Scipy.integrate.quad has value = 1.0 and error = 1.11-14 and evals = 15\n");
        Console.Error.Write("\n\n");

        Console.Error.Write($"Infinite integration of 1/x² on [-inf, -1] evaluates {ncalls1} times and its value is {inf1[0]} with error {inf1[1]} \n");
        Console.Error.Write("Scipy.integrate.quad has value = 1.0 and error = 1.11-14 and evals = 15\n");
        Console.Error.Write("\n\n");


        Console.Error.Write($"Infinite integration of exp(-x²) on [-inf, 0] evaluates {ncalls2} times and its value is {inf2[0]} with error {inf2[1]} \n");
        Console.Error.Write("Scipy.integrate.quad has value = 0.8862.. and error = 7.10-09 and evals = 135\n");
        Console.Error.Write("\n\n");


        Console.Error.Write($"Infinite integration of exp(-x²) on [-inf, inf] evaluates {ncalls3} times and its value is {inf3[0]} with error {inf3[1]} \n");
        Console.Error.Write("Scipy.integrate.quad has value = 1.7724.. and error = 1.42-08 and evals = 270\n");
        
        Console.Error.Write($"Scipy is more accurate but evaluates more times.");
    }
        
} //main stops