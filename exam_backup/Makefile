mat_lib = ../homework/presets
QR_lib = ../homework/linear-equations

# $@ er target, dvs det før : i din lille kode

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE) 
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default: cubic_cos.png cubic_outlier.png cubic_outlier1.png cubic_disc.png cubic_disc1.png

cubic_cos.png : cubic_cos.gpi out.data.txt
	gnuplot -e "load 'cubic_cos.gpi'"

cubic_outlier.png : cubic_outlier.gpi out.data.txt
	gnuplot -e "load 'cubic_outlier.gpi'"

cubic_outlier1.png : cubic_outlier1.gpi out.data.txt
	gnuplot -e "load 'cubic_outlier1.gpi'"

cubic_disc.png : cubic_disc.gpi out.data.txt
	gnuplot -e "load 'cubic_disc.gpi'"

cubic_disc1.png : cubic_disc1.gpi out.data.txt
	gnuplot -e "load 'cubic_disc1.gpi'"

out.data.txt: main.exe
	mono $< >$@ 2>log


main.exe : main.cs cubic.dll matrixlib.dll vector.dll linear.dll; $(MKEXE)



#biblioteker jeg måske skal bruge

cubic.dll: cubic.cs matrixlib.dll vector.dll linear.dll; $(MKLIB)

linear.dll : $(QR_lib)/main.cs matrixlib.dll vector.dll; $(MKLIB)

vector.dll : $(mat_lib)/vector.cs ; $(MKLIB)

matrixlib.dll:$(mat_lib)/matrix.cs vector.dll ; $(MKLIB)


clean:
	$(RM) *.exe [Oo]ut*