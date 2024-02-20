using static System.Console;

public static class main{
    public static int Main(){
        vec k = new vec(4,5,-7);
        vec u = new vec(11,-8,9);
        //vec j = new vec(-0.1,2,0);
        System.Console.Write("Addition tests: \n");
        //System.Console.Write($"checking k+u = u+k, vec({k+u}) = vec({u+k}) \n");
        //System.Console.Write($"checking k+j+u = u+j+k, vec({k+j+u}) = vec({u+j+k}) \n");
        vec exp1 = k+u;
        if (exp1.x == k.x+u.x){System.Console.Write("passed \n");}
        if (exp1.y == k.y+u.y){System.Console.Write("passed \n");}
        if (exp1.z == k.z+u.z){System.Console.Write("passed \n");}

        System.Console.Write("Subbtraction tests: \n");
        vec exp2 = k-u;
        if (exp2.x == k.x-u.x){System.Console.Write("passed \n");}
        if (exp2.y == k.y-u.y){System.Console.Write("passed \n");}
        if (exp2.z == k.z-u.z){System.Console.Write("passed \n");}

        System.Console.Write("Multiplication test: \n");
        double var = 3 ;
        vec exp5 = var*u ;     
        if (exp5.x == var*u.x){System.Console.Write("passed \n");}
        if (exp5.y == u.y*var){System.Console.Write("passed \n");}
        if (exp5.z == u.z*var){System.Console.Write("passed \n");}


        System.Console.Write("dot product test: \n");
        double exp3 = vec.dot(u,k);
        double exp4 = vec.dot(k,u);
        if (exp3 == exp4){System.Console.Write("passed \n");}

        System.Console.Write("cross product test: \n");
        vec exp6 = vec.cross(u,k);
        vec exp7 = vec.cross(k,u);
        if (exp6 != exp7){System.Console.Write("passed \n");}
        if (exp6.x == u.y*k.z-u.z*k.y){System.Console.Write("passed \n");}
        if (exp6.y == u.z*k.x-u.x*k.z){System.Console.Write("passed \n");}
        if (exp6.z == u.x*k.y-u.y*k.x){System.Console.Write("passed \n");}

        System.Console.Write("norm test\n");
        if (vec.norm(u)== System.Math.Pow(System.Math.Pow(u.x,2)+System.Math.Pow(u.y,2)+System.Math.Pow(u.z,2),0.5))
        {System.Console.Write("passed \n");}
        if (vec.norm(k)== System.Math.Pow(System.Math.Pow(k.x,2)+System.Math.Pow(k.y,2)+System.Math.Pow(k.z,2),0.5))
        {System.Console.Write("passed \n");}

        System.Console.Write("approximation test\n");
        if (u.approx(u)){System.Console.Write("passed \n");}
        if (!(u.approx(k))){System.Console.Write("passed \n");}
        if (!(k.approx(u))){System.Console.Write("passed \n");}

        return 0;
    } //Main
} //class