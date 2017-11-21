import numpy as np
import matplotlib.image as mpimg
import matplotlib.pyplot as plt
from scipy import misc as img 
import scipy.ndimage.interpolation as imrtt
import scipy.ndimage.filters as filt
import matplotlib.patches as patches
from scipy.ndimage import label
import copy
import xml.etree.ElementTree as ET

## Main Program
plt.close('all')

## Image Path
# experiment 01
im_01_RawLnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\left_high_nir.png'
im_01_RawLvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\left_high_vis.png'
im_01_PDLnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\left_mm_nir_photodiode_001.png'
im_01_PDLvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\left_mm_vis_photodiode_000.png'
im_01_LXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\left-motormatrix.xml'

im_01_RawRnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\right_high_nir.png'
im_01_RawRvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\right_high_vis.png'
im_01_PDRnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\right_mm_nir_photodiode_001.png'
im_01_PDRvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\right_mm_vis_photodiode_000.png'
im_01_RXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp1\\right-motormatrix.xml'

# experiment 02
im_02_RawLnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\left_high_nir.png'
im_02_RawLvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\left_high_vis.png'
im_02_PDLnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\left_mm_nir_photodiode_001.png'
im_02_PDLvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\left_mm_vis_photodiode_000.png'
im_02_LXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\left-motormatrix.xml'

im_02_RawRnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\right_high_nir.png'
im_02_RawRvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\right_high_vis.png'
im_02_PDRnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\right_mm_nir_photodiode_001.png'
im_02_PDRvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\right_mm_vis_photodiode_000.png'
im_02_RXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp2\\right-motormatrix.xml'

# experiment 03
im_03_RawLnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\left_high_nir.png'
im_03_RawLvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\left_high_vis.png'
im_03_PDLnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\left_mm_nir_photodiode_001.png'
im_03_PDLvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\left_mm_vis_photodiode_000.png'
im_03_LXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\left-motormatrix.xml'


im_03_RawRnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\right_high_nir.png'
im_03_RawRvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\right_high_vis.png'
im_03_PDRnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\right_mm_nir_photodiode_001.png'
im_03_PDRvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\right_mm_vis_photodiode_000.png'
im_03_RXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp3\\right-motormatrix.xml'

# experiment 04
im_04_RawLnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\left_high_nir.png'
im_04_RawLvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\left_high_vis.png'
im_04_PDLnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\left_mm_nir_photodiode_001.png'
im_04_PDLvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\left_mm_vis_photodiode_000.png'
im_04_LXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\left-motormatrix.xml'

im_04_RawRnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\right_high_nir.png'
im_04_RawRvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\right_high_vis.png'
im_04_PDRnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\right_mm_nir_photodiode_001.png'
im_04_PDRvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\right_mm_vis_photodiode_000.png'
im_04_RXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp4\\right-motormatrix.xml'

# experiment 05
im_05_RawLnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\left_high_nir.png'
im_05_RawLvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\left_high_vis.png'
im_05_PDLnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\left_mm_nir_photodiode_001.png'
im_05_PDLvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\left_mm_vis_photodiode_000.png'
im_05_LXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\left-motormatrix.xml'


im_05_RawRnir = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\right_high_nir.png'
im_05_RawRvis = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\right_high_vis.png'
im_05_PDRnir  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\right_mm_nir_photodiode_001.png'
im_05_PDRvis  = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\right_mm_vis_photodiode_000.png'
im_05_RXML    = 'C:\\Users\\GWA\\Desktop\\Internship DEMCON\\4. Data Acquisition\\3. IntensityAccuracy [FR0114]\\source\\exp5\\right-motormatrix.xml'


## Point Location
pRAWnir = np.array(([303,188],[341,264],[290,286],[316,345],[203,363],[189,328],[357,175],[417,290],[264,229],[253,339]))
pRAWvis = np.array(([315,214],[368,316],[228,275],[267,371],[371,197],[431,314],[252,200],[305,314],[166,259],[191,327]))
pPDnir  = np.array(([15,24],[15,94],[25,24],[25,94],[35,24],[35,94],[45,24],[45,94],[55,24],[55,94]))
pPDvis  = np.array(([15,24],[15,94],[25,24],[25,94],[35,24],[35,94],[45,24],[45,94],[55,24],[55,94]))

## Cek Location
a[0]  = img.imread(im_01_RawLnir)
A1 = copy.copy(b1>3000)
b1 = imrtt.rotate(a[0],25)
plt.figure(20)
plt.imshow(A1,cmap='gray')
plt.show()
##
tree = ET.parse(im_01_LXML)
root = tree.getroot()

for coord in root.iter('Coordinate'):
    row    = coord.get('Row')
    column = coord.get('Çolumn')
    print (column)
    
    print (row)
    # if row == 55 & column == 44:
    #     xcoord = coord.get('X')
    #     ycoord = coord.get('Y')
    #     print (xcoord,ycoord)
##
# Update using label form scipy to extract hand from the image
A3 = copy.copy(b1>3000)
# plt.figure(20)
# plt.imshow(A3,cmap='gray')
# plt.show()
# 
# Nhood = np.array([[1,1,1],[1,1,1],[1,1,1]])
# c = label(A3,Nhood)
# unique,counts = np.unique(c[0], return_counts=True)
# 
a1 = np.transpose(np.array([unique,counts]))
a2 = a1[a1[:,1].argsort()]
a3 = np.transpose(a2)
# ind_hand = a3[0,(len(a3[0])-2)] # index of hand in the image
# A3[np.where(c[0] != ind_hand)] = 0


##
d1 = np.where(a3[1] < 11)
d2 = a3[0,80:]
d3 = d2[0:20]

A3[np.where(d2 in c[0])] = 1000

# A3[np.where(c[0] != 70)] = 1
    



##
i = 250
print ('rect position')
fig,ax = plt.subplots(1)
ax.imshow(b1,cmap='gray')
while i<619:
    if b1[460][i+1]-b1[460][i] > 550:
        horPos = i
        rect = patches.Rectangle((i-10,450),20,20,linewidth=1,edgecolor='r',facecolor='none')
        ax.add_patch(rect)
        print(i)
        print(b1[460][i])
        i = i+20
    
    i = i + 1
    
plt.show()
        



## Image Data
# initialization
a     = np.zeros ((5,640,512))  
b     = np.zeros ((5,640,512))
c     = np.zeros ((5,100,120))
d     = np.zeros ((5,100,120)) 
a_avg = np.zeros((len(pRAWnir)))
b_avg = np.zeros((len(pRAWnir)))
c_avg = np.zeros((len(pRAWnir)))
d_avg = np.zeros((len(pRAWnir)))
a_absInt = np.zeros((5,10))
b_absInt = np.zeros((5,10))
c_absInt = np.zeros((5,10))
d_absInt = np.zeros((5,10))
a_avgInt = np.zeros((len(pRAWnir)))
b_avgInt = np.zeros((len(pRAWnir)))
c_avgInt = np.zeros((len(pRAWnir)))
d_avgInt = np.zeros((len(pRAWnir)))

# processing
a[0]  = img.imread(im_01_RawLnir)
a[1]  = img.imread(im_02_RawLnir)
a[2]  = img.imread(im_03_RawLnir)
a[3]  = img.imread(im_04_RawLnir)
a[4]  = img.imread(im_05_RawLnir)

b[0]  = img.imread(im_01_RawLvis)
b[1]  = img.imread(im_02_RawLvis)
b[2]  = img.imread(im_03_RawLvis)
b[3]  = img.imread(im_04_RawLvis)
b[4]  = img.imread(im_05_RawLvis)

c[0]  = img.imread(im_01_PDLnir)
c[1]  = img.imread(im_02_PDLnir)
c[2]  = img.imread(im_03_PDLnir)
c[3]  = img.imread(im_04_PDLnir)
c[4]  = img.imread(im_05_PDLnir)

d[0]  = img.imread(im_01_PDLvis)
d[1]  = img.imread(im_02_PDLvis)
d[2]  = img.imread(im_03_PDLvis)
d[3]  = img.imread(im_04_PDLvis)
d[4]  = img.imread(im_05_PDLvis)

## Analyzed Measurement

# step a : average intensity of the spot
for i in range (0,len(pRAWnir)):
    a_avg[i] = (a[0,pRAWnir[i,0],pRAWnir[i,1]] + a[1,pRAWnir[i,0],pRAWnir[i,1]] + a[2,pRAWnir[i,0],pRAWnir[i,1]] + a[3,pRAWnir[i,0],pRAWnir[i,1]] + a[4,pRAWnir[i,0],pRAWnir[i,1]])/5
    b_avg[i] = (b[0,pRAWvis[i,0],pRAWvis[i,1]] + b[1,pRAWvis[i,0],pRAWvis[i,1]] + b[2,pRAWvis[i,0],pRAWvis[i,1]] + b[3,pRAWvis[i,0],pRAWvis[i,1]] + b[4,pRAWvis[i,0],pRAWvis[i,1]])/5
    c_avg[i] = (c[0,pPDnir[i,0],pPDnir[i,1]] + c[1,pPDnir[i,0],pPDnir[i,1]] + c[2,pPDnir[i,0],pPDnir[i,1]] + c[3,pPDnir[i,0],pPDnir[i,1]] + c[4,pPDnir[i,0],pPDnir[i,1]])/5
    d_avg[i] = (d[0,pPDvis[i,0],pPDvis[i,1]] + d[1,pPDvis[i,0],pPDvis[i,1]] + d[2,pPDvis[i,0],pPDvis[i,1]] + d[3,pPDvis[i,0],pPDvis[i,1]] + d[4,pPDvis[i,0],pPDvis[i,1]])/5
    
# step b : absolute intensity values
for i in range (0,5):
    for j in range (0,len(pRAWnir)):
        a_absInt[i][j] =  a[i,pRAWnir[j,0],pRAWnir[j,1]]/a_avg[j]
        b_absInt[i][j] =  b[i,pRAWvis[j,0],pRAWvis[j,1]]/b_avg[j]
        c_absInt[i][j] =  c[i,pPDnir[j,0],pPDnir[j,1]]  /c_avg[j]
        d_absInt[i][j] =  d[i,pPDvis[j,0],pPDvis[j,1]]  /d_avg[j]

# step c : mean absolute intensity
a_avgInt = np.zeros((len(pRAWnir)))
b_avgInt = np.zeros((len(pRAWnir)))
c_avgInt = np.zeros((len(pRAWnir)))
d_avgInt = np.zeros((len(pRAWnir)))

for i in range (0,len(pRAWnir)):
    a_avgInt[i] = (a_absInt[0,i] + a_absInt[1,i] +a_absInt[2,i] +a_absInt[3,i]+ a_absInt[4,i])/5
    b_avgInt[i] = (b_absInt[0,i] + b_absInt[1,i] +b_absInt[2,i] +b_absInt[3,i]+ b_absInt[4,i])/5
    c_avgInt[i] = (c_absInt[0,i] + c_absInt[1,i] +c_absInt[2,i] +c_absInt[3,i]+ c_absInt[4,i])/5
    d_avgInt[i] = (d_absInt[0,i] + d_absInt[1,i] +d_absInt[2,i] +d_absInt[3,i]+ d_absInt[4,i])/5
    
# step d : relative absolute intensity
a_relInt = np.zeros((5,10))
b_relInt = np.zeros((5,10))
c_relInt = np.zeros((5,10))
d_relInt = np.zeros((5,10))

for i in range (0,5):
    for j in range (0,len(pRAWnir)):
        a_relInt[i][j] = a_absInt[i][j]/a_avgInt[j]
        b_relInt[i][j] = b_absInt[i][j]/b_avgInt[j]
        c_relInt[i][j] = c_absInt[i][j]/c_avgInt[j]
        d_relInt[i][j] = d_absInt[i][j]/d_avgInt[j]
        
## Plot Graph Camera
plt.figure(10,figsize=(11,4.5))
measurement = np.arange(5)+1
plt.subplot(121)
plt.title('NIR Camera Intensity')
plt.ylabel('Relative Intensity')
plt.xlabel('Measurement')
plt.axis([1,5.0,0.80,1.20])
plt.plot([1,5],[0.95,0.95],'r--')
plt.plot([1,5],[1.05,1.05],'r--')
for i in range (0,len(pRAWnir)):
    plt.plot(measurement, [a_relInt[0,i],a_relInt[1,i],a_relInt[2,i],a_relInt[3,i],a_relInt[4,i]])

plt.subplot(122)
plt.title('VIS Camera Intensity')
plt.ylabel('Relative Intensity')
plt.xlabel('Measurement')
plt.axis([1,5.0,0.8,1.20])
plt.plot([1,5],[0.95,0.95],'r--')
plt.plot([1,5],[1.05,1.05],'r--')
for i in range (0,len(pRAWnir)):
    plt.plot(measurement, [b_relInt[0,i],b_relInt[1,i],b_relInt[2,i],b_relInt[3,i],b_relInt[4,i]])

plt.show()

## Plot Graph PhotoDiode

plt.figure(11,figsize=(11,4.5))
measurement = np.arange(5)+1
plt.subplot(121)
plt.title('NIR PhotoDiode Intensity')
plt.ylabel('Relative Intensity')
plt.xlabel('Measurement')
plt.axis([1,5.0,0.90,1.10])
plt.plot([1,5],[0.95,0.95],'r--')
plt.plot([1,5],[1.05,1.05],'r--')
for i in range (0,len(pRAWnir)):
    lbl = 'Point '+str(i)
    plt.plot(measurement, [c_relInt[0,i],c_relInt[1,i],c_relInt[2,i],c_relInt[3,i],c_relInt[4,i]],label=lbl)

plt.subplot(122)
plt.title('VIS PhotoDiode Intensity')
plt.ylabel('Relative Intensity')
plt.xlabel('Measurement')
plt.axis([1,5.0,0.9,1.10])
plt.plot([1,5],[0.95,0.95],'r--')
plt.plot([1,5],[1.05,1.05],'r--')
for i in range (0,len(pRAWnir)):
    lbl = 'Point '+str(i)
    plt.plot(measurement, [d_relInt[0,i],d_relInt[1,i],d_relInt[2,i],d_relInt[3,i],d_relInt[4,i]],label=lbl)

plt.show()









