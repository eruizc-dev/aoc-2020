all: build

rebuild: clean build

run: build
	./program.out

clean:
	rm ./*.out

build:
	gfortran ./main.f90 -o ./program.out
