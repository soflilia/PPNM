CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

Erf.gnuplot.svg : Out.txt Makefile
	echo '\
        set terminal svg background "white" ;\
        set out "$@" ;\
        set xlabel "x" ;\
        set ylabel "erf" ;\
        set grid ;\
        set title "Error function" ;\
        plot \
            "$<" with lines notitle \
            , "erf.data.txt" using 1:2 with points pointtype 6 notitle \
            , "erf.data.txt" using (-(1)):(-($$2)) with points pointtype 6 notitle ;\
    ' | tee log.gpi | gnuplot 

#der ved using i erf.gnuplot.svg kan du egentlig sige til den at den skal regne hvadsomhelst
# åbn gnuplot med eog

Out.txt: main.exe
	mono $< >$@

sfuns.dll: sfuns.cs ; $(MKLIB)

main.exe : main.cs sfuns.dll ; $(MKEXE)


clean:
	$(RM) *.exe *.dll [Oo]ut*
