
using System;
using static genlist<double>;
using static System.Math;
using static System.Console;
using static System.Random;



class main_neuralA{
    public static void Main(){
    
        Error.Write("Takes a minute or two... \n");

        // OPG A

        Func<double,double> func_1 = x => Cos(5*x-1)*Exp(-x*x);

        // Det er svingende kvalitet, fx er seed2 meget flot og seed1 er okay
        Random seed1 = new Random(1);
        Random seed2 = new Random(2);

        Neural neuro1 = new Neural(5,func_1, random: seed2);

        (vector x1, vector y1) = train_it(20,func_1,-1,1);
        neuro1.train(x1,y1);

        Error.Write($"I have trained\n");

        // Har lavet derivatives inden i neural.cs
        for (double j = -1; j<1; j+=1.0/64){
            Write($"{j} {func_1(j)} {neuro1.response(j)} {neuro1.derivative(j)} {neuro1.derivative_2(j)} {neuro1.anti_derivative(j)}\n");
        }
    }

    //Giver dig et datasæt (vector x, vector y) at træne på
    public static (vector,vector) train_it(
        int L, // hvor mange datapunkter skal trænes
        Func<double,double> f,//funktion
        double a, double b // interval [a,b] du genererer datapunkter på
     ){
        vector xs = new vector(L);
        for(int i = 0; i<L; i++){
            xs[i] = ((b-a)/L)*i+a;
            }
        vector ys = xs.map(f);
        return (xs,ys);
    }
}