mat_lib = ../presets
QR_lib = ../linear-equations


# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)


#A

circle.png: circle.gpi out.data.txt
	gnuplot -e "load 'circle.gpi'"

out.data.txt: main.exe
	mono $< >$@ 2>log


main.exe : main.cs montecarlo.dll matrixlib.dll vector.dll linear.dll; $(MKEXE)

#B 

#outB.txt: mainB.exe
#	mono $< >$@ 2>log

#mainB.exe : mainB.cs quadspline.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)


montecarlo.dll: MC.cs matrixlib.dll vector.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*
