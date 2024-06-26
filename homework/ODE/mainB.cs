using static matrix;
using static QRGS;
using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;


class MainODEB{
    public static void Main(){


        //initierer en ODE af GR fra opg B
        vector y_gr = new vector("1,0");
        double agr=0 , bgr = agr+15; //interval

        Func<double,vector> solver = ODE.make_ode_ivp_interpolant(motion,(agr,bgr),y_gr);
        for (double i=0.1; i<15; i+=1.0/64){
            Console.Write($"{i} {solver(i)[0]}\n");
        }

        Console.Write("\n\n");

        //initierer en ODE af GR fra opg B
        vector y_gr1 = new vector("1,-0.5");

        var (phi,GR1) = ODE.driver(motion,(agr,bgr),y_gr1);

        for (int i=0; i<phi.Count; i++){
            Console.Write($"{phi[i]} {GR1[i][0]} {GR1[i][1]}\n");
        }

        Console.Write("\n\n");

        vector y_gr2 = new vector("1,-0.5");

        var (phi2,GR2) = ODE.driver(motion_eps,(agr,bgr),y_gr2);

        for (int i=0; i<phi2.Count; i++){
            Console.Write($"{phi2[i]} {GR2[i][0]} {GR2[i][1]}\n");
        }
        Console.Write("\n\n");

        // OPG C .................................................

        vector z = new vector(12);
        // alle x prikker'er
        z[4] = -0.93240737;
        z[0] = z[2] = z[4] / (-2);

        z[5] = -0.86473146;
        z[1] = z[3] = z[5] / (-2);

        z[6] = 0.97000436;
        z[8] = -z[6];
        z[10] = z[11] = 0;
        z[7] = -0.24308753;
        z[9] = -z[7];

        double start=0 ,finish = start+6.5; //interval

        var ( times, list_of_vs ) = ODE.driver(threebody,(start,finish),z);
        for (int i =0; i<times.Count; i++){
                Write($"{list_of_vs[i][6]} {list_of_vs[i][7]} {list_of_vs[i][8]} {list_of_vs[i][9]} {list_of_vs[i][10]} {list_of_vs[i][11]}\n");
        }

        Console.Write("\n\n");







        }//main stops

        public static vector motion(double x, vector y){
            double eps = 0.0;
            vector f= new vector(2);
            f[0] = y[1];
            f[1] = 1-y[0]+eps*Math.Pow(y[0],2);
            return f;
            }

        public static vector motion_eps(double x, vector y){
            double eps = 0.01;
            vector f= new vector(2);
            f[0] = y[1];
            f[1] = 1-y[0]+eps*Math.Pow(y[0],2);
            return f;
            }

        public static vector threebody(double x, vector z){
            //Error.Write($"{x}\n");
            if (z.size != 12){ throw new RankException();};
            vector S = new vector(12);

            // DET ER R-R ikke x-x eller y-y 
            double[] rs_x = new double[] {z[6], z[8], z[10]};
            double[] rs_y = new double[] {z[7], z[9], z[11]};

            for (int i =0; i<3; i++){
                for (int j = 0; j<3; j++){
                    if (j != i){
                        double dx = rs_x[j]-rs_x[i];
                        double dy = rs_y[j]-rs_y[i];
                        double norm = Pow(Pow(dx,2)+Pow(dy,2),0.5);
                        Console.Error.Write($"norm= {norm}\n");

                        S[2*i  ] += (rs_x[j]-rs_x[i] )/ (Pow(Abs(norm),3));
                        S[2*i+1] += (rs_y[j]-rs_y[i] )/ (Pow(Abs(norm),3));
                        /*
                        S[2*j  ] -= (rs_x[i]-rs_x[j] )/ (Pow(Abs(norm_oi-norm_oj),3));
                        S[2*j+1] -= (rs_y[i]-rs_y[j] )/ (Pow(Abs(norm_oi-norm_oj),3));
                        */
                    };
                //Error.Write($"inside loop {i}\n");
                }
            }
            for (int i =6; i<12; i++){
                S[i] = z[i-6];
            }
            return S;
        }

        
} // EVD STOPS
