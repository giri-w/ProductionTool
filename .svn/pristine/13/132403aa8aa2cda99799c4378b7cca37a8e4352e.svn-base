#function [] = display_grid(grid, photo_filename, fignr)
import ctypes


def display_grid(grid, photo_filename, fignr)
   #global LOG;
   #fprintf(LOG, '#s #d\n', 'display_grid', fignr);

   info = imfinfo(photo_filename);
   
   photo = imread(photo_filename);
   if info.BitDepth == 16
       photo = 2^16-1 - photo;    # saving printer toner
   elseif info.BitDepth == 8
       photo = 2^8-1 - photo;
   end
   
   photo = repmat(photo, [1 1 3]);

   grid_size = np.array(grid).shape
   for k in range (1,grid_size[0]):
      for n in range (1,grid_size[1]):

         if not(grid[k][n].status == 'initial'):

            if (grid[k][n].status == 'predicted'):
               color = [0 65535 65535] # cyan
            elif (grid[k][n].status == 'detected')
               color = [0 65535 0]    # green
            elif (grid[k][n].status == 'rejected')
               color = [65535 0 0]    # red
            elif (grid[k][n].status == 'extrapolated')
               color = [0 0 65535];    # blue
            

            x     = grid[k][n].photo_point  #shift 1 pixel due to one-based indexing in Matlab
            x_lim = np.amin([[512,640],np.amax([[1,1],x],[],2)],[],2)
            x_dec = np.amin([[512,640],np.amax([[1,1],x-1],[],2)],[],2)
            x_inc = np.amin([[512,640],np.amax([[1,1],x+1],[],2)],[],2)
#             x_dec = max([[  1 ;   1], x-1], [], 2);
#             x_inc = min([[512 ; 640], x+1], [], 2);
            
            #CEK BAGIAN INI
            photo(x_dec[1], x_lim[0], 1:3) = color;
            photo(x_inc[1], x_lim[0], 1:3) = color;
            photo(x_lim[1], x_lim[0], 1:3) = color;
            photo(x_lim[1], x_dec[0], 1:3) = color;
            photo(x_lim[1], x_inc[0], 1:3) = color;
         
      
   

   user32 = ctypes.windll.user32
   screensize = [1 , 1, user32.GetSystemMetrics(0), user32.GetSystemMetrics(1)]

  

   figure(fignr); clf(fignr)
   set(fignr, 'Position', [3 49 screensize(3)/2-6 screensize(4)-119])
   imagesc(photo)

   x = np.around(0.08*512)
   y = np.around(0.8*640)
   d = np.around(0.035*640)

   text(x, y,     'predicted',    'Color', [0 1 1]);
   text(x, y+d,   'detected',     'Color', [0 1 0]);
   text(x, y+2*d, 'rejected',     'Color', [1 0 0]);
   text(x, y+3*d, 'extrapolated', 'Color', [0 0 1]);

   # save as eps
   print(fignr, '-depsc', '-r150', ['.\application\display_grid_' num2str(fignr)])
return

