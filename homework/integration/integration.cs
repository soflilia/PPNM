using static vector;
using static matrix;
using System.Collections.Generic;
using System;
using static System.Console;
using static System.Math;

public class Int{


    public static vector integrate
    (Func<double,double> f, double a, double b,
    double abs_err=0.001, double rel_err=0.001, double f2=Double.NaN, double f3=Double.NaN){
    //Sørger for at limits ikke er uendelige
    if ( Double.IsInfinity(a) || Double.IsInfinity(b) ){
        var (f_new,a_new,b_new) = infin_var_trans(f,a,b);
        f = f_new;
        a = a_new;
        b = b_new;
    };
    // indmad af integrate
    double h=b-a;
    if(Double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); } // first call, no points to reuse
    double f1=f(a+h/6), f4=f(a+5*h/6);
    double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
    double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule
    double err = Math.Abs(Q-q);
    if (err <= abs_err+rel_err*Math.Abs(Q)) { vector answers = new vector(2);
        answers[0] = Q;
        answers[1]= err;
        return answers;}
    else return integrate(f,a,(a+b)/2,abs_err/Math.Sqrt(2),rel_err,f1,f2)+
        integrate(f,(a+b)/2,b,abs_err/Math.Sqrt(2),rel_err,f3,f4);
    }

    public static (Func<double,double>,double, double) var_trans(Func<double,double> f
    ,double a, double b){
        Func<double,double> f_trans = x=> f((a+b)/2+(b-a)/2*Cos(x))*Sin(x)*(b-a)/2;
        return (f_trans,0,PI);
    }

    public static (Func<double,double>,double, double) infin_var_trans(Func<double,double> f
    ,double a, double b){
        if (Double.IsNegativeInfinity(a) && Double.IsPositiveInfinity(b)){
            Func<double,double> f_trans = t => f( t/(1-Pow(t,2)) ) * (1 + Pow(t,2)) / (Pow(1-Pow(t,2),2));
            return (f_trans, -1 , 1);
        }
        if (Double.IsNegativeInfinity(a)){
            Func<double, double> f_trans = t => f(b - (1-t)/t ) *1.0/ Pow(t, 2);
            return (f_trans, 0, 1);};

        if (Double.IsPositiveInfinity(b)){
            Func<double,double> f_trans = t => f(a + (1-t)/t ) * 1/Pow(t,2);
            return (f_trans , 0, 1);
        }
        else {
            Error.Write($"Infinity transformation used without doing anything \n");
            return (f,a,b);};
    }

    public static vector  Clenshaw_Curtis_VT(Func<double,double> f, double a, double b){
        var (f_new,a_cc,b_cc) = var_trans(f,a,b);
        vector answer = integrate(f_new,a_cc,b_cc
        ,0.001,0.001,Double.NaN,Double.NaN);
        return answer;
    }


}