using static System.Console;
using System;
using static cmath;
using static complex;

static class exercisecomplex{
    static void Main(){
        complex res1 = cmath.sqrt(new complex(-1,0));

        Write($"{res1}\n");

        if (res1.approx(new complex(0,1))){
            Write("yes, sqrt(-1) is calculated properly\n");
        }
        else{Write("no, something is wrong\n");}

        //

        complex res2 = cmath.sqrt(new complex(0,1));

        Write($"{res2}\n");

        if (res2.approx(new complex(1/Math.Sqrt(2),1/Math.Sqrt(2)))){
            Write("yes, sqrt(-i) is calculated properly\n");
        }
        else{Write("no, something is wrong\n");}

        //

        complex res3 = cmath.exp(new complex(0,1));

        Write($"{res3}\n");

        if (res3.approx(new complex(Math.Cos(1), Math.Sin(1)))){
            Write("yes, e^i is calculated properly\n");
        }
        else{Write("no, something is wrong\n");}

        //

        complex res4 = cmath.exp(new complex(0,Math.PI));

        Write($"{res4}\n");

        if (res4.approx(new complex(-1,0))){
            Write("yes, e^(i pi) is calculated properly\n");
        }
        else{Write("no, something is wrong\n");}

        // 
        complex res5 = cmath.pow(new complex(0,1),new complex(0,1));

        Write($"{res5}\n");

        if (res5.approx(cmath.exp(new complex(-Math.PI/2,0)))){
            Write("yes, i^i is calculated properly\n");
        }
        else{Write("no, something is wrong\n");}

        //
        complex res6 = cmath.log(new complex(0,1));

        Write($"{res6}\n");

        if (res6.approx(new complex(0,Math.PI/2))){
            Write("yes, ln(i) is calculated properly\n");
        }
        else{Write("no, something is wrong\n");}

        //

        complex res7 = cmath.sin(new complex(0,Math.PI));

        Write($"{res7}\n");

        if (res7.approx(new complex(0,Math.Sinh(Math.PI)))){
            Write("yes, sin(i*pi) is calculated properly\n");
        }
        else{Write("no, something is wrong\n");}

    
    }
}