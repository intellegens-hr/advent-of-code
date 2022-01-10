# AOC 2021 TASK 2
lines = open("2zad.txt").read().splitlines()
horizontal, depth = 0, 0
depth_1 = 0
aim = 0
for line in lines:
    instruction = line.split()[0]
    step = int(line.split()[1])
    if instruction == 'forward':
        horizontal += step
        depth += aim*step
    elif instruction == 'down':
        aim += step
        depth_1 += step
    elif instruction == 'up':
        aim -= step
        depth_1 -= step
# P1
print(horizontal * depth_1)
#P2
print(horizontal * depth)


