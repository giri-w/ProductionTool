# function [] = lut_table_compilation(grid_left, grid_right, output_path)
#
# output_path is absolute or relative path of directory where xml-file
# will be written to.
#
#function [] = lut_table_compilation(grid_left, grid_right, output_path)
def lut_table_compilation(grid_left, grid_right, output_path):
  #import sys
  #sys.path.append("C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\-. Python Working Dir\\library\\")
  
  #import write_xml  
  #import calculate_table 
  #global LOG;
  #fprintf(LOG, '#s\n', 'lut_table_compilation');

  #fprintf(LOG, '#s #s\n\n', 'writing', [output_path 'Xxxxx-MotorMatrix-Yyyy.xml']);
  
  table = calculate_table(grid_left, 'high')
  write_xml(table, output_path + 'PyLeft-MotorMatrix-High.xml')

  table = calculate_table(grid_left, 'low')
  write_xml(table, output_path + 'PyLeft-MotorMatrix-Low.xml')

  table = calculate_table(grid_right, 'high')
  write_xml(table, output_path + 'PyRight-MotorMatrix-High.xml')

  table = calculate_table(grid_right, 'low')
  write_xml(table, output_path + 'PyRight-MotorMatrix-Low.xml')
  return

def write_xml(table,name_of_file):
  from lxml import etree
  import os.path
  motorMatrix = etree.Element('MotorMatrix')
  coordinates = etree.SubElement(motorMatrix,'Coordinates')
  for i in range (0,len(table)):
    child2 = etree.SubElement(coordinates, 'Coordinate')
    child2.set('Column',str(table[i].Column))
    child2.set('Row',str(table[i].Row))
    child2.set('X',str(table[i].X))
    child2.set('Y',str(table[i].Y))

  #Save location
  save_path = 'D:/'
  #name_of_file = ("MotorMatrix")
  completeName = os.path.join(save_path,name_of_file)

  tree = etree.ElementTree(motorMatrix)
  tree.write(completeName, pretty_print=True)
  return
  
## structures
class table_struct:
    Column = []
    Row    = []
    X      = []
    Y      = []

## functions calculate_table
def calculate_table(grid, high_or_low):
   import numpy as np
   if high_or_low == 'high':
     option = 1
   elif high_or_low == 'low':
     option = 2
   else:
     print(' - error in calculate_table: incorrect input');
     option = 3
   
   
   table = [table_struct() for i in range (0,12000)]
      
   for col in range (0,120):
      for row in range (60,100):
         line = 100*col+row
         table[line].Column = col
         table[line].Row    = row
         table[line].X      = 0
         table[line].Y      = 0
 
 
   if not(grid[6-1][3-1].status == 'initial'): 
      for col in range (0,120):
         for row in range (0,80):
            line = 100*col+row
            table[line].Column = col
            table[line].Row    = row

            index    = find_cell(grid, [col,row], False)
            vertices = get_vertices(grid, index)
     
           
            if option == 1:
               XY = np.array(np.round(mask_photo_map(vertices, [col,row])),dtype = 'int')
            elif option == 2:
               XY = np.array(np.round(0.5*mask_photo_map(vertices, [col,row]) + 0.25), dtype = 'int')
            else:
               XY = [0 , 0];
          
            table[line].X = XY[0]
            table[line].Y = XY[1]
   return table

#Private functions

# Find cell that is valid and closest to given mask point. It returns indices of the cell.
def find_cell(grid, point, clear_persistent):
  import numpy as np
  if clear_persistent:
      del col_dim_valid, row_dim_valid, centre_col_valid, centre_row_valid
      index = 0
      return index
  
  col_dim_valid = [i for i in range (1,12)]*5
  centre_col_valid = [i*10 for i in range (1,12)]*5
  centre_row_valid = [j*10 for j in range (1,6) for i in range (1,12)]
  col_dim_valid = [i for i in range (1,12)]*5
  row_dim_valid = [j for j in range (1,6) for i in range (1,12)]
  
  if not col_dim_valid:
    grid_size = np.array(grid).shape
    offset    = np.array(grid[0][0].mask_point)
    cell_size = np.array(grid[1][1].mask_point) - offset

    #create y and x matrix dimension
    col_dim = np.arange(1,y+1)
    col_dim = np.array(list(zip(y_dim))) * np.ones((y,x))
     
    row_dim = np.arrange(1,x+1)
    row_dim = np.array(x_dim) * np.ones((y,x)) 
    
  
    # determine centre of cells
    cell.centre_col = offset[0] + cell_size[0]*(col_dim-1) + cell_size[0]/2
    cell.centre_row = offset[1] + cell_size[1]*(row_dim-1) + cell_size[1]/2

    # determine validity of cells, i.e. whether its vertices are valid
    for colind in range (1,grid_size[0]-1):
       for rowind in range (1,grid_size[1]-1):

          cell.validity[colind][rowind] = ((valid(grid[colind][rowind].status) and valid(grid[colind+1][rowind].status)) and
            (valid(grid[colind][rowind+1].status) and  valid(grid[colind+1][rowind+1].status)))
       
    

    # using logic indexing
    col_dim_valid    = col_dim(cell.validity)
    row_dim_valid    = row_dim(cell.validity)
    centre_col_valid = cell.centre_col(cell.validity)
    centre_row_valid = cell.centre_row(cell.validity)
   
  
  centre_col_valid_temp = np.array (centre_col_valid)
  centre_row_valid_temp = np.array (centre_row_valid)
  res_temp = (np.power((centre_col_valid_temp - point[0]),2) + np.power((centre_row_valid_temp - point[1]),2));
  y = np.amin(res_temp)
  i = np.argmin(res_temp)
  
  index = np.array([0,0])
  index[0] = col_dim_valid[i]
  index[1] = row_dim_valid[i]
  return index

# auxiliary function
# function out = valid(status)
def valid(status):

  out = ((status == 'detected') or (status == 'extrapolated'))
  return out

class vertices_struct:
    mask     = []
    photo    = []
   
  
# get vertices of indicated cell
def get_vertices(grid, index):

  vertices = [[vertices_struct() for i in range (0,2)]]
  for j in range (0,1):
     vertices.append([vertices_struct() for i in range (0,2)])
  
  
    
    
  for i in range (0,2):
    for j in range (0,2):
      vertices[i][j].mask  = grid[index[0]-1+i][index[1]-1+j].mask_point
      vertices[i][j].photo = grid[index[0]-1+i][index[1]-1+j].photo_point
      
  

  return vertices


#-------------------------------------------------------------------------------------------------%
#     Private functions originating from \global mapping\mask_photo_map_i.m
#-------------------------------------------------------------------------------------------------%

# Composition of affine map and interpolation map
# References  \documents\mask photo transformation.jpg
# function y = mask_photo_map(vertices, x)
def mask_photo_map(vertices, x):

  y = interpolation_map(vertices, affine_map(vertices, x));

  return y

# Affine map, inverse of g
# Reference:  \documents\affine mapping.jpg
# function y = affine_map(vertices, x)
def affine_map(vertices, x):
   from numpy import linalg as LA
   import numpy as np
   p = np.array(vertices[0][0].mask)
   q = np.array(vertices[1][0].mask)
   r = np.array(vertices[0][1].mask)
   a = LA.inv([q-p,r-p])
   b = np.array(x-p)
   y = np.dot(a,b)

   return y

# Interpolation map f
# Reference:  \documents\interpolation.jpg
# function y = interpolation_map(vertices, x)
def interpolation_map(vertices, x):
   import numpy as np
   
   a = np.array(vertices[0][0].photo)
   b = np.array(vertices[1][0].photo)
   c = np.array(vertices[1][1].photo)
   d = np.array(vertices[0][1].photo)

   y = (1-x[1])*((1-x[0])*a + x[0]*b) + x[1]*((1-x[0])*d+x[0]*c)

   return y
