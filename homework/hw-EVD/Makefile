mat_lib = ../presets
QR_lib = ../hw-linear-equations

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

Out-%.txt: %.exe
	mono $< >$@

main.exe : main.cs matrixlib.dll vector.dll linear.dll jacobi.dll; $(MKEXE)

jacobi.dll: jacobi.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*

