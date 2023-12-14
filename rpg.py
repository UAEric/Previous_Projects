'''
Description:
The goal of this program create a class that will simulate an RPG game.
'''
import random

class Fighter:
    def __init__(self, name):
        '''
        This function will establish that the name of the fighter is the name that is passed to the function
        and that the hit points of the fighter starts at 10

        Parameters:
        self: a reference to an instance of the Fighter class
        name: a str representing the name of a fighter

        Returns: None
        '''
        self.name = name
        self.hit_points = 10

    def __repr__(self):
        '''
        This function will return a string that states a fighter's current HP

        Parameters:
        self: a reference to an instance of the Fighter class

        Returns: 
        result: a str returned that represents a fighter's current health
        '''
        result = self.name + ' (HP: ' + str(self.hit_points) + ')'
        return result

    def take_damage(self, damage_amount):
        '''
        This function will decrease a fighter's health by the hit points they have taken. If the HP is less than or equal to 0, the
        fighter has fallen.

        Parameters:
        self: a reference to an instance of the Fighter class
        damage_amount: an int representing the number of hit points of damage that has been inflicted on a fighter

        Returns: None
        '''
        self.hit_points -= damage_amount
        if self.hit_points <= 0:
            print('\tAlas, ' + self.name + ' has fallen!')
        else:
            print('\t' + self.name + ' has ' + str(self.hit_points) + ' hit points remaining.')

    def attack(self, other):
        '''
        This function is a representation of the one of the fighter's attacking the other. If a roll greater than or equal to 12 is 
        rolled, the fighter's attack has landed and the damage is determined by the_damage. 

        Parameters:
        self: a reference to an instance of the Fighter class (attacking)
        other: a reference to another instance of the Fighter class (being attacked)

        Returns: None
        '''
        print(str(self.name) + ' attacks ' + str(other.name) + '!')
        a_number = random.randrange(1,21)
        if a_number >= 12:
            the_damage = random.randrange(1, 7)
            print('\tHits for ' + str(the_damage) + ' hit points!')
            other.take_damage(the_damage)
        else:
            print('\tMisses!')

    def is_alive(self):
        '''
        This function will check to see if a fighter is alive.

        Parameters:
        self: a reference to an instance of the Fighter class

        Returns: 
        True: a bool that is returned if a fighter has HP greater than 0
        False: a bool that is returned if a fighter has HP less than or equal to 0
        '''
        if self.hit_points > 0:
            return True
        else:
            return False

def combat_round(fighter_one, fighter_two):
    '''
    This function will determine which fighter attacks first each round

    Parameters:
    fighter_one: an instance of the Fighter class
    fighter_two: another instance of the Fighter class

    Returns: None
    '''
    a_number = random.randrange(1,7)
    b_number = random.randrange(1,7)
    if a_number == b_number:
        print('Simultaneous!')
        fighter_one.attack(fighter_two)
        fighter_two.attack(fighter_one)
    elif a_number > b_number:
        fighter_one.attack(fighter_two)
        if fighter_two.is_alive():
            fighter_two.attack(fighter_one)
    else:
        fighter_two.attack(fighter_one)
        if fighter_one.is_alive():
            fighter_one.attack(fighter_two)

def main():
    '''
    This function will execute the roleplaying scenario, first printing out the round number, then each player's health, and then
    executing combat_round. 
    
    Parameters: None
    
    Returns: None
    '''
    a_fighter = Fighter('Death_Mongrel')
    b_fighter = Fighter('Hurt_then_Pain')
    i = 1
    while True:
        print()
        print('='*19 + ' ROUND ' + str(i) + ' ' + '='*19)
        print(a_fighter)
        print(b_fighter)
        input('Enter to Fight!')
        combat_round(a_fighter, b_fighter)
        i += 1
        if a_fighter.is_alive() == False or b_fighter.is_alive() == False:
            print()
            print('The battle is over!')
            print(a_fighter)
            print(b_fighter)
            return False

if __name__ == '__main__':
    main()
