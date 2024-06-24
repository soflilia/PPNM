using static EVD;
using static matrix;
using static QRGS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Random;


class MainEVD{
    public static void Main(string[] args){
        matrix A = QRGS.random_symmetric();
        if (A.approx(A.T)){Console.Error.Write("Starting with symmetric matrix A\n");}
        else{Console.Error.Write("False \n");};

        EVD instance_evd = new EVD(A);
        //A.print();

        //Kalder mine ting
        matrix V1 = instance_evd.V;
        matrix D1 = instance_evd.D;

        Console.Error.Write("Checking if V^T*V=1 \n");
        matrix VTV = V1.T*V1;
        if (VTV.approx(matrix.id(V1.size1))){Console.Error.Write("True\n");}
        else{Console.Error.Write("False\n");}


        Console.Error.Write("Checking if V*V^T=1 \n");
        matrix VVT = V1*V1.T;
        if (VVT.approx(matrix.id(V1.size1))){Console.Error.Write("True\n");}
        else{Console.Error.Write("False\n");}

        Console.Error.Write("Checking if V^T*A*V=D \n");
        matrix VTA = V1.T*A;
        matrix VTAV = VTA*V1;
        if (VTAV.approx(D1)){Console.Error.Write("True\n");}
        else{Console.Error.Write("False\n");}     

        Console.Error.Write("Checking if V*D*V^T=A \n");
        matrix VD= V1*D1;
        matrix VDVT= VD*V1.T;
        if (VDVT.approx(A)){Console.Error.Write("True\n");}
        else{Console.Error.Write("False\n");}

        // OPG B NU SKAL DEN LÆSE MIT INPUT I VERSION EX: "mono main.exe -rmax:10 -dr:0.3"......................................
        double rmax= 0;
        double dr = 0.0;
        foreach(var arg in args){
            var cuttings = arg.Split(new[] {':'}, 2);
            if(cuttings[0] == "-rmax") {rmax = double.Parse(cuttings[1]);}
            if(cuttings[0] == "-dr"){dr = double.Parse(cuttings[1]);}
            }
        Console.Error.Write($"we get rmax = {rmax}, and dr = {dr} \n");
        // if we have values we compute the hamiltonian for our manual values
        if(rmax == 0 || dr == 0.0){
            Console.Error.WriteLine("you didnt input values, to do this write: mono main.exe -rmax:<value> -dr:<value>");
            }
        else{
            // computing the Hamiltonian and solution
            EVD hydrogen = new EVD(hamiltonian(rmax,dr));
            vector ground_vec = hydrogen.V[0];
            double ground_energy = hydrogen.w[0];
            //printing for the case with our manually chosen rmax and dr
            Console.Error.Write("Computing your input data....");
            // finding lowest states = ground state
            Console.Error.Write($"Ground state has energy:{ground_energy}\n");
            Console.Error.Write("With corresponding eigen vector:\n");
            ground_vec.print();

            }//manual example over       

        // Plot E som function af dr og rmax
        var results1 = graph_dr(10);
        double [] drs = results1.Item1;
        double [] energies = results1.Item2;

        //rmax som funktion af dr:
        //NOGET ER GALT HER, DE KONVERGERER FOR STORE DR IKKE SMÅ???
        var results2 = graph_rmax(0.1);
        double [] rmaxs = results2.Item1;
        double [] energies2 = results2.Item2;
        //
        for(int i=0; i<drs.Length; i++){
            Console.Write($"{drs[i]} {energies[i]}\n");
            }
        Console.Write("\n\n");
        for(int i=0; i<rmaxs.Length; i++){
            Console.Write($"{rmaxs[i]} {energies2[i]}\n");
            }

        Console.Write("\n\n");
        double dr_opgb = 0.1;
        int rmax_opgb = 30;
        matrix wavess = hamiltonian(rmax_opgb, dr_opgb);
        EVD waves = new EVD(wavess);
        matrix wave_funcs = waves.V;
        double ground = waves.w[0];
        Console.Error.WriteLine($"the lowest energy for rmax={rmax_opgb} and dr = {dr_opgb} is {ground}");
        double [] f_1s = new double[wave_funcs.size1];
        double [] f_2s = new double[wave_funcs.size1];
        double [] f_3s = new double[wave_funcs.size1];
        double [] rs = new double[wave_funcs.size1];
        double normalisation = 1/Math.Sqrt(dr_opgb);
        Console.Write($"{0} {0} {0} {0}\n");
        for (int i = 0; i< wave_funcs.size1; i++){
            rs[i] = (i+1)*dr_opgb;
            f_1s[i] = wave_funcs[i,0]*normalisation*Math.Sign(wave_funcs[0,0]);
            f_2s[i] = wave_funcs[i,1]*normalisation*Math.Sign(wave_funcs[0,1]);
            f_3s[i] = wave_funcs[i,2]*normalisation*Math.Sign(wave_funcs[0,2]);
            Console.Write($"{rs[i]} {f_1s[i]} {f_2s[i]} {f_3s[i]}\n");
        }

        Console.Write($"\n\n");

        // OPG C TIMING AF EVD I PARALLEL OG TJEKKE OM DEN GÅR SOM N³
        int[] Ns = new int[] { 5, 10, 20, 30, 40, 50, 60, 70 };
        
        Parallel.ForEach(Ns, N => {
            Time_EVD(N);
        });



    } // main stops


    public static matrix hamiltonian(double rmax,double dr){
        int n = (int)(rmax/dr-1);
        if(n==0){n = 10;}
        //for(int i=0;i<npoints;i++){r[i]=dr*(i+1);}
        matrix H = new matrix(n,n);
        // adding the K matrix to H
        for(int i=0;i<n-1;i++){
            H[i,i]  =2*(0.5/(dr*dr));
            H[i,i+1]= -1*(0.5/(dr*dr));
            H[i+1,i]= -1*(0.5/(dr*dr));
            }
        // sætter lige den sidste som vi ikke fik med i tidligere loop
        H[n-1,n-1]=2*(0.5/(dr*dr));
        // computerer W matrice ind i H
        for(int i=0;i<n;i++){H[i,i]+=-1/(dr*(i+1));}
        return H;
    }

    public static (double[],double[]) graph_dr(double rmax){
        // initierer lister
        int iterations = 10;
        double[] drs = new double[iterations];
        double[] energies = new double[iterations];
        for(int i=0; i < iterations; i++){
            double dr = (i+1)*0.1;
            matrix hamils = hamiltonian(rmax,dr);
            EVD sols = new EVD(hamils);
            drs[i] = dr;
            energies[i] = sols.w[0];
            }
        return (drs,energies);
    }

    public static (double[],double[]) graph_rmax(double dr){
        // initierer lister
        int iterations = 10;
        double[] rmaxs = new double[iterations];
        double[] energies = new double[iterations];
        for(int i=0; i<iterations; i++){
            int rmax = 3+(10-3)*i/(iterations-1);
            matrix hamils = hamiltonian(rmax,dr);
            EVD sols = new EVD(hamils);
            rmaxs[i] = rmax;
            energies[i] = sols.w[0];
        }
        return (rmaxs,energies);
    }

    public static void Time_EVD(int n){
        int trials = 10;
        double time = 0.0;
        Random rnd = new Random(2);
        for (int t =0; t<trials; t++){
            //laver random square matrice med [n,n]

            matrix A = new matrix(n,n);
            for(int i =0 ; i<n; i++){
                for(int j=i; j< n; j++){
                    A[i,j] = A[j,i] = rnd.NextDouble()*20-10;
                    }
                }
            // Display the matrix for debugging
            /*
            for (int row = 0; row < n; row++) {
                for (int col = 0; col < n; col++) {
                    Console.Error.Write($"{A[row, col]}    ");
                }
                Console.Error.Write("\n");
            }*/

            Stopwatch timer = new Stopwatch();
            timer.Start();

            EVD new_evd = new EVD(A);

            timer.Stop();
            time += timer.Elapsed.TotalMilliseconds ;
            Console.Error.Write($"{n}{timer.Elapsed.TotalMilliseconds}\n");
            }
        time /= trials;
        Console.Error.WriteLine($"Matrix size:{n}, Time taken for diagonalization: {time} ms");
        Console.Write($"{n} {time}\n");
    }


} // MainEVD STOPS

