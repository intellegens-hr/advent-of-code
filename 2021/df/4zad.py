f = open("4zad.txt", "r")
data, *all = f.read().split('\n\n')
def addNums(data):
    new_data = [];
    for i in data.split(','):
        new_data.append(int(i))
    return new_data
def addRows(data):
    new_rows = []
    for row in data.split('\n'):
        rows = []
        for i in row.split():
            rows.append(int(i))
        new_rows.append((rows))
    return new_rows

def init(data, all):
    new_data = addNums(data)
    b = []
    for board in all:
        rows = addRows(board)
        temp = []
        for r in rows:
            temp.append(set(r))
        b.append(temp)
        temp = []
        for k in zip(*rows):
            temp.append(set(k))
        b.append(temp)
    return  b, new_data

def calc(b, n):
    s = 0
    for b_1 in b:
        s2 = 0
        for b_2 in b_1:
            s2+=b_2
        s+= s2
    s-=n
    s*=n
    return s


resultInit =  init(data, all)
nbrs = resultInit[1]
boards = resultInit[0]
breaking = False
for n in nbrs:
    if not breaking:
        for i in range(len(boards)):
            if {n} in boards[i]:
                print("P1", calc(boards[i], n))
                breaking = True
                break
            else:
                temp = []
                for g in boards[i]:
                    temp.append(g.difference({n}))
                boards[i]= temp

resultInit = init(data, all)
nbrs = resultInit[1]
boards = resultInit[0]
result = None
for n in nbrs:
    for i in range(len(boards)):
        if boards[i] != "EMPTY" and {n} in boards[i]:
                result = calc(boards[i], n)
                boards[i] = "EMPTY"
                if i%2:
                    boards[i-1] = "EMPTY"
                else:
                    boards[i+1] = "EMPTY"
        elif boards[i] != "EMPTY" :
            temp = []
            for g in boards[i]:
                temp.append(g.difference({n}))
            boards[i] = temp
print("P2", result)
