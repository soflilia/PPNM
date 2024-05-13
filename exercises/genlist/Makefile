CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

MKEXE = mcs -target:exe -out:$@ $(CODE) $(LIBS)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

output.txt: main.exe input.txt
	cat input.txt | mono main.exe > output.txt

main.exe : main.cs genlist.dll; $(MKEXE)

genlist.dll : genlist.cs 
	mcs -target:library -out:genlist.dll genlist.cs

clean:
	$(RM) *.exe [Oo]ut*