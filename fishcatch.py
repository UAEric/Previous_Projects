'''
Author: Eric Abrams
Date: 11/27/2021
Class: ISTA 130
Section Leader: Nick Tang

Description:
This goal of this program is to print a table that contains the number of a fish of a certain type caught, the name
of the types of fish, and the mean weight for a type of fish in grams.
'''

def fish_dict_from_file(a_file):
    '''
    This function will identify the weights of a type of fish, put them in a list, and match the list of weights with the 
    name of the respective type of fish.

    Parameters:
    a_file: a str representing a file name

    Returns:
    weight_dict: a dict representing the weights for each type of fish and is returned after all weights are organized
    '''
    fish_map = {1: "Bream", 2: "Whitefish", 3: "Roach", 4: "?", 5: "Smelt", 6: "Pike", 7: "Perch"}
    weight_dict = {}
    read_file = open(a_file, 'r')
    for line in read_file:
        line = line.strip().split()
        if line[2] == 'NA':
            continue
        species = fish_map[int(line[1])]
        weight = line[2]
        if species not in weight_dict:
            weight_dict[species] = []
        weight_dict[species].append(float(weight))
    read_file.close()
    return weight_dict


def main():
    '''
    This function will print out the number of fish caught of each type, the name of the types of fish, and the mean weight
    for each fish in a table.
    
    Parameters: None
    
    Returns: None
    '''
    fishes = fish_dict_from_file('fishcatch.dat')
    print(('#'.rjust(4)+(' NAME').ljust(12)+('MEAN WT').rjust(10)))
    sorted_fishes = dict(sorted(fishes.items()))
    for key in sorted_fishes:
        mean_weight = round(sum(sorted_fishes[key])/len(sorted_fishes[key]), 1)
        print(str(len(sorted_fishes[key])).rjust(4) + ' ' + key.ljust(10) + str(mean_weight).rjust(10) + 'g')

if __name__ == '__main__':
    main()
