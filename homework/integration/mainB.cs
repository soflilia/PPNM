using static matrix;
using static QRGS;
using System;


class MainINTB{
    public static void Main(){
        //testing that the functions give me what it says in the homework
        Console.Error.Write($"\n \nOPG B: \n");
        int ncalls = 0;
        Func <double,double> counts = z=> {ncalls++ ; return 1.0/(Math.Sqrt(z));};
        double normal_1 = Int.integrate(counts,0,1)[0];

        int ncalls1 = 0;
        Func <double,double> counts1 = z=> {ncalls1++ ; return 1.0/(Math.Sqrt(z));};
        double trans_1 = Int.Clenshaw_Curtis_VT(counts1,0,1)[0];
        Console.Error.Write($"We have the normal integration counted: {ncalls} and its value is {normal_1}\n");
        Console.Error.Write($"We have the transformed integration counted: {ncalls1} and its value is {trans_1}\n");
        Console.Error.Write($"Compared to scipy.qud in python: 231\n");

        //new function
        int ncalls2 = 0;
        Func <double,double> counts2 = z=> {ncalls2++ ; return Math.Log(z)/(Math.Sqrt(z));};
        double normal_2 = Int.integrate(counts2,0,1)[0];

        int ncalls3 = 0;
        Func <double,double> counts3 = z=> {ncalls3++ ; return Math.Log(z)/(Math.Sqrt(z));};
        double trans_2 = Int.Clenshaw_Curtis_VT(counts3,0,1)[0];
        Console.Error.Write($"We have the normal integration counted: {ncalls2} and its value is {normal_2}\n");
        Console.Error.Write($"We have the transformed integration counted: {ncalls3} and its value is {trans_2}\n");
        Console.Error.Write($"Compared to scipy.qud in python: 315\n");

    }
        
}