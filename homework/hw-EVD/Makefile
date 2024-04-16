mat_lib = ../presets
QR_lib = ../hw-linear-equations
GNUVAR= "dr = ('output_dr.txt')" 
GNUVAR2= "e1 = ('output_e1.txt')" 

GNUVAR3 = "rmax = ('output_rmax.txt')"
GNUVAR4 = "e2 = ('output_e2.txt')"

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

output_e1.txt: main.exe
	mono $< >$@

output_dr.txt: main.exe
	mono $< >$@

eigenval.svg : eigenplot.gpi output_dr.txt output_e1.txt
	gnuplot -e "load 'eigenplot.gpi'"


eigenval2.svg : eigenplot2.gpi output_rmax.txt output_e2.txt
	@echo "Building eigenval2.svg..."
	gnuplot -e "load 'eigenplot2.gpi'"

wavefunctions.svg : wavefunctions.gpi
	gnuplot -e "load 'wavefunctions.gpi'"

Out-%.txt: %.exe
	mono $< >$@

main.exe : main.cs matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

jacobi.dll: jacobi.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*

