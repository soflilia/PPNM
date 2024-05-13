mat_lib = ../presets
QR_lib = ../linear-equations
JCOBI = ../EVD/jacobi.cs
QUADSPLINE =  ../splines/quadspline.cs
# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default : sin_cos.png pendul.png LK.png motion.png

#opg a) u'' = -u
sin_cos.png: out.data.txt sin_cos.gpi
	gnuplot -e "load 'sin_cos.gpi'"

pendul.png: out.data.txt pendul.gpi
	gnuplot -e "load 'pendul.gpi'"

LK.png: out.data.txt LK.gpi
	gnuplot -e "load 'LK.gpi'"

out.data.txt: main.exe
	mono $< >$@ 2>log

#opg b) motion

motion.png: outB.txt motion.gpi
	gnuplot -e "load 'motion.gpi'"

outB.txt: mainB.exe
	mono $< >$@ 2>log

mainB.exe : mainB.cs ode.dll quadspline.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

main.exe : main.cs ode.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)


ode.dll: ode.cs matrixlib.dll vector.dll linear.dll quadspline.dll; $(MKLIB)

quadspline.dll: $(QUADSPLINE) matrixlib.dll vector.dll linear.dll; $(MKLIB)

jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)




clean:
	$(RM) *.exe [Oo]ut*
