file = open("14zad.txt")
input_line = file.readline()
#print(input_line)
lines = file.readlines()
rules = {}
for line in lines[1::]:
    splitted = line.strip().split("->")
    if splitted[0].strip() not in rules:
        rules[splitted[0].strip()]=splitted[1].strip()
#print(rules)

def most_frequent(List):
    return max(set(List), key = List.count)
def least_frequent(List):
    return min(set(List), key = List.count)
for k in range(10):
    i = 0
    concated = ""
    while i+1<len(input_line):
        key = input_line[i]+input_line[i+1]
        if key in rules:
            if i==0:
                concated += input_line[i] + rules[key] + input_line[i+1]
            else:
                concated += rules[key] + input_line[i + 1]

        i+=1
        #print(concated)
    input_line = concated
l = list(concated)
print("Part 1:", concated.count(most_frequent(l))-concated.count(least_frequent(l)))