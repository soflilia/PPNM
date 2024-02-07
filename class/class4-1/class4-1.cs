using System.Console;

public class my_class{ public string s;}
public struct my_struct { public string s;}
//public so you can access 

static class main{
static void set_to_7(double x) {x=7; }
static void set_to_7(double[] tmp) {for(int i=0;i<tmp.Length;i++)tmp=7;}
    static int Main(){
        my_class;
        A=new my_class();
        my_struct;
        a=new my_struct();
        A.s="hello";
        a.s="hello";
        WriteLine($"A.s= {A.s}");
        WriteLine($"a.s={a.s}");
        // sammenligne class og struct

        my_class B=A;
        /* in class B becomes the same object as A. 
        Big things like matrices in class. use copy if you have to
        */
        my_struct b=a;
        /* in struct b is made identical to a, but they are 2 seperate objects.
        Small things in structs */
        WriteLine($"B.s= {B.s}");
        WriteLine($"b.s={b.s}");

        B.s ="new string";
        b.s="new string";
        // to check we manually change the B and b
        WriteLine($"A.s= {A.s}");
        WriteLine($"a.s={a.s}");

        double x=1;
        set_to_7(x);
        Write($"x={x}\n")
        // will it be 1 or 7? 1 since a function creates a copy of x and does
        // not change the x.
return 0;

    }
}