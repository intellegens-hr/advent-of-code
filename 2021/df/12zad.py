

lines = [line.strip() for line in open('12zad.txt').readlines()]
g = {}
for line in lines:
    splitted = line.split("-")
    if splitted[0] in g:
        g[splitted[0]].append(splitted[1])
    else:
        g[splitted[0]] = [splitted[1]]
    if splitted[1] in g:
        g[splitted[1]].append(splitted[0])
    else:
        g[splitted[1]] = [splitted[0]]

end_point = "end"
start_point = "start"

def dfs(paths, again, p2=False):
    global g
    global count
    count = 0
    if paths[-1] == end_point:
        return 1
    for sym in g[paths[-1]]:
        if p2 == True:
            if sym.isupper() or sym not in paths:
                new_visited = paths.copy()
                new_visited.append(sym)
                count += dfs(new_visited, again, True)
            elif not sym.isupper() and sym in paths:
                if not again and sym != start_point and sym != end_point:
                    new_visited = paths.copy()
                    new_visited.append(sym)
                    count += dfs(new_visited,True, True)
        else:
            if sym.isupper() or sym not in paths:
                new_visited = paths.copy()
                new_visited.append(sym)
                count += dfs(new_visited, again)

    return count
count = 0
starting = [start_point]
dfs(starting, False,False)
print(count)

count = 0
starting = [start_point]
dfs(starting, False,True)
print(count)


