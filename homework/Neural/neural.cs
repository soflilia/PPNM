using static vector;
using static matrix;
using System;
using static System.Math;
using static System.Random;
using static System.Console;
using static QRGS;

public class Neural{
    int n; /* number of hidden neurons */
    Func<double,double> f = x => x*Exp(-x*x); /* activation function */
    vector p; /* network parameters */

    public Neural(int count, Func<double,double> f = null, Random random=null){
        //der skal være mulighed for selv at angive funktion
        if (f == null){f = this.f;}
        else{ this.f = f; };
    
        n = count;
        p  = new vector(3*n);

        // hvis ikke vi angiver hvilken random gennemgang laver den en helt ny random
        if (random == null){
            random = new Random();
        }
        // p initialiseres med random (om det så er seed eller ny Random class)
        for (int i = 0; i<n; i++){
            p[i] = random.NextDouble() ;
            p[i+n] = random.NextDouble() + 0.5;
            p[i+2*n] = random.NextDouble(); }   
        
    }

    public double cost(vector p, vector x, vector y){
        double cost = 0;
        for (int i =0 ; i< x.size; i++){
            cost += Pow(response(x[i],p)-y[i],2)/n;
        }
        return cost;
    }

    public void train(vector x,vector y){
      /* train the network to interpolate the given table {x,y} */
      Func<vector,double> cost_function = (vector p) => cost(p,x,y);
      MIN train1 = new MIN(cost_function,this.p,1e-3,3000);
      p = train1.min_point ;
    }

    public double derivative(double x){
        double dx = Pow(2,-26);
        double grad_F = (response(x+dx)-response(x))/dx ;
        return grad_F;
    }

    public double derivative_2(double x){
        double dx = Pow(2,-26);
        double grad_2_F = (derivative(x+dx)-derivative(x))/dx;
        return grad_2_F;
    }

    public double anti_derivative(double x, double a =0){
        int bits = 1000;
        double dx = (x-a)/bits;
        double integral = 0;

        for (int i = 0; i<1000; i++){
            double x_i = a + dx*i;
            double f_before = response(x_i);
            double f_after = response(x_i+dx);
            integral += (f_after+f_before)/2.0*dx;
        }
        return integral;
    }

    public double response(double x){
        return response(x,this.p);
    }

    public double response(double x, vector p){
        double result = 0;
        for (int i = 0; i< n; i++){
            double a_i = p[i]; 
            double b_i = p[n+i];
            double w_i = p[2*n+i];
            result += (f(x-a_i)/b_i)*w_i;
            //Error.Write($"Am i here?{result}\n");
        }
        //Error.Write($"{f(x)}\n");
        return result;
    }
}