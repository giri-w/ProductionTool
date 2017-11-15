import matplotlib
matplotlib.use('Agg')
import numpy as np
import copy
import matplotlib.image as mpimg
import matplotlib.pyplot as plt
from matplotlib import gridspec

# debug
a = "C:\\Users\\GWA\\Documents\\GitHub\\Demcon\\ProductionToolFramework\\ProductionToolFramework\\bin\\Debug\\Python"
b = "C:\\Users\\GWA\\Desktop\\Internship DEMCON\\2. Hemics production tools\\1. Test Source\\20170703_113239_297 SignalStability"


# 
# import sys
# # path location of output
# a0 = str(sys.argv[1]) 
# a1 = a0.rsplit("\\",1)
# a = a1[0] + "\\figure";
# 
# # location of image source
# b = str(sys.argv[2]) 
# b = b.replace("\\","\\\\")


# change dir work
import os.path
os.chdir(a) 


def ModuloCorrection(A):
   # Function to correct for the modulo 9 fluctuations in the temporal signal
   # of the Hemics HandScan

   q         = int(np.floor(len(A)/9))
   Q         = np.reshape(A[0:int(9*q)],(9,q),order='F')
   Mean_Q    = np.mean(Q,axis=1)
   Q_factor  = Mean_Q[0]/Mean_Q
   R = np.empty((9,q))
   R[0][:] = Q_factor[0]*Q[0][:]
   R[1][:] = Q_factor[1]*Q[1][:]
   R[2][:] = Q_factor[2]*Q[2][:]
   R[3][:] = Q_factor[3]*Q[3][:]
   R[4][:] = Q_factor[4]*Q[4][:]
   R[5][:] = Q_factor[5]*Q[5][:]
   R[6][:] = Q_factor[6]*Q[6][:]
   R[7][:] = Q_factor[7]*Q[7][:]
   R[8][:] = Q_factor[8]*Q[8][:]
   B = R.flatten(order = 'F')
   # B = np.reshape(R,(1,R.size),order='F')
   return B

# user variable
from os import walk
mypath = b + "\\Raw data"

laser= 'vis'

# Load images + averaging
L_Sup = np.zeros((320, 256))
R_Sup = np.zeros((320, 256))
n = 0

# variable for left hand
Ls1 = []
Ls2 = []
Ls3 = []
Ls4 = []
Ls5 = []
Mean_Ls1 = []
Mean_Ls2 = []
Mean_Ls3 = []
Mean_Ls4 = []
Mean_Ls5 = []

# Point Coordinate in the left hand
Lx1 = 100-1
Ly1 = 180-1
Lx2 = 180-1
Ly2 = 200-1
Lx3 = 115-1
Ly3 = 120-1
Lx4 = 195-1
Ly4 = 140-1
Lx5 = np.mean([Lx2,Lx3],dtype='int')
Ly5 = np.mean([Ly2,Ly3],dtype='int')

# variable for right hand
Rs1 = []
Rs2 = []
Rs3 = []
Rs4 = []
Rs5 = []
Mean_Rs1 = []
Mean_Rs2 = []
Mean_Rs3 = []
Mean_Rs4 = []
Mean_Rs5 = []

# Point Coordinate in the right hand
Rx1 = 256-Lx1+1
Ry1 = Ly1-1
Rx2 = 256-Lx2+1
Ry2 = Ly2-1
Rx3 = 256-Lx3+1
Ry3 = Ly3-1
Rx4 = 256-Lx4+1
Ry4 = Ly4-1
Rx5 = np.mean([Rx2,Rx3],dtype='int')
Ry5 = np.mean([Ry2,Ry3],dtype='int')


f = []
for (dirpath, dirnames, filenames) in walk(mypath):
    f.extend(filenames)
    break
    
for i in range (0,len(f)):
    if (f[i][-3:] == 'png'):
        if (f[i][0:12] == 'left_low_'+laser):
            n = n+1
            A = mpimg.imread(mypath+'\\'+f[i])
            L_Sup = L_Sup+A
            Ls1.extend([100*A[Ly1][Lx1]])
            Ls2.extend([100*A[Ly2][Lx2]])
            Ls3.extend([100*A[Ly3][Lx3]])
            Ls4.extend([100*A[Ly4][Lx4]])
            Ls5.extend([100*A[Ly5][Lx5]])
        
        if (f[i][0:13] == 'right_low_'+laser):
            n = n+1
            A = mpimg.imread(mypath+'\\'+f[i])
            R_Sup = R_Sup+A
            Rs1.extend([100*A[Ry1][Rx1]])
            Rs2.extend([100*A[Ry2][Rx2]])
            Rs3.extend([100*A[Ry3][Rx3]])
            Rs4.extend([100*A[Ry4][Rx4]])
            Rs5.extend([100*A[Ry5][Rx5]])
        
# find average of the image
LA = L_Sup/n
RA = R_Sup/n

# Modulo 9 correction
Ls1_c = ModuloCorrection(Ls1)
Ls2_c = ModuloCorrection(Ls2)
Ls3_c = ModuloCorrection(Ls3)
Ls4_c = ModuloCorrection(Ls4)
Ls5_c = ModuloCorrection(Ls5)
Rs1_c = ModuloCorrection(Rs1)
Rs2_c = ModuloCorrection(Rs2)
Rs3_c = ModuloCorrection(Rs3)
Rs4_c = ModuloCorrection(Rs4)
Rs5_c = ModuloCorrection(Rs5)
Ls1 = Ls1_c
Ls2 = Ls2_c
Ls3 = Ls3_c
Ls4 = Ls4_c
Ls5 = Ls5_c
Rs1 = Rs1_c
Rs2 = Rs2_c
Rs3 = Rs3_c
Rs4 = Rs4_c
Rs5 = Rs5_c

# Calculation of tolerance boundaries (Gaussian distribution: FWHM = 2.3548 x standard deviation)
print("40,Analyzing signal")
FWHM_factor = 0.05

Mean_Ls1 = np.mean(Ls1)
Mean_Ls2 = np.mean(Ls2)
Mean_Ls3 = np.mean(Ls3)
Mean_Ls4 = np.mean(Ls4)
Mean_Ls5 = np.mean(Ls5)
Mean_Rs1 = np.mean(Rs1)
Mean_Rs2 = np.mean(Rs2)
Mean_Rs3 = np.mean(Rs3)
Mean_Rs4 = np.mean(Rs4)
Mean_Rs5 = np.mean(Rs5)
d_Ls1 = 0.5*FWHM_factor*Mean_Ls1
d_Ls2 = 0.5*FWHM_factor*Mean_Ls2
d_Ls3 = 0.5*FWHM_factor*Mean_Ls3
d_Ls4 = 0.5*FWHM_factor*Mean_Ls4
d_Ls5 = 0.5*FWHM_factor*Mean_Ls5
d_Rs1 = 0.5*FWHM_factor*Mean_Rs1
d_Rs2 = 0.5*FWHM_factor*Mean_Rs2
d_Rs3 = 0.5*FWHM_factor*Mean_Rs3
d_Rs4 = 0.5*FWHM_factor*Mean_Rs4
d_Rs5 = 0.5*FWHM_factor*Mean_Rs5

    
if 2.3548*np.std(Ls1) > 2*d_Ls1:
    Ls1_Str = 'Nok'
    Ls1_Str_C = [1, 0, 0]
else: 
    Ls1_Str = 'Ok'
    Ls1_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Ls2) > 2*d_Ls2: 
    Ls2_Str = 'Nok'
    Ls2_Str_C = [1, 0, 0]
else:
    Ls2_Str = 'Ok'
    Ls2_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Ls3) > 2*d_Ls3:
    Ls3_Str = 'Nok'
    Ls3_Str_C = [1, 0, 0]
else:
    Ls3_Str = 'Ok'
    Ls3_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Ls4) > 2*d_Ls4:
    Ls4_Str = 'Nok'
    Ls4_Str_C = [1, 0, 0]
else:
    Ls4_Str = 'Ok'
    Ls4_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Ls5) > 2*d_Ls5:
    Ls5_Str = 'Nok'
    Ls5_Str_C = [1, 0, 0]
else:
    Ls5_Str = 'Ok'
    Ls5_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Rs1) > 2*d_Rs1:
    Rs1_Str = 'Nok'
    Rs1_Str_C = [1, 0, 0]
else:
    Rs1_Str = 'Ok'
    Rs1_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Rs2) > 2*d_Rs2: 
    Rs2_Str = 'Nok'
    Rs2_Str_C = [1, 0, 0]
else:
    Rs2_Str = 'Ok'
    Rs2_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Rs3) > 2*d_Rs3: 
    Rs3_Str = 'Nok'
    Rs3_Str_C = [1, 0, 0]
else:
    Rs3_Str = 'Ok'
    Rs3_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Rs4) > 2*d_Rs4: 
    Rs4_Str = 'Nok'
    Rs4_Str_C = [1, 0, 0]
else: 
    Rs4_Str = 'Ok'
    Rs4_Str_C = [0, 1, 0]
    
if 2.3548*np.std(Rs5) > 2*d_Rs5: 
    Rs5_Str = 'Nok'
    Rs5_Str_C = [1, 0, 0]
else:
    Rs5_Str = 'Ok'
    Rs5_Str_C = [0, 1, 0]

# still mystery   
# Plot of signals and boundaries

plt.close('all')
fig = plt.figure(1)
gs = gridspec.GridSpec(6,2, height_ratios=[5,1,1,1,1,1])


a = fig.add_subplot(gs[0])
plt.imshow(LA, cmap='gray')
plt.scatter(x = [Lx1, Lx2, Lx3, Lx4,Lx5], y = [Ly1,Ly2,Ly3,Ly4,Ly5],
       c = ['g','c','y','r','m'], s=40)

a = fig.add_subplot(gs[1])
plt.imshow(RA, cmap='gray')
plt.scatter(x = [Rx1,Rx2,Rx3,Rx4,Rx5], y = [Ry1,Ry2,Ry3,Ry4,Ry5], 
       c = ['g','c','y','r','m'], s=40)
       
# graph for the left hand
print("60,Processing graph for left hand")
a = fig.add_subplot(gs[2])
Ls1_Hor = np.array([Mean_Ls1 for i in range(0,len(Ls1))])
plt.plot(Ls1_Hor,'m')
plt.plot(Ls1,'m')
plt.axis([0, len(Ls1), Mean_Ls1-1.4*d_Ls1, Mean_Ls1+1.4*d_Ls1])
plt.text(len(Ls1)/2,Mean_Ls1-1.4*d_Ls1,Ls1_Str,fontsize=20, color=Ls1_Str_C)

a = fig.add_subplot(gs[4])
Ls2_Hor = np.array([Mean_Ls2 for i in range(0,len(Ls2))])
plt.plot(Ls2_Hor,'c')
plt.plot(Ls2,'c')
plt.axis([0, len(Ls2), Mean_Ls2-1.4*d_Ls2, Mean_Ls2+1.4*d_Ls2])
plt.text(len(Ls2)/2,Mean_Ls2-1.4*d_Ls2,Ls2_Str,fontsize=20, color=Ls2_Str_C)

a = fig.add_subplot(gs[6])
Ls3_Hor = np.array([Mean_Ls3 for i in range(0,len(Ls3))])
plt.plot(Ls3_Hor,'k')
plt.plot(Ls3,'k')
plt.axis([0, len(Ls3), Mean_Ls3-1.4*d_Ls3, Mean_Ls3+1.4*d_Ls3])
plt.text(len(Ls3)/2,Mean_Ls3-1.4*d_Ls3,Ls3_Str,fontsize=20, color=Ls3_Str_C)

a = fig.add_subplot(gs[8])
Ls4_Hor = np.array([Mean_Ls4 for i in range(0,len(Ls4))])
plt.plot(Ls4_Hor,'b')
plt.plot(Ls4,'b')
plt.axis([0, len(Ls4), Mean_Ls4-1.4*d_Ls4, Mean_Ls4+1.4*d_Ls4])
plt.text(len(Ls4)/2,Mean_Ls4-1.4*d_Ls4,Ls4_Str,fontsize=20, color=Ls4_Str_C)

a = fig.add_subplot(gs[10])
Ls5_Hor = np.array([Mean_Ls5 for i in range(0,len(Ls5))])
plt.plot(Ls5_Hor,'y')
plt.plot(Ls5,'y')
plt.axis([0, len(Ls5), Mean_Ls5-1.4*d_Ls5, Mean_Ls5+1.4*d_Ls5])
plt.text(len(Ls5)/2,Mean_Ls5-1.4*d_Ls5,Ls5_Str,fontsize=20, color=Ls5_Str_C)

# graph for the right hand
print("80,Processing graph for right hand")

a = fig.add_subplot(gs[3])
Rs1_Hor = np.array([Mean_Rs1 for i in range(0,len(Rs1))])
plt.plot(Rs1_Hor,'m')
plt.plot(Rs1,'m')
plt.axis([0, len(Rs1), Mean_Rs1-1.4*d_Rs1, Mean_Rs1+1.4*d_Rs1])
plt.text(len(Rs1)/2,Mean_Rs1-1.4*d_Rs1,Rs1_Str,fontsize=20, color=Rs1_Str_C)

a = fig.add_subplot(gs[5])
Rs2_Hor = np.array([Mean_Rs2 for i in range(0,len(Rs2))])
plt.plot(Rs2_Hor,'c')
plt.plot(Rs2,'c')
plt.axis([0, len(Rs2), Mean_Rs2-1.4*d_Rs2, Mean_Rs2+1.4*d_Rs2])
plt.text(len(Rs2)/2,Mean_Rs2-1.4*d_Rs2,Rs2_Str,fontsize=20, color=Rs2_Str_C)

a = fig.add_subplot(gs[7])
Rs3_Hor = np.array([Mean_Rs3 for i in range(0,len(Rs3))])
plt.plot(Rs3_Hor,'k')
plt.plot(Rs3,'k')
plt.axis([0, len(Rs3), Mean_Rs3-1.4*d_Rs3, Mean_Rs3+1.4*d_Rs3])
plt.text(len(Rs3)/2,Mean_Rs3-1.4*d_Rs3,Rs3_Str,fontsize=20, color=Rs3_Str_C)

a = fig.add_subplot(gs[9])
Rs4_Hor = np.array([Mean_Rs4 for i in range(0,len(Rs4))])
plt.plot(Rs4_Hor,'b')
plt.plot(Rs4,'b')
plt.axis([0, len(Rs4), Mean_Rs4-1.4*d_Rs4, Mean_Rs4+1.4*d_Rs4])
plt.text(len(Rs4)/2,Mean_Rs4-1.4*d_Rs4,Rs4_Str,fontsize=20, color=Rs4_Str_C)

a = fig.add_subplot(gs[11])
Rs5_Hor = np.array([Mean_Rs5 for i in range(0,len(Rs5))])
plt.plot(Rs5_Hor,'y')
plt.plot(Rs5,'y')
plt.axis([0, len(Rs5), Mean_Rs5-1.4*d_Rs5, Mean_Rs5+1.4*d_Rs5])
plt.text(len(Rs5)/2,Mean_Rs5-1.4*d_Rs5,Rs5_Str,fontsize=20, color=Rs5_Str_C)
plt.show()
plt.savefig('SignalStability.png')

print("100,Signal Stability Test : PASS")