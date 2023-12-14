'''
Author: Eric Abrams
Date: 11/18/2021
Class: ISTA 130
Section Leader: Nick Tang

Description:
This program will ask a user for a mammal name, check if the name is in a predetermined list, and give the brain and body
weight of that mammal. If the mammal is not in the list, the user will be prompted if they want to enter the mammal into the list.
'''

def find_insert_position(a_mammal, list_strings):
    '''
    This function will find the position of a_mammal in list_strings
    
    Parameters:
    a_mammal: a str representing a mammal's name
    list_strings: a list that contains strings in alphabetical order
    
    Returns: 
    i: an int returned after the str of list_strings[i] and a_mammal are compared
    len(list_strings): an int that represents the position of a_mammal in list_strings
    '''
    for i in range(len(list_strings)):
        if a_mammal <= list_strings[i]:
            return i
    return len(list_strings)


def populate_lists(list_mammals, list_body, list_brain):
    '''
    This function will populate (fill) each list with a list of mammal names, body weights, and 
    brain weights respectively.
    
    Parameters:
    list_mammals: a list representing mammal names
    list_body: a list representing the mammals' body weights
    list_brain: a list representing the mammals' brain weights

    Returns: None
    '''
    read_file = open('BrainBodyWeightKilos.csv')
    the_file = read_file.readlines()
    for line in the_file:
        stats = line.split(',')
        list_mammals.append(stats[0].title())
        list_body.append(float(stats[1]))
        list_brain.append(float(stats[2]))
    read_file.close()

def write_converted_csv(a_file, list_mammals, list_body, list_brain):
    '''
    This function will create a new csv file that converts the weights from kilograms and grams to
    pounds.
    
    Parameters:
    a_file: a str representing the name of a file
    list_mammals: a list representing the names of mammals
    list_body: a list representing the mammals' body weights
    list_brain: a list representing the mammals' brain weights

    Returns: None
    '''
    write_file = open(a_file, 'w')
    for i in range(len(list_mammals)):
        body_weight = round(list_body[i] * 2.205, 2)
        brain_weight = round(list_brain[i]*0.0022, 2)
        write_file.write(list_mammals[i] + ',')
        write_file.write(str(body_weight)+ ',')
        write_file.write(str(brain_weight) + '\n')
    write_file.close()

def main():
    '''
    This function will execute the other 3 functions and ask a user to input a mammal name. 
    If the mammal is not in the list, the user will be asked if they want to add the mammal to the list.
    
    Parameters: None
    
    Returns: None
    '''
    mammal_names = []
    body_weights = []
    brain_weights = []
    populate_lists(mammal_names, body_weights, brain_weights)
    while True:
        print()
        name_input = input('Enter animal name (or "q" to quit): ').title()
        if name_input == 'Q':
            break
        if name_input in mammal_names:
            i = mammal_names.index(name_input)
            print(name_input + ': body = ' + str(body_weights[i]) + ', brain = ' + str(brain_weights[i]))
            delete_input = input('Delete "' + name_input + '"? (y|n) ')
            if delete_input == 'y':
                mammal_names.remove(name_input)
                body_weights.pop(i)
                brain_weights.pop(i)
        else:
            print('File does not contain "' + name_input + '".')
            add_input = input('Add "' + name_input + '" to file? (y|n) ')
            if add_input == 'y':
                body = input('Enter body weight for "' + name_input + '" in kilograms: ')
                brain = input('Enter brain weight for "' + name_input + '" in grams: ')
                the_position = find_insert_position(name_input, mammal_names)
                mammal_names.insert(the_position, name_input)
                body_weights.insert(the_position, float(body))
                brain_weights.insert(the_position, float(brain))
        write_converted_csv("BrainBodyWeightPounds.csv", mammal_names, body_weights, brain_weights)


if __name__ == '__main__':
    main()
