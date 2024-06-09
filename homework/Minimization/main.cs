
using System;
using static genlist<double>;
using static System.Math;
using static System.Console;



class minimization{
    public static void Main(){
        
        vector start = new vector(2,3);
        MIN Rosen = new MIN(Rosenbrocks,start);
        Error.Write("Computing minima at start point = (2,3)...\n");
        Error.Write($"Rosenbrucks function: n = {Rosen.count} iterations, value = {Rosen.value_min}\n");
        Error.Write($"(x,y) = ({Rosen.min_point[0]},{Rosen.min_point[1]})\n");

        MIN Himmel = new MIN(Himmelblau,start);
        Error.Write($"Himmelblau function: n = {Himmel.count} iterations, value = {Himmel.value_min}\n");
        Error.Write($"(x,y) = ({Himmel.min_point[0]},{Himmel.min_point[1]})\n");
        
        // OPG B


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

/*
Jeg havde en masse bøvl med at få den til at virke 
        Error.Write($"from many to one funktion: {from_many_to_1(start_gæt)}\n");

        Error.Write($"Breit Wigner: {breit_wigner(125, start_gæt[0],start_gæt[1],start_gæt[2])}\n");

        Error.Write($"bare fra D: {D(energy1,cross1,error1,start_gæt)}\n");
*/ 

        //Jeg har fundet at mange iterations ikke gør noget når den sidder fast, istedet sætter jeg accuracy op
        MIN A1 = new MIN(from_many_to_1,start_gæt, 0.00001, 400);

        Error.Write($"My values are: n = {A1.count} iterations, value = {A1.value_min}\n");
        Error.Write($"and the params are m= {A1.min_point[0]}, gamma= {A1.min_point[1]} A = {A1.min_point[2]}\n");
        
        for(double i=100; i<160 ;i+=0.5){
            Write($"{i} {breit_wigner(i,A1.min_point[0],A1.min_point[1],A1.min_point[2])}\n");
        }
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