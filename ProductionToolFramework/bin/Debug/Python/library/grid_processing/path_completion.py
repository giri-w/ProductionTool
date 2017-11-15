 """
 function absolute_path = path_completion(input_path)

 Makes path of directory complete, i.e.
 - path is absolute
 - path ends it with '\'
"""

def path_completion(input_path)
    
   full_path = os.getcwd() 
   if input_path[0] == '.':
      absolute_path = full_path + input_path[1:]
   else:
      absolute_path = input_path
   
   if absolute_path[-1] != '\':
      absolute_path = absolute_path + '\'

return absolute_path
