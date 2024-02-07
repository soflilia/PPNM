Out.txt : class4.exe              # Out.txt depends on hello.exe
	mono class4.exe one two tree > Out.txt # one two three are the arguments 

class4.exe : class4.cs             # hello.exe depends on hello.cs
	mcs -target:exe -out: $@ $<     

clean 