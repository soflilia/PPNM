mat_lib = ../presets
QR_lib = ../linear-equations

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default : linearspline.png quadspline.png cubicc_outlier.png cubicc_disc.png cubicc_disc1.png 

#A

linearspline.png : linearspline.gpi out.data.txt
	gnuplot -e "load 'linearspline.gpi'"


out.data.txt: main.exe
	mono $< >$@ 2>log


main.exe : main.cs splines.dll matrixlib.dll vector.dll linear.dll; $(MKEXE)

#B 

quadspline.png : quadspline.gpi outBC.txt
	gnuplot -e "load 'quadspline.gpi'"

cubicc_outlier.png : cubicc_outlier.gpi outBC.txt
	gnuplot -e "load 'cubicc_outlier.gpi'"

cubicc_disc.png : cubicc_disc.gpi outBC.txt
	gnuplot -e "load 'cubicc_disc.gpi'"

cubicc_disc1.png : cubicc_disc1.gpi outBC.txt
	gnuplot -e "load 'cubicc_disc1.gpi'"


mainBC.exe : mainBC.cs cubic.dll quadspline.dll matrixlib.dll vector.dll linear.dll; $(MKEXE)

outBC.txt: mainBC.exe
	mono $< >$@ 2>log



splines.dll: splines.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

quadspline.dll: quadspline.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

cubic.dll: cubic.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*
