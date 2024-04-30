#GENEREL KODE FOR AT LÆSE EN random.cs FIL!

CODE = $(filter %.cs,$^)
#LIBS = $(addprefix -reference:,$(filter %.dll,$^))

MKEXE = mcs -target:exe -out:$@ $(CODE) $(LIBS)
#MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

Out.txt: main.exe
	mono $< >$@

#sfuns.dll: sfuns.cs ; $(MKLIB)

main.exe : inout.cs; $(MKEXE)


clean:
	$(RM) *.exe [Oo]ut*
