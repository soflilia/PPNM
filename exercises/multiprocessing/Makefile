N=1e9
TIME = time --portability --append --output $@

default: Out.txt

main.exe: main.cs multi.dll
	mcs -target:exe -out:main.exe -reference:multi.dll main.cs

Out.txt : main.exe multi.dll Makefile
	>$@ 2>log
	echo "Hav tålmodighed... " >> $@
	echo "threads = 1">>$@ 
	$(TIME) mono $< -terms:$N -threads:1 -method:1 >>$@ 2>>log
	echo "threads = 2">>$@ 
	$(TIME) mono $< -terms:$N -threads:2 -method:1 >>$@ 2>>log
	echo "method 2">>$@ 
	$(TIME) mono $< -terms:$N -threads:2 -method:2 >>$@ 2>>log
	echo "method 3">>$@ 
	$(TIME) mono $< -terms:$N -threads:2 -method:3 >>$@ 2>>log

multi.dll : multi.cs 
	mcs -target:library -out:multi.dll multi.cs

clean:
	rm -f Out.txt main.exe