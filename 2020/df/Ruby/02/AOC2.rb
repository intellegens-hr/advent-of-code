input = File.read("day02.txt").split("\n")
size = input.size-1
cnt = 0
for i in 0..size
    tmp = input[i].split
    signToCheck = tmp[1].delete_suffix(':')
    limits = tmp[0].split('-')
    noOfSign = 0
    tmp[2].split("").each do |j|
        if j == signToCheck
            noOfSign = noOfSign + 1
        end
    end
    if noOfSign >= limits[0].to_i && noOfSign <= limits[1].to_i
        cnt = cnt + 1
    end
end
puts cnt

cnt = 0
for i in 0..size
    tmp = input[i].split
    signToCheck = tmp[1].delete_suffix(':')
    positions = tmp[0].split('-')
    first = positions[0].to_i - 1
    second = positions[1].to_i - 1
    passw = tmp[2]
    if (passw[first] == signToCheck && passw[second] != signToCheck) || (passw[first] != signToCheck && passw[second] == signToCheck)
        cnt = cnt + 1
    end
end
puts cnt
