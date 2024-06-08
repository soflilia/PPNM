using static matrix;
using static QRGS;
using System;


class MainODE{
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
        
        //initierer en ODE pendul
        vector y_pendul = new vector(2);
        y_pendul[0] = Math.PI - 0.1;
        y_pendul[1] = 0.0;
        //y_pendul.print();
        double ap=0 , bp = ap+10; //interval
        var(ts,pendul) = ODE.driver(pendul,(ap,bp),y_pendul);
        for (int i=0; i<ts.Count; i++){
            //Console.Error.Write($"is everythign ok? {i}\n");
            Console.Write($"{ts[i]} {pendul[i][0]} {pendul[i][1]}\n");
        }

        Console.Write($"\n\n");

        //initierer en ODE Lotka-Volterra/predator prey
        vector y_LK = new vector("10,5");
        //y_pendul.print();
        double aLK=0 , bLK = aLK+15; //interval
        var(tsLK,LK) = ODE.driver(Lotka_Volterra,(aLK,bLK),y_LK);
        for (int i=0; i<tsLK.Count; i++){
            //Console.Error.Write($"is everythign ok? {i}\n");
            Console.Write($"{tsLK[i]} {LK[i][0]} {LK[i][1]}\n");
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
            new_y[1] = -b*y[1]-c*Math.Sin(y[0]);
            return new_y;
        }

        public static vector Lotka_Volterra(double x, vector y){
            double a=1.5, b =1.0, c=3.0, d=1.0;
            vector f= new vector(2);
            f[1] = -c*y[1] + d*y[0]*y[1];
            f[0] = a*y[0] - b*y[0]*y[1];
            return f;
        }        
} // EVD STOPS
