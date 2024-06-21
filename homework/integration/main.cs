using static matrix;
using static QRGS;
using System;


class MainINT{
    public static void Main(){
        //testing that the functions give me what it says in the homework
        Func<double,double> f1 = x => Math.Sqrt(x);
        double result1 = Int.integrate(f1,0,1);
        Console.Error.Write($"So the definite integral of sqrt(x) from 0 to 1 is {result1}\n");
        //accuracy
        if (Math.Abs(result1 - 2.0/3.0) <= 0.001 && Math.Abs(result1 - 2.0/3.0)/(2.0/3.0) <= 0.001){Console.Error.Write("accuracy adequate\n");}
        else{Console.Error.Write("Accuracy not adequate\n");}

        Func<double,double> f2 = x => 1.0/(Math.Sqrt(x));
        double result2 = Int.integrate(f2,0,1);
        Console.Error.Write($"So the definite integral of 1/sqrt(x) from 0 to 1 is {result2}\n");
        if (Math.Abs(result2 - 2.0) <= 0.001 && Math.Abs(result2 - 2.0)/2.0 <= 0.001){Console.Error.Write("accuracy adequate\n");}
        else{Console.Error.Write("Accuracy not adequate\n");}

        Func<double,double> f3 = x => 4*(Math.Sqrt(1-Math.Pow(x,2)));
        double result3 = Int.integrate(f3,0,1);
        Console.Error.Write($"So the definite integral of 4*sqrt(1-x²) from 0 to 1 is {result3}\n");
        if (Math.Abs(result3 - Math.PI) <= 0.001 && Math.Abs(result3 - Math.PI)/Math.PI <= 0.001){Console.Error.Write("accuracy adequate\n");}
        else{Console.Error.Write("Accuracy not adequate\n");}

        Func<double,double> f4 = x => Math.Log(x)/(Math.Sqrt(x));
        double result4 = Int.integrate(f4,0,1);
        Console.Error.Write($"So the definite integral of ln(x)/sqrt(x) from 0 to 1 is {result4}\n");
        if (Math.Abs(result4 + 4.0) <= 0.001 && Math.Abs(result4 + 4.0)/4.0 <= 0.001){Console.Error.Write("accuracy adequate\n");}
        else{Console.Error.Write("Accuracy not adequate\n");}

        //plotte erf(x)
        for(double z = -10; z<10; z+=1.0/128){
            Console.Write($"{z} {erf(z)}\n");
        }
        Console.Write("\n\n");
    }

    public static double erf(double z){
        if(z<0){return -erf(-z);}
        double a = 0.0, b=1.0;
        if(a <=z && z<=b){
            //Console.Error.Write($"we have z = {z}\n");
            Func <double,double> f = x => Math.Exp(-Math.Pow(x,2));  
            return 2.0/Math.Sqrt(Math.PI)*Int.integrate(f,0,z);
            }
        else{
            Func <double, double> f1 = x=> Math.Exp(-(Math.Pow(z+(1-x)/x,2))/x/x);
            return 1-2.0/Math.Sqrt(Math.PI)*Int.integrate(f1,0,1);
            }
        }
} // EVD STOPS
