using static System.Math;
using static System.Console;

public class vec{
    public double x,y,z;

    //constructors = initial conditions:
    public vec(){ x=y=z=0; }
    public vec(double x,double y,double z){ this.x=x; this.y=y; this.z=z; }

    // operators = defining how *, + and - are done in this special vec class
    public static vec operator*(vec v, double c){return new vec(c*v.x,c*v.y,c*v.z);}
    public static vec operator*(double c, vec v){return v*c;}
    public static vec operator+(vec u, vec v){return new vec(u.x+v.x,u.y+v.y,u.z+v.z);}
    public static vec operator-(vec u){return new vec(-u.x,-u.y,-u.z);}
    public static vec operator-(vec u, vec v){return new vec(u.x-v.x,u.y-v.y,u.z-v.z);}


    //methods = making a new print method
    public void print(string s){Write(s); WriteLine($"{x} {y} {z}");}
    public void print(){this.print("");}

    // dot product
    public double dot(vec other) /* to be called as u.dot(v) */
	{return this.x*other.x+this.y*other.y+this.z*other.z;}
    public static double dot(vec v,vec w) /* to be called as vec.dot(u,v) */
	{return v.x*w.x+v.y*w.y+v.z*w.z;}

    // cross product
    public static vec cross(vec v,vec w) /* to be called as vec.cross(u,v) */
	{return new vec(v.y*w.z-v.z*w.y, v.z*w.x-v.x*w.z, v.x*w.y-v.y*w.x);
    }

    //norm 
    public static double norm(vec v) /* to be called as vec.norm(v) */
	{return System.Math.Pow(System.Math.Pow(v.x,2)+System.Math.Pow(v.y,2)+System.Math.Pow(v.z,2),0.5);
    }

    static bool approx(double a,double b,double acc=1e-9,double eps=1e-9){
	if(Abs(a-b)<acc)return true;
	if(Abs(a-b)<(Abs(a)+Abs(b))*eps)return true;
	return false;
	}

    public bool approx(vec other){
	if(!approx(this.x,other.x))return false;
	if(!approx(this.y,other.y))return false;
	if(!approx(this.z,other.z))return false;
	return true;
	}

    public override string ToString(){ return $"{x} {y} {z}";}
}
