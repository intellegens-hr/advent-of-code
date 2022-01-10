lines = [line.strip() for line in open('9zad.txt').readlines()]
matrix = [ [ 0 for i in range(len(lines[j])) ] for j in range(len(lines)) ]
for i in range(len(lines)):
    for j in range(len(lines[i])):
        matrix[i][j] = int(lines[i][j])
# PRINT MATRIX
for i in range(len(lines)):
    for j in range(len(lines[i])):
        print(matrix[i][j], end= " ")
    print()

def c_n(i,j, matrix, max_len_i, max_len_j):
    val = []
    if i-1<0:
        val.append(10000000000000)
    else:
        val.append(matrix[i-1][j])
    if i +1 >= max_len_i:
        val.append(10000000000000)
    else:
        val.append(matrix[i + 1][j])
    if j - 1 < 0:
        val.append(10000000000000)
    else:
        val.append(matrix[i][j-1])
    if j +1 >= max_len_j:
        val.append(10000000000000)
    else:
        val.append(matrix[i][j+1])
    return val
visited = []
ml= []

def calc_b(k, matrix):
  basins = []
  for x, y in k:
    visited = set()
    stack = [(x, y)]
    while len(stack):
      x, y = stack.pop()
      if (x, y) in visited or matrix[y][x] == 9:
        continue
      visited.add((x, y))
      if x > 0:
        stack.append((x - 1, y))
      if x < len(matrix[0]) - 1:
        stack.append((x + 1, y))
      if y > 0:
        stack.append((x, y - 1))
      if y < len(matrix) - 1:
        stack.append((x, y + 1))
    basins.append(len(visited))
  return basins

l = []
k = []
for i in range(len(lines)):
    for j in range(len(lines[i])):
        if matrix[i][j]<min(c_n(i,j,matrix, len(lines), len(lines[i]))):
            l.append(matrix[i][j]+1)
            k.append((j,i))
# P1
s = 0
for i in l:
    s+=i
print(s)
b = []
b = calc_b(k, matrix)
b.sort(reverse=True)
multi = b[0]*b[1]*b[2]
print(multi)


