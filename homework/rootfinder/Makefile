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

default: conv1.png conv2.png acc_eps.png

conv1.png: out.data.txt conv1.gpi
	gnuplot -e "load 'conv1.gpi'"

conv2.png: out.data.txt conv2.gpi
	gnuplot -e "load 'conv2.gpi'"


acc_eps.png: out.data.txt acc_eps.gpi
	gnuplot -e "load 'acc_eps.gpi'"

out.data.txt: main.exe
	mono $< >$@ 2>log

main.exe : main.cs rf.dll matrixlib.dll vector.dll linear.dll ode.dll quadspline.dll; $(MKEXE)


rf.dll: rf.cs matrixlib.dll vector.dll jacobi.dll linear.dll ode.dll quadspline.dll; $(MKLIB)

ode.dll: $(ODE) matrixlib.dll vector.dll linear.dll quadspline.dll; $(MKLIB)

quadspline.dll: $(QUADSPLINE) matrixlib.dll vector.dll linear.dll; $(MKLIB)

jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*
