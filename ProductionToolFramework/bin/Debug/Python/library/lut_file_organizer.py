import os
import shutil
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
from scipy import misc as img 

def lut_file_organizer(sourcepath,destinationpath):
            
    im_pathLeft  = sourcepath + "\\display_grid19.png"
    im_pathRight = sourcepath + "\\display_grid29.png"
    
    A = mpimg.imread(im_pathLeft)
    B = mpimg.imread(im_pathRight)
    
    fig = plt.figure(1)
    
    # left hand
    ax1 = fig.add_subplot(1,2,1)
    ax1.axis('off')
    ax1.title.set_text('Left Hand')
    plt.imshow(A)
    
    # right hand
    ax2 = fig.add_subplot(1,2,2)
    ax2.axis('off')
    ax2.title.set_text('Right Hand')
    plt.imshow(B)
    plt.savefig('LUTDetermination.png')
    # show image
    # plt.show()  
    
	# create a new folder
    if not os.path.exists(destinationpath):
        os.makedirs(destinationpath)

    source = os.listdir(sourcepath)
    for files in source:
        if files.endswith('.png'):
            shutil.move(os.path.join(sourcepath,files), os.path.join(destinationpath,files))