CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

Out.txt: main.exe
	echo 1 2 3 4 5 | mono $< 1>$@ 2>log

#sfuns.dll: sfuns.cs ; $(MKLIB)

main.exe : inout.cs; $(MKEXE)


clean:
	$(RM) *.exe [Oo]ut*
