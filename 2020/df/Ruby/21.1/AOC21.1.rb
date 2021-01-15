input = File.read("day21.txt")
possible = {}
ing = []
alerg = []

for line in input.split("\n")
    left = line.split(" (contains ")[0]
    right = line.split(" (contains ")[1][0..-2]
    ing = left.split(" ")
    alerg = right.split(", ")
    
    for a in alerg
        if !possible.key? a
            possible[a] = ing.clone
        else
            possible[a] &= ing
        end
    end
end
