def get_fuel(lines, part):
  min = 100000000000000
  maximum_number = max(lines)
  for i in range(0, maximum_number):
    if (part == 1):
        score = get_total(lines,i)
    elif (part ==2):
        score = get_total_part_2(lines,i)
    if score < min:
        min = score
  return min

def get_total(numbers, i):
    score = 0
    for number in numbers:
        score += abs(i - number)
    return score
def get_total_part_2(numbers, i):
    score = 0
    for number in numbers:
        absolute = abs(i-number)
        score += (absolute * (absolute + 1)) // 2
    return score

lines = [int(line.strip()) for line in open('7zad.txt').read().split(",")]
print(get_fuel(lines,1))
print(get_fuel(lines,2))