# function mask_grid = compose_mask_grid(hand, png_path)
#
# hand = 'left' or 'right'
# png_path is optional input
#
# Outputs Matlab structure mask_grid and optionally image '.\application\motor_mask_grid.png'.
#
# Motor mask coordinate [col ; row] <=> motor mask matrix (row+1, col+1)
# with col = 0..119 and row = 0..59.
#
# Photo pixel coordinates [x ; y] <=> photo pixel matrix (y, x)
# with x = 1..512 and y = 1..640. (or 256x320)
# Photo pixel [0 ; 0] has special meaning, i.e. 'not applicable'.
#
# If png_path is given then an indexed png with gray scale colormap will be written.


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
