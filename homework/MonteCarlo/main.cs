using System;
using static System.Console;
using static System.Math;
using static MonteCarlo;
using static vector;


public class MC{
    public static void Main(){

        double r_1 = 1.0;
        Func <vector,double> circle = x => {
            if(x.norm() <= 1){return 1;}
            return 0;
            };
        vector a_1 = new vector(-r_1,-r_1);
        vector b_1 = new vector(r_1,r_1);
        //Error.Write($"{circle(b_1)}\n");
        int N_1 = 1000000;
        MonteCarlo instance = new MonteCarlo();
        var (result_1,error_1) = instance.plainmc(circle,a_1,b_1,N_1);
        Error.Write($"Result with N={N_1}: {result_1}, Error: {error_1}\n");
        

        //OK nu laver du et plot af error som funktion af N:
        for(int N = 30; N<500000; N+=10000){
            var (est_result_qr,est_error_qr) = new MonteCarlo().quasimc(circle,a_1,b_1,N);
            var (est_result,est_error) = new MonteCarlo().plainmc(circle,a_1,b_1,N);
            double act_error = Abs(PI-est_result);
            double act_error_qr = Abs(PI-est_result_qr);
            Write($"{N} {est_error} {act_error} {est_error_qr} {act_error_qr}\n");
        }
        Write($"\n\n");
        for(int N = 30; N<500000; N+=10000){Write($"{N} {1.0/Sqrt(N)}\n");}
        //snabel-a gør dig i stand til at splitte string i flere linjer
        Error.Write("The actual error is larger than 1/sqrt(N) for small N,\n"+
        "then converges towards 1/sqrt(N) for larger N>10 000 \n");

        // Try to calculate big thing...
        vector a_2 = new vector(0,0,0);
        vector b_2 = new vector(PI,PI,PI);
        Func <vector,double> cosin = x => {
            return 1/(Pow(PI,3) * (1-Cos(x[0])*Cos(x[1])*Cos(x[2])));};

        int N_2 = 1000000;
        var (result_2,error_2) = new MonteCarlo().plainmc(cosin,a_2,b_2,N_2);
        Error.Write($"Result with N={N_2}: {result_2}, Error: {error_2}\n");


        //OPG B

        var (result_qr,error_qr) = instance.quasimc(circle,a_1,b_1,N_1);
        Error.Write($"Result with quasi random sequence N={N_1}: {result_qr}, Error: {error_qr}\n");

    }

    
}