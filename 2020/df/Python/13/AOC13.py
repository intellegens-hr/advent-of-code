
class Item:
    def __init__(self,t,ind):
        self.time = t
        self.ind = ind
class BusProblem:
    def __init__(self):
        self.input = [i.strip() for i in open("day13.txt","r").readlines()]
        self.offsets = self.input[1].split(",")
        self.first = int(self.input[0])
        self.temp = []
        self.position = 0
    def add_temp(self):
        i = 0
        for j in self.offsets:
            if j != "x":
                self.temp.append(Item(int(j),i))
            i += 1
    def calc_position(self):
        k = self.temp[0].time
        #self.position+=k
        for item in self.temp[1:]:
            while (self.position + item.ind) % item.time != 0:
                self.position += k
            k = k*item.time
        return self.position
    def part_one(self):
        busses = [int(c) for c in self.offsets if c != 'x']
        s = min((n - self.first % n, n) for n in busses)
        return s[0] * s[1]


#MAIN
bus_problem = BusProblem()
bus_problem.add_temp()

#PART ONE
print(bus_problem.part_one())

#PART TWO
print(bus_problem.calc_position())