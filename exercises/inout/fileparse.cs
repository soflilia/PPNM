using System;
using static System.Console;
using static System.Math;

// opg c
public class inout{
    public static int Main(string[] args){ 
        //her indlæser den hver enhed sepereret af whitespace som ny arg i args

        string infile = null;
        string outfile = null;
        foreach(var arg in args){
            var cuttings = arg.Split(':');
            if (cuttings[0]== "-input") {infile = cuttings[1];}
            if (cuttings[0]== "-output") {outfile = cuttings[1];}
        }
        if(infile == null || outfile == null){
            Error.WriteLine("wrong filename argument, make input");
            return 1;}
        
         // || = "eller" && = "og"

        var instream =new System.IO.StreamReader(infile);
        var outstream=new System.IO.StreamWriter(outfile,append:false);

        for(string line = instream.ReadLine(); line !=null; line = instream.ReadLine()){
	        double x=double.Parse(line);
	        outstream.WriteLine($"{x} {Sin(x)} {Cos(x)}");
        }
        instream.Close();
        outstream.Close();
        return 0;
    }
}