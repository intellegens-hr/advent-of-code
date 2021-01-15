def findError(input, flag)
    for i in 25..input.size-1
        for j in i-25..i
            for k in i-25..i
                if input[j] != input[k] && input[j].to_i + input[k].to_i == input[i].to_i
                    flag = true
                    break
                end
            end
            if flag == true
                break
            end
        end
        if flag == false
            return input[i], i
        end
        flag = false
    end
end

def findWeakness(input, ind)
    for i in 0..ind
        sum = input[i].to_i
        for j in i+1..ind
            if sum + input[j].to_i != input[ind].to_i
                sum = sum + input[j].to_i
            else
                return i, j
            end
        end
    end
end

def calculateWeakness(input, i , j)
    min = input[i].to_i
    max = input[i]
    for k in i..j
        if input[k].to_i < min
            min = input[k]
        end
    end
    for k in i..j
        if input[k].to_i > max.to_i
            max = input[k]
        end
    end
    return min, max
end
    

input = File.read("day9.txt").split("\n")
flag = false
result = findError(input, flag)[0]
index = findError(input, flag)[1]
i = findWeakness(input, index)[0]
j = findWeakness(input, index)[1]
weakness = calculateWeakness(input, i , j)[0].to_i + calculateWeakness(input, i , j)[1].to_i
puts result
puts weakness


