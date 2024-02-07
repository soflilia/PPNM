using System;
using static System.Console;


class main{
static int Main(){
    double x=2, y=1;
    if (x>y) {Write("x>y\n"); }
    else {Write("x<=y\n");}

    /*
    int i;
    for(int i=1; i<10; ++i)Write($"i={i}\n");
    i=0;
    do {Write($"i={i}\n");i++;} while(i<10);
    // do-while first executes then checks, so it will exectue once
    i=0;
    while(i<10) {Write($"i={i}\n");i++;};
    //checks before it does
    */

    int n=5;
    double[] a=new double[n];
    for(int i=0;i<n;i++) a[i]=i+1;
    for(int i=0;i<n;i++) Write($"a[{i}]={a[i]}\n");
    // for X do X

    foreach(var ai in a) Write($"ai={ai}\n");

    foreach(arg in args) Write($"arg = {arg}")
    // for loop = foreach(arg in args)Write($"argument = {arg}\n")
    // args will then be passed when you

return 0;
}//Main
}//class main