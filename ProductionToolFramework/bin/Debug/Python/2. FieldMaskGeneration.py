import numpy as np
import matplotlib
matplotlib.use('Agg')
import PIL
import scipy.misc
import copy
import scipy.ndimage.morphology as scp
import matplotlib.image as mpimg
import matplotlib.pyplot as plt

from PIL import Image
from skimage.morphology import watershed
from scipy import misc as img 
from scipy import ndimage as ndi
from scipy.ndimage import label
from skimage.feature import peak_local_max

## import argument
import os
import sys
import time

# path location of output
a0 = str(sys.argv[1]) 
a1 = a0.rsplit("\\",1)
a = a1[0] + "\\figure\\FAT1FieldMaskGeneration";

if not os.path.exists(a):
	os.makedirs(a)


# location of image source
b = str(sys.argv[2]) 
#b = b.replace("\\","\\\\")

# threshold value
c = float(sys.argv[3]) 

# choosen hand
d = str(sys.argv[4]) 

## debug
# a = r'C:\Users\GWA\Documents\GitHub\Demcon\ProductionToolFramework\ProductionToolFramework\bin\Debug\Python\figure\FAT1FieldMaskGeneration'

# if not os.path.exists(a):
	# os.makedirs(a)
		
# b = r'C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\1. Test Source\20170629_090733_297 Fieldmask'
# c = 40
# d = 'left'


## check dir location
import os, os.path, errno

if not os.path.exists(a):
    try:
        os.makedirs(a)
    except OSError as e:
        if e.errno != errno.EEXIST:
            raise

## initiate value
os.chdir(a) 
ThresholdSlider = c

def mainProgram(ThresholdSlider):
    print('60,Processing ' + d + ' Hand')
    FMGCreation(ThresholdSlider,d)
   
    print('100,Field Mask Generation: PASS')
    sys.stdout.flush()
    
    
def FMGCreation(ThresholdSlider,hand):
    
    ## source location
    if hand == "Right":
        im_path = b + "\\Raw data\\right_high_reflection.png"
        name = 'right_mask_high.png'
        name_s = 'right_mask.png'
    else :
        im_path = b + "\\Raw data\\left_high_reflection.png"
        name = 'left_mask_high.png'
        name_s = 'left_mask.png'
    
    ## image reading
    A = mpimg.imread(im_path)
    
    # Image processing
    # 1) Thresholding image
    Th = int(ThresholdSlider)/255
    A1 = copy.copy(A > Th)
    
    # 2) Resize image
    image_size = np.array(np.array(A).shape)
    
    A2 = A1[:]
    
    # 3) Erosion
    Nhood = np.array([[1,1,1],[1,1,1],[1,1,1]])
    
    for i in range (1,3):
        A2 = scp.binary_erosion(A2, structure = Nhood)
    A3 = A2[:]
    
    # 4) Blob selection
    
    # Note : 10/10/2017
    # Update using label form scipy to extract hand from the image
    c = label(A3,Nhood)
    unique,counts = np.unique(c[0], return_counts=True)
    
    a1 = np.transpose(np.array([unique,counts]))
    a2 = a1[a1[:,1].argsort()]
    a3 = np.transpose(a2)
    ind_hand = a3[0,(len(a3[0])-2)] # index of hand in the image
    A3[np.where(c[0] != ind_hand)] = 0
    A4 = A3[:]
    
    # 5) Dilation
    for i in range (1,3):
        A4 = scp.binary_dilation(A4,Nhood)
    
    B = A4[:];
    
    # Visualization of mask with red overlay:
    C = np.dstack((A[:,:],A[:,:],A[:,:])) # mask with ori image
    C[:,:,0] = C[:,:,0] + 0.5*B
    D = copy.copy(C[:,:,0])
    D[np.where(D>1)] = 1
    C[:,:,0] = D
    
    
    ## Plot of image
    fig = plt.figure(1)
    a = fig.add_subplot(1,2,1)
    a.axis('off')
    plt.imshow(A,cmap="gray")
    a = fig.add_subplot(1,2,2)
    a.axis('off')
    plt.imshow(C)
    plt.savefig(hand + 'FieldMask.png')
    # show image
    plt.show()  
    
    
    ## generate image
    scipy.misc.imsave(name,B)
    
    ## resize image
    im = Image.open(name)
    image_size_s = np.array(image_size/2,dtype='int')
    im_s = im.resize((image_size_s[1],image_size_s[0]), Image.NEAREST)
    scipy.misc.imsave(name_s,im_s)

    return

## main program
mainProgram(ThresholdSlider)

