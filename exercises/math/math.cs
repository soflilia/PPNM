using System;

class M1{
    static double one=Math.Sqrt(2.0);
    static double two=Math.Pow(2.0,0.2);
    static double three=Math.Pow(Math.Exp(1),Math.PI);
    static double four=Math.Pow(Math.PI,Math.Exp(1));
    static void Main(){
        System.Console.Write($"squareroot of two: {one}\n");
        System.Console.Write($"2 to the power of 1/5: {two}\n");
        System.Console.Write($"e to the power of pi: {three}\n");
        System.Console.Write($"pi to the power of e: {four}\n");
    }
}