using static System.Console;
using System;
using static genlist<double[]>;

static class ListeLav{
    static void Main(){
        var list = new genlist<double[]>();
        char[] delimiters = {' ','\t', '\n'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        for(string line = ReadLine(); line!=null; line = ReadLine()){
	        var words = line.Split(delimiters,options);
	        int n = words.Length;
	        var numbers = new double[n];
	        for(int i=0;i<n;i++) {numbers[i] = double.Parse(words[i]);
	            list.add(numbers);}
       	    }
        for(int i=0;i<list.size;i++){
	        var numbers = list[i];
	        foreach(var number in numbers)Write($"{number : 0.00e+00;-0.00e+00} ");
	            WriteLine();
            }
        
        var list2 = new genlist<double>();
        for (double i = 0; i<10; i++){
            list2.add(i);
        }
        Write($"before I remove:{list2.size} \n");
        for(int i = 0; i<list2.size; i++){Write($"list2[i]={list2[i]}\n");}
        list2.remove(7);
        Write($"after I remove:{list2.size}\n");
        for(int i = 0; i<list2.size; i++){Write($"list2[i]={list2[i]}\n");}
    }
}