#PART 1
input = [1721,979,366,299,675,1456]
for i in input:
    for j in input:
        if i+j == 2020:
            print(i*j)
            break
    break

#PART 2
found = False
for i in input:
    for j in input:
        for k in input:
            if i+j+k == 2020 and not found:
                print(i*j*k)
                found = True
                break

