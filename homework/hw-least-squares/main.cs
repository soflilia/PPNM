using static lsfit;
using static matrix;
using System;

class Hello{
    public static void Main(){
        vector time = new vector("1,  2,  3, 4, 6, 9,   10,  13,  15");
        vector y1 = new vector("117,100,88,72,53,29.5,25.2,15.2,11.1");
        vector lny = y1.map(Math.Log);
        vector dy1 = new vector("6,5,4,4,4,3,3,2,2");
        Func<double , double> [] fs = new Func<double,double> [2];
        fs[0] = (double x) => 1;
        fs[1] = (double x) => -x;
        //System.Console.Write($"{fs[0](3)}\n");
        lsfit fit_decay = new lsfit(fs, time, lny, dy1);

    }
}