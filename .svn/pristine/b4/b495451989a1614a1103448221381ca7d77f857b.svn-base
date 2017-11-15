#function mask_grid4 = compose_mask_grid4(varargin)
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
   
   #GAK JELAS #################################################
   for colind in range (0,grid_size[0]):              # mask grid column index
      for rowind in range (0,grid_size[1]):           # mask grid row index

         col = offset[0] + cell_size[0]*(colind)
         row = offset[1] + cell_size[1]*(rowind)

         #grid[colind][rowind]            = grid_init() ##CEK BAGIAN INI
         grid[colind][rowind].mask_point = [col,row]

         matrix[row+1][col+1] = power_index      # grid of single points
      
   

   mask_grid4 = grid;
   return mask_grid4
