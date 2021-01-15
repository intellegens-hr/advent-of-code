
class Times:
    def __init__(self, input):
        self.count = {}
        self.ind = [1] + [0] * (max(input) + 2)
        self.maximum = max(input)
    def add(self,key):
        if key in self.count:
            self.count[key]+=1
        else:
            self.count[key] = 1
    def part_one(self):
        return self.count[1] * (self.count[3]+1)
    def count_index(self,i):
        return self.ind[i - 1] + self.ind[i - 2] + self.ind[i - 3]
    def part_two(self, input):
        for i in input:
            self.ind[i] = self.count_index(i)
            if i == self.maximum:
                return self.ind[i]

f = open("aoc10.txt","r")
input = [int(i.strip()) for i in f.readlines()]
copy_input = input
input.sort()

#PART ONE
times = Times(copy_input)
for i,j in zip([0] + input, input):
    times.add(j-i)
print(times.part_one())

#PART TWO
print(times.part_two(copy_input))
