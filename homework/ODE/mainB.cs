using static matrix;
using static QRGS;
using System;


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

        
} // EVD STOPS
