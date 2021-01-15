def parseData(rules, input)
    for i in 0..input.size-1
        bag = input[i].split("bags contain")
        for j in 1..bag.size-1
            content = bag[j].split(/[.,]/)
            for k in 0..content.size-1
                content[k] = content[k].reverse!.split(' ', 2)[1].reverse!
                #content[k] = content[k].split(' ', 2)[1]
                content[k] = content[k][1..-1]
            end
            bag[0] = bag[0][0..-2]
            rules[bag[0]] = [content[0]]
            for k in 1..content.size-1
                rules[bag[0]] += [content[k]]
            end
        end
    end
    return rules
end

def findBags(rules)
    num = 0
    br = 0
    rules.each do |k, v|
        if recursiveBags(k, rules, br) > 0
            num = num + recursiveBags(k, rules, br)
        end
    end
    return num
end

def recursiveBags(k, rules, br)
    for i in 0..rules[k].size-1
        if rules[k][i][2..-1] == "shiny gold"
            br = 1
            elsif rules[k][i][2..-1] == " other"
            next
        else
        br = 0 + recursiveBags(rules[k][i][2..-1], rules, br)
        end
    end
    return br
end

def findSumOfBags(rules, sumBags, bag)
    sumBags = 0
    for i in 0..rules[bag].size-1
        if rules[bag][i][2..-1] == " other"
            return 0
        else
        sumBags = sumBags + rules[bag][i][0..1].to_i + rules[bag][i][0..1].to_i * findSumOfBags(rules, sumBags, rules[bag][i][2..-1])
        end
    end
    return sumBags
end

input = File.read("input7.txt").split("\n")
rules = {}
rules = parseData(rules, input)
result = findBags(rules)
puts result
sumBags = 0
bag = "shiny gold"
sumOfBags = findSumOfBags(rules, sumBags, bag)
puts sumOfBags
    
