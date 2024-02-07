using System;
using static sfuns;

static class M1{
    static int Main(){
    double one=Math.Sqrt(2.0);
    double two=Math.Pow(2.0,0.2);
    double three=Math.Pow(Math.Exp(1),Math.PI);
    double four=Math.Pow(Math.PI,Math.Exp(1));
        System.Console.Write($"squareroot of 2: {one}\n");
        System.Console.Write($"checking if {one*one} = 2\n");
        System.Console.Write($"2 to the power of 1/5: {two}\n");
        System.Console.Write($"checking if {Math.Pow(two, 5)} = 2\n");
        System.Console.Write($"e to the power of pi: {three}\n");
        System.Console.Write($"checking if {three} = 23.1406926\n");
        System.Console.Write($"pi to the power of e: {four}\n");
        System.Console.Write($"checking if {four} = 22.4591577\n");
        // opg 2
        int prod = 1;
        for(int i=1;i<10;i+=1)
        {System.Console.Write($"fgamma[{i}] = {sfuns.fgamma(i)} should equal {i-1}! = {prod} \n");
        prod *= i;}

        // opg 3
        int prod2 = 1;
        System.Console.Write("It is often easier to calculate the logarithm of the gammas instead: \n");
        for(int i=1;i<10;i+=1)
        {System.Console.Write($"lngamma[{i}] = {sfuns.lngamma(i)}\n");
        prod *= i;}
    return 0;
    }
}
