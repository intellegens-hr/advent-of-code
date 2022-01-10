num = 9
lines = [int(line.strip()) for line in open('6zad.txt').read().split(",")]
def init_dict(lines):
    mydict = {}
    for i in range(0,num):
        mydict[i]=0
    for line in lines:
        mydict[line]+=1
    return mydict
def count(number, lines):
    mydict = init_dict(lines)
    for day in range(number):
        my_val = mydict[0]
        for i in range(1, num):
            mydict[i - 1] = mydict[i]
        mydict[8] = my_val
        mydict[6] += my_val
    return mydict
def calc_sum(mydict):
    my_sum =0
    for val in mydict.values():
        my_sum+=val
    print(my_sum)
mydict = count(80, lines)
calc_sum(mydict)

mydict = count(256, lines)
calc_sum(mydict)


