mat_lib = ../presets
QR_lib = ../linear-equations
JCOBI = ../EVD/jacobi.cs
ODE = ../ODE/ode.cs
QUADSPLINE =  ../splines/quadspline.cs
GENLIST = ../../exercises/genlist/genlist.cs


# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default: neural.png neuralderivative.png neural-antideriv.png

out.data.txt: main.exe
	mono $< >$@ 2>log

neural.png: out.data.txt neural.gpi
	gnuplot -e "load 'neural.gpi'"

neuralderivative.png: out.data.txt neuralderivative.gpi
	gnuplot -e "load 'neuralderivative.gpi'"

neural-antideriv.png: out.data.txt neural-antideriv.gpi
	gnuplot -e "load 'neural-antideriv.gpi'"

main.exe : main.cs neural.dll matrixlib.dll vector.dll linear.dll genlist.dll min.dll; $(MKEXE)

neural.dll: neural.cs matrixlib.dll vector.dll jacobi.dll linear.dll genlist.dll min.dll; $(MKLIB)

min.dll: ../Minimization/min.cs matrixlib.dll vector.dll jacobi.dll linear.dll genlist.dll; $(MKLIB)

genlist.dll: $(GENLIST) matrixlib.dll vector.dll; $(MKLIB)

jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*