f = open("6aoc.txt","r").read()
input = f.strip().split("\n\n")

#PART 1
pom_input = []
for i in range(0,len(input)):
    pom_input.append(input[i].replace("\n",""))
num_of_corrects = 0
for i in pom_input:
    num_of_corrects+=len(set(i))
print(num_of_corrects)

#PART 2
real_counter = 0
for i in input:
    my_dict = {}
    for j in i.split("\n"):
        for k in set(j):
            if k not in my_dict:
                my_dict[k]=1
            else:
                my_dict[k]+=1
    for key in my_dict:
        if my_dict[key]==len(i.split("\n")):
            real_counter+=1
print(real_counter)
