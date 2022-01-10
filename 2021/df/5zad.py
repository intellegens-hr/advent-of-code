lines = open("5zad.txt").readlines()
data = []
ds = []
maximumRowValue = 0
maximumColumnValue = 0
def generate(to_split):
    new = []
    for i in to_split.split(","):
        new.append(int(i))
    return tuple(new)
def maximum(a, b, c):
    if (a >= b) and (a >= c):
        largest = a
    elif (b >= a) and (b >= c):
        largest = b
    else:
        largest = c
    return largest
def generateDiagram(max_r, max_c):
    d = []
    for i in range(max_r+1):
        temp = []
        for j in range(max_c+1):
            temp.append(0)
        d.append(temp)
    return d
def maxAll(myList):
    return max(myList)
def minAll(myList):
    return min(myList)
def changeDiagramRows(diagram, minRow, maxRow, col):
    for row in range(minRow, maxRow + 1):
        diagram[row][col] += 1
    return diagram
def changeDiagramColumns(diagram, minCol, maxCol, row):
    for col in range(minCol, maxCol + 1):
        diagram[row][col] += 1
    return diagram
def printsolution(part, diagram):
    c = 0
    for d in diagram:
        for v in d:
            if v >= 2:
                c += 1
    print(part, c)
for line in lines:
    splitted_lines = line.strip().split(' -> ')
    data.append((generate(splitted_lines[0]), generate(splitted_lines[1])))
    maximumRowValue = maximum(maximumRowValue, data[-1][0][0], data[-1][1][0])
    maximumColumnValue = maximum(maximumColumnValue, data[-1][0][1], data[-1][1][1])
diagram = generateDiagram(maximumRowValue,maximumColumnValue)

for s in data:
    if s[0][1] == s[1][1]:
        tempList = [s[0][0], s[1][0]]
        minR = minAll(tempList)
        maximumRowValue = maxAll(tempList)
        diagram = changeDiagramRows(diagram, minR, maximumRowValue, s[0][1])
    elif s[0][0] == s[1][0]:
        tempList = [s[0][1], s[1][1]]
        minC = minAll(tempList)
        maximumColumnValue = maxAll(tempList)
        diagram = changeDiagramColumns(diagram, minC, maximumColumnValue, s[0][0])
    else:
        ds.append(s)

printsolution("P1", diagram)
for i in range(len(ds)):
    tempX = -1
    tempY = -1
    if ds[i][0][0] < ds[i][1][0]:
        tempX = 1
    if ds[i][0][1] < ds[i][1][1]:
        tempY = 1
    a = ds[i][0][0]
    b = ds[i][0][1]
    diagram[a][b] += 1
    a += tempX
    b += tempY
    diagram[a][b] += 1
    while (a, b) != ds[i][1]:
        a += tempX
        b += tempY
        diagram[a][b] += 1

printsolution("P2", diagram)
