import numpy as np
from numpy import linalg as LA
from decimal import Decimal, ROUND_HALF_UP
#NOTE VER 0.0 = Using round function
#NOTE VER 1.0 = Using Decimal function
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

