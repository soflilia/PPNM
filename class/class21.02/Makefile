CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)


#timer hvor lang tid
TIME= time --out=$@ --apend --format "$$nthreads %e %U" mono
N = 2e8

Out.times.svg : out.times.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set xlabel "number of threads" ;\
		set ylabel "real time" ;\
		set title "Running time as function of number of threads" ;\
		plot "$<" with lp ;\
		' | tee log.gpi | gnuplot


out.times.txt : main.exe
	for nthreads in 1 2 3 4; do\
	$(TIME) mono main.exe -nthreads::$$nthreads -nterms: $(N)

main.exe : main.cs ; $(MKEXE)


clean:
	$(RM) *.exe *.dll [Oo]ut* [Ll]og*

test : out1 out2 out3 out4
out1: main.exe ; mono $< -nthreads:1 -nterms:1e8
out2: main.exe ; mono $< -nthreads:1 -nterms:1e8
out3: main.exe ; mono $< -nthreads:1 -nterms:1e8
out4: main.exe ; mono $< -nthreads:1 -nterms:1e8


# the & makes it run in the background
test 2: out1 out2 out3 out4
	mono $< -nthreads:1 -nterms:1e8 > out1 &
	mono $< -nthreads:1 -nterms:1e8 > out2 &
	mono $< -nthreads:1 -nterms:1e8 > out3 &
	mono $< -nthreads:1 -nterms:1e8 > out4 &
