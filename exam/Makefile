mat_lib = ../homework/presets
QR_lib = ../homework/linear-equations

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)


cubic.png : cubic.gpi out.data.txt
	gnuplot -e "load 'cubic.gpi'"

out.data.txt: main.exe
	mono $< >$@ 2>log


main.exe : main.cs quadspline.dll cubic.dll matrixlib.dll vector.dll linear.dll; $(MKEXE)



#biblioteker jeg måske skal bruge

quadspline.dll: quadspline.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

cubic.dll: cubic.cs quadspline.dll matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*