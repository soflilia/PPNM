Out.txt : math.exe              # Out.txt depends on hello.exe
	mono math.exe > Out.txt # run hello.exe, send output to Out.txt

math.exe : math.cs sfuns.dll             # compiler exe fil med library også, ikke kun math.cs
	mcs -target:exe -out:math.exe -reference:sfuns.dll math.cs

sfuns.dll : sfuns.cs 
	mcs -target:library -out:sfuns.dll sfuns.cs

clean:                           # a phoney target, no dependencies
	rm -f Out.txt math.exe  # remove secondary files
