## change dir work
import os.path
os.chdir('C:\\Users\\GWA\\Documents\\GitHub\\Demcon\\ProductionToolFramework\\ProductionToolFramework\\bin\\Debug\\Python\\figure\\') 


import PIL
import numpy as np
import copy
import scipy.ndimage.morphology as scp
import matplotlib.image as mpimg
import matplotlib.pyplot as plt
import sys

from skimage.morphology import watershed
from scipy import misc as img 
from scipy import ndimage as ndi
from scipy.ndimage import label
from skimage.feature import peak_local_max




## variable 
im_path = r'C:\source\2. FieldMaskGeneration\Raw data\left_high_reflection.png'


ThresholdSlider = 40

## image reading
A = mpimg.imread(im_path)

# Image processing
# 1) Thresholding image
Th = int(ThresholdSlider)/255
A1 = copy.copy(A > Th)

# 2) Resize image
image_size = np.array(A).shape
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
plt.savefig('FieldMask.png')
	# show image
plt.show()  
print('Field Mask Generation: PASS')


