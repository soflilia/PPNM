mat_lib = ../presets
QR_lib = ../linear-equations
JCOBI = ../EVD/jacobi.cs

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

#default : sin_cos.png pendul.png LK.png

erf.png: out.data.txt erf.gpi
	gnuplot -e "load 'erf.gpi'"

#opg b) variable trans

#VT.png: outB.txt motion.gpi
#	gnuplot -e "load 'motion.gpi'"

outB.txt: mainB.exe
	mono $< >$@ 2>log

mainB.exe : mainB.cs integration.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)


out.data.txt: main.exe
	mono $< >$@ 2>log

main.exe : main.cs integration.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

integration.dll: integration.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)


jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*