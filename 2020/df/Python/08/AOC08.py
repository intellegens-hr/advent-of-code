# define constatnts
JMP = "jmp"
ACC = "acc"

# define classes
class Ins:
    def __init__(self, l):
        self.inst = l[0]
        self.num = int(l[1])

    def change(self, swap):
        self.inst = swap[self.inst]
        return self


class Result:
    def __init__(self):
        self.ind = 0
        self.acc = 0
        self.previous = set()

    def increase(self, instruction):
        if instruction.inst == JMP:
            self.ind += instruction.num
        else:
            self.ind += 1
        if instruction.inst == ACC:
            self.acc += instruction.num
        else:
            self.acc += 0

    def add(self):
        self.previous.add(self.ind)


# main
f = open("day08.txt", "r").readlines()
result = Result()
l = [Ins(i.strip().split(" ")) for i in f]
def execute_program(result, l, part_one):
    while True:
        if result.ind in result.previous or result.ind >= len(l):
            # PART ONE PRINT ONLY
            if part_one:
                print(result.acc)
			#PART TWO
            if result.ind == len(l):
                return result
            else:
                return None
        else:
            result.add()
            result.increase(l[result.ind])


def get_final_acc(l):
    cmds = {'jmp': 'nop', 'acc': 'acc', 'nop': 'jmp'}
    i = 0
    for instruction in l:
        l[i] = instruction.change(cmds)
        final_result = execute_program(Result(), l, False)

        if final_result is not None:
            return final_result.acc

        l[i] = instruction.change(cmds)
        i += 1

# -- PART ONE RESULT
res = execute_program(result, l,True)

# -- PART TWO RESULT
print(get_final_acc(l))


