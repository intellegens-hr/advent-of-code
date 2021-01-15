def parse(s, rules)
    tmp = [[]]
    help = []
    n = s.split(": ")[0]
    e = s.split(": ")[1]
    if e.include? '"'
        rules[n.to_i] = e
        return rules
    else
        for t in e.split("|")
            for r in t.split
                help.append(r.to_i)
            end
            if !rules.include? n.to_i
                rules[n.to_i] = Array.new { Array.new }
            end
            rules[n.to_i].append(help.dup)
            help.clear
        end
        return rules
    end
end

def p1(s, a, rules)
    if a == '' or a == []
        if s == '' and a == []
            return 1
        else
            return 0
        end
    end
    r = rules[a[0]]
    if r.include? '"'
        if r.include? s[0]
            return p1(s[1..-1], a[1..-1], rules)
        else
            return 0
        end
    else
        for t in r
            if p1(s, t + a[1..-1], rules) == 1
                return 1
            end
        end
        return 0
    end
end

input = File.read("day19.txt").split("\n\n")
r_t = input[0].split("\n")
msg = input[1].split("\n")
rules = {}
for s in r_t
    rules = parse(s, rules)
end

fin = 0
for i in msg
    fin = fin + p1(i, [0], rules)
end

print(fin)




