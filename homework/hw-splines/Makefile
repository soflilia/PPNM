mat_lib = ../presets
QR_lib = ../hw-linear-equations
JCOBI = ../hw-EVD/jacobi.cs

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

Out-%.txt: %.exe
	mono $< >$@

main.exe : main.cs splines.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

splines.dll: splines.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)

splinesA.svg : splinesA.gpi output_xs.txt output_splines.txt
	gnuplot -e "load 'splinesA.gpi'"



clean:
	$(RM) *.exe [Oo]ut*