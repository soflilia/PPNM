using System;


class Mainquad{
    public static void Main(){
        double [] xs = {0,1,2,3,4,5,6,7,8,9};
        double [] ys = new double [xs.Length];
        for(int i=0; i<xs.Length; i++){
            double tal = xs[i];
            ys[i]=Math.Sin(tal);
            //skrive min første output til xs , ys
		    Console.WriteLine($"{xs[i]} {ys[i]}");
	        }
        //Laver mellemrum så gnuplot kan læse min outfil
	    Console.Write("\n\n");

        qspline sine_func = new qspline(xs,ys);

        for(int j=0; j<xs.Length-1; j++){
            Console.Error.Write($"{sine_func.c[j]}\n");
            Console.Error.Write($"{j}\n");
            Console.Error.Write($"{sine_func.b[j]}\n");
            }
    
        // man kunne også kalde i forloop som "new qspline(xs,ys).evaluate(x[i])"

        for(double z=xs[0]; z<=xs[xs.Length-1]; z+=1.0/64){
            Console.WriteLine($"{z} {sine_func.evaluate(z)} {sine_func.integral(z)}");
            }


        }//main stops

} // EVD STOP