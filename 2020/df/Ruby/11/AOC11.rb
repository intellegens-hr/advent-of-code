def checkStatus(seat)
    if seat == '.'  || seat == 'L'
        return true
    end
end

def checkToOccupy(seats, i, j, input)
    if checkStatus(seats[i][j])
        return 1
    end
    return 0
end

def changeOrder(input, seats, tmp)
    for i in 0..input.size-1
        for j in 0..input[0].size-1
            if seats[i+1][j+1] == 'L'
                if checkToOccupy(seats, i+1, j+2, input) + checkToOccupy(seats, i+2, j+1, input) + checkToOccupy(seats, i+2, j+2, input) + checkToOccupy(seats, i+2, j, input) + checkToOccupy(seats, i+1, j, input) + checkToOccupy(seats, i, j+1, input) + checkToOccupy(seats, i, j, input) + checkToOccupy(seats, i, j+2, input) == 8
                    tmp[i][j] = '#'
                end
            elsif seats[i+1][j+1] == '#'
                if checkToOccupy(seats, i+1, j+2, input) + checkToOccupy(seats, i+2, j+1, input) + checkToOccupy(seats, i+2, j+2, input) + checkToOccupy(seats, i+2, j, input) + checkToOccupy(seats, i+1, j, input) + checkToOccupy(seats, i, j+1, input) + checkToOccupy(seats, i, j, input) + checkToOccupy(seats, i, j+2, input) < 5
                    tmp[i][j] = 'L'
                end
            end
        end
    end
    return tmp
end

def setArrays(input, tmp, seats)
    for i in 0..input.size+1
        for j in 0..input[0].size+1
            if i == 0
                seats[i][j] = '.'
            elsif j == 0
                seats[i][j] = '.'
            elsif i == 0 && j == 0
                seats[i][j] = '.'
            elsif i == input.size+1
                seats[i][j] = '.'
            elsif j == input[0].size+1
                seats[i][j] = '.'
            elsif i == input.size+1 && j == input[0].size+1
                seats[i][j] = '.'
            end
        end
    end
    for i in 0..input.size-1
        for j in 0..input[0].size-1
            tmp[i].push(input[i][j])
            seats[i+1][j+1] = (input[i][j])
        end
    end
    return seats, tmp
end

def stabilazeChaos(input, seats, tmp)
    oldTmp = Array.new(input.size) { Array.new() }
    for i in 0..input.size-1
        for j in 0..input[0].size-1
            oldTmp[i][j] = tmp[i][j]
        end
    end
    while true
        tmp = changeOrder(input, seats, tmp)
        #puts "*****"
        seats.each { |x|
         #puts x.join(" ")
        }
        #puts "*****"
        if tmp == oldTmp
            return tmp
        else
        for i in 0..input.size-1
            for j in 0..input[0].size-1
                oldTmp[i][j] = tmp[i][j]
            end
        end
        for i in 0..input.size-1
            for j in 0..input[0].size-1
                seats[i+1][j+1] = (oldTmp[i][j])
            end
        end
        end
    end
end

def countOccupied(tmp, input)
    cnt = 0
    for i in 0..input.size-1
        for j in 0..input[0].size-1
            if tmp[i][j] == '#'
                cnt = cnt + 1
            end
        end
    end
    return cnt
end

def changeOrderTwo(input, seats, tmp)
    for i in 0..input.size-1
        for j in 0..input[0].size-1
            if seats[i+1][j+1] == 'L'
                if checkUp(seats, i+1, j+1, input) + checkDown(seats, i+1, j+1, input) + checkRight(seats, i+1, j+1, input) + checkLeft(seats, i+1, j+1, input) + checkUpRight(seats, i+1, j+1, input) + checkUpLeft(seats, i+1, j+1, input) + checkDownLeft(seats, i+1, j+1, input) + checkDownRight(seats, i+1, j+1, input) == 0
                    tmp[i][j] = '#'
                end
            elsif seats[i+1][j+1] == '#'
                if checkUp(seats, i+1, j+1, input) + checkDown(seats, i+1, j+1, input) + checkRight(seats, i+1, j+1, input) + checkLeft(seats, i+1, j+1, input) + checkUpRight(seats, i+1, j+1, input) + checkUpLeft(seats, i+1, j+1, input) + checkDownLeft(seats, i+1, j+1, input) + checkDownRight(seats, i+1, j+1, input) >= 5
                    tmp[i][j] = 'L'
                end
            end
        end
    end
    return tmp
end

def checkUp(seats, i, j, input)
    i = i - 1
    while i >= 1
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        i = i - 1
    end
    return 0
end

def checkDown(seats, i, j, input)
    i = i + 1
    while i <= input.size
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        i = i + 1
    end
    return 0
end

def checkRight(seats, i, j, input)
    j = j + 1
    while j <= input[0].size
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        j = j + 1
    end
    return 0
end

def checkLeft(seats, i, j, input)
    j = j - 1
    while j >= 1
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        j = j - 1
    end
    return 0
end

def checkUpRight(seats, i, j, input)
    i = i - 1
    j = j + 1
    while j <= input[0].size && i >= 1
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        i = i - 1
        j = j + 1
    end
    return 0
end

def checkDownRight(seats, i, j, input)
    i = i + 1
    j = j + 1
    while i <= input.size && j <= input[0].size
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        i = i + 1
        j = j + 1
    end
    return 0
end

def checkDownLeft(seats, i, j, input)
    i = i + 1
    j = j - 1
    while i <= input.size && j >= 1
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        i = i + 1
        j = j - 1
    end
    return 0
end

def checkUpLeft(seats, i, j, input)
    i = i - 1
    j = j - 1
    while i >= 1 && j >= 1
        if seats[i][j] == '#'
            return 1
        elsif seats[i][j] == 'L'
            return 0
        end
        i = i - 1
        j = j - 1
    end
    return 0
end

def equilibrium(input, seats, tmp)
    oldTmp = Array.new(input.size) { Array.new() }
    for i in 0..input.size-1
        for j in 0..input[0].size-1
            oldTmp[i][j] = tmp[i][j]
        end
    end
    while true
        tmp = changeOrderTwo(input, seats, tmp)
        #puts "*****"
        tmp.each { |x|
            #puts x.join(" ")
        }
        #puts "*****"
        if tmp == oldTmp
            return tmp
        else
        for i in 0..input.size-1
            for j in 0..input[0].size-1
                oldTmp[i][j] = tmp[i][j]
            end
        end
        for i in 0..input.size-1
            for j in 0..input[0].size-1
                seats[i+1][j+1] = (oldTmp[i][j])
            end
        end
        end
    end
end
    
        

input = File.read("day11.txt").split("\n")
seats = Array.new(input.size+2) { Array.new() }
tmp = Array.new(input.size) { Array.new() }
seats, tmp = setArrays(input, tmp, seats)
tmp = stabilazeChaos(input, seats, tmp)
occupied = countOccupied(tmp, input)
puts occupied
seats = Array.new(input.size+2) { Array.new() }
tmp = Array.new(input.size) { Array.new() }
seats, tmp = setArrays(input, tmp, seats)
tmp = equilibrium(input, seats, tmp)
occupied = countOccupied(tmp, input)
puts occupied













    
