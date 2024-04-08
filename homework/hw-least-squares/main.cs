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
        for(int i=0;i<y1.size; i++){
            dy1[i] /= y1[i];
        }
        lsfit fit_decay = new lsfit(fs, time, lny, dy1);
        vector solutions = fit_decay.cks;
        double half_time = Math.Log(2)/solutions[1] ;
        double c1 = solutions[0];
        double c2 = solutions[1];
        //VIGTIGT IKKE AT PRINTE NOGET FØR c1 og c2 da gnuplot læser output fil!
        System.Console.Write($"{c1}\n");
        System.Console.Write($"{c2}\n");
        System.Console.Write($"{fit_decay.c1_unc}\n");
        System.Console.Write($"{fit_decay.c2_unc}\n");
        // første 4 console.write ikke pilles ved
        System.Console.Write($"so our half time from fit is: {half_time} days\n");
        System.Console.Write("Google says experimental value is: 3.631 days\n");
        System.Console.Write($"Our result is thus {(half_time-3.631) / 3.631}% larger\n");

        // opg b
        double lifetime_unc = Math.Log(2)/Math.Pow(c2,2) * fit_decay.c2_unc;
        System.Console.Write($"uncertainty: {lifetime_unc} days\n"); 
        System.Console.Write("which is not within range of the experimental value.\n");       
        //System.Console.Write($"{fit_decay.c2_unc}");
        //fit_decay.cov.print();

}
}