# AOC TASK 1
def find_differences(lines, step):
    difference_count = 0
    for i in range(len(lines) - step):
        if lines[i] < lines[i + step]:
            difference_count += 1
    return difference_count


def main():
    f = open("1zad.txt")
    lines = [int(line.strip()) for line in f.readlines()]
    # A
    print("Part A", find_differences(lines, 1))
    # B
    print("Part B", find_differences(lines, 3))


main()
