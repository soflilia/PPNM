using System;
using static System.Console;
using static System.Math;


public class MonteCarlo{
    static int[] bases = {2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61};
    public (double,double) plainmc(Func<vector,double> f,vector a,vector b,int N){
        int dim=a.size; double V=1; 
        for(int i=0;i<dim;i++){V*=b[i]-a[i];}
        double sum=0,sum2=0;
        var x=new vector(dim);
        var rnd=new Random(1);
        for(int i=0;i<N;i++){
            for(int k=0;k<dim;k++){x[k]=a[k]+rnd.NextDouble()*(b[k]-a[k]);}
            double fx=f(x); sum+=fx; sum2+=fx*fx;
        }
            double mean=sum/N, sigma=Sqrt(sum2/N-mean*mean);
            var result=(mean*V,sigma*V/Sqrt(N));
            return result;
    }

    public (double,double) quasimc(Func<vector,double> f,vector a,vector b,int N){
        int dim = a.size; double V=1;
        for(int i=0; i<dim; i++){V*=b[i]-a[i];} // så V er et slags areal/rumfang
        var x = new vector(dim);
        double sum=0,sum2=0;
        var x_next = new vector(dim);
        double sum_next=0,sum2_next=0;
        for(int i=0; i<N; i++){
            for(int k=0;k<dim;k++){
                x[k]=a[k]+halton(i,dim)[k]*(b[k]-a[k]);
                x_next[k]= a[k]+halton(i,dim,1)[k]*(b[k]-a[k]);}
            //initierer værdier
            double fx = f(x); 
            double fx_next=f(x_next);
            sum+=fx;
            sum_next+=fx_next;
            sum2+=fx*fx;
            sum2_next+=fx_next*fx_next;
        }
        double result = sum/N*V;
        double result_next = sum_next/N*V;
        return (result, Abs(result-result_next));


    }

    public static double corput ( int n , int b){
        //spytter alle mulige værdier ud mellem 0 og 1
        double q=0;
        double bk=(double)1.0/b ;
        while (n>0){ 
        q += (n % b)* bk ;
        n /= b ;
        bk /= b ; 
        //Error.Write($"{q}");
        }
        return q ; 
    }

    public static vector halton( int n, int d, int sequence=0){
        vector halton = new vector(d);
        if( d<bases.Length){
        for(int i=0; i<d; i++){
            if(sequence==0) halton[i]=corput(n,bases[i]);
            if(sequence==1) halton[i]=corput(n,bases[i+d]);}
        }
        else{Error.Write("wrong dimensions \n");}
        return halton;
    }

/*
    public static void Str_sampling(Func<vector,double> f,vector a,vector b, int N, int nmin){
        if (N<=nmin){
            var (result, error) = plainmc(f,a,b,N);
            return (result,error);
        }
        else{
        double[] res = new double[a.size];
        double[] var = new double[a.size];
        for(int i; i < a.size; i++)
            (res[i],var[i])= plainmc(f,a[i],b[i],nmin);
        }

        
    }
    */
}
