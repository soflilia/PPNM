using static data;
using System;
using System.Linq;

public static class main{
    public static void Main(string[] args){
        int n_threads = 1, nterms = (int)1e8, method = 1; /* default values */
        foreach(var arg in args) {
            var words = arg.Split(':');
            if(words[0]=="-threads"){n_threads=int.Parse(words[1]);}
            if(words[0]=="-terms"  ){nterms  =(int)float.Parse(words[1]);}
            if(words[0]=="-method"){method = int.Parse(words[1]);}
        }
        
        if(method==1){
        data[] instance = new data[n_threads];

        
        for(int i=0;i<n_threads;i++) {
            instance[i] = new data(); //initierer classen
            instance[i].a = 1 + nterms/n_threads*i;
            instance[i].b = 1 + nterms/n_threads*(i+1);
        }
        instance[instance.Length-1].b=nterms+1; /* the enpoint might need adjustment */

        var threads = new System.Threading.Thread[n_threads]; //her laver jeg en liste af threads
        //så når threads[0] threads[1] kaldes vil de være en instances af "Threading.Thread"
        for(int i=0;i<n_threads;i++) {
            threads[i] = new System.Threading.Thread(data.harm); /* create a thread */
            threads[i].Start(instance[i]); /* run it with instance[i] as argument to "harm" */
        }

        foreach(var thread in threads) thread.Join(); //venter til hver thread har regnet

        double total=0; 
        foreach(var p in instance) total+=p.sum;
        Console.Error.Write($"Sum for harm() is: {total}\n");
        }

        // OPG B
        if(method==2){
        double sum1 = par_harm(nterms);
        Console.Error.Write( $"Sum for parrallel harm() is: {sum1}\n");
        }

        if(method==3){
        double totalsum = Linq_harm(nterms);
        Console.Error.Write( $"Sum for Linq harm() is: {totalsum}\n");
        }

    }
    public static double par_harm(int N){
        // bruger forskellige threads til at køre, og har snakket sammen om hvilke 
        // "int" der køres på hvilke, however går de ind og gemmer deres svar
        // efter hver iteration, og hvis de gemmer på samme tid bliver kun den 
        // seneste gemt, der ikke fik den andens ændring med, dvs data går tabt
        // og summen returneret er mindre end faktiske sum
        double sum=0;
        System.Threading.Tasks.Parallel.For( 1, N+1, (int i) => sum+=1.0/i );
        return sum;
    }

    public static double Linq_harm(int N){
        // også et antal threads (2), men efter hver udregning
        // skal de ind og hente et nyt "i", istedet for at de er tildelt
        // liste af integers at regne fra start
        var sum = new System.Threading.ThreadLocal<double>( ()=>0, trackAllValues:true);
        System.Threading.Tasks.Parallel.For( 1, N+1, (int i)=>sum.Value+=1.0/i );
        double totalsum=sum.Values.Sum();
        return totalsum;
    }         

}