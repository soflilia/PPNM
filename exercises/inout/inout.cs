using System;
using static System.Console;
using static System.Math;

// opg 1

/*
public class inout{
    public static void Main(string[] args){ //læser en liste af strings (type string[]) og kalder dem args
    WriteLine("x sin(x) cos(x)");

    //OPGAVE 
        foreach(var arg in args){
            var stuff = arg.Split(':');
            if (stuff[0]=="-numbers"){
                var numbers = stuff[1].Split(',');
                foreach(var num in numbers){
                    double x = double.Parse(num);
                    Write($"{x} {Sin(x)} {Cos(x)}\n");
                }
            }
        }
    } //main
}//class
*/

/*
public class inout{
    public static void Main(){
        char[] splitters = {' ', '\t', '\n'};
        var split_opt = StringSplitOptions.RemoveEmptyEntries;
        string line;
        while( (line = ReadLine()) != null){
            var numbers = line.Split(splitters,split_opt);
            foreach(var num in numbers)
            {
                // blev ved med at få en System.FormatException fejl 
                try
                {
                    double x = double.Parse(num);
                    Write($"{x} {Sin(x)} {Cos(x)}\n");
                }
                catch (FormatException)
                {
                    Error.WriteLine($"Failed to parse: {num}"); // Debug output
                }
            }
        }

    }
}
*/

//mono main.exe -input:input.txt -output:out.txt
// args[0]= -input:input.txt
//args[1]= -output:output.txt

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
            Error.WriteLine("wrong filename argument");
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