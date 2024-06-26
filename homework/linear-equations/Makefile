mat_lib = ../presets

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)


default: timeQR.png

out.data.txt: main.exe
	mono $< >$@ 2>log


timeQR.png : timeQR.gpi out.data.txt
	gnuplot -e "load 'timeQR.gpi'"

main.exe : main.cs matrixlib.dll vector.dll ; $(MKEXE)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll: $(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*

