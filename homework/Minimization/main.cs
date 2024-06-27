
using System;
using static genlist<double>;
using static System.Math;
using static System.Console;
using System.Diagnostics;



class minimization{
    public static void Main(){
        // OPG A 
        Error.WriteLine("OPG A...\n");
        
        vector start = new vector(2,3);
        Stopwatch timer = new Stopwatch();
        timer.Start();
        MIN Rosen = new MIN(Rosenbrocks,start);
        timer.Stop();
        Error.Write("Computing minima at start point = (2,3)...\n");
        Error.Write($"Rosenbrucks function: n = {Rosen.count} iterations, value = {Rosen.value_min}\n");
        Error.Write($"(x,y) = ({Rosen.min_point[0]},{Rosen.min_point[1]})\n");

        Stopwatch timer1 = new Stopwatch();
        timer1.Start();
        MIN Himmel = new MIN(Himmelblau,start);
        timer1.Stop();
        Error.Write($"Himmelblau function: n = {Himmel.count} iterations, value = {Himmel.value_min}\n");
        Error.Write($"(x,y) = ({Himmel.min_point[0]},{Himmel.min_point[1]})\n");
        Error.Write("\n\n");
        
        // OPG B

        Error.WriteLine("OPG B...\n");
        var energy = new genlist<double>();
        var cross = new genlist<double>();
        var error  = new genlist<double>();

        var separators = new char[] {' ','\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;    
        do{
            string line = Console.In.ReadLine();
            if(line==null)break;
            string[] words=line.Split(separators,options);
            energy.add(double.Parse(words[0]));
            cross.add(double.Parse(words[1]));
            error .add(double.Parse(words[2]));
        }while(true);

        vector energy1 = new vector(energy.data);
        vector cross1 = new vector(cross.data);
        vector error1 = new vector(error.data);

        Func<vector,double> from_many_to_1 = (vector gæt) => D(energy1,cross1,error1,gæt);
        vector start_gæt = new vector(125,2,13);

        //Jeg har fundet at mange iterations ikke gør noget når den sidder fast, istedet sætter jeg accuracy op
        MIN A1 = new MIN(from_many_to_1,start_gæt, 0.00001, 400);

        Error.Write($"My values for the Breit Wigner fit are: n = {A1.count} iterations, value = {A1.value_min}\n");
        Error.Write($"and the params are m= {A1.min_point[0]}, gamma= {A1.min_point[1]} A = {A1.min_point[2]}\n");
        
        for(double i=100; i<160 ;i+=0.5){
            Write($"{i} {breit_wigner(i,A1.min_point[0],A1.min_point[1],A1.min_point[2])}\n");
        }
        //OPG C
        Error.Write("\n\n");
        Error.WriteLine("OPG C... NOW WITH CENTRAL DIFFERENTIATION\n");
        Error.Write("Computing minima at start point = (2,3)...\n");



        Stopwatch timer2 = new Stopwatch();
        timer2.Start();
        MIN Rosen1 = new MIN(Rosenbrocks,start, central:true);
        timer2.Stop();

        Console.Error.Write($"Rosenbrocks central:\n");
        Error.Write($"Rosenbrucks function: n = {Rosen1.count} iterations, value = {Rosen1.value_min}\n");
        Error.Write($"(x,y) = ({Rosen1.min_point[0]},{Rosen1.min_point[1]})\n");
        Console.Error.Write($"central: {timer2.Elapsed.TotalMilliseconds} ms \n");
        Console.Error.Write($"forward: {timer.Elapsed.TotalMilliseconds} ms \n");

        Stopwatch timer3 = new Stopwatch();
        timer3.Start();
        MIN Himmel1 = new MIN(Himmelblau,start,central:true);
        timer3.Stop();
        Console.Error.Write($"Himmelblau central:\n");
        Error.Write($"Himmelblau function: n = {Himmel1.count} iterations, value = {Himmel1.value_min}\n");
        Error.Write($"(x,y) = ({Himmel1.min_point[0]},{Himmel1.min_point[1]})\n");
        Console.Error.Write($"central: {timer3.Elapsed.TotalMilliseconds} ms \n");
        Console.Error.Write($"forward: {timer1.Elapsed.TotalMilliseconds} ms \n");
        Error.Write("\n\n");

        Console.Error.Write("Conclusion...\n");
        Console.Error.Write("it seems central derivative method is slightly better, because the resulting minima are smaller and thus\n");
        Console.Error.Write("the found values (x,y) must be equaly closer to the true value. We also have a time difference in favour\n");
        Console.Error.Write("of the central algorithm implementation.\n");


        }// main stops


    static public double breit_wigner(double E, double m, double gamma, double A){
        return A / (Pow(E-m,2)+Pow(gamma,2)/4);
        }

    static double D(vector E, vector sigma, vector error, vector gæts){
        double result = 0;
        for (int i=0; i<E.size; i++){
            double værdi = breit_wigner(E[i],gæts[0],gæts[1],gæts[2]);
            result += Pow((værdi-sigma[i])/error[i],2);
        }
        return result;
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