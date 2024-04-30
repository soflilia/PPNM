mat_lib = ../presets
QR_lib = ../linear-equations
JCOBI = ../EVD/jacobi.cs

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

#opg a) u'' = -u
sin_cos.png: out.data.txt sin_cos.gpi
	gnuplot -e "load 'sin_cos.gpi'"

pendul.png: out.data.txt pendul.gpi
	gnuplot -e "load 'pendul.gpi'"

out.data.txt: main.exe
	mono $< >$@ 2>log


main.exe : main.cs ode.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)


ode.dll: ode.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)


jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*
