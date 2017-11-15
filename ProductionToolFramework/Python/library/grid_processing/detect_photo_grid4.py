import matplotlib.image as mpimg
import matplotlib.pyplot as plt
from numpy import linalg as LA
import numpy as np

def detect_photo_grid4(grid4, hand, photo_filename, vThreshold, vDistance):

   #global LOG;
   #fprintf(LOG, '%s\n', 'detect_photo_grid4');

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

   photo = mpimg.imread(photo_filename)  # [psu] = pixel separation unit

   threshold         = vThreshold  # [-]
   radius1           = 75          # [psu]
   radius2           = 15          # [psu]
   distance          = vDistance   # [psu]
   moment_of_inertia = 24          # [psu^2]

   for colind in range (0,grid_size[0]):      # mask grid4 column index
      for rowind in range (0,grid_size[1]):   # mask grid4 row index

         point = grid4[colind][rowind].photo_point
         disc  = disc_selection([512,640], point, radius1)
         com,moi,photo_disc = centre_of_mass(photo, threshold, disc)
         disc  = disc_selection([512,640], com, radius2)
         com,moi,photo_disc = centre_of_mass(photo, threshold, disc)
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
   
   # print(point)
   # np.savetxt('col_dim.csv',np.array(col_ind,dtype='int'),delimiter=",")
   # np.savetxt('row_dim.csv',np.array(row_ind,dtype='int'),delimiter=",")
   
   disc[np.where((col_ind + row_ind) < np.power(radius,2))] = True
   
   return disc

# com = centre of mass
# moi = moment of inertia divided by mass
# photo_disc output for displaying
# function [com moi photo_disc] = centre_of_mass(photo, threshold, disc)
def centre_of_mass(photo, threshold, disc):
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