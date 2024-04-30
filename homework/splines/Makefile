mat_lib = ../presets
QR_lib = ../linear-equations
JCOBI = ../EVD/jacobi.cs


# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default : linearspline.png quadspline.png

#A

linearspline.png : linearspline.gpi out.data.txt
	gnuplot -e "load 'linearspline.gpi'"


out.data.txt: main.exe
	mono $< >$@ 2>log


main.exe : main.cs splines.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

#B 

quadspline.png : quadspline.gpi outB.txt
	gnuplot -e "load 'quadspline.gpi'"

outB.txt: mainB.exe
	mono $< >$@ 2>log

mainB.exe : mainB.cs quadspline.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)




splines.dll: splines.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

quadspline.dll: quadspline.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*
