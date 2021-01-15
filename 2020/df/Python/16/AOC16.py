f = open("day16.txt").read().split("\n\n")
nearby_tickets = [i for i in f[2].split("\n")[1::]]
rules = {i.split(":")[0]: (i.split(":")[1].split("or")[0].strip(), i.split(":")[1].split("or")[1].strip()) for
         i in f[0].split("\n")}
invalid = []
def check(number,rule,rules):
    if (int(number)>=int(rules[rule][0].split("-")[0]) and int(number)<=int(rules[rule][0].split("-")[1])) or (int(number)>=int(rules[rule][1].split("-")[0]) and int(number)<=int(rules[rule][1].split("-")[1])):
        return True
    else:
        return False
def all(final_rules):
    for rule in final_rules:
        if len(final_rules[rule])!=1:
            return False
    return True

#PART ONE
new_tickets = []
for ticket in nearby_tickets:
    state = False
    for number in ticket.split(","):
        inv = 0
        for rule in rules:
            if not check(number,rule,rules):
                inv+=1
        if inv == len(rules):
            invalid.append(number)
            state = True
    if state == False:
        new_tickets.append(ticket)

sum = 0
for i in invalid:
    sum+=int(i)
print(sum)

#PART TWO
temp_rules = {i:[-2] for i in rules}
final_rules = {i:[-1] for i in rules}
k = len(new_tickets[0].split(","))

for i in range(0,k):
    temp_rules = {i: [-2] for i in rules}
    for ticket in new_tickets:
        for temp_rule in temp_rules:
            if not check(ticket.split(",")[i],temp_rule,rules):
                temp_rules[temp_rule].append(-1)
    for rule in temp_rules:
        if len(temp_rules[rule]) == 1:
            final_rules[rule].append(i)

taken = set()
for i in final_rules:
    final_rules[i].remove(-1)

while not all(final_rules):
    for i in final_rules:
        if len(final_rules[i]) == 1:
            taken.add(final_rules[i][0])
        else:
            for k in final_rules[i]:
                if k in taken:
                    final_rules[i].remove(k)

prod = 1
my_ticket = f[1].split("\n")[1].split(",")
for rule in final_rules:
    if rule.startswith("departure"):
        prod*=int(my_ticket[final_rules[rule][0]])
print(prod)




