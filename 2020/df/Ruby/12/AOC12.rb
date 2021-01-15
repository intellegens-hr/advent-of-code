def startSailing(orientation, positions, input)
    for i in 0..input.size-1
        letter = input[i][0]
        number = input[i][1..-1].to_i
        if letter == 'N'
            positions[0] = positions[0] + number
        elsif letter == 'E'
            positions[1] = positions[1] + number
        elsif letter == 'S'
            positions[2] = positions[2] + number
        elsif letter == 'W'
            positions[3] = positions[3] + number
        elsif letter == 'L'
            number = number / 90
            orientation = orientation - number
            if orientation > 3 || orientation < 0
                orientation = orientation % 4
            end
        elsif letter == 'R'
            number = number / 90
            orientation = orientation + number
            if orientation > 3 || orientation < 0
                orientation = orientation % 4
            end
        elsif letter == 'F'
            if orientation == 0
                positions[0] = positions[0] + number
            elsif orientation == 1
                positions[1] = positions[1] + number
            elsif orientation == 2
                positions[2] = positions[2] + number
            elsif orientation == 3
                positions[3] = positions[3] + number
            end
        end
    end
    n_s = (positions[0] - positions[2]).abs
    e_w = (positions[1] - positions[3]).abs
    return n_s + e_w
end

def actualInstructions(input, ship, waypoint)
    for i in 0..input.size-1
        letter = input[i][0]
        number = input[i][1..-1].to_i
        if letter == 'N'
            waypoint[0] = waypoint[0] + number
        elsif letter == 'E'
            waypoint[1] = waypoint[1] + number
        elsif letter == 'S'
            waypoint[2] = waypoint[2] + number
        elsif letter == 'W'
            waypoint[3] = waypoint[3] + number
        elsif letter == 'L'
            waypoint = checkNumber(waypoint, number, true)
        elsif letter == 'R'
            waypoint = checkNumber(waypoint, number, false)
        elsif letter == 'F'
        for j in 0..3
            ship[j] = ship[j] + number * waypoint[j]
        end
        end
    end
    n_s = (ship[0] - ship[2]).abs
    e_w = (ship[1] - ship[3]).abs
    return n_s + e_w
end

def checkNumber(waypoint, number, check)
    number = number / 90
    if number > 3 || number < 0
        number = number % 4
    end
    tmp = waypoint.clone
    for k in 0..3
        if check == true
            amount = k + number
            if amount > 3
                amount = amount % 4
            end
            waypoint[k] = tmp[amount]
        elsif check == false
            waypoint[k] = tmp[k-number]
        end
    end
    return waypoint
end

input = File.read("day12.txt").split("\n")
orientation = 1
positions = Array.new(4){ 0 }
result = startSailing(orientation, positions, input)
puts result
ship = Array.new(4){ 0 }
waypoint = [1, 10, 0, 0]
finalResult = actualInstructions(input, ship, waypoint)
puts finalResult

