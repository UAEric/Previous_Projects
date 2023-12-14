'''
Author: Eric Abrams

Description:
The goal of this program is to identify the most commonly used emoticon from a file (files get data from tweets), 
get rid of it, then find the next most commonly used one, get rid of it, and so on.
'''

def load_twitter_dicts_from_file(filename, emoticons_to_ids, ids_to_emoticons):
    '''
    This function will fill two dictionaries: one with emoticons as keys and one with UserIDs as keys. The emoticons as
    keys dict will have the values being a list of UserIDs using the emoticon. The UserIDs as keys dict will have the values
    being a list of emoticons used by that user.
    
    Parameters:
    filename: a str representing the name of a file
    emoticns_to_ids: a dict representing emoticons matched up with a list of UserIDs
    ids_to_emoticons: a dict representing UserIDs matched up with a list of emoticons used
    
    Returns: None
    '''
    read_file = open(filename, 'r')
    for line in read_file:
        line = line.replace('"', '')
        line = line.strip().strip().split()
        emoticon = line[0]
        id = line[2]
        if emoticon not in emoticons_to_ids:
            emoticons_to_ids[emoticon] = []
        emoticons_to_ids[emoticon].append(id)
        if id not in ids_to_emoticons:
            ids_to_emoticons[id] = []
        ids_to_emoticons[id].append(emoticon)
    read_file.close()

def find_most_common(a_dict):
    '''
    This function will find the key with the longest value in a_dict (the values are lists) 
    and print out how many times it occurs
    
    Parameters:
    a_dict: a dict representing emoticons as keys/values and UserIDs as keys/values
    
    Returns:
    longest_key: a str representing the key with the longest value and is returned after it is found
    '''
    occurences = {}
    for key in a_dict:
        occurences[key] = len(a_dict[key])
    for key in occurences:
        if occurences[key] == max(occurences.values()):
            longest_key = key
    print(str(longest_key).ljust(20), 'occurs', str(occurences[longest_key]).rjust(8), 'times')
    return longest_key

def main():
    '''
    This function will print out how many emoticons occur in a file, how many UserIDs occur in a file, and the top 5 most 
    commonly used emoticons and how many times they occur.
    
    Parameters: None
    
    Returns: None
    '''
    emoticons_to_ids = {}
    ids_to_emoticons = {}
    load_twitter_dicts_from_file('twitter_emoticons.dat', emoticons_to_ids, ids_to_emoticons)
    print('Emoticons:', str(len(emoticons_to_ids)))
    print('UserIDs:  ' , str(len(ids_to_emoticons)))
    i = 0
    while i <= 4:
        most_common = find_most_common(emoticons_to_ids)
        emoticons_to_ids.pop(most_common)
        i += 1

if __name__ == '__main__':
    main()
