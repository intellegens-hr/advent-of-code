def parse_rows(input):
    row_pom = ""
    for i in input[:7]:
        row_pom += str(int(i=="B"))
    return int(row_pom,2)
def parse_cols(input):
    col_pom = ""
    for i in input[7:]:
        col_pom += str(int(i=="R"))
    return int(col_pom,2)

input = [item.strip() for item in open("peti.txt","r").readlines()]
dictionary = {}
for i in input:
    row_pom = ""
    col_pom = ""
    rows = parse_rows(i)
    cols = parse_cols(i)
    dictionary[i] = rows*8+cols
minimum = min(dictionary.values())
maximum = max(dictionary.values())
print(maximum)
for i in range(minimum,maximum):
    if i in dictionary.values():
        pass
    else:
        print(i)



