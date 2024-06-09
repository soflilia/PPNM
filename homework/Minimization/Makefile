mat_lib = ../presets
QR_lib = ../linear-equations
JCOBI = ../EVD/jacobi.cs
ODE = ../ODE/ode.cs
QUADSPLINE =  ../splines/quadspline.cs


# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

out.data.txt: main.exe
	mono $< >$@ 2>log

main.exe : main.cs min.dll matrixlib.dll vector.dll linear.dll; $(MKEXE)


min.dll: min.cs matrixlib.dll vector.dll jacobi.dll linear.dll; $(MKLIB)

#ode.dll: $(ODE) matrixlib.dll vector.dll linear.dll quadspline.dll; $(MKLIB)

#quadspline.dll: $(QUADSPLINE) matrixlib.dll vector.dll linear.dll; $(MKLIB)

jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*
