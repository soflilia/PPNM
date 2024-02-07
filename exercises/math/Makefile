Out.txt : math.exe              # Out.txt depends on hello.exe
	mono math.exe > Out.txt # run hello.exe, send output to Out.txt

hello.exe : math.cs             # hello.exe depends on hello.cs
	mcs math.cs             # compile hello.cs into hello.exe

clean:                           # a phoney target, no dependencies
	rm -f Out.txt math.exe  # remove secondary files