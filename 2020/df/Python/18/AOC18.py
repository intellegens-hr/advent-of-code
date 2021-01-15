class Calculator():
    def __init__(self):
        self.possible_operators = ["+","*"]
        self.not_priority = "*"
        self.start = "";
    def calc_p2(self, exp):
        if len(exp) == 0:
            return 0
        pom = []
        sign = self.not_priority
        n_p = 0
        while len(exp) > 0:
            s = exp.pop(0)
            if s.isdigit():
                n_p = n_p * 10 + int(s)
            if s == '(':
                n_p = self.calc_p2(exp)
            if len(exp) == 0 or (s in self.possible_operators or s == ')'):
                if sign == self.possible_operators[1]:
                    pom.append(n_p)
                elif sign == self.possible_operators[0]:
                    pom[-1] = pom[-1] + n_p
                sign = s
                n_p = 0
                if sign == ')':
                    break
        prod = 1
        for p in pom:
            prod*=p
        return prod
    def calc_p1(self, exp):
        if len(exp) == 0:
            return 0
        pom = []
        sign = self.start
        n_p = 0
        while len(exp) > 0:
            s = exp.pop(0)
            if s.isdigit():
                n_p = n_p * 10 + int(s)
            if s == '(':
                n_p = self.calc_p1(exp)
            if len(exp) == 0 or (s in self.possible_operators or s == ')'):
                if sign == "":
                    pom.append(n_p)
                if sign == self.possible_operators[1]:
                    pom[-1] = pom[-1] * n_p
                elif sign == self.possible_operators[0]:
                    pom[-1] = pom[-1] + n_p
                sign = s
                n_p = 0
                if sign == ')':
                    break
        return pom[0]
a = Calculator()
f = open("day18.txt").read().splitlines()
sum_p1 = 0
sum_p2 = 0
for i in f:
    k = i.replace(")", " )").replace("(", "( ").split()
    sum_p1+=a.calc_p1(k)
for i in f:
    k = i.replace(")", " )").replace("(", "( ").split()
    sum_p2+=a.calc_p2(k)
print(sum_p1,sum_p2)



