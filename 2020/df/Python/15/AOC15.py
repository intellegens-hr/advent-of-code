class Solution:
    def __init__(self):
        self.numbers = [11,18,0,20,1,7,16]
        self.dictionary = {}
        self.sol = 0
    def solve(self,NUMBER):
        last = self.numbers.pop()
        self.fill_dictionary()
        self.numbers.append(last)
        for i in range(len(self.numbers), NUMBER):
            self.numbers.append(i - self.dictionary.get(self.numbers[-1], i))
            self.dictionary[self.numbers[-2]] = i
        self.sol = self.numbers[-1]
        self.reset()
        return self.sol
    def reset(self):
        self.numbers = [11, 18, 0, 20, 1, 7, 16]
        self.dictionary = {}
    def fill_dictionary(self):
        i = 0
        for num in self.numbers:
            self.dictionary[num] = i + 1
            i += 1
#MAIN
solution = Solution()
#PART ONE
print(solution.solve(2020))
#PART TWO
print(solution.solve(30000000))
