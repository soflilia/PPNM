Out.txt : epsilon.exe              # Out.txt depends on hello.exe
	mono epsilon.exe > Out.txt 

epsilon.exe : epsilon.cs             # hello.exe depends on hello.cs
	mcs epsilon.cs             

clean:                           # a phoney target, no dependencies
	rm -f Out.txt epsilon.exe 