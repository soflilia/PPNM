using static matrix;
using static QRGS;
using System;


class Mainspline{
    public static void Main(){
        //initierer en ODE u''= -u
        vector y_start = new vector(2);
        y_start[0] = 2; //start værdi i punkt a
        y_start[1] = 3; //start hældning i punkt a
        double a=10 , b = a+22; //interval
        var(xs,all_y) = ODE.driver(ode_func1,(a,b),y_start);
        for (int i=0; i<xs.Count; i++){
            Console.Write($"{xs[i]} {all_y[i][0]}\n");
        }

        Console.Write($"\n\n");
        
        //exempel med pendul
        vector y_pendul = new vector("Math.Pi-0.1,0.0");
        double ap=0 , bp = a+10; //interval
        var(ts,pendul) = ODE.driver(pendul,(ap,bp),y_pendul);
        for (int i=0; i<ts.Count; i++){
            Console.Write($"{ts[i]} {pendul[i][0]} {pendul[i][1]}\n");
        }


        }//main stops

        public static vector ode_func1(double x, vector y){
            vector new_y = new vector(2);
            new_y[0] = y[1];
            new_y[1] = -y[0];
            return new_y;
        }

        public static vector pendul(double x, vector y){
            vector new_y = new vector(2);
            double b = 0.25, c = 5.0;
            new_y[0] = y[1];
            new_y[1] = -b*y[1]-c*Math.Sin(y[1]);
            return new_y;
        }

} // EVD STOPS
