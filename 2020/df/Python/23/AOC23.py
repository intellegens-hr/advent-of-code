input = [1,2,3,4,8,7,5,9,6]
class Cups:
    def __init__(self, input):
        self.values = input
cups = Cups(input)
def initialize_dict():
    my_dict = {}
    for i in range(0, len(input)):
        if i != len(input) - 1:
            my_dict[input[i]] = input[i + 1]
        else:
            my_dict[input[-1]] = input[0]
    return my_dict
def manipulate(my_dict):
    picked = []
    index = 1
    for i in range(3):
        picked.append(cups.values[index])
        index = cups.values[index]
    return my_dict, picked
def set_dest(dest, my_dict):
    if dest<min(my_dict.keys()):
        return max(my_dict.keys())
    else:
        return dest
def change_dict(temp, my_dict, current, destination):
    my_dict[current] = my_dict[temp[2]]
    my_dict[temp[2]] = my_dict[destination]
    my_dict[destination] = temp[0]
    return my_dict
def take_three(my_dict, current):
    temp = []
    for i in range(3):
        temp.append(my_dict[current])
        current = my_dict[current]
    return temp
def parse_dict(cups):
    my_dict = {}
    count, j = 0, 1
    while j < len(cups.values):
        my_dict[cups.values[count]] = cups.values[j]
        j+=1
        count += 1

    my_dict[cups.values[len(cups.values) - 1]] = max(cups.values) + 1
    s_count = max(cups.values)
    while s_count< 1000000:
        my_dict[s_count] = s_count+1
        s_count+=1
    my_dict[1000000] = cups.values[0]
    return my_dict
def play(cups, n):
    current = cups.values[0]
    if n==100:
        my_dict = initialize_dict()
    else:
        my_dict = parse_dict(cups)
    for i in range(n):
        print(i)
        temp = take_three(my_dict, current)
        destination = current -1
        destination = set_dest(destination, my_dict)
        while True:
            if destination not in temp:
                break
            else:
                destination -= 1
                destination = set_dest(destination, my_dict)
        my_dict = change_dict(temp, my_dict, current, destination)
        current = my_dict[current]
    return  my_dict
#p1
def p1(my_dict):
    l = []
    i = 0
    c = 1
    while True:
        if i == len(my_dict)-1:
            break
        else:
            l.append(str(my_dict[c]))
            c = my_dict[c]
            i+=1
    return l

my_dict = play(cups,100)
print("".join(p1(my_dict)))
cups = Cups(input)
my_dict = play(cups,10000000)
print(my_dict)
print(my_dict[1]*my_dict[my_dict[1]])



