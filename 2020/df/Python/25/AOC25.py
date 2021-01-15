def loop_size(pk=7734663):
    value, number = 1, 1
    while value != pk:
        number += 1
        value *= 7
        value %= 20201227
    return number - 1


def encryption_key(cls, dpk=1614360):
    value, number = 1, 1
    while number <= cls:
        value *= dpk
        value %= 20201227
        number += 1
    return value

print(encryption_key(loop_size()))
