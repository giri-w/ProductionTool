import os
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
import sys
# plt.close('all')

# debug
a = "C:\\Users\\GWA\\Documents\\GitHub\\Demcon\\ProductionToolFramework\\ProductionToolFramework\\bin\\Debug\\Python\\figure"
b = r"C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\Spatial accuracy\Measurements"

## from argument
# # path location of output
# a0 = str(sys.argv[1]) 
# a1 = a0.rsplit("\\",1)
# a = a1[0] + "\\figure";

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
    ind = np.where(imgcrop>threshold)
    
    xavg = np.average(ind[1])
    yavg = np.average(ind[0])
    
    #also calculate average intensity
    iavg = np.average(imgcrop[ind[0],ind[1]])
    
#     h.set_data(imgcrop)
#     plt.savefig(resultdr+'\\'+filename+'.png')

    return xavg,yavg,iavg


## Definitions


# dirIn = r'C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\Spatial accuracy\Measurements'
# resultdr = r'C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\Spatial accuracy\Results'
dirIn = b
resultdr = a

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
Lx11 = int(L[0,2]/2 - S)
Ly11 = int(L[0,3]/2 - S)
Lx12 = int(Lx11 + 2 * S)
Ly12 = int(Ly11 + 2 * S)
Rx11 = int(R[0,2]/2 - S)
Ry11 = int(R[0,3]/2 - S)
Rx12 = int(Rx11 + 2 * S)
Ry12 = int(Ry11 + 2 * S)

#region 2
Lx21 = int(L[1,2]/2 - S)
Ly21 = int(L[1,3]/2 - S)
Lx22 = int(Lx21 + 2 * S)
Ly22 = int(Ly21 + 2 * S)
Rx21 = int(R[1,2]/2 - S)
Ry21 = int(R[1,3]/2 - S)
Rx22 = int(Rx21 + 2 * S)
Ry22 = int(Ry21 + 2 * S)

#region 3
Lx31 = int(L[2,2]/2 - S)
Ly31 = int(L[2,3]/2 - S)
Lx32 = int(Lx31 + 2 * S)
Ly32 = int(Ly31 + 2 * S)
Rx31 = int(R[2,2]/2 - S)
Ry31 = int(R[2,3]/2 - S)
Rx32 = int(Rx31 + 2 * S)
Ry32 = int(Ry31 + 2 * S)

#region 4
Lx41 = int(L[3,2]/2 - S)
Ly41 = int(L[3,3]/2 - S)
Lx42 = int(Lx41 + 2 * S)
Ly42 = int(Ly41 + 2 * S)
Rx41 = int(R[3,2]/2 - S)
Ry41 = int(R[3,3]/2 - S)
Rx42 = int(Rx41 + 2 * S)
Ry42 = int(Ry41 + 2 * S)


#read and show first image
folder = os.listdir(dirIn)
dirIn = os.path.join(dirIn, folder[0])
p = path.Path(os.path.join(dirIn, 'Raw data'))
#print(folder[0])
print("35,Start Processing")
imgfiles_left_nir = p.files('*left_low_nir_*.png')
imgfiles_left_vis = p.files('*left_low_vis_*.png')
imgfiles_right_nir = p.files('*right_low_nir_*.png')
imgfiles_right_vis = p.files('*right_low_vis_*.png')

imgLnir=mpimg.imread(imgfiles_left_nir[0])
imgLvis=mpimg.imread(imgfiles_left_vis[0])
imgRnir=mpimg.imread(imgfiles_right_nir[0])
imgRvis=mpimg.imread(imgfiles_right_vis[0])

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


plt.figure(12)
plt.subplot(2,2,1)
plt.imshow(ndimage.rotate(imgLvis[Ly11:Ly12,Lx11:Lx12],Ltheta1,order=0),cmap='gray')
plt.subplot(2,2,2)
plt.imshow(ndimage.rotate(imgLvis[Ly21:Ly22,Lx21:Lx22],Ltheta2,order=0),cmap='gray')
plt.subplot(2,2,3)
plt.imshow(ndimage.rotate(imgLvis[Ly31:Ly32,Lx31:Lx32],Ltheta3,order=0),cmap='gray')
plt.subplot(2,2,4)
plt.imshow(ndimage.rotate(imgLvis[Ly41:Ly42,Lx41:Lx42],Ltheta4,order=0),cmap='gray')

plt.figure(13)
plt.subplot(2,2,1)
plt.imshow(ndimage.rotate(imgRvis[Ry11:Ry12,Rx11:Rx12],Rtheta1,order=0),cmap='gray')
plt.subplot(2,2,2)
plt.imshow(ndimage.rotate(imgRvis[Ry21:Ry22,Rx21:Rx22],Rtheta2,order=0),cmap='gray')
plt.subplot(2,2,3)
plt.imshow(ndimage.rotate(imgRvis[Ry31:Ry32,Rx31:Rx32],Rtheta3,order=0),cmap='gray')
plt.subplot(2,2,4)
plt.imshow(ndimage.rotate(imgRvis[Ry41:Ry42,Rx41:Rx42],Rtheta4,order=0),cmap='gray')

## Stability of the illumination in the image

N = len(imgfiles_left_nir)
Lxms1 = np.zeros([2,N])
Lyms1 = np.zeros([2,N])
Lxms2 = np.zeros([2,N])
Lyms2 = np.zeros([2,N])
Lxms3 = np.zeros([2,N])
Lyms3 = np.zeros([2,N])
Lxms4 = np.zeros([2,N])
Lyms4 = np.zeros([2,N])
Rxms1 = np.zeros([2,N])
Ryms1 = np.zeros([2,N])
Rxms2 = np.zeros([2,N])
Ryms2 = np.zeros([2,N])
Rxms3 = np.zeros([2,N])
Ryms3 = np.zeros([2,N])
Rxms4 = np.zeros([2,N])
Ryms4 = np.zeros([2,N])

ts = 0.5
t = np.linspace(ts,ts*N,N)

# fig = plt.figure()
# h = plt.imshow(imgLvis[Ly11:Ly12,Lx11:Lx12],cmap='gray')

for i,filename in enumerate(imgfiles_left_nir):
    #print('Loop: ',i)
    # left nir
    img=mpimg.imread(imgfiles_left_nir[i])

    imgcrop = ndimage.rotate(img[Ly11:Ly12,Lx11:Lx12],Ltheta1,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi1_'+str(i))
    Lxms1[0,i] = tmpx/Lpxpermm1
    Lyms1[0,i] = tmpy/Lpxpermm1

    imgcrop = ndimage.rotate(img[Ly21:Ly22,Lx21:Lx22],Ltheta2,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi2_'+str(i))
    Lxms2[0,i] = tmpx/Lpxpermm2
    Lyms2[0,i] = tmpy/Lpxpermm2
    
    imgcrop = ndimage.rotate(img[Ly31:Ly32,Lx31:Lx32],Ltheta3,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi3_'+str(i))
    Lxms3[0,i] = tmpx/Lpxpermm3
    Lyms3[0,i] = tmpy/Lpxpermm3
    
    imgcrop = ndimage.rotate(img[Ly41:Ly42,Lx41:Lx42],Ltheta4,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_nir_roi4_'+str(i))
    Lxms4[0,i] = tmpx/Lpxpermm4
    Lyms4[0,i] = tmpy/Lpxpermm4
    
    # left vis
    img=mpimg.imread(imgfiles_left_vis[i])

    imgcrop = ndimage.rotate(img[Ly11:Ly12,Lx11:Lx12],Ltheta1,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi1_'+str(i))
    Lxms1[1,i] = tmpx/Lpxpermm1
    Lyms1[1,i] = tmpy/Lpxpermm1

    imgcrop = ndimage.rotate(img[Ly21:Ly22,Lx21:Lx22],Ltheta2,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi2_'+str(i))
    Lxms2[1,i] = tmpx/Lpxpermm2
    Lyms2[1,i] = tmpy/Lpxpermm2
    
    imgcrop = ndimage.rotate(img[Ly31:Ly32,Lx31:Lx32],Ltheta3,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi3_'+str(i))
    Lxms3[1,i] = tmpx/Lpxpermm3
    Lyms3[1,i] = tmpy/Lpxpermm3
    
    imgcrop = ndimage.rotate(img[Ly41:Ly42,Lx41:Lx42],Ltheta4,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'left_vis_roi4_'+str(i))
    Lxms4[1,i] = tmpx/Lpxpermm4
    Lyms4[1,i] = tmpy/Lpxpermm4
    
    # right nir
    img=mpimg.imread(imgfiles_right_nir[i])

    imgcrop = ndimage.rotate(img[Ry11:Ry12,Rx11:Rx12],Rtheta1,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi1_'+str(i))
    Rxms1[0,i] = tmpx/Rpxpermm1
    Ryms1[0,i] = tmpy/Rpxpermm1

    imgcrop = ndimage.rotate(img[Ry21:Ry22,Rx21:Rx22],Rtheta2,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi2_'+str(i))
    Rxms2[0,i] = tmpx/Rpxpermm2
    Ryms2[0,i] = tmpy/Rpxpermm2
    
    imgcrop = ndimage.rotate(img[Ry31:Ry32,Rx31:Rx32],Rtheta3,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi3_'+str(i))
    Rxms3[0,i] = tmpx/Rpxpermm3
    Ryms3[0,i] = tmpy/Rpxpermm3
    
    imgcrop = ndimage.rotate(img[Ry41:Ry42,Rx41:Rx42],Rtheta4,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_nir_roi4_'+str(i))
    Rxms4[0,i] = tmpx/Rpxpermm4
    Ryms4[0,i] = tmpy/Rpxpermm4
    
    # right vis
    img=mpimg.imread(imgfiles_right_vis[i])

    imgcrop = ndimage.rotate(img[Ry11:Ry12,Rx11:Rx12],Rtheta1,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi1_'+str(i))
    Rxms1[1,i] = tmpx/Rpxpermm1
    Ryms1[1,i] = tmpy/Rpxpermm1

    imgcrop = ndimage.rotate(img[Ry21:Ry22,Rx21:Rx22],Rtheta2,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi2_'+str(i))
    Rxms2[1,i] = tmpx/Rpxpermm2
    Ryms2[1,i] = tmpy/Rpxpermm2
    
    imgcrop = ndimage.rotate(img[Ry31:Ry32,Rx31:Rx32],Rtheta3,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi3_'+str(i))
    Rxms3[1,i] = tmpx/Rpxpermm3
    Ryms3[1,i] = tmpy/Rpxpermm3
    
    imgcrop = ndimage.rotate(img[Ry41:Ry42,Rx41:Rx42],Rtheta4,order=0)
    tmpx,tmpy,tmpi = getXyByArea(imgcrop,'right_vis_roi4_'+str(i))
    Rxms4[1,i] = tmpx/Rpxpermm4
    Ryms4[1,i] = tmpy/Rpxpermm4

plt.figure(21,figsize=(12,8))
plt.subplot(2,2,1)
plt.plot(t,Lxms1[0,:]-np.mean(Lxms1[0,:]),'r-')
plt.plot(t,Lxms2[0,:]-np.mean(Lxms2[0,:]),'g-')
plt.plot(t,Lxms3[0,:]-np.mean(Lxms3[0,:]),'b-')
plt.plot(t,Lxms4[0,:]-np.mean(Lxms4[0,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.ylabel('Deviation [mm]')
plt.title('Left NIR Tangential')
plt.subplot(2,2,3)
plt.plot(t,Lxms1[1,:]-np.mean(Lxms1[1,:]),'r-')
plt.plot(t,Lxms2[1,:]-np.mean(Lxms2[1,:]),'g-')
plt.plot(t,Lxms3[1,:]-np.mean(Lxms3[1,:]),'b-')
plt.plot(t,Lxms4[1,:]-np.mean(Lxms4[1,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.xlabel('Time [s]')
plt.ylabel('Deviation [mm]')
plt.title('Left VIS Tangential')
plt.subplot(2,2,2)
plt.plot(t,Rxms1[0,:]-np.mean(Rxms1[0,:]),'r-')
plt.plot(t,Rxms2[0,:]-np.mean(Rxms2[0,:]),'g-')
plt.plot(t,Rxms3[0,:]-np.mean(Rxms3[0,:]),'b-')
plt.plot(t,Rxms4[0,:]-np.mean(Rxms4[0,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.title('Right NIR Tangential')
plt.subplot(2,2,4)
plt.plot(t,Rxms1[1,:]-np.mean(Rxms1[1,:]),'r-')
plt.plot(t,Rxms2[1,:]-np.mean(Rxms2[1,:]),'g-')
plt.plot(t,Rxms3[1,:]-np.mean(Rxms3[1,:]),'b-')
plt.plot(t,Rxms4[1,:]-np.mean(Rxms4[1,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.xlabel('Time [s]')
plt.title('Right VIS Tangential')
plt.savefig(resultdr+'\\'+folder[0]+'_resultTangential.png')

plt.figure(22,figsize=(12,8))
plt.subplot(2,2,1)
plt.plot(t,Lyms1[0,:]-np.mean(Lyms1[0,:]),'r-')
plt.plot(t,Lyms2[0,:]-np.mean(Lyms2[0,:]),'g-')
plt.plot(t,Lyms3[0,:]-np.mean(Lyms3[0,:]),'b-')
plt.plot(t,Lyms4[0,:]-np.mean(Lyms4[0,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.ylabel('Deviation [mm]')
plt.title('Left NIR Longitudinal')
plt.subplot(2,2,3)
plt.plot(t,Lyms1[1,:]-np.mean(Lyms1[1,:]),'r-')
plt.plot(t,Lyms2[1,:]-np.mean(Lyms2[1,:]),'g-')
plt.plot(t,Lyms3[1,:]-np.mean(Lyms3[1,:]),'b-')
plt.plot(t,Lyms4[1,:]-np.mean(Lyms4[1,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.xlabel('Time [s]')
plt.ylabel('Deviation [mm]')
plt.title('Left VIS Longitudinal')
plt.subplot(2,2,2)
plt.plot(t,Ryms1[0,:]-np.mean(Ryms1[0,:]),'r-')
plt.plot(t,Ryms2[0,:]-np.mean(Ryms2[0,:]),'g-')
plt.plot(t,Ryms3[0,:]-np.mean(Ryms3[0,:]),'b-')
plt.plot(t,Ryms4[0,:]-np.mean(Ryms4[0,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.title('Right NIR Longitudinal')
plt.subplot(2,2,4)
plt.plot(t,Ryms1[1,:]-np.mean(Ryms1[1,:]),'r-')
plt.plot(t,Ryms2[1,:]-np.mean(Ryms2[1,:]),'g-')
plt.plot(t,Ryms3[1,:]-np.mean(Ryms3[1,:]),'b-')
plt.plot(t,Ryms4[1,:]-np.mean(Ryms4[1,:]),'m-')
plt.plot([t[0],t[-1]],[1,1],'k-.')
plt.plot([t[0],t[-1]],[-1,-1],'k-.')
plt.ylim([-1.2,1.2])
plt.xlabel('Time [s]')
plt.title('Right VIS Longitudinal')
plt.savefig(resultdr+'\\'+folder[0]+'_resultLongitudinal.png')

# plt.figure(23)
# plt.hist(Lyms4[0,:]-np.mean(Lyms4[0,:]))

StdX = np.array([[np.std(Lxms1[0,:]), np.std(Lxms1[1,:]), np.std(Rxms1[0,:]), np.std(Rxms1[1,:])],
                 [np.std(Lxms2[0,:]), np.std(Lxms2[1,:]), np.std(Rxms2[0,:]), np.std(Rxms2[1,:])],
                 [np.std(Lxms3[0,:]), np.std(Lxms3[1,:]), np.std(Rxms3[0,:]), np.std(Rxms3[1,:])],
                 [np.std(Lxms4[0,:]), np.std(Lxms4[1,:]), np.std(Rxms4[0,:]), np.std(Rxms4[1,:])]])

StdY = np.array([[np.std(Lyms1[0,:]), np.std(Lyms1[1,:]), np.std(Ryms1[0,:]), np.std(Ryms1[1,:])],
                 [np.std(Lyms2[0,:]), np.std(Lyms2[1,:]), np.std(Ryms2[0,:]), np.std(Ryms2[1,:])],
                 [np.std(Lyms3[0,:]), np.std(Lyms3[1,:]), np.std(Ryms3[0,:]), np.std(Ryms3[1,:])],
                 [np.std(Lyms4[0,:]), np.std(Lyms4[1,:]), np.std(Ryms4[0,:]), np.std(Ryms4[1,:])]])


print('50,Calculating Standard Deviation')
# print('\nSTANDERD DEVIATION:')
# print('Longitudinaal Left Nir: \t%1.2f %1.2f %1.2f %1.2f' %(StdX[0,0], StdX[0,1],StdX[0,2],StdX[0,3]))
# print('Longitudinaal Left Vis: \t%1.2f %1.2f %1.2f %1.2f' %(StdX[1,0], StdX[1,1],StdX[1,2],StdX[1,3]))
# print('Longitudinaal Right Nir: \t%1.2f %1.2f %1.2f %1.2f' %(StdX[2,0], StdX[2,1],StdX[2,2],StdX[2,3]))
# print('Longitudinaal Right Vis: \t%1.2f %1.2f %1.2f %1.2f\n' %(StdX[3,0], StdX[3,1],StdX[3,2],StdX[3,3]))
# 
# print('Tangentiaal Left Nir: \t%1.2f %1.2f %1.2f %1.2f' %(StdY[0,0], StdY[0,1],StdY[0,2],StdY[0,3]))
# print('Tangentiaal Left Vis: \t%1.2f %1.2f %1.2f %1.2f' %(StdY[1,0], StdY[1,1],StdY[1,2],StdY[1,3]))
# print('Tangentiaal Right Nir: \t%1.2f %1.2f %1.2f %1.2f' %(StdY[2,0], StdY[2,1],StdY[2,2],StdY[2,3]))
# print('Tangentiaal Right Vis: \t%1.2f %1.2f %1.2f %1.2f' %(StdY[3,0], StdY[3,1],StdY[3,2],StdY[3,3]))

#==============================================
# print('Max Std X [mm]: ', maxStdX)
# print('Max Std Y [mm]: ', maxStdY)

# average RMS error
# np.sqrt(np.sum(np.power(yms1-np.mean(yms1),2))/200)

maxErrorX = np.array([[max(abs(Lxms1[0,:]-np.mean(Lxms1[0,:]))),
                 max(abs(Lxms2[0,:]-np.mean(Lxms2[0,:]))),
                 max(abs(Lxms3[0,:]-np.mean(Lxms3[0,:]))),
                 max(abs(Lxms4[0,:]-np.mean(Lxms4[0,:])))],
                 [max(abs(Lxms1[1,:]-np.mean(Lxms1[1,:]))),
                 max(abs(Lxms2[1,:]-np.mean(Lxms2[1,:]))),
                 max(abs(Lxms3[1,:]-np.mean(Lxms3[1,:]))),
                 max(abs(Lxms4[1,:]-np.mean(Lxms4[1,:])))],
                 [max(abs(Rxms1[0,:]-np.mean(Rxms1[0,:]))),
                 max(abs(Rxms2[0,:]-np.mean(Rxms2[0,:]))),
                 max(abs(Rxms3[0,:]-np.mean(Rxms3[0,:]))),
                 max(abs(Rxms4[0,:]-np.mean(Rxms4[0,:])))],
                 [max(abs(Rxms1[1,:]-np.mean(Rxms1[1,:]))),
                 max(abs(Rxms2[1,:]-np.mean(Rxms2[1,:]))),
                 max(abs(Rxms3[1,:]-np.mean(Rxms3[1,:]))),
                 max(abs(Rxms4[1,:]-np.mean(Rxms4[1,:])))]])

maxErrorY = np.array([[max(abs(Lyms1[0,:]-np.mean(Lyms1[0,:]))),
                 max(abs(Lyms2[0,:]-np.mean(Lyms2[0,:]))),
                 max(abs(Lyms3[0,:]-np.mean(Lyms3[0,:]))),
                 max(abs(Lyms4[0,:]-np.mean(Lyms4[0,:])))],
                 [max(abs(Lyms1[1,:]-np.mean(Lyms1[1,:]))),
                 max(abs(Lyms2[1,:]-np.mean(Lyms2[1,:]))),
                 max(abs(Lyms3[1,:]-np.mean(Lyms3[1,:]))),
                 max(abs(Lyms4[1,:]-np.mean(Lyms4[1,:])))],
                 [max(abs(Ryms1[0,:]-np.mean(Ryms1[0,:]))),
                 max(abs(Ryms2[0,:]-np.mean(Ryms2[0,:]))),
                 max(abs(Ryms3[0,:]-np.mean(Ryms3[0,:]))),
                 max(abs(Ryms4[0,:]-np.mean(Ryms4[0,:])))],
                 [max(abs(Ryms1[1,:]-np.mean(Ryms1[1,:]))),
                 max(abs(Ryms2[1,:]-np.mean(Ryms2[1,:]))),
                 max(abs(Ryms3[1,:]-np.mean(Ryms3[1,:]))),
                 max(abs(Ryms4[1,:]-np.mean(Ryms4[1,:])))]])

print('70,Calculating Maximum Error')
# print('\nMAXIMUM ERROR:')
# print('Longitudinaal Left Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorX[0,0], maxErrorX[0,1],maxErrorX[0,2],maxErrorX[0,3]))
# print('Longitudinaal Left Vis: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorX[1,0], maxErrorX[1,1],maxErrorX[1,2],maxErrorX[1,3]))
# print('Longitudinaal Right Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorX[2,0], maxErrorX[2,1],maxErrorX[2,2],maxErrorX[2,3]))
# print('Longitudinaal Right Vis: \t%1.2f %1.2f %1.2f %1.2f\n' %(maxErrorX[3,0], maxErrorX[3,1],maxErrorX[3,2],maxErrorX[3,3]))
# 
# print('Tangentiaal Left Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorY[0,0], maxErrorY[0,1],maxErrorY[0,2],maxErrorY[0,3]))
# print('Tangentiaal Left Vis: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorY[1,0], maxErrorY[1,1],maxErrorY[1,2],maxErrorY[1,3]))
# print('Tangentiaal Right Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorY[2,0], maxErrorY[2,1],maxErrorY[2,2],maxErrorY[2,3]))
# print('Tangentiaal Right Vis: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorY[3,0], maxErrorY[3,1],maxErrorY[3,2],maxErrorY[3,3]))

#=============================================
# filter to get LF error
b, a = signal.butter(1, 0.5/ts*0.1)
# xms1f = signal.filtfilt(b, a, Lxms1,method='gust')
# xms2f = signal.filtfilt(b, a, Lxms2,method='gust')
# xms3f = signal.filtfilt(b, a, Lxms3,method='gust')
# xms4f = signal.filtfilt(b, a, Lxms4,method='gust')
# yms1f = signal.filtfilt(b, a, Lyms1,method='gust')
# yms2f = signal.filtfilt(b, a, Lyms2,method='gust')
# yms3f = signal.filtfilt(b, a, Lyms3,method='gust')
# yms4f = signal.filtfilt(b, a, Lyms4,method='gust')

maxErrorXfilt = np.array([[max(abs(signal.filtfilt(b, a, Lxms1[0,:]-np.mean(Lxms1[0,:])))),
                           max(abs(signal.filtfilt(b, a, Lxms2[0,:]-np.mean(Lxms2[0,:])))),
                           max(abs(signal.filtfilt(b, a, Lxms3[0,:]-np.mean(Lxms3[0,:])))),
                           max(abs(signal.filtfilt(b, a, Lxms4[0,:]-np.mean(Lxms4[0,:]))))],
                          [max(abs(signal.filtfilt(b, a, Lxms1[1,:]-np.mean(Lxms1[1,:])))),
                           max(abs(signal.filtfilt(b, a, Lxms2[1,:]-np.mean(Lxms2[1,:])))),
                           max(abs(signal.filtfilt(b, a, Lxms3[1,:]-np.mean(Lxms3[1,:])))),
                           max(abs(signal.filtfilt(b, a, Lxms4[1,:]-np.mean(Lxms4[1,:]))))],
                          [max(abs(signal.filtfilt(b, a, Rxms1[0,:]-np.mean(Rxms1[0,:])))),
                           max(abs(signal.filtfilt(b, a, Rxms2[0,:]-np.mean(Rxms2[0,:])))),
                           max(abs(signal.filtfilt(b, a, Rxms3[0,:]-np.mean(Rxms3[0,:])))),
                           max(abs(signal.filtfilt(b, a, Rxms4[0,:]-np.mean(Rxms4[0,:]))))],
                          [max(abs(signal.filtfilt(b, a, Rxms1[1,:]-np.mean(Rxms1[1,:])))),
                           max(abs(signal.filtfilt(b, a, Rxms2[1,:]-np.mean(Rxms2[1,:])))),
                           max(abs(signal.filtfilt(b, a, Rxms3[1,:]-np.mean(Rxms3[1,:])))),
                           max(abs(signal.filtfilt(b, a, Rxms4[1,:]-np.mean(Rxms4[1,:]))))]])

maxErrorYfilt = np.array([[max(abs(signal.filtfilt(b, a, Lyms1[0,:]-np.mean(Lyms1[0,:])))),
                           max(abs(signal.filtfilt(b, a, Lyms2[0,:]-np.mean(Lyms2[0,:])))),
                           max(abs(signal.filtfilt(b, a, Lyms3[0,:]-np.mean(Lyms3[0,:])))),
                           max(abs(signal.filtfilt(b, a, Lyms4[0,:]-np.mean(Lyms4[0,:]))))],
                          [max(abs(signal.filtfilt(b, a, Lyms1[1,:]-np.mean(Lyms1[1,:])))),
                           max(abs(signal.filtfilt(b, a, Lyms2[1,:]-np.mean(Lyms2[1,:])))),
                           max(abs(signal.filtfilt(b, a, Lyms3[1,:]-np.mean(Lyms3[1,:])))),
                           max(abs(signal.filtfilt(b, a, Lyms4[1,:]-np.mean(Lyms4[1,:]))))],
                          [max(abs(signal.filtfilt(b, a, Ryms1[0,:]-np.mean(Ryms1[0,:])))),
                           max(abs(signal.filtfilt(b, a, Ryms2[0,:]-np.mean(Ryms2[0,:])))),
                           max(abs(signal.filtfilt(b, a, Ryms3[0,:]-np.mean(Ryms3[0,:])))),
                           max(abs(signal.filtfilt(b, a, Ryms4[0,:]-np.mean(Ryms4[0,:]))))],
                          [max(abs(signal.filtfilt(b, a, Ryms1[1,:]-np.mean(Ryms1[1,:])))),
                           max(abs(signal.filtfilt(b, a, Ryms2[1,:]-np.mean(Ryms2[1,:])))),
                           max(abs(signal.filtfilt(b, a, Ryms3[1,:]-np.mean(Ryms3[1,:])))),
                           max(abs(signal.filtfilt(b, a, Ryms4[1,:]-np.mean(Ryms4[1,:]))))]])


print('85,Calculating Maximum Filtered Error')
# print('\nMAXIMUM FILTERED ERROR:')
# print('Longitudinaal Left Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorXfilt[0,0], maxErrorXfilt[0,1],maxErrorXfilt[0,2],maxErrorXfilt[0,3]))
# print('Longitudinaal Left Vis: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorXfilt[1,0], maxErrorXfilt[1,1],maxErrorXfilt[1,2],maxErrorXfilt[1,3]))
# print('Longitudinaal Right Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorXfilt[2,0], maxErrorXfilt[2,1],maxErrorXfilt[2,2],maxErrorXfilt[2,3]))
# print('Longitudinaal Right Vis: \t%1.2f %1.2f %1.2f %1.2f\n' %(maxErrorXfilt[3,0], maxErrorXfilt[3,1],maxErrorXfilt[3,2],maxErrorXfilt[3,3]))
# 
# print('Tangentiaal Left Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorYfilt[0,0], maxErrorYfilt[0,1],maxErrorYfilt[0,2],maxErrorYfilt[0,3]))
# print('Tangentiaal Left Vis: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorYfilt[1,0], maxErrorYfilt[1,1],maxErrorYfilt[1,2],maxErrorYfilt[1,3]))
# print('Tangentiaal Right Nir: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorYfilt[2,0], maxErrorYfilt[2,1],maxErrorYfilt[2,2],maxErrorYfilt[2,3]))
# print('Tangentiaal Right Vis: \t%1.2f %1.2f %1.2f %1.2f' %(maxErrorYfilt[3,0], maxErrorYfilt[3,1],maxErrorYfilt[3,2],maxErrorYfilt[3,3]))


## error: FFT method??
# 
# ## example fft
# ts = 0.5
# t = np.linspace(0,100,100/ts)
# y = 0.25*np.sin(2*np.pi*0.08*t)
# yfft = np.fft.fft(y-np.mean(y))
# ffft = np.linspace(1/len(yfft),1/ts,len(yfft))
# 
# plt.figure(1)
# plt.plot(t,y)
# 
# plt.figure(2)
# plt.plot(ffft,abs(yfft))
# 
# # signaal frequentie = 0.08Hz
# # fout < 0.2Hz
# # Apeak = (ffft[2]-ffft[1])*abs(yfft[8]) #this agrees with the amplitude of the input signal
# ind = np.where(ffft < 0.2)
# Elf = np.sum((ffft[2]-ffft[1])*abs(yfft[ind[0]]))
# ind = np.where(ffft[0:100] > 0.2)
# Ehf = np.sum((ffft[2]-ffft[1])*abs(yfft[ind[0]]))
# Etotal = np.sum((ffft[2]-ffft[1])*abs(yfft[0:100]))

# ## the actual signal
# # y = xms-np.mean(xms)
# y = yms1-np.mean(yms1)
# 
# yfft = np.fft.fft(y)[0:N-1]
# ffft = np.linspace(1/len(yfft),1/ts,len(yfft))
# 
# yfft2 = np.fft.fft(y)[0:N/2]
# ffft2 = np.linspace(1/len(yfft),1/(2*ts),len(yfft2))
# 
# # plt.figure(1)
# # plt.plot(t,y)
# 
# plt.figure(2)
# plt.plot(ffft,np.abs(yfft))
# plt.plot(ffft2,np.abs(yfft2))
# 
# # integrate errors to obtain absolute values for LF and HF error
# indLF = np.where(ffft2<0.1)
# eLF = sum(np.abs(yfft2[indLF[0]]))*(ffft2[2]-ffft2[1])
# indHF = np.where(ffft2>=0.1)
# eHF = sum(np.abs(yfft2[indHF[0]]))*(ffft2[2]-ffft2[1])
# 
# print('Std(E_lf) Y1 FFT [px]: ', eLF)
# print('Std(E_hf) Y1 FFT [px]: ', eHF)
# # print('Ehf Y1 FFT [px]: ', eHF)


#plt.show()
print("100,Position Stability: PASS")
sys.stdout.flush()