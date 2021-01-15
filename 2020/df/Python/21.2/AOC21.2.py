class Solution:
    def __init__(self, ai):
        self.allergens_ingredients = ai
        self.temp = set()
    def check(self):
        while True:
            if len(self.temp) == len(allergens_ingredients):
                break;
            for allergens, ings in allergens_ingredients.items():
                if len(ings) > 1:
                    ings -= self.temp
                else:  # len(ings) == 1
                    self.temp |= ings

#from ruby code -- part 1 ruby
allergens_ingredients = {'dairy': {'pgnpx', 'ksdgk', 'khqsk'}, 'sesame': {'pgnpx', 'dskjpq', 'khqsk', 'nvbrx'}, 'wheat': {'xzb'}, 'soy': {'khqsk', 'nvbrx', 'zbkbgp', 'xzb'}, 'peanuts': {'dskjpq', 'khqsk'}, 'fish': {'ksdgk', 'khqsk', 'srmsh'}, 'eggs': {'dskjpq', 'srmsh'}, 'shellfish': {'khqsk', 'xzb'}}

#not alphabetically by their allergen ... just add one condition ...
result = set()
solution = Solution(allergens_ingredients)
for alg in sorted(solution.allergens_ingredients):
    for a in allergens_ingredients[alg]:
        result.add(a)
print(result)


