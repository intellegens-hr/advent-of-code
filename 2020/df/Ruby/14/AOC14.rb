def findSum(input, mask, mem)
    for i in 0..input.size-1
        if input[i][0..3] == "mask"
            mask = input[i].split(" = ")[1]
        elsif input[i][0..2] == "mem"
            pom = "%36b" % input[i].split(" = ")[1]
            for j in 0..35
                if pom[j] == ' '
                    pom[j] = '0'
                end
            end
            for j in 0..35
                if mask[j] == '1'
                    pom[j] = '1'
                elsif mask[j] == '0'
                    pom[j] = '0'
                end
            end
            key = input[i].split(" = ")[0][4..-2]
            mem[key] = pom.to_i(2)
        end
    end
    sum = 0
    mem.each do |k, v|
        if v != 0
           sum = sum + v
        end
    end
    return sum
end

def findFixedSum(input, mask, mem)
    for i in 0..input.size-1
        if input[i][0..3] == "mask"
            mask = input[i].split(" = ")[1]
            numOfX = 0
            for k in 0..mask.size-1
                if mask[k] == 'X'
                    numOfX = numOfX + 1
                end
            end
        elsif input[i][0..2] == "mem"
            key = "%36b" % input[i].split(" = ")[0][4..-2]
            value = input[i].split(" = ")[1][0..-1].to_i
            for j in 0..35
                if key[j] == ' '
                    key[j] = '0'
                end
            end
            for w in 0..35
                if mask[w] == '1'
                    key[w] = '1'
                end
            end
            tmp = []
            for q in 0..(2 ** numOfX)-1
                tmp.append(("%b" % q).to_s)
                if tmp[q].size != numOfX
                    for d in 0..numOfX-tmp[q].size-1
                        tmp[q] = '0' + tmp[q]
                    end
                end
            end
            for r in 0..(2 ** numOfX)-1
                help = key
                p = 0
                for s in 0..35
                    if mask[s] == 'X'
                        help[s] = tmp[r][p]
                        p = p + 1
                    end
                end
                mem[help] = value
            end
        end
    end
    sum = 0
    mem.each do |k, v|
        if v != 0
           sum = sum + v
        end
    end
    return sum
end



input = File.read("day14.txt").split("\n")
mask = ""
mem = {}
sum = findSum(input, mask, mem)
puts sum
mask = ""
mem = {}
fixedSum = findFixedSum(input, mask, mem)
print(fixedSum)
