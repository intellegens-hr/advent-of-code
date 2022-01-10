

lines = [line.strip() for line in open('11zad.txt').readlines()]
matrix = [[0 for i in range(10)] for j in range(10)]
for i in range(10):
    for j in range(10):
        matrix[i][j] = int(lines[i][j])


# PRINT MATRIX
def print_matrix(matrix):
    for i in range(10):
        for j in range(10):
            print(matrix[i][j], end=" ")
        print()


# print(levels)
# print_matrix(matrix)
adjacent=lambda i,j:[(a,b)for a in range(i-1,i+2)for b in range(j-1,j+2)if a>=0 and b>=0 and a<len(matrix)and b<len(matrix[0])]

def step(matrix,i,j):
    global flashes_counter
    global flashed
    if (matrix[i][j] > 9):
        matrix[i][j] = 0
        flashed[(i,j)] = True
        flashes_counter+=1
        for n in adjacent(i,j):
            if (n[0], n[1]) not in flashed:
                matrix[n[0]][n[1]] += 1
            step(matrix,n[0],n[1])


flashes_counter = 0
for k in range(100):
    flashed = {}
    for i in range(10):
        for j in range(10):
            matrix[i][j]+=1
    for i in range(10):
        for j in range(10):
            step(matrix,i,j)
print(flashes_counter)

k = 0
while True:
    flashed = {}
    for i in range(10):
        for j in range(10):
            matrix[i][j] += 1
    for i in range(10):
        for j in range(10):
            step(matrix, i, j)
    zeros = 0
    for i in range(10):
        for j in range(10):
            if matrix[i][j] == 0:
                zeros += 1
    k += 1
    if zeros == 100:
        break
print(k+100)






