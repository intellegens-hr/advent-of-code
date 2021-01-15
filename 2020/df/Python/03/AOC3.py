file = open("3zad.txt","r").read().splitlines()
matrix = []
for line in file:
    pom_list = []
    for i in line:
        pom_list.append(i=="#")
    matrix.append(pom_list)

def num_of_trees(matrix, x, y):
    k=1
    sum = 0
    for line in matrix[y::y]:
        sum += line[x*(k)%len(line)]
        k += 1
    return sum

# Part1
print(num_of_trees(matrix, 3, 1))

# Part2
trail = [(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)]
prod = 1
for i in trail:
    prod*=(num_of_trees(matrix, i[0], i[1]))
print(prod)
