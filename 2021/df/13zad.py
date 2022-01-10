def display(coords):
    yval=int(max([z[1] for z in coords]))
    xval=int(max([z[0] for z in coords]))
    for y in range(yval+1):
        row=''
        for x in range(xval+1):
            if (x,y) in coords:
                row+='#'
            else:
                row+='.'
        print(row)
    print('\n')

input = open('13zad.txt').readlines()
coords = []
folds = []
for i in input:
    if not i.startswith("fold") and i != "\n":
        input_coords = i.strip().split(",")
        coords.append((int(input_coords[0]), int(input_coords[1])))
    elif i.startswith("fold"):
        fold_coords = i.strip().split("=")
        folds.append((fold_coords[0].split(" ")[2], int(fold_coords[1])))
x_maximum = sorted(coords, key=lambda x: x[0], reverse=True)[0][0]
y_maximum = sorted(coords, key=lambda x: x[1], reverse=True)[0][1]
matrix = [[0 for j in range(y_maximum)] for i in range(x_maximum)]
signs = {".": 0, "#": 1}
all_p = []
for i in range(x_maximum):
    for j in range(y_maximum):
        all_p.append((i,j))

# PRINT MATRIX
def print_matrix(matrix, x, y):
    for i in range(x):
        for j in range(y):
            print(matrix[i][j], end="")
        print()

def make_matrix():
    for i in range(x_maximum):
        for j in range(y_maximum):
            if (i, j) in coords:
                matrix[i][j] = "#"
            else:
                matrix[i][j] = " "
def fold(direction, step, coords, first=False):
    if direction == "y":
        new_coords = list(set(list(filter(lambda c: c[1] <= step, coords))))
        for coordinate in filter(lambda c: c[1] > step, coords):
            new_y = 2*step - (coordinate[1])
            new_coords.append((coordinate[0], new_y))
    elif direction == "x":
        new_coords = list(set(list(filter(lambda c: c[0] <= step, coords))))
        for coordinate in filter(lambda c: c[0] > step, coords):
            new_x = 2*step - (coordinate[0])
            new_coords.append((new_x, coordinate[1]))
    coords = new_coords.copy()
    if (first):
        print(len(set(coords)))
first = True
for i in range(len(folds)):
    fold(folds[i][0], folds[i][1], coords, first)
    first = False
#display(coords)




