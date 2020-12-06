def part_two(numbers):
    for i, number1 in enumerate(numbers):
        for j, number2 in enumerate(numbers[i:-1]):
            for number3 in numbers[j:-1]:
                if number1 == number2 == number3:
                    continue
                if number1 + number2 + number3 == 2020:
                    return number1 * number2 * number3

def part_one(numbers):
    for i, number1 in enumerate(numbers):
        for number2 in numbers[i:-1]:
            if number1 == number2:
                continue
            if number1 + number2 == 2020:
                return number1 * number2

def main():
    numbers = []
    with open("./report.txt", "r") as file:
        numbers = [int(x) for x in file.read().split()]
    print("Part one: ", part_one(numbers))
    print("Part two: ", part_two(numbers))


if __name__ == "__main__":
    main()
