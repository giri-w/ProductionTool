#NOTE VER 0.0 = Using round function
#NOTE VER 1.0 = Using Decimal function
import numpy as np
import matplotlib.image as mpimg
import matplotlib.pyplot as plt
import copy
import time
from scipy import misc as img 
from numpy import linalg as LA
from decimal import Decimal, ROUND_HALF_UP, ROUND_HALF_DOWN


def lut_photo_processing(hand, data4_path, data_path, dsp_out, thrIntensity, thrMass, thrDistance):
   # print picture
   picture = False
   lastSave= time.time()
   
   if hand == 'left':

     photo4 = data4_path + '\\Raw data\\left_high_nir.png'
     photo  = data_path + '\\Raw data\\left_high_nir.png'
     i = 10

   elif hand == 'right':

     photo4 = data4_path + '\\Raw data\\right_high_nir.png'
     photo  = data_path + '\\Raw data\\right_high_nir.png'
     i = 20

   else:
    print(' - error in lut_photo_processing: incorrect input')
    return
  
   print("- compose mask grid4")
   grid4 = compose_mask_grid4(hand)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- detect photo grid4")
   grid4 = detect_photo_grid4(grid4, hand, photo4, thrIntensity, thrDistance)   
   if dsp_out and picture:
      display_grid(grid4, photo4, i+1)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- compose mask grid")
   grid  = compose_mask_grid(hand) 
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   
   print("- predict photo grid")
   grid  = predict_photo_grid(grid, grid4)                                      
   if dsp_out and picture: 
      display_grid(grid, photo, i+2)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- detect photo grid[1]")
   grid  = detect_photo_grid(grid, photo, thrIntensity, thrMass)                
   if dsp_out and picture:
      display_grid(grid, photo, i+3)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- extrapolate photo grid[1]")
   grid  = extrapolate_photo_grid(grid)                                         
   if dsp_out and picture:
      display_grid(grid, photo, i+4)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- detect photo grid[2]")
   grid  = detect_photo_grid(grid, photo, thrIntensity, thrMass)                
   if dsp_out and picture:
      display_grid(grid, photo, i+5)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- extrapolate photo grid[2]")
   grid  = extrapolate_photo_grid(grid)                                         
   if dsp_out and picture :
      display_grid(grid, photo, i+6)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- detect photo grid[3]")
   grid  = detect_photo_grid(grid, photo, thrIntensity, thrMass)                
   if dsp_out and picture:
      display_grid(grid, photo, i+7)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   print("- extrapolate photo grid[3]")
   grid  = extrapolate_photo_grid(grid)                                         
   if dsp_out and picture :
      display_grid(grid, photo, i+8)
   print("-time: "+str(time.time()-lastSave))
   lastSave = time.time()
   
   if hand == 'left':
      print("- finish photo grid left")
      grid  = finish_photo_gridL(grid)                                          
      if dsp_out and picture:
         display_grid(grid, photo, i+9)
      print("-time: "+str(time.time()-lastSave))
      lastSave = time.time()
   
   else:
      print("- finish photo grid right")
      grid  = finish_photo_gridR(grid)                                          
      if dsp_out and picture :
         display_grid(grid, photo, i+9)
      print("-time: "+str(time.time()-lastSave))
      lastSave = time.time()
   
   return grid,grid4
  
  
###########################################################
#PRIVATE FUNCTION
###########################################################

#KETERANGAN
#[1]  compose_mask_grid4
#[2]  detect_photo_grid4
#[3]  disc_selection4
#[4]  centre_of_mass4
#[5]  compose_mask_grid
#[6]  predict_photo_grid | 346
#[7]  mask_photo_map     | 360
#[8]  affine_map         | 366
#[9]  interpolation_map  | 378
#[10] detect_photo_grid  | 393
#[11] extrapolate_photo_grid  | 447
#[12] make_local_grid    | 482
#[13] extrapolate_point  | 508
#[14] calculate_weight   | 529
#[15] calculate_m        | 549
#[16] calculate_q        | 561
#[17] calculate_pab      | 570
#[18] finish_photo_gridL | 601
#[19] finish_photo_gridR | 657
#[20] display_grid       | 717






#[1] compose_mask_grid4
def compose_mask_grid4(hand):
   import numpy as np
   if hand == 'left':
     left_hand = True
     right_hand = False
   elif hand == 'right':
     left_hand = False
     right_hand = True
   else:
     print(' - error in compose_mask_grid4: incorrect input')
     return
   
   class grid_init:
    mask_point  = [0,0]       # motor mask coordinates
    photo_point = [0,0]       # photo pixel coordinates
    status      = 'initial'   # 'initial', 'predicted', 'detected', 'rejected'
    
   # grid parameters
   mask_size = [120 , 60]
   grid_size = [2 , 2]
   cell_size = [45 , 30]
   
   offset    = [20 , 15]      # mask_point closest to origin (left)
   if right_hand:
      offset[0] = 54;
   
   
   # make grid
   gray_map = np.array([[i for i in range (0,225+1)],[j for j in range (0,225+1)],[k for k in range (0,225+1)]])
   gray_map = np.array(list(zip(*gray_map)))/255
   matrix = np.zeros((100,120),dtype=np.uint8)
   power_index = 1            # laser power index
   
   grid = [[grid_init() for i in range (0,2)]]
   for j in range (0,1):
     grid.append([grid_init() for i in range (0,2)])
   
   for colind in range (0,grid_size[0]):              # mask grid column index
      for rowind in range (0,grid_size[1]):           # mask grid row index

         col = offset[0] + cell_size[0]*(colind)
         row = offset[1] + cell_size[1]*(rowind)
         
         grid[colind][rowind].mask_point = [col,row]
         matrix[row+1][col+1] = power_index           # grid of single points
      
   mask_grid4 = grid
   return mask_grid4
   
   
#[2] detect_photo_grid4
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
      print(' - error in detect_photo_grid4: incorrect input')
      return
   
   grid_size = np.array(grid4).shape
   
   # read image
   photo = img.imread(photo_filename)  # [psu] = pixel separation unit

   threshold         = vThreshold  # [-]
   radius1           = 75          # [psu]
   radius2           = 15          # [psu]
   distance          = vDistance   # [psu]
   moment_of_inertia = 24          # [psu^2]
   lastSave          = time.time()
   
   for colind in range (0,grid_size[0]):      # mask grid4 column index
      for rowind in range (0,grid_size[1]):   # mask grid4 row index
         point = grid4[colind][rowind].photo_point
         disc  = disc_selection4([512,640], point, radius1)
         # print("-time Disc1: "+str(time.time()-lastSave))
         lastSave = time.time()
         
         com,moi,photo_disc,mass = centre_of_mass4(photo, threshold, disc)
         # print("-time moi 1: "+str(time.time()-lastSave))
         lastSave = time.time()
         
         disc  = disc_selection4([512,640], com, radius2)
         # print("-time disc2: "+str(time.time()-lastSave))
         lastSave = time.time()
         
         com,moi,photo_disc,mass = centre_of_mass4(photo, threshold, disc)
         # print("-time moi2: "+str(time.time()-lastSave))
         lastSave = time.time()
         
         
         if (LA.norm(com-point)<distance and moi<moment_of_inertia):
            grid4[colind][rowind].photo_point = com
            grid4[colind][rowind].status = 'detected'    # expect to happen for all
         else:
            grid4[colind][rowind].status = 'rejected'    # problem if this happens
            print(' - error in detect_photo_grid4: photo_point rejected')
   
   return grid4

#[3] disc_selection4
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
   
   # disc value
   disc[np.where((col_ind + row_ind) < np.power(radius,2))] = True
   
   return disc

#[4] centre_of_mass4
def centre_of_mass4(photo, threshold, disc):
   # Note 6 October 2017
   # Increase performance by using flattening Fortran indexing
   disc_flat = np.array(disc).flatten('F')
   photo_flat = np.array(photo).flatten('F')
   photo_disc = np.array(photo_flat[disc_flat])
   
   # Method earlier, access index by using for loop
   # photo_disc = np.array([])
   # for i in range (0, len(photo[0])):
   #    for j in range (0,len(photo)):
   #       if (disc[j][i]):
   #          photo_disc = np.append(photo_disc,photo[j][i])
   
   intensity = threshold * np.amax(photo_disc) #use absolute threshold
   
   photo_disc[np.where(photo_disc<intensity)] = 0
   
   
   matrix_size = np.array(photo).shape
   x = matrix_size[1]   # 512 or 256
   y = matrix_size[0]   # 640 or 320
   
   
   # Note 6 October 2017
   # Increase performance by using flattening Fortran indexing
   photo       = np.zeros(matrix_size)
   photo_flat = np.empty(y*x,order = 'F')
   photo_flat[disc_flat] = photo_disc
   photo = photo_flat.reshape(y,x,order = 'F')
   
   # k = 0
   # 
   # lastSave = time.time()
   # for i in range (0, len(photo[0])):
   #    for j in range (0,len(photo)):
   #       if (disc[j][i]):
   #          photo[j][i] = photo_disc[k]
   #          k = k+1
   # 
   
   #create y and x matrix dimension
   y_dim = np.arange(1,y+1)
   y_dim = np.array(list(zip(y_dim))) * np.ones((y,x))
   
   x_dim = np.arange(1,x+1)
   x_dim = np.array(x_dim) * np.ones((y,x)) 

   # mass value
   mass   = photo_disc.sum()
   
   # com value
   com = []
   com.append((np.sum(photo*x_dim))/mass)
   com.append((np.sum(photo*y_dim))/mass)
   com    = np.around(com)       #rounding using numpy.around function

   # r_quad value
   r_quad = np.power((x_dim-com[0]),2) + np.power((y_dim-com[1]),2)
   
   # moi value
   moi    = np.sum(photo*r_quad)/ mass
   
   # photo_disc value
   photo_disc = np.array(photo_disc,dtype='uint16')
   
   return com, moi, photo_disc, mass
 
#[5] compose_mask_grid 
def compose_mask_grid(hand):
   if hand == 'left':
      left_hand = True
      right_hand = False
   elif hand == 'right':
      left_hand = False
      right_hand = True
   else:
    print(' - error in compose_mask_grid4: incorrect input')
    return
    
   class grid_init:
      mask_point  = [0,0]     # motor mask coordinates
      photo_point = [0,0]     # photo pixel coordinates
      status      = 'initial' # 'initial', 'predicted', 'detected', 'rejected'

   # grid parameters
   mask_size = [120, 60]
   cell_size = [10, 10]       # must be divisor of mask_size
   grid_size = np.array(np.divide(mask_size,cell_size),dtype='int')

   
   #   in motor mask this is (5,5) for Left and (4,5) for Right

   #    settings on 10-04-2016: these settings are quite ok
   #    offset    = [6 ; 5]; # LUT+1
   #    if right_hand:
   #       offset = [4 ; 6]; #LUT+1
    
   #   settings for testing 31-05-2016: these should be ok
   offset    = [5, 5]
   if right_hand:
      offset = [4, 5]
   
   # make grid
   gray_map = np.array([[i for i in range (0,225+1)],[j for j in range (0,225+1)],[k for k in range (0,225+1)]])
   gray_map = np.array(list(zip(*gray_map)))/255
   matrix = np.zeros((100,120),dtype=np.uint8)
   power_index = 1                                      # laser power index

   grid = [[grid_init() for i in range (0,grid_size[1])]]
   for j in range (0,grid_size[0]-1):
     grid.append([grid_init() for i in range (0,grid_size[1])])
    

   for colind in range (0,grid_size[0]):                 # mask grid column index
      for rowind in range (0,grid_size[1]):              # mask grid row index
         col = offset[0] + cell_size[0]*(colind)
         row = offset[1] + cell_size[1]*(rowind)
         grid[colind][rowind].mask_point = [col,row];
         matrix[row][col] = power_index                  # grid of single points
   
   # Mmask_grid value
   mask_grid = grid

   return mask_grid
   
#[6] predict_photo_grid | 346
def predict_photo_grid(grid, grid4):
   # print result in terminal 
   print_result = False
   
   grid_size = np.array(grid).shape
   
   for colind in range (0,grid_size[0]):      # mask grid column index
      for rowind in range (0,grid_size[1]):   # mask grid row index

         x = grid[colind][rowind].mask_point
         y = np.around(mask_photo_map(grid4, x))
         if (((y[0]>=1 and y[0]<=512) and (y[1]>=1 and y[1]<=640)) and (0<colind and colind<grid_size[0]-1)):
            grid[colind][rowind].photo_point = y
            grid[colind][rowind].status = 'predicted'
   
   # print result in terminal
   if print_result:
      for i in range (0,12):
         for j in range (0,6):
           print(grid[i][j].mask_point, grid[i][j].photo_point, grid[i][j].status)
         print('new line',i+1)
   
   return grid

#[7] mask_photo_map     | 360
def mask_photo_map(grid4, x):
   y = interpolation_map(grid4, affine_map(grid4, x))
   
   return y

#[8] affine_map         | 366
def affine_map(grid4, x):
   p = np.array(grid4[0][0].mask_point)
   q = np.array(grid4[1][0].mask_point)
   r = np.array(grid4[0][1].mask_point)
   a = np.linalg.inv([q-p,r-p])
   b = np.array(x-p)
   y = np.dot(a,b)
   
   return y

#[9]  interpolation_map | 382
def interpolation_map(grid4, x):
   a = grid4[0][0].photo_point
   b = grid4[1][0].photo_point
   c = grid4[1][1].photo_point
   d = grid4[0][1].photo_point
   y = (1-x[1])*((1-x[0])*a+x[0]*b) + x[1]*((1-x[0])*d+x[0]*c)
   
   return y
   
#[10] detect_photo_grid  | 393
def detect_photo_grid(grid, photo_filename, vThreshold, vMass):
   # print result in terminal 
   print_result = False
   
   grid_size = np.array(grid).shape
   
   # read image matrix
   photo = img.imread(photo_filename)  # [psu] = pixel separation unit
   
   # Note: Tuesday, 3 October 2017
   # distance should be 11, it will match with matlab result
   
   threshold         = vThreshold  # [-]
   radius1           = 20          # [psu]
   radius2           = 8           # [psu]
   distance          = 11          # [psu] def: 12
   moment_of_inertia = 30          # [psu^2]
   minMass           = vMass
   lastSave          = time.time()


   for colind in range (0,grid_size[0]):      # mask grid column index
      for rowind in range (0,grid_size[1]):   # mask grid row index
         if  ((grid[colind][rowind].status == 'predicted') or 
              (grid[colind][rowind].status == 'extrapolated')):
            point = grid[colind][rowind].photo_point
            disc  = disc_selection4([512,640], point, radius1)
            com, moi, photo_disc, mass = centre_of_mass4(photo, threshold, disc)
            disc  = disc_selection4([512,640], com, radius2)
            com, moi, photo_disc, mass = centre_of_mass4(photo, threshold, disc)
           
            if ((LA.norm(com-point)<distance and moi<moment_of_inertia) and mass>minMass):
               grid[colind][rowind].photo_point = com
               grid[colind][rowind].status = 'detected'
            else:
               grid[colind][rowind].status = 'rejected'
      
      
      
   # print result in terminal
   if print_result:
      for i in range (0,12):
         for j in range (0,6):
           print(grid[i][j].mask_point, grid[i][j].photo_point, grid[i][j].status)
         print('new line',i+1)
            
   return grid

#[11] extrapolate_photo_grid  | 447
def extrapolate_photo_grid(grid):
   # print result in terminal 
   print_result = False
  
   # Note: 4 October 2017
   # use deepcopy to avoid making refrence to an object
   grid1 =copy.deepcopy(grid)
   
   
   # show result
   np.errstate(divide='ignore')
   grid_size = np.array(grid).shape
   for colind in range (0,grid_size[0]):      # mask grid column index
      for rowind in range (0,grid_size[1]):   # mask grid row index
         if   ((grid[colind][rowind].status == 'initial') or 
               (grid[colind][rowind].status == 'rejected')):
            local_grid = make_local_grid(grid, colind, rowind)
            well_conditioned, point = extrapolate_point(local_grid)
            
            if (well_conditioned and ((point[0]>=1 and point[0]<=512) and
               (point[1]>=1 and point[1]<=640))):
               grid1[colind][rowind].photo_point = point
               grid1[colind][rowind].status = 'extrapolated'
   
   # grid value
   grid = copy.deepcopy(grid1)
   
   # print result in terminal
   if print_result:
      for i in range (0,12):
         for j in range (0,6):
           print(grid[i][j].mask_point, grid[i][j].photo_point, grid[i][j].status)
         print('new line',i+1)
   
   return grid

#[12] make_local_grid    | 482
def make_local_grid(grid, colind, rowind):
   grid_size = np.array(grid).shape
   
   class local_init:
    point  = [0,0]         # motor mask coordinates
    validity = False
    
   # initialize local grid value
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

#[13] extrapolate_point  | 508
def extrapolate_point(local_grid):
   # calculate weight
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
   
   # well_conditioned and point value
   well_conditioned, point = calculate_pab(m00, m10, m01, m11, m20, m02, q00, q10, q01)
   
   return well_conditioned, point

#[14] calculate_weight   | 529 #EDITED
def calculate_weight(local_grid):
   weight = np.zeros((5,5),dtype=np.double)
   
   for k in range (-2,2+1):
      for n in range (-2,2+1):
         # if (k==0 and n == 0):
            # weight[3-1][3-1] = 0
         # else:
            # weight[k+3-1][n+3-1] = (1/np.power((k*k+n*n),2)) * local_grid[k+3-1][n+3-1].validity 
   
         a = np.power((k*k+n*n),2) * local_grid[k+3-1][n+3-1].validity
         if (a == 0):
            weight[k+3-1][n+3-1] = 0
         else :
            weight[k+3-1][n+3-1] = 1/a
      
      
      
   return weight

#[15] calculate_weight   | 549
def calculate_m(weight, i, j):
   m_ij = 0
   
   for k in range (-2,2+1):
      for n in range (-2,2+1):
         m_ij = m_ij + np.power(k,i) * np.power(n,j) * weight[k+3-1][n+3-1]
      
   return m_ij

#[16] calculate_q   | 561
def calculate_q(local_grid, weight, i, j):

   q_ij = 0
   for k in range (-2,2+1):
      for n in range (-2,2+1):
         q_ij = q_ij + np.power(k,i) * np.power(n,j) * np.dot(weight[k+3-1][n+3-1],local_grid[k+3-1][n+3-1].point)
      
   return q_ij

#[17] calculate_pab      | 570
def calculate_pab(m00, m10, m01, m11, m20, m02, q00, q10, q01):
   
   M = [[m00, m10, m01],[m10, m20, m11],[m01, m11, m02 ]]
   
   # rounding the value to 4 decimals
   for x in range (0,len(M)):
      for y in range (0,len(M[x])):
        M[x][y] = Decimal(M[x][y])
        M[x][y] = Decimal(M[x][y].quantize(Decimal('.0001'), rounding=ROUND_HALF_UP))
   M = np.array(M,dtype='double')
   
   # well conditioned check condition
   
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
   
   return well_conditioned,point
   
#[18] finish_photo_gridL | 604
def finish_photo_gridL(grid):
   # print result in terminal 
   print_result = False
   
   grid1 = copy.deepcopy(grid)
   grid_size = np.array(grid).shape
   
   for rowind in range (grid_size[1]-1,1-2,-1):# split direction for finishing halfway the columns
       for colind in range (6,grid_size[0]):      # mask grid column index
         if   ((grid[colind][rowind].status == 'initial') or
               (grid[colind][rowind].status == 'rejected')):
            dirx = 1
            diry = -1
            base = [colind-dirx,rowind-diry]
            
            # v1 and v2 calculation
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0] , 
                 grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0] , 
                 grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
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
            base = [colind-dirx,rowind-diry]
            
            # v1 and v2 calculation
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0], 
                 grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0], 
                 grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v = v1+v2
            
            point = np.array([grid1[base[0]][base[1]].photo_point[0]+v[0] , grid1[base[0]][base[1]].photo_point[1]+v[1]])
            grid1[colind][rowind].photo_point = point
            grid1[colind][rowind].status = 'extrapolated'
   
   # grid value
   grid = copy.deepcopy(grid1)
   
   # print result in terminal
   if print_result:
      for i in range (0,12):
         for j in range (0,6):
           print(grid[i][j].mask_point, grid[i][j].photo_point, grid[i][j].status)
         print('new line',i+1)
   
   return grid
   
#[19] finish_photo_gridR | 657
def finish_photo_gridR(grid): 
   # print result in terminal 
   print_result = False
   
   grid1 = copy.deepcopy(grid)
   grid_size = np.array(grid).shape
   
   for rowind in range (grid_size[1]-1,1-2,-1):
       # split direction for finishing halfway the columns
       for colind in range (5,1-2,-1):     # mask grid column index
         if   ((grid[colind][rowind].status == 'initial') or 
               (grid[colind][rowind].status == 'rejected')):
            dirx = -1
            diry = -1
            base = [colind-dirx , rowind-diry]
            
            # v1 and v2 calculation
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v = v1+v2
            
            # point value
            point = np.array([grid1[base[0]][base[1]].photo_point[0]+v[0],
                    grid1[base[0]][base[1]].photo_point[1]+v[1]])
            
            grid1[colind][rowind].photo_point = point;
            grid1[colind][rowind].status = 'extrapolated';
         
       
       # split direction for finishing halfway the columns
       for colind in range (6,grid_size[0]):   # mask grid column index
         if   ((grid[colind][rowind].status == 'initial') or 
               (grid[colind][rowind].status == 'rejected')):
            dirx = 1
            diry = -1
            base = [colind-dirx,rowind-diry]
            
            # v1 and v2 calculation
            v1 = np.array([grid1[base[0]][base[1]+diry].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]][base[1]+diry].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v2 = np.array([grid1[base[0]+dirx][base[1]].photo_point[0] - grid1[base[0]][base[1]].photo_point[0],
                  grid1[base[0]+dirx][base[1]].photo_point[1] - grid1[base[0]][base[1]].photo_point[1]])
            
            v = v1+v2
            
            point = np.array([grid1[base[0]][base[1]].photo_point[0]+v[0],
                    grid1[base[0]][base[1]].photo_point[1]+v[1]])
                    
            grid1[colind][rowind].photo_point = point
            grid1[colind][rowind].status = 'extrapolated'
   
   # grid value
   grid = copy.deepcopy(grid1)
   
   # print result in terminal
   if print_result:
      for i in range (0,12):
         for j in range (0,6):
           print(grid[i][j].mask_point, grid[i][j].photo_point, grid[i][j].status)
         print('new line',i+1)
   
   return grid

   
def display_grid(grid, photo_filename, fignr):
   # print result as image 
   print_result = False
   
   # read image
   photo = plt.imread(photo_filename);
   
   # inverse photo color
   photo = 1 - np.array(photo)
   photo_size = np.array(photo).shape
   
   # repmat photo from 2D to 3D
   photo_t = np.empty((photo_size[0],photo_size[1],3))
   
   for i in range (0,photo_size[0]):
       for j in range (0,photo_size[1]):
           photo_t[i][j][0] = photo[i][j]
           photo_t[i][j][1] = photo[i][j]
           photo_t[i][j][2] = photo[i][j]
           
   photo = np.array(photo_t)
   grid_size = np.array(grid).shape
   
   for k in range (0,grid_size[0]):
      for n in range (0,grid_size[1]):
         if not(grid[k][n].status == 'initial'):
            if (grid[k][n].status == 'predicted'):
               color = [0,1,1] # cyan
               
            elif (grid[k][n].status == 'detected'):
               color = [0,1,0]    # green
               
            elif (grid[k][n].status == 'rejected'):
               color = [1,0,0]    # red
               
            elif (grid[k][n].status == 'extrapolated'):
               color = [0,0,1];    # blue
            

            x     = grid[k][n].photo_point 
            x_lim = np.amin([[512-1,640-1],np.amax([[1,1],x],axis=0)],axis=0)
            x_dec = np.amin([[512-1,640-1],np.amax([[1,1],x-1],axis=0)],axis=0)
            x_inc = np.amin([[512-1,640-1],np.amax([[1,1],x+1],axis=0)],axis=0)
            
            # give the color to the pixel
            for i in range (0,3):
               photo[x_dec[1]][x_lim[0]][i] = color[i]
               photo[x_inc[1]][x_lim[0]][i] = color[i]
               photo[x_lim[1]][x_lim[0]][i] = color[i]
               photo[x_lim[1]][x_dec[0]][i] = color[i]
               photo[x_lim[1]][x_inc[0]][i] = color[i]
         
   # figure for the picture
   fig = plt.figure(fignr)
   ax = fig.add_subplot(111)
   
   # show image
   if print_result:
      plt.imshow(photo)

   x = np.around(0.10*512)
   y = np.around(0.85*640)
   d = np.around(0.035*640)
   ax.text(x,y,'predicted',color='cyan')
   ax.text(x,y+d,'detected',color='green')
   ax.text(x,y+2*d,'rejected',color='red')
   ax.text(x,y+3*d,'extrapolated',color='blue')
   fig.savefig('display_grid'+str(fignr)+'.png')
   
   # other options for the picture
   # fig.savefig('display_grid'+str(fignr)+'.png',bbox_inches='tight', pad_inches = 0)
   return