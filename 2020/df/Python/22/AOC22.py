import collections
class Player:
    def __init__(self, name, arr):
        self.name = name
        self.arr = collections.deque(arr)

play_1 = Player("First player", (48,23,9,34,37,36,40,26,49,7,12,20,6,45,14,42,18,31,39,47,44,15,43,10,35))
play_2 = Player("Second player", (13,19,21,32,27,16,11,29,41,46,33,1,30,22,38,5,17,4,50,2,3,28,8,25,24))
list_of_players = []

list_of_players.append(play_1)
list_of_players.append(play_2)

def game(list_of_players):
    while list_of_players[0].arr and list_of_players[1].arr:
        card_fp = list_of_players[0].arr.popleft()
        card_sp = list_of_players[1].arr.popleft()
        if card_fp > card_sp:
            list_of_players[0].arr+=[card_fp,card_sp]
        else:
            list_of_players[1].arr+= [card_sp,card_fp]
    return list_of_players

def calc_score(i):
    sum=0
    while i.arr:
        sum+=len(i.arr)*i.arr.popleft()
    return sum
def calc_score_p2(i):
    sum=0
    while i:
        sum+=len(i)*i.popleft()
    return sum

result_list = game(list_of_players)
sum = 0
for i in result_list:
    sum+=calc_score(i)
print(sum)

#P2
class Combat:
    def __init__(self, list_of_players):
        self.winner = None
        self.prev_games = set()
        self.players = list_of_players

play_1 = Player("First player", (48,23,9,34,37,36,40,26,49,7,12,20,6,45,14,42,18,31,39,47,44,15,43,10,35))
play_2 = Player("Second player", (13,19,21,32,27,16,11,29,41,46,33,1,30,22,38,5,17,4,50,2,3,28,8,25,24))
list_of_players = []

list_of_players.append(play_1)
list_of_players.append(play_2)
combat = Combat(list_of_players)


def playCombat(player1, player2, combat):
    prev_games = set()
    while player1 and player2:
        if str(player1) + str(player2) in prev_games:
            return combat.players[0].name
        prev_games.add(str(player1) + str(player2))
        card_fp = player1.popleft()
        card_sp = player2.popleft()
        if not (card_fp <= len(player1) and card_sp <= len(player2)):
            if card_fp > card_sp:
                player1 += [card_fp, card_sp]
            else:
                player2 += [card_sp, card_fp]
        else:
            new_player_1 = collections.deque(list(player1)[:card_fp])
            new_player_2 = collections.deque(list(player2)[:card_sp])
            winner = playCombat(new_player_1,new_player_2, combat)
            if winner == combat.players[0].name:
                player1 += [card_fp, card_sp]
            else:
                player2 += [card_sp, card_fp]
    return combat.players[0].name if player1 else combat.players[1].name

p1 = collections.deque((48,23,9,34,37,36,40,26,49,7,12,20,6,45,14,42,18,31,39,47,44,15,43,10,35))
p2 = collections.deque((13,19,21,32,27,16,11,29,41,46,33,1,30,22,38,5,17,4,50,2,3,28,8,25,24))
winner = playCombat(p1,p2, combat)
score = 0
if winner == combat.players[0].name:
    print(calc_score_p2(p1))
else:
    print(calc_score_p2(p2))








