using static matrix;
using static QRGS;
using System;


class MainINTB{
    public static void Main(){
        //testing that the functions give me what it says in the homework
        int ncalls = 0;
        Func <double,double> count = z=> (ncalls++ ; return z*z);
        double j = integrate(count,3,4)

    }
        
} // EVD STOPS
