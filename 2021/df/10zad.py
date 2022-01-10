lines = [line.strip() for line in open('10zad.txt').readlines()]
def add_corrupted(line):
    global s
    breaking = False
    stack = []
    for l in line:
        if breaking == False:
            if l in left:
                stack.append(l)
            else:
                if right[l] != stack[-1]:
                     s+=score[l]
                     breaking = True
                else:
                    stack.pop()

    if breaking == False:
        temp = 0
        while stack:
            last = stack.pop()
            temp = 5 * temp + vals[last]
        if temp > 0:
            scores.append(temp)

vals = {"(": 1, "[": 2, "{":3, "<":4}
left = ["(", "[", "{", "<"]
right = {")": "(", "]": "[",  "}": "{", ">": "<"}
score = {")":  3, "]": 57, "}":  1197, ">":  25137}
s = 0
scores = []
for line in lines:
        add_corrupted(line)
print("P1:", s)
scores.sort()
print('P2:', scores[len(scores) // 2])





