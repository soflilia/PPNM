Out.txt : class4.exe              # Out.txt depends on hello.exe
	mono class4.exe > Out.txt # run hello.exe, send output to Out.txt

class4.exe : class4.cs             # hello.exe depends on hello.cs
	mcs class4.cs             # compile hello.cs into hello.exe

clean:                           # a phoney target, no dependencies
	rm -f Out.txt class4.exe  # remove secondary files