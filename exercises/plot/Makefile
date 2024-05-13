
default: erfplot.png gamma.png lngamma.png

out.data.txt: main.exe
	mono $< >$@ 2>log

erfplot.png: out.data.txt tab-erf.txt erfplot.gpi
	gnuplot -e "load 'erfplot.gpi'"

gamma.png: out.data.txt tab-gamma.txt gamma.gpi
	gnuplot -e "load 'gamma.gpi'"

lngamma.png: out.data.txt lngamma.gpi
	gnuplot -e "load 'lngamma.gpi'"

main.exe : main.cs
	mcs main.cs  

clean:
	rm -f Out.txt main.exe  # remove secondary files