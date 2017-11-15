#function grid = finish_photo_gridL(grid)
import numpy as np
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

