s11=`python ./brainfuck.py ./slope11.bf ./input.txt | tr -cd '#' | wc -c`
s31=`python ./brainfuck.py ./slope31.bf ./input.txt | tr -cd '#' | wc -c`
s51=`python ./brainfuck.py ./slope51.bf ./input.txt | tr -cd '#' | wc -c`
s71=`python ./brainfuck.py ./slope71.bf ./input.txt | tr -cd '#' | wc -c`
s12=`python ./brainfuck.py ./slope12.bf ./input.txt | tr -cd '#' | wc -c`

echo "Part 1: $s31"
echo "Part 2: $(($s11 * $s31 * $s51 * $s71 * $s12))"
