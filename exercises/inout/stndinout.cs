using System;
using static System.Console;
using static System.Math;

public class inout{
    public static void Main(){
        char[] splitters = {' ', '\t', '\n'};
        var split_opt = StringSplitOptions.RemoveEmptyEntries;
        string line;
        while( (line = ReadLine()) != null){
            var numbers = line.Split(splitters,split_opt);
            foreach(var num in numbers)
            {
                // blev ved med at få en System.FormatException fejl 
                try
                {
                    double x = double.Parse(num);
                    Write($"{x} {Sin(x)} {Cos(x)}\n");
                }
                catch (FormatException)
                {
                    Error.WriteLine($"Failed to parse: {num}"); // Debug output
                }
            }
        }

    }
}