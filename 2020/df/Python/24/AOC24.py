my_dict = {
    "e": (1, 0),
    "w": (-1, 0),
    "ne": (1, -1),
    "nw": (0, -1),
    "se": (0, 1),
    "sw": (-1, 1)
}
def rule(x, y=my_dict):
    if x in y:
        return True
    else:
        return False
f = open("day24.txt").read().splitlines()
my_set = set()
def workWith(a, b):
    if rule(a,b):
        b.remove(a)
    else:
        b.add(a)
    return b

for line in f:
    l = [0,0,0]
    while True:
        if l[0] >= len(line):
            break
        else:
            if rule(line[l[0]]):
                l[1]+=my_dict[line[l[0]]][0]
                l[2]+=my_dict[line[l[0]]][1]
            if rule(l[0] < len(line) - 1 and line[l[0]:l[0] + 2]):
                l[1] += my_dict[line[l[0]:l[0] + 2]][0]
                l[2] += my_dict[line[l[0]:l[0] + 2]][1]
                l[0] += 1
            l[0]+=1
    my_set = workWith((l[1],l[2]), my_set)

print(len(my_set))

#p2 ruby
