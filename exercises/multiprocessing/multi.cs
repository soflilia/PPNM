using System;



public class data{
    public int a,b; public double sum;

    public static void harm(object obj){
	    var arg = (data)obj;
	    arg.sum=0;
	    for(int i=arg.a;i<arg.b;i++)arg.sum+=1.0/i;
	    }
} //CLASS DATA ENDS