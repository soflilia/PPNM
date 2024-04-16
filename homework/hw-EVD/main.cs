using static EVD;
using static matrix;
using static QRGS;
using System;
using System.Collections.Generic;
using System.IO;


class MainEVD{
    public static void Main(string[] args){
        matrix A = QRGS.random_symmetric();
        if (A.approx(A.T)){Console.Write("True \n");}
        else{Console.Write("False \n");};

        //A.print();
        EVD instance_evd = new EVD(A);
        //A.print();

        //Kalder mine ting
        matrix V1 = instance_evd.V;
        matrix D1 = instance_evd.D;

        Console.Write("Checking if V^T*V=1 \n");
        matrix VTV = V1.T*V1;
        //VTV.print();
        if (VTV.approx(matrix.id(V1.size1))){Console.Write("True\n");}
        else{Console.Write("False\n");}


        Console.Write("Checking if V*V^T=1 \n");
        matrix VVT = V1*V1.T;
        //VTV.print();
        if (VVT.approx(matrix.id(V1.size1))){Console.Write("True\n");}
        else{Console.Write("False\n");}

        Console.Write("Checking if V^T*A*V=D \n");
        matrix VTA = V1.T*A;
        matrix VTAV = VTA*V1;
        if (VTAV.approx(D1)){Console.Write("True\n");}
        else{Console.Write("False\n");}     

        Console.Write("Checking if V*D*V^T=A \n");
        matrix VD= V1*D1;
        matrix VDVT= VD*V1.T;
        if (VDVT.approx(A)){Console.Write("True\n");}
        else{Console.Write("False\n");}

        // OPG B NU SKAL DEN LÆSE MIT INPUT I VERSION EX: "mono main.exe -rmax:10 -dr:0.3"
        int rmax= 0;
        double dr = 0.0;
        foreach(var arg in args){
            var cuttings = arg.Split(new[] {':'}, 2);
            if(cuttings[0] == "-rmax") {rmax = int.Parse(cuttings[1]);}
            if(cuttings[0] == "-dr"){dr = double.Parse(cuttings[1]);}
            }
        Console.Write($"we get rmax = {rmax}, and dr = {dr} \n");
        // if we have values we compute the hamiltonian for our manual values
        if(rmax == 0 || dr == 0.0){
            Console.Error.WriteLine("wrong filename argument, use instead: mono main.exe -rmax:<value> -dr:<value>");
            }
        else{
            // computing the Hamiltonian and solution
            EVD hydrogen = new EVD(hamiltonian(rmax,dr));
            matrix eigenvecs_h= hydrogen.V;
            vector eigenvals_h = hydrogen.w;
            vector ground_vec = hydrogen.ground_eigenvec;
            double ground_energy = hydrogen.ground_value;
            //printing for the case with our manually chosen rmax and dr
            Console.Write("eigenvectors:\n");
            eigenvecs_h.print();
            Console.Write("eigenvalues:\n");
            eigenvals_h.print();
            // finding lowest states = ground state
            Console.Write($"Meaning our ground state has energy:{ground_energy}\n");
            Console.Write("With corresponding eigen vector:\n");
            ground_vec.print();

            }//manual example over       

        // Plot E som function af dr og rmax
        var results1 = graph_dr(10);
        double [] drs = results1.Item1;
        double [] energies = results1.Item2;

        //rmax som funktion af dr:
        //NOGET ER GALT HER, DE KONVERGERER FOR STORE DR IKKE SMÅ???
        var results2 = graph_rmax(0.3);
        double [] rmaxs = results2.Item1;
        double [] energies2 = results2.Item2;

        // Specify the path to the output files
        string output_dr = "output_dr.txt";
        string output_e1 =  "output_e1.txt";
        string output_rmax = "output_rmax.txt";
        string output_e2 = "output_e2.txt";

        //laver mine resultatet til txt filer
        try{
            //kode der skriver ting ind i txt filer
            using(StreamWriter writer = new StreamWriter(output_dr))
            {foreach(double val in drs){writer.WriteLine($"{val}");}}

            using(StreamWriter writer2 = new StreamWriter(output_rmax))
            {foreach(double val in rmaxs){writer2.WriteLine($"{val}");}}

            using(StreamWriter writere1 = new StreamWriter(output_e1))
            {foreach(double val in energies){writere1.WriteLine($"{val}");}}

            using(StreamWriter writere2 = new StreamWriter(output_e2))
            {foreach(double val in energies2){writere2.WriteLine($"{val}");}}
            }
        catch (Exception ex){
            Console.WriteLine($"not computing converge-data into txt files: {ex.Message}\n");
            }

        //Plot wavefunction (jeg sætter rmax=10 og dr=0.3)
        double dr_opgb = 0.3;
        int rmax_opgb = 10;
        matrix wavess = hamiltonian(rmax_opgb,dr_opgb);
        EVD waves = new EVD(wavess);
        // V VEKTOR ER PUNTKER MED VÆRDIER I DR AFSTAND FRA HINANDEN
        matrix wave_funcs = waves.V;
        vector wave_energies = waves.w;
        //wave_funcs[0].print();
        //wave_energies.print();
        double [] f_1s = new double[wave_funcs.size1];
        double [] f_2s = new double[wave_funcs.size1];
        double [] f_3s = new double[wave_funcs.size1];
        double [] rs = new double[wave_funcs.size1];
        double normalisation = 1/Math.Sqrt(dr_opgb);
        for (int i = 0; i< wave_funcs.size1; i++){
            rs[i] = (i+1)*dr_opgb;
            f_1s[i] = Math.Pow(wave_funcs[0,i],2)*normalisation;
            f_2s[i] = Math.Pow(wave_funcs[1,i],2)*normalisation;
            f_3s[i] = Math.Pow(wave_funcs[2,i],2)*normalisation;
        }

        string output_f1 = "f1.txt";
        string output_f2 = "f2.txt";
        string output_f3 = "f3.txt";
        string output_rs = "rs.txt";

        try{
            using(StreamWriter wavewriter1 = new StreamWriter(output_f1))
            {foreach(double val in f_1s){wavewriter1.WriteLine($"{val}");}}

            using(StreamWriter wavewriter2 = new StreamWriter(output_f2))
            {foreach(double val in f_2s){wavewriter2.WriteLine($"{val}");}}

            using(StreamWriter wavewriter3 = new StreamWriter(output_f3))
            {foreach(double val in f_3s){wavewriter3.WriteLine($"{val}");}}

            using(StreamWriter wavewriter4 = new StreamWriter(output_rs))
            {foreach(double val in rs){wavewriter4.WriteLine($"{val}");}}
        }
        catch(Exception ex){
            Console.WriteLine($"not computing wave-data into txt files: {ex.Message}\n");
        }

    } // main stops

    public static matrix jacobian(int p,int q,double theta, matrix A){
        matrix jacobian = matrix.id(A.size1);
        jacobian[p,p] = Math.Cos(theta);
        jacobian[q,q] = Math.Cos(theta);
        jacobian[q,p] = - Math.Sin(theta);
        jacobian[p,q] = Math.Sin(theta);
        return jacobian;
    }

    public static matrix hamiltonian(int rmax,double dr){
        int npoints = (int)(rmax/dr)-1;
        if(npoints==0){npoints = 1;}
        //for(int i=0;i<npoints;i++){r[i]=dr*(i+1);}
        matrix H = new matrix(npoints,npoints);
        // adding the K matrix to H
        for(int i=0;i<npoints-1;i++){
            H[i,i]  =-2*(-0.5/dr/dr);
            H[i,i+1]= 1*(-0.5/dr/dr);
            H[i+1,i]= 1*(-0.5/dr/dr);
            }
        // sætter lige den sidste som vi ikke fik med i tidligere loop
        H[npoints-1,npoints-1]=-2*(-0.5/dr/dr);
        // computerer W matrice ind i H
        for(int i=0;i<npoints;i++){H[i,i]+=-1/(dr*(i+1));}
        return H;
    }

    public static (double[],double[]) graph_dr(int rmax){
        // initierer lister
        int iterations = 10;
        double[] drs = new double[iterations];
        double[] energies = new double[iterations];
        for(int i=0; i < iterations; i++){
            double dr = (i+1)*0.1;
            matrix hamils = hamiltonian(rmax,dr);
            EVD sols = new EVD(hamils);
            drs[i] = dr;
            energies[i] = sols.ground_value;
            }
        return (drs,energies);
    }

    public static (double[],double[]) graph_rmax(double dr){
        // initierer lister
        int iterations = 10;
        double[] rmaxs = new double[iterations];
        double[] energies = new double[iterations];
        for(int i=0; i<iterations; i++){
            int rmax = 3+3*i;
            matrix hamils = hamiltonian(rmax,dr);
            EVD sols = new EVD(hamils);
            rmaxs[i] = rmax;
            energies[i] = sols.ground_value;
        }
        return (rmaxs,energies);
    }


} // EVD STOPS