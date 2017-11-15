#function [] = display_grid(grid, photo_filename, fignr)
import numpy.matlib
import numpy as np
from scipy import misc as img
import matplotlib.pyplot as plt


def display_grid(grid, photo_filename, fignr):
   
   photo = plt.imread(photo_filename);
   #invers the color
   photo = 1 - np.array(photo)
   photo_size = np.array(photo).shape
   
  
   
   #repmat photo from 2D to 3D
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
            

            x     = grid[k][n].photo_point  #shift 1 pixel due to one-based indexing in Matlab
            print(x)
            x_lim = np.amin([[512-1,640-1],np.amax([[1,1],x],axis=0)],axis=0)
            print(x_lim)
            x_dec = np.amin([[512-1,640-1],np.amax([[1,1],x-1],axis=0)],axis=0)
            print(x_dec)
            x_inc = np.amin([[512-1,640-1],np.amax([[1,1],x+1],axis=0)],axis=0)
            print(x_inc)
          
            #Give the color to the pixel
            for i in range (0,3):
               photo[x_dec[1]][x_lim[0]][i] = color[i]
               photo[x_inc[1]][x_lim[0]][i] = color[i]
               photo[x_lim[1]][x_lim[0]][i] = color[i]
               photo[x_lim[1]][x_dec[0]][i] = color[i]
               photo[x_lim[1]][x_inc[0]][i] = color[i]
         
   fig = plt.figure(fignr)
   ax = fig.add_subplot(111)
   plt.imshow(photo)

   x = np.around(0.10*512)
   y = np.around(0.85*640)
   d = np.around(0.035*640)
   ax.text(x,y,'predicted',color='cyan')
   ax.text(x,y+d,'detected',color='green')
   ax.text(x,y+2*d,'rejected',color='red')
   ax.text(x,y+3*d,'extrapolated',color='blue')
   fig.savefig('display_grid'+str(fignr)+'.png')
   # fig.savefig('display_grid'+str(fignr)+'.png',bbox_inches='tight', pad_inches = 0)
   return

