using static EVD;
using static matrix;
using static QRGS;
using System;



class MainEVD{
    public static void Main(){
        matrix A1 = QRGS.random_symmetric();
        if (A1.approx(A1.T)){Console.Write("True \n");}
        else{Console.Write("False \n");};
        matrix A = A1.copy();
        
        
        EVD instance_evd = new EVD(A1);
        matrix JA = jacobian(2,3,2.5,A)*A;
        jacobian(2,3,2.5,A).print();
        Console.Write("Here comes JA and JtimesA \n");
        JA.print();
        Jtimes(A,2,3,2.5);
        A.print();
        if (A.approx(JA)){Console.Write("True \n");}
        else{Console.Write("False \n");};


        matrix V1 = instance_evd.V;
        //V1.print();

        Console.Write("Checking if V^T*V=1 \n");
        matrix VTV = V1.T*V1;
        //VTV.print();
        if (VTV.approx(matrix.id(V1.size1))){Console.Write("True\n");}
        else{Console.Write("False\n");}
        //matrix D1 = instance_evd.D;


    } // main stops
    public static matrix jacobian(int p,int q,double theta, matrix A){
    matrix jacobian = matrix.id(A.size1);
    jacobian[p,p] = Math.Cos(theta);
    jacobian[q,q] = Math.Cos(theta);
    jacobian[q,p] = - Math.Sin(theta);
    jacobian[p,q] = Math.Sin(theta);
    return jacobian;
        }


} // EVD STOPS