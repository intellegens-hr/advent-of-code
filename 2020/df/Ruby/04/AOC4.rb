def findValids(j, input, points, valid)
    tmp = input[j].split(/[\s:]/)
    k = 0
    for k in 0..tmp.size-1
        case tmp[k]
        when "byr"
            if tmp[k+1].size == 4 && tmp[k+1].to_i >= 1920 && tmp[k+1].to_i <= 2002
                points = points + 1
            end
        when "iyr"
            if tmp[k+1].size == 4 && tmp[k+1].to_i >= 2010 && tmp[k+1].to_i <= 2020
                points = points + 1
            end
        when "eyr"
            if tmp[k+1].size == 4 && tmp[k+1].to_i >= 2020 && tmp[k+1].to_i <= 2030
                points = points + 1
            end
        when "hgt"
            unit = tmp[k+1][-2..-1]
            if unit == "cm" && tmp[k+1][0..-2].to_i >= 150 && tmp[k+1][0..-2].to_i <= 193
                points = points + 1
            end
            if unit == "in" && tmp[k+1][0..-2].to_i >= 59 && tmp[k+1][0..-2].to_i <= 76
                points = points + 1
            end
        when "hcl"
            if tmp[k+1][0] == '#' && tmp[k+1][1..-1] =~ /^[0-9a-f]+$/
                points = points + 1
            end
        when "ecl"
            if tmp[k+1] == "amb" || tmp[k+1] == "blu" || tmp[k+1] == "brn" || tmp[k+1] == "gry" || tmp[k+1] == "grn" || tmp[k+1] == "hzl" || tmp[k+1] == "oth"
                points = points + 1
            end
        when "pid"
            if tmp[k+1] =~ /\A\d{9}\z/
                points = points + 1
            end
        end
    end
    if points == 7
        valid = valid + 1
    end
    j = j + 1
    return j, points, valid
end
    
input = File.read("day04.txt").split("\n")
size = input.size-1
cnt = 0
valid = 0
for i in 0..size
    if input[i] == ''
        cnt = cnt + 1
    end
end

i = 0
j = 0
while i <= cnt
    points = 0
    if i != cnt
        while input[j] != ''
           result = findValids(j, input, points, valid)
           j = result[0]
           points = result[1]
           valid = result[2]
        end
        i = i + 1
        j = j + 1
    else
        while j <= size
            result = findValids(j, input, points, valid)
            j = result[0]
            points = result[1]
            valid = result[2]
        end
        j = j + 1
        i = i + 1
    end
end

puts valid


