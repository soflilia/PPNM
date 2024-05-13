CODE = $(filter %.cs,$^)
#LIBS = $(addprefix -reference:,$(filter %.dll,$^))

MKEXE = mcs -target:exe -out:$@ $(CODE) $(LIBS)
#MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default: out-arcparse.txt out-fileparse.txt out-stndinout.txt



out-fileparse.txt: fileparse.exe
	mono $< -input:input.txt -output:out-fileparse.txt

out-arcparse.txt: arcparse.exe
	mono $< -numbers:1,2,3,4,5 > $@

out-stndinout.txt: stndinout.exe
	echo 1 2 3 4 5 | mono stndinout.exe > $@


arcparse.exe : arcparse.cs; $(MKEXE)

fileparse.exe : fileparse.cs; $(MKEXE)

stndinout.exe : stndinout.cs; $(MKEXE)


clean:
	$(RM) *.exe [Oo]ut*
