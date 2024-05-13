main.exe: main.cs multi.dll
	mcs -target:exe -out:main.exe -reference:multi.dll main.cs

N=1e8
TIME = time --portability --append --output $@

Out.txt : main.exe multi.dll Makefile
	>$@ 
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	$(TIME) mono $< -terms:$N -threads:4 >>$@


multi.dll : multi.cs 
	mcs -target:library -out:multi.dll multi.cs

clean:
	rm -f Out.txt main.exe