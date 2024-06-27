
using System;
using static ODE;
using static qspline;
using static System.Math;
using static System.Console;
using System.Diagnostics;



class mainrootfinder{
    public static void Main(){
        vector start_rosen = new vector("0,0");
        vector Rosen_grad1 = RF.newton(Rosen_grad,start_rosen);
        Error.Write($"The Rosenbrucks valley function has an extremum at : x={Rosen_grad1[0]:G3} y={Rosen_grad1[1]:G3}\n");

        //Since I know the function I have good guesses
        vector start1 = new vector("-3,-3");
        vector start2 = new vector("4,-2");
        vector start3 = new vector("-2,2");
        vector start4 = new vector("2,2");

        Stopwatch timer1 = new Stopwatch();
        timer1.Start();
        vector Himmel_grad1 = RF.newton(Himmel_grad,start1);
        vector Himmel_grad2 = RF.newton(Himmel_grad,start2);
        vector Himmel_grad3 = RF.newton(Himmel_grad,start3);
        vector Himmel_grad4 = RF.newton(Himmel_grad,start4);
        timer1.Stop();

        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad1[0]:G3} y={Himmel_grad1[1]:G3}\n");
        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad2[0]:G3} y={Himmel_grad2[1]:G3}\n");
        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad3[0]:G3} y={Himmel_grad3[1]:G3}\n");
        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad4[0]:G3} y={Himmel_grad4[1]:G3}\n");

        // OPG B
        Error.Write("\n\n");
        Error.Write(".....OPG B.....\n");
        Error.Write("Udskriver grafer....\n");

        //løsning af schrodinger
        vector start_E = new vector(1);
        start_E[0] = -5;
        vector hydro = RF.newton((vector E) => schrodinger(E),start_E);
        Error.Write($"The energy should be E= {hydro[0]:G3}\n");

        //convergence plots = JEG WRAPPER TING IND I LAMBDA
        for(double i =0.01; i<0.7; i+=0.01){
        Console.Write($"{i} {RF.newton((vector E) => schrodinger(E, rmin: i),start_E)[0]}\n");}
        Console.Write("\n\n");

        for(double i =2; i<10; i+=0.1){
        Console.Write($"{i} {RF.newton((vector E) => schrodinger(E, rmax: i),start_E)[0]}\n");}
        Console.Write("\n\n");

        for(double i =0.01; i<5; i+=0.3){
        Console.Write($"{i} {RF.newton((vector E) => schrodinger(E, acc: i),start_E)[0]}\n");}
        Console.Write("\n\n");
        for(double i =0.01; i<5; i+=0.3){
        Console.Write($"{i} {RF.newton((vector E) => schrodinger(E, eps: i),start_E)[0]}\n");}
        
        Error.Write("\n\n");
        Error.Write(".....OPG C.....\n");
        Error.Write("Encoding the quadratic interpolation line-search\n");

        Stopwatch timer2 = new Stopwatch();
        timer2.Start();
        vector Himmel_grad_quad1 = RF.newton(Himmel_grad,start1,quad_linesearch:true);
        vector Himmel_grad_quad2 = RF.newton(Himmel_grad,start2,quad_linesearch:true);
        vector Himmel_grad_quad3 = RF.newton(Himmel_grad,start3,quad_linesearch:true);
        vector Himmel_grad_quad4 = RF.newton(Himmel_grad,start4,quad_linesearch:true);
        timer2.Stop();

        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad_quad1[0]:G3} y={Himmel_grad_quad1[1]:G3}\n");
        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad_quad2[0]:G3} y={Himmel_grad_quad2[1]:G3}\n");
        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad_quad3[0]:G3} y={Himmel_grad_quad3[1]:G3}\n");
        Error.Write($"The Himmelblaus function has minima at: x={Himmel_grad_quad4[0]:G3} y={Himmel_grad_quad4[1]:G3}\n");

        Error.Write("The quadratic linesearch algorithm gives the same results (at 2 decimal precision). Let us see the difference in time:..\n\n");
        Error.Write("Testing on finding all 4 minima in Himmelblau:\n");
        Error.Write($"backtracking linesearch: {timer1.Elapsed.TotalMilliseconds} ms \n;");
        Error.Write($"quadratic backtracking linesearch: {timer2.Elapsed.TotalMilliseconds} ms \n;");




        
        

        }// Main stops

        static vector Rosen_grad(vector xy){
            vector result = new vector(2);
            result[0] = 2*xy[0]-2+400*Pow(xy[0],3)-400*xy[0]*xy[1];
            result[1] = 200*xy[1]-200*Pow(xy[0],2);
            return result;
        }

        static vector Himmel_grad(vector xy){
            vector result = new vector(2);
            result[0] = 2*(2*xy[0]*(Pow(xy[0],2)+xy[1]-11)+xy[0]+Pow(xy[1],2)-7);
            result[1] = 2*(Pow(xy[0],2)+2*xy[1]*(Pow(xy[1],2)+xy[0]-7)+xy[1]-11);
            return result;
        }


        static vector schrodinger(vector E_guess, double rmax = 8, double rmin=0.01, double eps=0.01, double acc=0.01){
            //DETTE ER SÅ MIN ODE
            Func<double, vector, vector> schrod= (x,y) => {
                double y2 = y[1];
                double dy2 = -2*y[0]*(E_guess[0]+1/x);
                vector svar = new vector(y2,dy2);
                return svar;
                };

            //initierer en ODE
            vector initial = new vector(rmin-Pow(rmin,2),1-2*rmin);
            double a=rmin , b = rmax; //interval
            var(xs,all_y) = ODE.driver(schrod,(a,b),initial,0.125,acc,eps);
            int size = all_y.Count;
            vector energy = new vector(1);
            energy[0]=all_y[size-1][0];

            return energy;
        }

} // EVD STOPS
