from sys import stdin, stdout, stderr, argv

class Runtime:
    def __init__(self, stream):
        self._stream = stream
        self._pointer = 0
        self._array = [0] * 30000
        self._jumpstack = []

    def get_pointer(self):
        return self._pointer

    def set_pointer(self, pointer):
        self._pointer = pointer % 30000

    def get_value(self):
        return self._array[self._pointer]

    def set_value(self, value):
        self._array[self._pointer] = value % 256

    def increase_pointer(self):
        self.set_pointer(self.get_pointer() + 1)

    def decrease_pointer(self):
        self.set_pointer(self.get_pointer() - 1)

    def increase_value(self):
        self.set_value(self.get_value() + 1)

    def decrease_value(self):
        self.set_value(self.get_value() - 1)

    def to_stdout(self):
        character = chr(self.get_value())
        stdout.write(character)

    def from_input(self):
        character = None
        try:
            character = ord(self._stream.read(1))
        except:
            character = 0
        self.set_value(character)

def exec_code(code, input_stream):
    bracket_stack = []
    code_pointer = 0
    runtime = Runtime(input_stream)

    while code_pointer < len(code):
        if code[code_pointer] == ">":
            runtime.increase_pointer()
        elif code[code_pointer] == "<":
            runtime.decrease_pointer()
        elif code[code_pointer] == "+":
            runtime.increase_value()
        elif code[code_pointer] == "-":
            runtime.decrease_value()
        elif code[code_pointer] == ".":
            runtime.to_stdout()
        elif code[code_pointer] == ",":
            runtime.from_input()
        elif code[code_pointer] == "[":
            bracket_stack.append(code_pointer)
            if runtime.get_value() == 0:
                balance = len(bracket_stack) - 1
                while len(bracket_stack) != balance:
                    code_pointer = code_pointer + 1
                    if code[code_pointer] == "[":
                        bracket_stack.append(code_pointer)
                    elif code[code_pointer] == "]":
                        bracket_stack.pop()
        elif code[code_pointer] == "]":
            if runtime.get_value() != 0:
                code_pointer = bracket_stack[-1]
            else:
                bracket_stack.pop()
        code_pointer = code_pointer + 1

def validate_args(argv):
    length = len(argv)
    return length == 2 or length == 3

def read_file(filename):
    with open(filename, "r") as f:
        return f.read()

def main(argv):

    valid_args = validate_args(argv)

    if not valid_args:
        print("Invalid parameters", file=stderr)
        exit(1)

    code = read_file(argv[1])

    input_stream = stdin if len(argv) == 2 else open(argv[2], "r")
    exec_code(code, input_stream)
    input_stream.close()

if __name__ == "__main__":
    main(argv)
