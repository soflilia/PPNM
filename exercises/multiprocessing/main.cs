public static class main{
    public void Main(string[] args){
        int n_threads = 1, nterms = (int)1e8; /* default values */   
        foreach(var arg in args) {
            var words = arg.Split(':');
            if(words[0]=="-threads") n_threads=int.Parse(words[1]);
            if(words[0]=="-terms"  ) nterms  =(int)float.Parse(words[1]);
        }

        data[] params = new data[n_threads];
        for(int i=0;i<n_threads;i++) {
            params[i] = new data();
            params[i].a = 1 + nterms/n_threads*i;
            params[i].b = 1 + nterms/n_threads*(i+1);
        }
        params[params.Length-1].b=nterms+1; /* the enpoint might need adjustment */

        var threads = new System.Threading.Thread[n_threads];
        for(int i=0;i<n_threads;i++) {
            threads[i] = new System.Threading.Thread(harm); /* create a thread */
            threads[i].Start(params[i]); /* run it with params[i] as argument to "harm" */
        }

        foreach(var thread in threads) thread.Join();


        double total=0; 
        foreach(var p in params) total+=p.sum;





    }
}