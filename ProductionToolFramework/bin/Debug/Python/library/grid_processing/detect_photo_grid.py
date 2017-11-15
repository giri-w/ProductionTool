#function grid = detect_photo_grid(grid, photo_filename, vThreshold, vMass)
#NOTE VER 0.0 = Using round function
#NOTE VER 1.0 = Using Decimal function
import numpy as np
import matplotlib.image as mpimg
from scipy import misc as img 
from numpy import linalg as LA
from decimal import Decimal, ROUND_HALF_UP
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
