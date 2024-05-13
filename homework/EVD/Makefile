mat_lib = ../presets
QR_lib = ../linear-equations
GNUVAR= "dr = ('output_dr.txt')" 
GNUVAR2= "e1 = ('output_e1.txt')" 

GNUVAR3 = "rmax = ('output_rmax.txt')"
GNUVAR4 = "e2 = ('output_e2.txt')"

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

#output_e1.txt: main.exe
#mono $< >$@

#output_dr.txt: main.exe
#mono $< >$@

all: dr_energy.png  rmax_energy.png wavefunctions.png

dr_energy.png : eigenplot.gpi out.data.txt
	gnuplot -e "load 'eigenplot.gpi'"

rmax_energy.png : eigenplot2.gpi out.data.txt
	gnuplot -e "load 'eigenplot2.gpi'"

wavefunctions.png : wavefunctions.gpi out.data.txt
	gnuplot -e "load 'wavefunctions.gpi'"

out.data.txt: main.exe
	mono $< >$@ 2>log

main.exe : main.cs matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

jacobi.dll: jacobi.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*

