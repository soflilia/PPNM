using System;
using static System.Console;
using static System.Math;

public class inout{
    public static void Main(string[] args){ //læser en liste af strings (type string[]) og kalder dem args
    WriteLine("x sin(x) cos(x)");

    //OPGAVE 
        foreach(var arg in args){
            var stuff = arg.Split(':');
            if (stuff[0]=="-numbers"){
                var numbers = stuff[1].Split(',');
                foreach(var num in numbers){
                    double x = double.Parse(num);
                    Write($"{x} {Sin(x)} {Cos(x)}\n");
                }
            }
        }
    } //main
}//class