using static System.Console;


public class main{
    public static int Main(){
        // opg 1
        int i = 1 ; while(i+1>i) {i++;}
        Write($"my max int = {i} \n");
        Write($"which should equal {int.MaxValue} \n");

        
        int x = 1 ; while(x-1<i) {x--;}
        Write($"my min int = {x} \n ");
        Write($"which should equal {int.MinValue} \n");

        // opg 2
        double y=1; while(1+y!=1) {y/=2;} y*=2;
        /* imens while loop er sandt halverer vi y, når den ikke længere er sand 
        ganger vi y med 2 for at finde den sidste sande værdi af y. */
        Write($"my epsilon in double is = {y} \n");
        Write($"which should equal {System.Math.Pow(2,-52)} \n");

        float z = 1F; while((float)(1F+z) != 1F){z/=2F;} z*=2F;
        // samme for float
        Write($"my epsilon in float is = {z} \n");
        Write($"which should equal {System.Math.Pow(2,-23)} \n");
        Write("so I am happy \n");

        //opg 3
        double epsilon=System.Math.Pow(2,-52);
        double tiny = epsilon/2;
        double a = 1+tiny+tiny;
        double b = tiny+tiny+1;
        Write($"so is 1+tiny+tiny = tiny+tiny+1? {a==b}\n");
        Write($"is 1+tiny+tiny > 1 ? {a>1} \n");
        Write($"tiny+tiny+1 > 1  ? {b>1}\n");
        Write("which means when you add something tiny to something that is big, it does not register \n");
    

        // opg 4
        double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
        double d2 = 8*0.1;
        Write($"so is d1 = d2? My function says approx(d1,d2) = {approx(d1,d2)} \n");
    return 0;
    }

    public static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
	if(System.Math.Abs(b-a) <= acc) return true;
	if(System.Math.Abs(b-a) <= System.Math.Max(System.Math.Abs(a),System.Math.Abs(b))*eps) return true;
	return false;
    }
}