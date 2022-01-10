lines = [line.strip() for line in open('3zad.txt').readlines()]
theta = ''
epsilon = ''
def getCount(lines):
	counter = {}
	for line in lines:
		counter[line[i]] = counter.get(line[i], 0) + 1
	return counter
def print_result(theta,epsilon):
	thetaInt = int(theta, 2)
	epsilonInt = int(epsilon, 2)
	result = thetaInt * epsilonInt
	print(result)

for i in range(len(lines[0])):
	counter = getCount(lines)
	if counter['0'] > counter['1']:
		theta += '0'
		epsilon += '1'
	else:
		theta += '1'
		epsilon += '0'
print_result(theta, epsilon)


lines = [line.strip() for line in open('3zad.txt').readlines()]
theta = ''
epsilon = ''
l = []
def countBytes(l, i, sign):
	newList = []
	for b in l:
		if b[i]== sign:
			newList.append(b)
	return newList

for i in range(len(lines[0])):
	counter = getCount(lines)
	if counter['0'] > counter['1']:
		lines = countBytes(lines, i, '0')
	else:
		lines = countBytes(lines, i, '1')
	theta = lines[0]
lines = [line.strip() for line in open('3zad.txt').readlines()]
for i in range(len(lines[0])):
	counter = getCount(lines)
	if '0' in counter and counter['0'] > counter['1']:
		lines = countBytes(lines, i, '1')
	else:
		lines = countBytes(lines, i, '0')
	if lines:
		epsilon = lines[0]
print_result(theta,epsilon)