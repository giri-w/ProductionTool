#function grid = predict_photo_grid(grid, grid4)
import numpy as np
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

