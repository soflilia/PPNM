CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

MKEXE = mcs -target:exe -out:$@ $(CODE) $(LIBS)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

output.txt: main.exe
	mono $< > $@

main.exe : main.cs cmath.dll complex.dll; $(MKEXE)

cmath.dll : cmath.cs complex.dll ; $(MKLIB)
complex.dll : complex.cs ; $(MKLIB)
clean:
	$(RM) *.exe [Oo]ut*