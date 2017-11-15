#function grid = detect_photo_grid(grid, photo_filename, vThreshold, vMass)
#NOTE VER 0.0 = Using round function
#NOTE VER 1.0 = Using Decimal function
import numpy as np
import matplotlib.image as mpimg
import matplotlib.pyplot as plt
from scipy import misc as img 
from numpy import linalg as LA
from decimal import Decimal, ROUND_HALF_UP

#function [grid,grid4] = lut_photo_processing(hand, data4_path, data_path, dsp_out, thrIntensity, thrMass, thrDistance)
def lut_photo_processing(hand, data4_path, data_path, dsp_out, thrIntensity, thrMass, thrDistance):


   if hand == 'left':

     photo4 = data4_path + 'Raw data\\left_high_nir.png'
     photo  = data_path + 'Raw data\\left_high_nir.png'
     i = 10

   elif hand == 'right':

     photo4 = data4_path + 'Raw data\\right_high_nir.png'
     photo  = data_path + 'Raw data\\right_high_nir.png'
     i = 20

   else:
    #fprintf(LOG, '%s\n', ' - error in lut_photo_processing: incorrect input');
     return
  
   grid4 = compose_mask_grid4(hand);
   grid4 = detect_photo_grid4(grid4, hand, photo4, thrIntensity, thrDistance)   #if dsp_out display_grid(grid4, photo4, i+1); end;

   grid  = compose_mask_grid(hand) 
   grid  = predict_photo_grid(grid, grid4)                                      #if dsp_out display_grid(grid, photo, i+2); end;
   grid  = detect_photo_grid(grid, photo, thrIntensity, thrMass)                #if dsp_out display_grid(grid, photo, i+3); end;
   grid  = extrapolate_photo_grid(grid)                                         #if dsp_out display_grid(grid, photo, i+4); end;
   grid  = detect_photo_grid(grid, photo, thrIntensity, thrMass)                #if dsp_out display_grid(grid, photo, i+5); end;
   grid  = extrapolate_photo_grid(grid)                                         #if dsp_out display_grid(grid, photo, i+6); end;
   grid  = detect_photo_grid(grid, photo, thrIntensity, thrMass)                #if dsp_out display_grid(grid, photo, i+7); end;
   grid  = extrapolate_photo_grid(grid)                                         #if dsp_out display_grid(grid, photo, i+8); end;
   
   if hand == 'left':
      grid  = finish_photo_gridL(grid)                                          #if dsp_out display_grid(grid, photo, i+6); end;
   else:
      grid  = finish_photo_gridR(grid)                                          #if dsp_out display_grid(grid, photo, i+6); end;
   

   return grid,grid4
  
  
###########################################################
#PRIVATE FUNCTION
###########################################################

##### compose_mask_grid4
def compose_mask_grid4(hand):
   import numpy as np
   if hand == 'left':
     left_hand = True
     right_hand = False
   elif hand == 'right':
     left_hand = False
     right_hand = True
   else:
     #fprintf(LOG, '%s\n', ' - error in compose_mask_grid4: incorrect input');
     return
    
    
   class grid_init:
    mask_point  = [0,0] # motor mask coordinates
    photo_point = [0,0] # photo pixel coordinates
    status      = 'initial' # 'initial', 'predicted', 'detected', 'rejected'
    
   # grid parameters
   mask_size = [120 , 60]
   grid_size = [2 , 2]

   cell_size = [45 , 30]
   offset    = [20 , 15]                             # mask_point closest to origin (left)


   if right_hand:
      offset[0] = 54;
   
   
   # make grid
   gray_map = np.array([[i for i in range (0,225+1)],[j for j in range (0,225+1)],[k for k in range (0,225+1)]])
   gray_map = np.array(list(zip(*gray_map)))/255
   matrix = np.zeros((100,120),dtype=np.uint8)
   power_index = 1;                                  # laser power index
   
   grid = [[grid_init() for i in range (0,2)]]
   for j in range (0,1):
     grid.append([grid_init() for i in range (0,2)])
   
   for colind in range (0,grid_size[0]):              # mask grid column index
      for rowind in range (0,grid_size[1]):           # mask grid row index

         col = offset[0] + cell_size[0]*(colind)
         row = offset[1] + cell_size[1]*(rowind)

         #grid[colind][rowind]            = grid_init() ##CEK BAGIAN INI
         grid[colind][rowind].mask_point = [col,row]

         matrix[row+1][col+1] = power_index      # grid of single points
      
   

   mask_grid4 = grid;
   return mask_grid4
   
   
######detect_photo_grid4
def detect_photo_grid4(grid4, hand, photo_filename, vThreshold, vDistance):

   # predicting grid4 points in photo
   if hand == 'left':
      
      grid4[0][0].photo_point   = [240,120]
      grid4[1][0].photo_point   = [400,180]
      grid4[0][1].photo_point   = [200,270]
      grid4[1][1].photo_point   = [320,330]
      
      grid4[0][0].status        = 'predicted'
      grid4[1][0].status        = 'predicted'
      grid4[0][1].status        = 'predicted'
      grid4[1][1].status        = 'predicted'

   elif hand == 'right':

      grid4[0][0].photo_point   = [140,170]
      grid4[1][0].photo_point   = [300,110]
      grid4[0][1].photo_point   = [210,320]
      grid4[1][1].photo_point   = [330,260]


      grid4[0][0].status        = 'predicted'
      grid4[1][0].status        = 'predicted'
      grid4[0][1].status        = 'predicted'
      grid4[1][1].status        = 'predicted'

   else:
      #fprintf(LOG, '%s\n', ' - error in detect_photo_grid4: incorrect input');
      return
   
   
   grid_size = np.array(grid4).shape

   photo = img.imread(photo_filename)  # [psu] = pixel separation unit

   threshold         = vThreshold  # [-]
   radius1           = 75          # [psu]
   radius2           = 15          # [psu]
   distance          = vDistance   # [psu]
   moment_of_inertia = 24          # [psu^2]

   for colind in range (0,grid_size[0]):      # mask grid4 column index
      for rowind in range (0,grid_size[1]):   # mask grid4 row index

         point = grid4[colind][rowind].photo_point
         disc  = disc_selection4([512,640], point, radius1)
         com,moi,photo_disc = centre_of_mass4(photo, threshold, disc)
         disc  = disc_selection4([512,640], com, radius2)
         com,moi,photo_disc = centre_of_mass4(photo, threshold, disc)
         if (LA.norm(com-point)<distance and moi<moment_of_inertia):

            grid4[colind][rowind].photo_point = com
            grid4[colind][rowind].status = 'detected'    # expect to happen for all
         else:

            grid4[colind][rowind].status = 'rejected'    # problem if this happens
            #fprintf(LOG, '#s\n', ' - error in detect_photo_grid4: photo_point rejected');
         
      
   
   
   return grid4

#-------------------------------------------------------------------------------------------------#
#     Private functions
#-------------------------------------------------------------------------------------------------#

def disc_selection4(photo_size, point, radius):
   x = photo_size[0]   # 512 or 256
   y = photo_size[1]   # 640 or 320
   disc = np.zeros((y,x),dtype=np.bool)
   
   #create y and x matrix dimension
   y_dim = np.arange(1,y+1)
   y_dim = np.array(list(zip(y_dim))) * np.ones((y,x))
   
   x_dim = np.arange(1,x+1)
   x_dim = np.array(x_dim) * np.ones((y,x)) 
   
   col_ind = np.power((x_dim-point[0]),2)
   row_ind = np.power((y_dim-point[1]),2)
   
   disc[np.where((col_ind + row_ind) < np.power(radius,2))] = True
   
   return disc

def centre_of_mass4(photo, threshold, disc):
   photo_disc = photo[disc]
   
   intensity = threshold * np.amax(photo_disc) #use absolute threshold
   
   photo_disc[np.where(photo_disc<intensity)] = 0
   matrix_size = np.array(photo).shape
   photo       = np.zeros(matrix_size)
   photo[disc] = photo_disc

   x = matrix_size[1]   # 512 or 256
   y = matrix_size[0]   # 640 or 320

   #create y and x matrix dimension
   y_dim = np.arange(1,y+1)
   y_dim = np.array(list(zip(y_dim))) * np.ones((y,x))
   
   x_dim = np.arange(1,x+1)
   x_dim = np.array(x_dim) * np.ones((y,x)) 

   mass   = photo_disc.sum()
   com = []
   com.append((np.sum(photo*x_dim))/mass)
   com.append((np.sum(photo*y_dim))/mass)
   com    = np.around(com)

   r_quad = np.power((x_dim-com[0]),2) + np.power((y_dim-com[1]),2)
   moi    = np.sum(photo*r_quad)/ mass
   photo_disc = np.array(photo_disc,dtype='uint16')
   return com, moi, photo_disc
   
#COMPOSE MASK GRID
#function mask_grid = compose_mask_grid(varargin)
def compose_mask_grid(hand):
   #global LOG;
   #fprintf(LOG, '#s\n', 'compose_mask_grid');
   import numpy as np

   if hand == 'left':
    left_hand = True
    right_hand = False
   elif hand == 'right':
    left_hand = False
    right_hand = True
   else:
    #fprintf(LOG, '%s\n', ' - error in compose_mask_grid4: incorrect input');
    return
    
    
   class grid_init:
    mask_point  = [0,0] # motor mask coordinates
    photo_point = [0,0] # photo pixel coordinates
    status      = 'initial' # 'initial', 'predicted', 'detected', 'rejected'

   # grid parameters
   mask_size = [120, 60]
   cell_size = [10, 10]                             # must be divisor of mask_size
   grid_size = np.array(np.divide(mask_size,cell_size),dtype='int')
#   in motor mask this is (5,5) for Left and (4,5) for Right

# #  settings on 10-04-2016: these settings are quite ok
#    offset    = [6 ; 5]; # LUT+1
#    if right_hand
#       offset = [4 ; 6]; #LUT+1
#    end

   #   settings for testing 31-05-2016: these should be ok
   offset    = [5, 5]
   if right_hand:
      offset = [4, 5]
   
   
   # make grid
   gray_map = np.array([[i for i in range (0,225+1)],[j for j in range (0,225+1)],[k for k in range (0,225+1)]])
   gray_map = np.array(list(zip(*gray_map)))/255
   matrix = np.zeros((100,120),dtype=np.uint8)
   power_index = 1;                                  # laser power index

   grid = [[grid_init() for i in range (0,grid_size[1])]]
   for j in range (0,grid_size[0]-1):
     grid.append([grid_init() for i in range (0,grid_size[1])])
    

   for colind in range (0,grid_size[0]):                        # mask grid column index
      for rowind in range (0,grid_size[1]):                     # mask grid row index

         col = offset[0] + cell_size[0]*(colind);
         row = offset[1] + cell_size[1]*(rowind);

         #grid[colind][rowind]            = grid_init() #CEK BAGIAN INI
         grid[colind][rowind].mask_point = [col,row];

         matrix[row][col] = power_index;      # grid of single points
   
   mask_grid = grid;

   return mask_grid
   
## PREDICT PHOTO
def predict_photo_grid(grid, grid4):

   #global LOG;
   #fprintf(LOG, '#s\n', 'predict_photo_grid');

   # predicting grid points in photo, using grid4
   grid_size = np.array(grid).shape
   
   #a = []
   for colind in range (0,grid_size[0]):      # mask grid column index
      for rowind in range (0,grid_size[1]):   # mask grid row index

         x = grid[colind][rowind].mask_point
         y = np.around(mask_photo_map(grid4, x))
         #a.append(y)
         if (((y[0]>=1 and y[0]<=512) and (y[1]>=1 and y[1]<=640)) and (0<colind and colind<grid_size[0]-1)):
            grid[colind][rowind].photo_point = y
            grid[colind][rowind].status = 'predicted'
            
   #np.savetxt('y2_dim.csv',np.array(a,dtype='int'),delimiter=",")
   return grid

#-------------------------------------------------------------------------------------------------#
#     Private functions originating from \global mapping\mask_photo_map_i.m
#-------------------------------------------------------------------------------------------------#

# Composition of affine map and interpolation map
# References  \documents\mask photo transformation.jpg
#function y = mask_photo_map(grid4, x)
def mask_photo_map(grid4, x):
   y = interpolation_map(grid4, affine_map(grid4, x))
   return y

# Affine map, inverse of g
# Reference:  \documents\affine mapping.jpg
#function y = affine_map(grid4, x)
def affine_map(grid4, x):

   p = np.array(grid4[0][0].mask_point)
   q = np.array(grid4[1][0].mask_point)
   r = np.array(grid4[0][1].mask_point)
   a = np.linalg.inv([q-p,r-p])
   b = np.array(x-p)
   y = np.dot(a,b)
   return y

# Interpolation map f
# Reference:  \documents\interpolation.jpg
#function y = interpolation_map(grid4, x)
def interpolation_map(grid4, x):
   a = grid4[0][0].photo_point
   b = grid4[1][0].photo_point
   c = grid4[1][1].photo_point
   d = grid4[0][1].photo_point

   y = (1-x[1])*((1-x[0])*a+x[0]*b) + x[1]*((1-x[0])*d+x[0]*c);
   return y
   
### Detect PHOTO GRID
def detect_photo_grid(grid, photo_filename, vThreshold, vMass):
   #global LOG;
   #fprintf(LOG, '#s\n', 'detect_photo_grid');

   grid_size = np.array(grid).shape
   photo = img.imread(photo_filename)  # [psu] = pixel separation unit
            
   threshold         = vThreshold  # [-]
   radius1           = 20          # [psu]
   radius2           = 8           # [psu]
   distance          = 12          # [psu]
   moment_of_inertia = 30          # [psu^2]
   minMass           = vMass


   for colind in range (0,grid_size[0]):      # mask grid column index
      for rowind in range (0,grid_size[1]):   # mask grid row index

         if  ((grid[colind][rowind].status == 'predicted') or 
              (grid[colind][rowind].status == 'extrapolated')):

            point = grid[colind][rowind].photo_point
            disc  = disc_selection([512,640], point, radius1)
            com, moi, photo_disc, mass = centre_of_mass(photo, threshold, disc)
            
            #np.savetxt('disc.csv',np.array(disc,dtype='int'),delimiter=",")
            #exit()
            disc  = disc_selection([512,640], com, radius2)
            com, moi, photo_disc, mass = centre_of_mass(photo, threshold, disc)

            if ((LA.norm(com-point)<distance and moi<moment_of_inertia) and mass>minMass):

               grid[colind][rowind].photo_point = com
               grid[colind][rowind].status = 'detected'
            else:
               grid[colind][rowind].status = 'rejected'
            
   return grid

#-------------------------------------------------------------------------------------------------#
#     Private functions
#-------------------------------------------------------------------------------------------------#

# Photo pixel coordinates [x ; y] <=> photo pixel matrix (y, x)
# with x = 1..256 and y = 1..320. (or 512x640)
#function disc = disc_selection(photo_size, point, radius)
def disc_selection(photo_size, point, radius):
   x = photo_size[0]   # 512 or 256
   y = photo_size[1]   # 640 or 320
   disc = np.zeros((y,x),dtype=np.bool)
   
   #create y and x matrix dimension
   y_dim = np.arange(1,y+1)
   y_dim = np.array(list(zip(y_dim))) * np.ones((y,x))
   
   x_dim = np.arange(1,x+1)
   x_dim = np.array(x_dim) * np.ones((y,x)) 
   
   col_ind = np.power((x_dim-point[0]),2)
   row_ind = np.power((y_dim-point[1]),2)
   
   # for i in range (0,y+1):
    # for j in range (0,x+1):
      # if ((np.power((x_dim[i][j]-point[0]),2) + np.power((y_dim[i][j]-point[0]),2)) < np.power(radius,2)):
        # disc[i][j] = True
   disc[np.where((col_ind + row_ind) < np.power(radius,2))] = True
   return disc

# com = centre of mass
# moi = moment of inertia divided by mass
# photo_disc output for displaying
#function [com moi photo_disc mass] = centre_of_mass(photo, threshold, disc)
def centre_of_mass(photo, threshold, disc):
   #np.savetxt('photo.csv',np.array(photo,dtype='int'),delimiter=",")
   photo_disc = photo[disc]
   intensity = threshold * np.amax(photo_disc) #use absolute threshold
   #np.savetxt('photo_disc.csv',np.array(disc,dtype='int'),delimiter=",")
            
   photo_disc[np.where(photo_disc<intensity)] = 0
   
   matrix_size = np.array(photo).shape
   
   photo       = np.zeros(matrix_size)
   photo[disc] = photo_disc
   
   x = matrix_size[1]   # 512 or 256
   y = matrix_size[0]   # 640 or 320
  
   #create y and x matrix dimension
   y_dim = np.arange(1,y+1)
   y_dim = np.array(list(zip(y_dim))) * np.ones((y,x))
   
   x_dim = np.arange(1,x+1)
   x_dim = np.array(x_dim) * np.ones((y,x))

   mass   = photo_disc.sum()
   com = []
   com.append((np.sum(photo*x_dim))/mass) #shift 1 due to one-based indexing in Matlab
   com.append((np.sum(photo*y_dim))/mass) #shift 1 due to one-based indexing in Matlab
   #com    = np.vectorize(np.around(np.array(com)))
   # com = np.floor(com)
   # com = np.rint(com)
   for x in range (0,len(com)):
     com[x] = Decimal(com[x])
     com[x] = Decimal(com[x].quantize(Decimal('1'), rounding=ROUND_HALF_UP))
   com = np.array(com,dtype='int')
   r_quad = np.power((x_dim-com[0]),2) + np.power((y_dim-com[1]),2)
   moi    = np.sum(photo*r_quad) / mass
   photo_disc = np.array(photo_disc,dtype='uint16')
   return com, moi, photo_disc, mass
   
###--------------------------------------------
# EXTRAPOLATE PHOTO
def extrapolate_photo_grid(grid):
#global LOG;
   #fprintf(LOG, '#s\n', 'extrapolate_photo_grid');

   grid1 = grid

   # show result
   np.errstate(divide='ignore')
   grid_size = np.array(grid).shape
   for colind in range (0,grid_size[0]):      # mask grid column index
      for rowind in range (0,grid_size[1]):   # mask grid row index

         if   ((grid[colind][rowind].status == 'initial') or (grid[colind][rowind].status == 'rejected')):
           
            local_grid = make_local_grid(grid, colind, rowind)
            
            well_conditioned, point = extrapolate_point(local_grid)
       
            if (well_conditioned and ((point[0]>=1 and point[0]<=512) and (point[1]>=1 and point[1]<=640))):

               grid1[colind][rowind].photo_point = point
               grid1[colind][rowind].status = 'extrapolated'
            
   grid = grid1
   return grid


#-------------------------------------------------------------------------------------------------#
#     Private functions
#-------------------------------------------------------------------------------------------------#

# make local grid
# function local_grid = make_local_grid(grid, colind, rowind)
def make_local_grid(grid, colind, rowind):
   grid_size = np.array(grid).shape
   
   
   class local_init:
    point  = [0,0] # motor mask coordinates
    validity = False
    
   
   local_grid = [[local_init() for i in range (0,5)]]
   for j in range (0,4):
     local_grid.append([local_init() for i in range (0,5)])
   
   for k in range (-2,2+1):
      for n in range (-2,2+1):

         if ((0<=colind+k and colind+k<=grid_size[0]-1) and
            (0<=rowind+n and rowind+n<=grid_size[1]-1)):

            local_grid[k+3-1][n+3-1].point    = grid[colind+k][rowind+n].photo_point
            local_grid[k+3-1][n+3-1].validity = not((grid[colind+k][rowind+n].status == 'initial') or (grid[colind+k][rowind+n].status == 'rejected'))

         else:
            local_grid[k+3-1][n+3-1].point    = [0 , 0]  # photo coordinates of grid point
            local_grid[k+3-1][n+3-1].validity = 0        # 0 or 1 whether or not coordinates valid
         
      
   
   return local_grid

# extrapolate point in local_grid
#function [well_conditioned point] = extrapolate_point(local_grid)
def extrapolate_point(local_grid):
   weight = calculate_weight(local_grid)

   m00 = calculate_m(weight, 0, 0)
   m10 = calculate_m(weight, 1, 0)
   m01 = calculate_m(weight, 0, 1)
   m11 = calculate_m(weight, 1, 1)
   m20 = calculate_m(weight, 2, 0)
   m02 = calculate_m(weight, 0, 2)

   q00 = calculate_q(local_grid, weight, 0, 0)
   q10 = calculate_q(local_grid, weight, 1, 0)
   q01 = calculate_q(local_grid, weight, 0, 1)

   well_conditioned, point = calculate_pab(m00, m10, m01, m11, m20, m02, q00, q10, q01)
   return well_conditioned, point

#-------------------------------------------------------------------------------------------------#
#     Private functions from \patchwork\extrapolation_local.m
#-------------------------------------------------------------------------------------------------#

# equation (3) of [1]
#function weight = calculate_weight(local_grid)
def calculate_weight(local_grid):
   weight = np.zeros((5,5),dtype=np.double)
   for k in range (-2,2+1):
      for n in range (-2,2+1):
         if (k!=0 and n != 0):
            weight[k+3-1][n+3-1] = (1/np.power((k*k+n*n),2)) * local_grid[k+3-1][n+3-1].validity
            
               
            
            # print(local_grid[k+3-1][n+3-1].validity)
            # print(weight[k+2][n+2])
         else:
            weight[3-1][3-1] = 0
   
   
   return weight

# first of equation (7) of [1]
#function m_ij = calculate_m(weight, i, j)
def calculate_m(weight, i, j):

   m_ij = 0
   for k in range (-2,2+1):
      for n in range (-2,2+1):
         m_ij = m_ij + np.power(k,i) * np.power(n,j) * weight[k+3-1][n+3-1]
      
   
   return m_ij

# second of equation (7) of [1]
#function q_ij = calculate_q(local_grid, weight, i, j)
def calculate_q(local_grid, weight, i, j):

   q_ij = 0
   for k in range (-2,2+1):
      for n in range (-2,2+1):
         q_ij = q_ij + np.power(k,i) * np.power(n,j) * np.dot(weight[k+3-1][n+3-1],local_grid[k+3-1][n+3-1].point)
      
   
   return q_ij

# equation (10) of [1]
# function [well_conditioned point] = calculate_pab(m00, m10, m01, m11, m20, m02, q00, q10, q01)
def calculate_pab(m00, m10, m01, m11, m20, m02, q00, q10, q01):
    #global LOG;

   M = [[m00, m10, m01],[m10, m20, m11],[m01, m11, m02 ]]
   # M = np.around(M,decimals=4)
   for x in range (0,len(M)):
      for y in range (0,len(M[x])):
        M[x][y] = Decimal(M[x][y])
        M[x][y] = Decimal(M[x][y].quantize(Decimal('.0001'), rounding=ROUND_HALF_UP))
   M = np.array(M,dtype='double')
  
   #fprintf(LOG, '#s #6.2f\n', 'condition number:', cond(M));

   well_conditioned = LA.cond(M) < 1e3   # not well_conditioned probably
                                         # due to insufficient set of
                                         # valid coordinates in local_grid
   if well_conditioned:
      invM = LA.inv(M)
      invM = np.around(invM,decimals=4)
      
     
      p = invM[0][0]*q00 + invM[0][1]*q10 + invM[0][2]*q01
      a = invM[1][0]*q00 + invM[1][1]*q10 + invM[1][2]*q01 # for the engineer
      b = invM[2][0]*q00 + invM[2][1]*q10 + invM[2][2]*q01 # for the engineer
   else:
      p = [0,0]
   
   
   point = np.around(p)    # should I limit point here?
   # point = np.rint(p)
   return well_conditioned,point
   
###----------------------------------------------------
# FINISH PHOTO GRID LA
def finish_photo_gridL(grid):
  
  # additional rough extrapolation of initial or rejected points: required for
   # finishing lookuptable
   
   grid1 = grid
   grid_size = np.array(grid).shape
   for rowind in range (grid_size[1]-1,1-2,-1):# split direction for finishing halfway the columns
       for colind in range (6,grid_size[0]):      # mask grid column index
         if   ((grid[colind][rowind].status == 'initial') or
               (grid[colind][rowind].status == 'rejected')):

            dirx = 1
            diry = -1
            base = [colind-dirx,rowind-diry]
            
            
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0] , grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0] , grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            if (colind == 11):
            
               print(rowind)
               print(base)
               print(v1)
               print(v2)
            v = v1+v2
            
            point = np.array([grid1[base[0]][base[1]].photo_point[0]+v[0] , grid1[base[0]][base[1]].photo_point[1]+v[1]])
            grid1[colind][rowind].photo_point = point
            grid1[colind][rowind].status = 'extrapolated'

         
       
       # split direction for finishing halfway the columns
       for colind in range (5,1-2,-1):      # mask grid column index
         if ((grid[colind][rowind].status == 'initial') or
             (grid[colind][rowind].status == 'rejected')):

            dirx = -1
            diry = -1
            base = [colind-dirx-1,rowind-diry-1]
            
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0], grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0], grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            v = v1+v2
            
            point = np.array([grid1[base[0]][base[1]].photo_point[0]+v[0] , grid1[base[0]][base[1]].photo_point[1]+v[1]])
            grid1[colind-1][rowind-1].photo_point = point
            grid1[colind-1][rowind-1].status = 'extrapolated'
   
   grid = grid1
   return grid
   
###------------------------------------------------------------
# FINISH PHOTO GRID R

def finish_photo_gridR(grid):  
   # additional rough extrapolation of initial or rejected points: required for
   # finishing lookuptable
   
   grid1 = grid
   grid_size = np.array(grid).shape
   
   for rowind in range (grid_size[1],1-2,-1):
       # split direction for finishing halfway the columns
       for colind in range (5,1-2,-1):     # mask grid column index
         if   ((grid[colind][rowind].status == 'initial') or
               (grid[colind][rowind].status == 'rejected')):
            
            dirx = -1
            diry = -1
            base = [colind-dirx , rowind-diry]
            
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            v = v1+v2
            
            point = np.array([grid1[base[0]][base[1]].photo_point[0]+v[0] , grid1[base[0]][base[1]].photo_point[1]+v[1]])
            grid1[colind][rowind].photo_point = point;
            grid1[colind][rowind].status = 'extrapolated';
         
       
       # split direction for finishing halfway the columns
       for colind in range (6,grid_size[0]):   # mask grid column index
         if   ((grid[colind][rowind].status, 'initial') or
              (grid[colind][rowind].status, 'rejected')):
            
            dirx = 1
            diry = -1
            base = [colind-dirx-1,rowind-diry-1]
            
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            v = v1+v2
            
            point = np.array([grid1[base[0]][base[1]].photo_point[0]+v[0] , grid1[base[0]][base[1]].photo_point[1]+v[1]])
            grid1[colind][rowind].photo_point = point
            grid1[colind][rowind].status = 'extrapolated'
         
      
   
   
   grid = grid1
   return grid
