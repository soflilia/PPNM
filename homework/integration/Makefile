mat_lib = ../presets
QR_lib = ../linear-equations
JCOBI = ../EVD/jacobi.cs

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default : main.exe mainB.exe erf.png out.data.txt

erf.png: out.data.txt erf.gpi
	gnuplot -e "load 'erf.gpi'"

out.data.txt: main.exe mainB.exe
	(mono main.exe > out.data.txt 2> log)
	(mono mainB.exe >> out.data.txt 2>> log)

main.exe : main.cs integration.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

mainB.exe : mainB.cs integration.dll matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

integration.dll: integration.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)


jacobi.dll: $(JCOBI) matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*