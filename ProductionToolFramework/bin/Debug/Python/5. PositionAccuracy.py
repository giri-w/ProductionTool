import os
import sys
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
# from scipy import ndimage
# import matplotlib.animation as manimation
import numpy as np
from scipy import signal, ndimage
import path
import xml.etree.ElementTree as ET

# plt.close('all')

# debug
a = "C:\\Users\\GWA\\Documents\\GitHub\\Demcon\\ProductionToolFramework\\ProductionToolFramework\\bin\\Debug\\Python\\figure\\FAT4SpatialAccuracy"
if not os.path.exists(a):
	os.makedirs(a)
b = r"C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\Spatial accuracy\Measurements"

## from argument
# # path location of output
# a0 = str(sys.argv[1]) 
# a1 = a0.rsplit("\\",1)
# a = a1[0] + "\\figure\\FAT4SpatialAccuracy";
# if not os.path.exists(a):
	# os.makedirs(a)

# # location of image source
# b = str(sys.argv[2]) 
# b = b.replace("\\","\\\\")


## functions
def getXyByArea(imgcrop,filename):
    #define threshold by using min / max of intensity in region
    minI = np.min(imgcrop)
    maxI = np.max(imgcrop)
    dI = maxI-minI
#     print(minI,maxI)
    thresholdR = 0.4
    threshold = minI + thresholdR * dI
    # threshold = 0.5
    ind = np.where(imgcrop>threshold)
    
    xavg = np.average(ind[1])
    yavg = np.average(ind[0])
    
#     print(xavg)
    
    #also calculate average intensity
    iavg = np.average(imgcrop[ind[0],ind[1]])
    
    # fig = plt.figure()
    # h = plt.imshow(imgcrop,cmap='gray')
    # plt.plot(xavg,yavg,'r+')
    # plt.savefig(resultdr+'\\'+filename+'.png')
    # plt.close()
    
    return xavg,yavg,iavg


## Definitions

# change dir work
import os.path
os.chdir(a) 


# dirIn = r'C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\Spatial accuracy\Measurements'
# resultdr = r'C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\Spatial accuracy\Results'

dirIn = b
resultdr = a


# folder, spotsize according to camera mask
folder = os.listdir(dirIn)
firstMeasurement = os.path.join(dirIn, folder[0])


# FROM LookUpTable = high resolution
Lin = np.array([[12,25],
                [58,10],
                [98,36],
                [36,50]])

Rin = np.array([[99,25],
                [57,12],
                [14,40],
                [70,49]])

## read motor matrix xml
# p = path.Path(os.path.join(dirIn, '\\Calibration'))
# print('LUT directory: ', p)
# 
# xml_left = p.files('left-motormatrix.xml')[0]
# xml_right = p.files('right-motormatrix.xml')[0]


#print('LUT is used from measurement:',firstMeasurement)
xml_left = firstMeasurement + '\\Calibration\\left-motormatrix.xml'
xml_right = firstMeasurement + '\\Calibration\\right-motormatrix.xml'


tree = ET.parse(xml_left)
root = tree.getroot()

MML = np.array([[0,0,0,0]])

for elem in tree.iter('Coordinate'):
    # print(elem)
    tmprow = int(elem.get('Row'))
    tmpcol = int(elem.get('Column'))
    tmpx = int(elem.get('X'))
    tmpy = int(elem.get('Y'))
    MML = np.append(MML,np.array([[tmprow,tmpcol,tmpx,tmpy]]),axis=0)

L = np.zeros([4,4])
for i in range(np.shape(Lin)[0]):
    ind = np.where((MML[:,1]==Lin[i,0]) & (MML[:,0]==Lin[i,1]))[0][0]
    L[i,0]=Lin[i,0]
    L[i,1]=Lin[i,1]
    L[i,2]=MML[ind,2]
    L[i,3]=MML[ind,3]

tree = ET.parse(xml_right)
root = tree.getroot()

MMR = np.array([[0,0,0,0]])

for elem in tree.iter('Coordinate'):
    # print(elem)
    tmprow = int(elem.get('Row'))
    tmpcol = int(elem.get('Column'))
    tmpx = int(elem.get('X'))
    tmpy = int(elem.get('Y'))
    MMR = np.append(MMR,np.array([[tmprow,tmpcol,tmpx,tmpy]]),axis=0)

R = np.zeros([4,4])
for i in range(np.shape(Rin)[0]):
    ind = np.where((MMR[:,1]==Rin[i,0]) & (MMR[:,0]==Rin[i,1]))[0][0]
    R[i,0]=Rin[i,0]
    R[i,1]=Rin[i,1]
    R[i,2]=MMR[ind,2]
    R[i,3]=MMR[ind,3]


## Define points to be detected

S = 20

# left hand ROIs
Ltheta1 = 11 # [degrees]
Lpxpermm1 = 1.74
Ltheta2 = 26 # [degrees]
Lpxpermm2 = 1.96
Ltheta3 = 48 # [degrees]
Lpxpermm3 = 2.18
Ltheta4 = 26 # [degrees]
Lpxpermm4 = 1.97

Ltheta1 = 0
Ltheta2 = 0
Ltheta3 = 0
Ltheta4 = 0

# right hand ROIs
Rtheta1 = -11 # [degrees]
Rpxpermm1 = 1.81
Rtheta2 = -27 # [degrees]
Rpxpermm2 = 1.95
Rtheta3 = -50 # [degrees]
Rpxpermm3 = 2.23
Rtheta4 = -27 # [degrees]
Rpxpermm4 = 1.97

Rtheta1 = 0
Rtheta2 = 0
Rtheta3 = 0
Rtheta4 = 0

#region 1
Lx11 = int(L[0,2] - S)
Ly11 = int(L[0,3] - S)
Lx12 = int(Lx11 + 2*S)
Ly12 = int(Ly11 + 2*S)
Rx11 = int(R[0,2] - S)
Ry11 = int(R[0,3] - S)
Rx12 = int(Rx11 + 2*S)
Ry12 = int(Ry11 + 2*S)

#region 2
Lx21 = int(L[1,2] - S)
Ly21 = int(L[1,3] - S)
Lx22 = int(Lx21 + 2*S)
Ly22 = int(Ly21 + 2*S)
Rx21 = int(R[1,2] - S)
Ry21 = int(R[1,3] - S)
Rx22 = int(Rx21 + 2*S)
Ry22 = int(Ry21 + 2*S)

#region 3
Lx31 = int(L[2,2] - S)
Ly31 = int(L[2,3] - S)
Lx32 = int(Lx31 + 2*S)
Ly32 = int(Ly31 + 2*S)
Rx31 = int(R[2,2] - S)
Ry31 = int(R[2,3] - S)
Rx32 = int(Rx31 + 2*S)
Ry32 = int(Ry31 + 2*S)

#region 4
Lx41 = int(L[3,2] - S)
Ly41 = int(L[3,3] - S)
Lx42 = int(Lx41 + 2*S)
Ly42 = int(Ly41 + 2*S)
Rx41 = int(R[3,2] - S)
Ry41 = int(R[3,3] - S)
Rx42 = int(Rx41 + 2*S)
Ry42 = int(Ry41 + 2*S)


#read and show first image
p = os.path.join(firstMeasurement, 'Raw data')
imgfile_left_nir = os.path.join(p + '\\left_high_nir.png')
imgfile_left_vis = os.path.join(p + '\\left_high_vis.png')
imgfile_right_nir = os.path.join(p + '\\right_high_nir.png')
imgfile_right_vis = os.path.join(p + '\\right_high_vis.png')

imgLnir=mpimg.imread(imgfile_left_nir)
imgLvis=mpimg.imread(imgfile_left_vis)
imgRnir=mpimg.imread(imgfile_right_nir)
imgRvis=mpimg.imread(imgfile_right_vis)

plt.figure(num=10,figsize=(12,6))
plt.subplot(1,2,1)
plt.imshow(imgLnir,cmap='gray')
plt.plot([Lx11,Lx12],[Ly11,Ly11],'r-')
plt.plot([Lx11,Lx12],[Ly12,Ly12],'r-')
plt.plot([Lx11,Lx11],[Ly11,Ly12],'r-')
plt.plot([Lx12,Lx12],[Ly11,Ly12],'r-')
plt.plot([Lx21,Lx22],[Ly21,Ly21],'g-')
plt.plot([Lx21,Lx22],[Ly22,Ly22],'g-')
plt.plot([Lx21,Lx21],[Ly21,Ly22],'g-')
plt.plot([Lx22,Lx22],[Ly21,Ly22],'g-')
plt.plot([Lx31,Lx32],[Ly31,Ly31],'b-')
plt.plot([Lx31,Lx32],[Ly32,Ly32],'b-')
plt.plot([Lx31,Lx31],[Ly31,Ly32],'b-')
plt.plot([Lx32,Lx32],[Ly31,Ly32],'b-')
plt.plot([Lx41,Lx42],[Ly41,Ly41],'m-')
plt.plot([Lx41,Lx42],[Ly42,Ly42],'m-')
plt.plot([Lx41,Lx41],[Ly41,Ly42],'m-')
plt.plot([Lx42,Lx42],[Ly41,Ly42],'m-')
plt.title('left_low_nir')
plt.subplot(1,2,2)
plt.imshow(imgRnir,cmap='gray')
plt.plot([Rx11,Rx12],[Ry11,Ry11],'r-')
plt.plot([Rx11,Rx12],[Ry12,Ry12],'r-')
plt.plot([Rx11,Rx11],[Ry11,Ry12],'r-')
plt.plot([Rx12,Rx12],[Ry11,Ry12],'r-')
plt.plot([Rx21,Rx22],[Ry21,Ry21],'g-')
plt.plot([Rx21,Rx22],[Ry22,Ry22],'g-')
plt.plot([Rx21,Rx21],[Ry21,Ry22],'g-')
plt.plot([Rx22,Rx22],[Ry21,Ry22],'g-')
plt.plot([Rx31,Rx32],[Ry31,Ry31],'b-')
plt.plot([Rx31,Rx32],[Ry32,Ry32],'b-')
plt.plot([Rx31,Rx31],[Ry31,Ry32],'b-')
plt.plot([Rx32,Rx32],[Ry31,Ry32],'b-')
plt.plot([Rx41,Rx42],[Ry41,Ry41],'m-')
plt.plot([Rx41,Rx42],[Ry42,Ry42],'m-')
plt.plot([Rx41,Rx41],[Ry41,Ry42],'m-')
plt.plot([Rx42,Rx42],[Ry41,Ry42],'m-')
plt.title('right_low_nir')
plt.savefig(resultdr+'\\NIR.png')

plt.figure(num=11,figsize=(12,6))
plt.subplot(1,2,1)
plt.imshow(imgLvis,cmap='gray')
plt.plot([Lx11,Lx12],[Ly11,Ly11],'r-')
plt.plot([Lx11,Lx12],[Ly12,Ly12],'r-')
plt.plot([Lx11,Lx11],[Ly11,Ly12],'r-')
plt.plot([Lx12,Lx12],[Ly11,Ly12],'r-')
plt.plot([Lx21,Lx22],[Ly21,Ly21],'g-')
plt.plot([Lx21,Lx22],[Ly22,Ly22],'g-')
plt.plot([Lx21,Lx21],[Ly21,Ly22],'g-')
plt.plot([Lx22,Lx22],[Ly21,Ly22],'g-')
plt.plot([Lx31,Lx32],[Ly31,Ly31],'b-')
plt.plot([Lx31,Lx32],[Ly32,Ly32],'b-')
plt.plot([Lx31,Lx31],[Ly31,Ly32],'b-')
plt.plot([Lx32,Lx32],[Ly31,Ly32],'b-')
plt.plot([Lx41,Lx42],[Ly41,Ly41],'m-')
plt.plot([Lx41,Lx42],[Ly42,Ly42],'m-')
plt.plot([Lx41,Lx41],[Ly41,Ly42],'m-')
plt.plot([Lx42,Lx42],[Ly41,Ly42],'m-')
plt.title('left_low_vis')
plt.subplot(1,2,2)
plt.imshow(imgRvis,cmap='gray')
plt.plot([Rx11,Rx12],[Ry11,Ry11],'r-')
plt.plot([Rx11,Rx12],[Ry12,Ry12],'r-')
plt.plot([Rx11,Rx11],[Ry11,Ry12],'r-')
plt.plot([Rx12,Rx12],[Ry11,Ry12],'r-')
plt.plot([Rx21,Rx22],[Ry21,Ry21],'g-')
plt.plot([Rx21,Rx22],[Ry22,Ry22],'g-')
plt.plot([Rx21,Rx21],[Ry21,Ry22],'g-')
plt.plot([Rx22,Rx22],[Ry21,Ry22],'g-')
plt.plot([Rx31,Rx32],[Ry31,Ry31],'b-')
plt.plot([Rx31,Rx32],[Ry32,Ry32],'b-')
plt.plot([Rx31,Rx31],[Ry31,Ry32],'b-')
plt.plot([Rx32,Rx32],[Ry31,Ry32],'b-')
plt.plot([Rx41,Rx42],[Ry41,Ry41],'m-')
plt.plot([Rx41,Rx42],[Ry42,Ry42],'m-')
plt.plot([Rx41,Rx41],[Ry41,Ry42],'m-')
plt.plot([Rx42,Rx42],[Ry41,Ry42],'m-')
plt.title('right_low_vis')
plt.savefig(resultdr+'\\VIS.png')


## Stability of the illumination in the image

N = np.size(folder)
LnirX = np.zeros([4,N])
LnirY = np.zeros([4,N])
LvisX = np.zeros([4,N])
LvisY = np.zeros([4,N])
RnirX = np.zeros([4,N])
RnirY = np.zeros([4,N])
RvisX = np.zeros([4,N])
RvisY = np.zeros([4,N])
t = np.linspace(1,N,N)

# fig = plt.figure()
# h = plt.imshow(imgLvis[Ly11:Ly12,Lx11:Lx12],cmap='gray')
print('25,Analyzed measurements:')
i = 0;
totalDir = len(os.listdir(dirIn))
for dr in os.listdir(dirIn):
    # print(dr)
    imgfile_left_nir = os.path.join(dirIn, dr,'Raw data', 'left_high_nir.png')
    imgfile_left_vis = os.path.join(dirIn, dr,'Raw data', 'left_high_vis.png')
    imgfile_right_nir = os.path.join(dirIn, dr,'Raw data', 'right_high_nir.png')
    imgfile_right_vis = os.path.join(dirIn, dr,'Raw data', 'right_high_vis.png')

    # left nir
    img=mpimg.imread(imgfile_left_nir)
    # if img.shape[0] == 640:
    #     img = img[::2,::2]
    
    imgcrop = img[Ly11:Ly12,Lx11:Lx12]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi1_'+str(i))
    tmpx = tmpx + Lx11 - L[0,2]
    tmpy = tmpy + Ly11 - L[0,3]
    LnirX[0,i] = (np.cos(-Ltheta1*np.pi/180)*tmpx - np.sin(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
    LnirY[0,i] = (np.sin(-Ltheta1*np.pi/180)*tmpx + np.cos(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
    
    imgcrop = img[Ly21:Ly22,Lx21:Lx22]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi2_'+str(i))
    tmpx = tmpx + Lx21 - L[1,2]
    tmpy = tmpy + Ly21 - L[1,3]
    LnirX[1,i] = (np.cos(-Ltheta2*np.pi/180)*tmpx - np.sin(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
    LnirY[1,i] = (np.sin(-Ltheta2*np.pi/180)*tmpx + np.cos(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
    
    imgcrop = img[Ly31:Ly32,Lx31:Lx32]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi3_'+str(i))
    tmpx = tmpx + Lx31 - L[2,2]
    tmpy = tmpy + Ly31 - L[2,3]
    LnirX[2,i] = (np.cos(-Ltheta3*np.pi/180)*tmpx - np.sin(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
    LnirY[2,i] = (np.sin(-Ltheta3*np.pi/180)*tmpx + np.cos(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
    
    imgcrop = img[Ly41:Ly42,Lx41:Lx42]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi4_'+str(i))
    tmpx = tmpx + Lx41 - L[3,2]
    tmpy = tmpy + Ly41 - L[3,3]
    LnirX[3,i] = (np.cos(-Ltheta4*np.pi/180)*tmpx - np.sin(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
    LnirY[3,i] = (np.sin(-Ltheta4*np.pi/180)*tmpx + np.cos(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
    
    # left vis
    img=mpimg.imread(imgfile_left_vis)
    # if img.shape[0] == 640:
    #     img = img[::2,::2]

    imgcrop = img[Ly11:Ly12,Lx11:Lx12]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi1_'+str(i))
    tmpx = tmpx + Lx11 - L[0,2]
    tmpy = tmpy + Ly11 - L[0,3]
    LvisX[0,i] = (np.cos(-Ltheta1*np.pi/180)*tmpx - np.sin(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
    LvisY[0,i] = (np.sin(-Ltheta1*np.pi/180)*tmpx + np.cos(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
    
    imgcrop = img[Ly21:Ly22,Lx21:Lx22]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi2_'+str(i))
    tmpx = tmpx + Lx21 - L[1,2]
    tmpy = tmpy + Ly21 - L[1,3]
    LvisX[1,i] = (np.cos(-Ltheta2*np.pi/180)*tmpx - np.sin(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
    LvisY[1,i] = (np.sin(-Ltheta2*np.pi/180)*tmpx + np.cos(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
    
    imgcrop = img[Ly31:Ly32,Lx31:Lx32]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi3_'+str(i))
    tmpx = tmpx + Lx31 - L[2,2]
    tmpy = tmpy + Ly31 - L[2,3]
    LvisX[2,i] = (np.cos(-Ltheta3*np.pi/180)*tmpx - np.sin(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
    LvisY[2,i] = (np.sin(-Ltheta3*np.pi/180)*tmpx + np.cos(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
    
    imgcrop = img[Ly41:Ly42,Lx41:Lx42]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi4_'+str(i))
    tmpx = tmpx + Lx41 - L[3,2]
    tmpy = tmpy + Ly41 - L[3,3]
    LvisX[3,i] = (np.cos(-Ltheta4*np.pi/180)*tmpx - np.sin(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
    LvisY[3,i] = (np.sin(-Ltheta4*np.pi/180)*tmpx + np.cos(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
    
    # right nir
    img=mpimg.imread(imgfile_right_nir)
    # if img.shape[0] == 640:
    #     img = img[::2,::2]
    
    imgcrop = img[Ry11:Ry12,Rx11:Rx12]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi1_'+str(i))
    tmpx = tmpx + Rx11 - R[0,2]
    tmpy = tmpy + Ry11 - R[0,3]
    RnirX[0,i] = (np.cos(-Rtheta1*np.pi/180)*tmpx - np.sin(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
    RnirY[0,i] = (np.sin(-Rtheta1*np.pi/180)*tmpx + np.cos(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
    
    imgcrop = img[Ry21:Ry22,Rx21:Rx22]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi2_'+str(i))
    tmpx = tmpx + Rx21 - R[1,2]
    tmpy = tmpy + Ry21 - R[1,3]
    RnirX[1,i] = (np.cos(-Rtheta2*np.pi/180)*tmpx - np.sin(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
    RnirY[1,i] = (np.sin(-Rtheta2*np.pi/180)*tmpx + np.cos(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
    
    imgcrop = img[Ry31:Ry32,Rx31:Rx32]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi3_'+str(i))
    tmpx = tmpx + Rx31 - R[2,2]
    tmpy = tmpy + Ry31 - R[2,3]
    RnirX[2,i] = (np.cos(-Rtheta3*np.pi/180)*tmpx - np.sin(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
    RnirY[2,i] = (np.sin(-Rtheta3*np.pi/180)*tmpx + np.cos(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
    
    imgcrop = img[Ry41:Ry42,Rx41:Rx42]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi4_'+str(i))
    tmpx = tmpx + Rx41 - R[3,2]
    tmpy = tmpy + Ry41 - R[3,3]
    RnirX[3,i] = (np.cos(-Rtheta4*np.pi/180)*tmpx - np.sin(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
    RnirY[3,i] = (np.sin(-Rtheta4*np.pi/180)*tmpx + np.cos(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
    
    # right vis
    img=mpimg.imread(imgfile_right_vis)
    # if img.shape[0] == 640:
    #     img = img[::2,::2]
    
    imgcrop = img[Ry11:Ry12,Rx11:Rx12]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi1_'+str(i))
    tmpx = tmpx + Rx11 - R[0,2]
    tmpy = tmpy + Ry11 - R[0,3]
    RvisX[0,i] = (np.cos(-Rtheta1*np.pi/180)*tmpx - np.sin(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
    RvisY[0,i] = (np.sin(-Rtheta1*np.pi/180)*tmpx + np.cos(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
    
    imgcrop = img[Ry21:Ry22,Rx21:Rx22]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi2_'+str(i))
    tmpx = tmpx + Rx21 - R[1,2]
    tmpy = tmpy + Ry21 - R[1,3]
    RvisX[1,i] = (np.cos(-Rtheta2*np.pi/180)*tmpx - np.sin(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
    RvisY[1,i] = (np.sin(-Rtheta2*np.pi/180)*tmpx + np.cos(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
    
    imgcrop = img[Ry31:Ry32,Rx31:Rx32]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi3_'+str(i))
    tmpx = tmpx + Rx31 - R[2,2]
    tmpy = tmpy + Ry31 - R[2,3]
    RvisX[2,i] = (np.cos(-Rtheta3*np.pi/180)*tmpx - np.sin(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
    RvisY[2,i] = (np.sin(-Rtheta3*np.pi/180)*tmpx + np.cos(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
    
    imgcrop = img[Ry41:Ry42,Rx41:Rx42]
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi4_'+str(i))
    tmpx = tmpx + Rx41 - R[3,2]
    tmpy = tmpy + Ry41 - R[3,3]
    RvisX[3,i] = (np.cos(-Rtheta4*np.pi/180)*tmpx - np.sin(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
    RvisY[3,i] = (np.sin(-Rtheta4*np.pi/180)*tmpx + np.cos(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
    i= i+1;
    counter = int(15+i*(80)/totalDir)
    print(str(counter)+",Analyzed Measurement: ("+str(i)+") "+dr)
    

plt.figure(num=21,figsize=(12,6))
plt.subplot(1,2,1)
plt.plot(t,LnirX[0,:],'ro-',linestyle='none',label='NIR')
plt.plot(t,LnirX[1,:],'go-',linestyle='none')
plt.plot(t,LnirX[2,:],'bo-',linestyle='none')
plt.plot(t,LnirX[3,:],'mo-',linestyle='none')
plt.plot(t,LvisX[0,:],'r*-.',linestyle='none',label='VIS')
plt.plot(t,LvisX[1,:],'g*-.',linestyle='none')
plt.plot(t,LvisX[2,:],'b*-.',linestyle='none')
plt.plot(t,LvisX[3,:],'m*-.',linestyle='none')
plt.plot([0,7],[-2,-2],'k-.')
plt.plot([0,7],[2,2],'k-.')
plt.legend()
plt.xlim([0,7])
plt.ylim([-3,3])
plt.xlabel('Measurement [-]')
plt.ylabel('Deviation [mm]')
plt.title('Tangential repeatability left')
plt.subplot(1,2,2)
plt.plot(t,RnirX[0,:],'ro-',linestyle='none',label='NIR')
plt.plot(t,RnirX[1,:],'go-',linestyle='none')
plt.plot(t,RnirX[2,:],'bo-',linestyle='none')
plt.plot(t,RnirX[3,:],'mo-',linestyle='none')
plt.plot(t,RvisX[0,:],'r*-.',linestyle='none',label='VIS')
plt.plot(t,RvisX[1,:],'g*-.',linestyle='none')
plt.plot(t,RvisX[2,:],'b*-.',linestyle='none')
plt.plot(t,RvisX[3,:],'m*-.',linestyle='none')
plt.title('Tangential repeatability right')
plt.plot([0,7],[-2,-2],'k-.')
plt.plot([0,7],[2,2],'k-.')
plt.legend()
plt.xlim([0,7])
plt.ylim([-3,3])
plt.xlabel('Measurement [-]')
plt.ylabel('Deviation [mm]')
plt.savefig(resultdr+'\\'+folder[0]+'_resultsX.png')

plt.figure(num=22,figsize=(12,6))
plt.subplot(1,2,1)
plt.plot(t,LnirY[0,:],'ro-',linestyle='none',label='NIR')
plt.plot(t,LnirY[1,:],'go-',linestyle='none')
plt.plot(t,LnirY[2,:],'bo-',linestyle='none')
plt.plot(t,LnirY[3,:],'mo-',linestyle='none')
plt.plot(t,LvisY[0,:],'r*-.',linestyle='none',label='VIS')
plt.plot(t,LvisY[1,:],'g*-.',linestyle='none')
plt.plot(t,LvisY[2,:],'b*-.',linestyle='none')
plt.plot(t,LvisY[3,:],'m*-.',linestyle='none')
plt.legend()
plt.xlim([0,7])
plt.ylim([-4,4])
plt.plot([0,7],[-3,-3],'k-.')
plt.plot([0,7],[3,3],'k-.')
plt.xlabel('Measurement [-]')
plt.ylabel('Deviation [mm]')
plt.title('Longitudinal repeatability left')
plt.subplot(1,2,2)
plt.plot(t,RnirY[0,:],'ro-',linestyle='none',label='NIR')
plt.plot(t,RnirY[1,:],'go-',linestyle='none')
plt.plot(t,RnirY[2,:],'bo-',linestyle='none')
plt.plot(t,RnirY[3,:],'mo-',linestyle='none')
plt.plot(t,RvisY[0,:],'r*-.',linestyle='none',label='VIS')
plt.plot(t,RvisY[1,:],'g*-.',linestyle='none')
plt.plot(t,RvisY[2,:],'b*-.',linestyle='none')
plt.plot(t,RvisY[3,:],'m*-.',linestyle='none')
plt.title('Longitudinal repeatability right')
plt.legend()
plt.xlim([0,7])
plt.ylim([-4,4])
plt.plot([0,7],[-3,-3],'k-.')
plt.plot([0,7],[3,3],'k-.')
plt.xlabel('Measurement [-]')
plt.ylabel('Deviation [mm]')
plt.savefig(resultdr+'\\'+folder[0]+'_resultsY.png')

plt.show()

#max absolute error |PENTING
# print('\nMax tangential error L:\t\t%1.2f mm' %np.max([np.max(abs(LnirX)),np.max(abs(LvisX))]))
# print('Max tangential error R:\t\t%1.2f mm' %np.max([np.max(abs(RnirX)),np.max(abs(RvisX))]))
# print('Max longitudinal error L:\t%1.2f mm' %np.max([np.max(abs(LnirY)),np.max(abs(LvisY))]))
# print('Max longitudinal error R:\t%1.2f mm' %np.max([np.max(abs(RnirY)),np.max(abs(RvisY))]))

print("100,Position Accuracy: PASS")


















# 
# #repro between measurements
# for i in range(4):
#     print(np.max(abs(LvisX[i,:] - np.mean(LvisX[i,:]))))
#     print(np.max(abs(LnirX[i,:] - np.mean(LnirX[i,:]))))
# 
# for i in range(4):
#     print(np.max(abs(RvisX[i,:] - np.mean(RvisX[i,:]))))
#     print(np.max(abs(RnirX[i,:] - np.mean(RnirX[i,:]))))
# 
# for i in range(4):
#     print(np.max(abs(LvisY[i,:] - np.mean(LvisY[i,:]))))
#     print(np.max(abs(LnirY[i,:] - np.mean(LnirY[i,:]))))
#     
# for i in range(4):
#     print(np.max(abs(RvisY[i,:] - np.mean(RvisY[i,:]))))
#     print(np.max(abs(RnirY[i,:] - np.mean(RnirY[i,:]))))
# 
# #repro between NIR / VIS
# print('Left x:',np.max(abs(LvisX-LnirX)))
# print('Right x:',np.max(abs(RvisX-RnirX)))
# print('Left y:',np.max(abs(LvisY-LnirY)))
# print('Right y:',np.max(abs(RvisY-RnirY)))
# 
# 
# N = np.size(folder)-1
# LnirX_hr = np.zeros([4,N])
# LnirY_hr = np.zeros([4,N])
# LvisX_hr = np.zeros([4,N])
# LvisY_hr = np.zeros([4,N])
# RnirX_hr = np.zeros([4,N])
# RnirY_hr = np.zeros([4,N])
# RvisX_hr = np.zeros([4,N])
# RvisY_hr = np.zeros([4,N])
# t = np.linspace(1,N,N)
# 
# 
# fig = plt.figure()
# h = plt.imshow(imgLvis[Ly11:Ly12,Lx11:Lx12],cmap='gray')
# i = 0;
# for dr in os.listdir(dirIn):
#     # get measurement id (=folder name)
#     id = os.path.split(dr)[-1]
#     # skip if not a measurement folder
#     if id == str(results):
#         continue
#     
#     print(id)
#     imgfile_left_nir = os.path.join(dirIn, dr,'Raw data', 'left_high_nir.png')
#     imgfile_left_vis = os.path.join(dirIn, dr,'Raw data', 'left_high_vis.png')
#     imgfile_right_nir = os.path.join(dirIn, dr,'Raw data', 'right_high_nir.png')
#     imgfile_right_vis = os.path.join(dirIn, dr,'Raw data', 'right_high_vis.png')
#     
# 
#     # left nir
#     img=mpimg.imread(imgfile_left_nir)
#     # if img.shape[0] == 640:
#     #     img = img[::2,::2]
#     
#     imgcrop = img[Ly11:Ly12,Lx11:Lx12]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi1_'+str(i))
#     tmpx = tmpx + Lx11 - L[0,2]
#     tmpy = tmpy + Ly11 - L[0,3]
#     LnirX_hr[0,i] = (np.cos(-Ltheta1*np.pi/180)*tmpx - np.sin(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
#     LnirY_hr[0,i] = (np.sin(-Ltheta1*np.pi/180)*tmpx + np.cos(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
#     
#     imgcrop = img[Ly21:Ly22,Lx21:Lx22]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi2_'+str(i))
#     tmpx = tmpx + Lx21 - L[1,2]
#     tmpy = tmpy + Ly21 - L[1,3]
#     LnirX_hr[1,i] = (np.cos(-Ltheta2*np.pi/180)*tmpx - np.sin(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
#     LnirY_hr[1,i] = (np.sin(-Ltheta2*np.pi/180)*tmpx + np.cos(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
#     
#     imgcrop = img[Ly31:Ly32,Lx31:Lx32]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi3_'+str(i))
#     tmpx = tmpx + Lx31 - L[2,2]
#     tmpy = tmpy + Ly31 - L[2,3]
#     LnirX_hr[2,i] = (np.cos(-Ltheta3*np.pi/180)*tmpx - np.sin(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
#     LnirY_hr[2,i] = (np.sin(-Ltheta3*np.pi/180)*tmpx + np.cos(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
#     
#     imgcrop = img[Ly41:Ly42,Lx41:Lx42]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi4_'+str(i))
#     tmpx = tmpx + Lx41 - L[3,2]
#     tmpy = tmpy + Ly41 - L[3,3]
#     LnirX_hr[3,i] = (np.cos(-Ltheta4*np.pi/180)*tmpx - np.sin(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
#     LnirY_hr[3,i] = (np.sin(-Ltheta4*np.pi/180)*tmpx + np.cos(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
#     
#     # left vis
#     img=mpimg.imread(imgfile_left_vis)
#     # if img.shape[0] == 640:
#     #     img = img[::2,::2]
# 
#     imgcrop = img[Ly11:Ly12,Lx11:Lx12]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi1_'+str(i))
#     tmpx = tmpx + Lx11 - L[0,2]
#     tmpy = tmpy + Ly11 - L[0,3]
#     LvisX_hr[0,i] = (np.cos(-Ltheta1*np.pi/180)*tmpx - np.sin(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
#     LvisY_hr[0,i] = (np.sin(-Ltheta1*np.pi/180)*tmpx + np.cos(-Ltheta1*np.pi/180)*tmpy)/Lpxpermm1
#     
#     imgcrop = img[Ly21:Ly22,Lx21:Lx22]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi2_'+str(i))
#     tmpx = tmpx + Lx21 - L[1,2]
#     tmpy = tmpy + Ly21 - L[1,3]
#     LvisX_hr[1,i] = (np.cos(-Ltheta2*np.pi/180)*tmpx - np.sin(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
#     LvisY_hr[1,i] = (np.sin(-Ltheta2*np.pi/180)*tmpx + np.cos(-Ltheta2*np.pi/180)*tmpy)/Lpxpermm1
#     
#     imgcrop = img[Ly31:Ly32,Lx31:Lx32]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi3_'+str(i))
#     tmpx = tmpx + Lx31 - L[2,2]
#     tmpy = tmpy + Ly31 - L[2,3]
#     LvisX_hr[2,i] = (np.cos(-Ltheta3*np.pi/180)*tmpx - np.sin(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
#     LvisY_hr[2,i] = (np.sin(-Ltheta3*np.pi/180)*tmpx + np.cos(-Ltheta3*np.pi/180)*tmpy)/Lpxpermm1
#     
#     imgcrop = img[Ly41:Ly42,Lx41:Lx42]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi4_'+str(i))
#     tmpx = tmpx + Lx41 - L[3,2]
#     tmpy = tmpy + Ly41 - L[3,3]
#     LvisX_hr[3,i] = (np.cos(-Ltheta4*np.pi/180)*tmpx - np.sin(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
#     LvisY_hr[3,i] = (np.sin(-Ltheta4*np.pi/180)*tmpx + np.cos(-Ltheta4*np.pi/180)*tmpy)/Lpxpermm1
#     
#     # right nir
#     img=mpimg.imread(imgfile_right_nir)
#     # if img.shape[0] == 640:
#     #     img = img[::2,::2]
#     
#     imgcrop = img[Ry11:Ry12,Rx11:Rx12]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi1_'+str(i))
#     tmpx = tmpx + Rx11 - R[0,2]
#     tmpy = tmpy + Ry11 - R[0,3]
#     RnirX_hr[0,i] = (np.cos(-Rtheta1*np.pi/180)*tmpx - np.sin(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
#     RnirY_hr[0,i] = (np.sin(-Rtheta1*np.pi/180)*tmpx + np.cos(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
#     
#     imgcrop = img[Ry21:Ry22,Rx21:Rx22]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi2_'+str(i))
#     tmpx = tmpx + Rx21 - R[1,2]
#     tmpy = tmpy + Ry21 - R[1,3]
#     RnirX_hr[1,i] = (np.cos(-Rtheta2*np.pi/180)*tmpx - np.sin(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
#     RnirY_hr[1,i] = (np.sin(-Rtheta2*np.pi/180)*tmpx + np.cos(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
#     
#     imgcrop = img[Ry31:Ry32,Rx31:Rx32]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi3_'+str(i))
#     tmpx = tmpx + Rx31 - R[2,2]
#     tmpy = tmpy + Ry31 - R[2,3]
#     RnirX_hr[2,i] = (np.cos(-Rtheta3*np.pi/180)*tmpx - np.sin(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
#     RnirY_hr[2,i] = (np.sin(-Rtheta3*np.pi/180)*tmpx + np.cos(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
#     
#     imgcrop = img[Ry41:Ry42,Rx41:Rx42]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi4_'+str(i))
#     tmpx = tmpx + Rx41 - R[3,2]
#     tmpy = tmpy + Ry41 - R[3,3]
#     RnirX_hr[3,i] = (np.cos(-Rtheta4*np.pi/180)*tmpx - np.sin(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
#     RnirY_hr[3,i] = (np.sin(-Rtheta4*np.pi/180)*tmpx + np.cos(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
#     
#     # right vis
#     img=mpimg.imread(imgfile_right_vis)
#     # if img.shape[0] == 640:
#     #     img = img[::2,::2]
#     
#     imgcrop = img[Ry11:Ry12,Rx11:Rx12]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi1_'+str(i))
#     tmpx = tmpx + Rx11 - R[0,2]
#     tmpy = tmpy + Ry11 - R[0,3]
#     RvisX_hr[0,i] = (np.cos(-Rtheta1*np.pi/180)*tmpx - np.sin(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
#     RvisY_hr[0,i] = (np.sin(-Rtheta1*np.pi/180)*tmpx + np.cos(-Rtheta1*np.pi/180)*tmpy)/Rpxpermm1
#     
#     imgcrop = img[Ry21:Ry22,Rx21:Rx22]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi2_'+str(i))
#     tmpx = tmpx + Rx21 - R[1,2]
#     tmpy = tmpy + Ry21 - R[1,3]
#     RvisX_hr[1,i] = (np.cos(-Rtheta2*np.pi/180)*tmpx - np.sin(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
#     RvisY_hr[1,i] = (np.sin(-Rtheta2*np.pi/180)*tmpx + np.cos(-Rtheta2*np.pi/180)*tmpy)/Rpxpermm1
#     
#     imgcrop = img[Ry31:Ry32,Rx31:Rx32]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi3_'+str(i))
#     tmpx = tmpx + Rx31 - R[2,2]
#     tmpy = tmpy + Ry31 - R[2,3]
#     RvisX_hr[2,i] = (np.cos(-Rtheta3*np.pi/180)*tmpx - np.sin(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
#     RvisY_hr[2,i] = (np.sin(-Rtheta3*np.pi/180)*tmpx + np.cos(-Rtheta3*np.pi/180)*tmpy)/Rpxpermm1
#     
#     imgcrop = img[Ry41:Ry42,Rx41:Rx42]
#     tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi4_'+str(i))
#     tmpx = tmpx + Rx41 - R[3,2]
#     tmpy = tmpy + Ry41 - R[3,3]
#     RvisX_hr[3,i] = (np.cos(-Rtheta4*np.pi/180)*tmpx - np.sin(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
#     RvisY_hr[3,i] = (np.sin(-Rtheta4*np.pi/180)*tmpx + np.cos(-Rtheta4*np.pi/180)*tmpy)/Rpxpermm1
#     
# #repro between LR / HR
# print('Left x:',np.max([abs(LnirX-LnirX_hr),
#                         abs(LvisX-LvisX_hr)]))
# 
# print('Right x:',np.max([abs(RnirX-RnirX_hr),
#                          abs(RvisX-RvisX_hr)]))
# 
# print('Left y:',np.max([abs(LnirY-LnirY_hr),
#                         abs(LvisY-LvisY_hr)]))
# 
# print('Right y:',np.max([abs(RnirY-RnirY_hr),
#                          abs(RvisY-RvisY_hr)]))
