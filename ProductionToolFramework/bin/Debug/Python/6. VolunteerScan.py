import matplotlib.pyplot as plt
# import matplotlib
# matplotlib.use('Agg')
import matplotlib.image as mpimg
import numpy as np
from pylab import *
from scipy import signal, ndimage
import path
import math
import xml.etree.ElementTree as ET
import linecache 
import warnings
import matplotlib.cbook
warnings.filterwarnings("ignore",category=matplotlib.cbook.mplDeprecation)

import shutil
import os.path
os.chdir(r'C:\Users\GWA\Documents\GitHub\Demcon\ProductionToolFramework\ProductionToolFramework\bin\Debug\Python\figure\FAT4VolunteerScan') 
savePath = r'C:\Users\GWA\Documents\GitHub\Demcon\ProductionToolFramework\ProductionToolFramework\bin\Debug\Python\figure\FAT4VolunteerScan'

#Close all figures
close('all')

#Path direction
dr = r'C:\TestFolder'
ms = '20170911_101149_297 jesse new settings'


## get joint positions from phase2.xml
# p = path.Path(dr+'\\'+ms+'\\Result data')
# p2 = p.dirs()[0]
# xml = p2.files('rois.xml')[0]
for i in range(0,1): #Process all measurements. Insert number of measurements manually.
    
    # Uncomment when using multiple measurements
    p1 = path.Path(dr+'\\')
    p2 = p1.dirs()[i+4]   #Should be [i] when using multiple measurements
    p = path.Path(p2+'\\Result data')
    p4 = p.dirs()[0]
    xml = p4.files('rois.xml')[0]  
    ms = p2.basename() 
    print(p2)
    
    #Defined for system check
    k_cal = 0
    k_hmsr = 0
    k_pass = 0
    
    close('all')
    
   
    logfile = open(dr+'\\'+ms+'\\'+ms+'_checks.txt', 'a')
    
    tree = ET.parse(xml)
    root = tree.getroot()
    
    jointstructL = np.array([('wristL',float(root[0][0][0][0][1].text)/2+float(root[0][0][0][0][3].text)/2,
    float(root[0][0][0][0][2].text)/2+float(root[0][0][0][0][4].text)/2),
                            ('PIP1L',float(root[0][0][0][6][1].text)/2+float(root[0][0][0][6][3].text)/2,
                            float(root[0][0][0][6][2].text)/2+float(root[0][0][0][6][4].text)/2),
                            ('PIP2L',float(root[0][0][0][7][1].text)/2+float(root[0][0][0][7][3].text)/2,
                            float(root[0][0][0][7][2].text)/2+float(root[0][0][0][7][4].text)/2),
                            ('PIP3L',float(root[0][0][0][8][1].text)/2+float(root[0][0][0][8][3].text)/2,
                            float(root[0][0][0][8][2].text)/2+float(root[0][0][0][8][4].text)/2),
                            ('PIP4L',float(root[0][0][0][9][1].text)/2+float(root[0][0][0][9][3].text)/2,
                            float(root[0][0][0][9][2].text)/2+float(root[0][0][0][9][4].text)/2),
                            ('PIP5L',float(root[0][0][0][10][1].text)/2+float(root[0][0][0][10][3].text)/2,
                            float(root[0][0][0][10][2].text)/2+float(root[0][0][0][10][4].text)/2),
                            ('MCP1L',float(root[0][0][0][11][1].text)/2+float(root[0][0][0][11][3].text)/2,
                            float(root[0][0][0][11][2].text)/2+float(root[0][0][0][11][4].text)/2),
                            ('MCP2L',float(root[0][0][0][12][1].text)/2+float(root[0][0][0][12][3].text)/2,
                            float(root[0][0][0][12][2].text)/2+float(root[0][0][0][12][4].text)/2),
                            ('MCP3L',float(root[0][0][0][13][1].text)/2+float(root[0][0][0][13][3].text)/2,
                            float(root[0][0][0][13][2].text)/2+float(root[0][0][0][13][4].text)/2),
                            ('MCP4L',float(root[0][0][0][14][1].text)/2+float(root[0][0][0][14][3].text)/2,
                            float(root[0][0][0][14][2].text)/2+float(root[0][0][0][14][4].text)/2),
                            ('MCP5L',float(root[0][0][0][15][1].text)/2+float(root[0][0][0][15][3].text)/2,
                            float(root[0][0][0][15][2].text)/2+float(root[0][0][0][15][4].text)/2)])
    jointstructR = np.array([('wristR',float(root[0][1][0][0][1].text)/2+float(root[0][1][0][0][3].text)/2,
    float(root[0][1][0][0][2].text)/2+float(root[0][1][0][0][4].text)/2),
                            ('PIP1R',float(root[0][1][0][6][1].text)/2+float(root[0][1][0][6][3].text)/2,
                            float(root[0][1][0][6][2].text)/2+float(root[0][1][0][6][4].text)/2),
                            ('PIP2R',float(root[0][1][0][7][1].text)/2+float(root[0][1][0][7][3].text)/2,
                            float(root[0][1][0][7][2].text)/2+float(root[0][1][0][7][4].text)/2),
                            ('PIP3R',float(root[0][1][0][8][1].text)/2+float(root[0][1][0][8][3].text)/2,
                            float(root[0][1][0][8][2].text)/2+float(root[0][1][0][8][4].text)/2),
                            ('PIP4R',float(root[0][1][0][9][1].text)/2+float(root[0][1][0][9][3].text)/2,
                            float(root[0][1][0][9][2].text)/2+float(root[0][1][0][9][4].text)/2),
                            ('PIP5R',float(root[0][1][0][10][1].text)/2+float(root[0][1][0][10][3].text)/2,
                            float(root[0][1][0][10][2].text)/2+float(root[0][1][0][10][4].text)/2),
                            ('MCP1R',float(root[0][1][0][11][1].text)/2+float(root[0][1][0][11][3].text)/2,
                            float(root[0][1][0][11][2].text)/2+float(root[0][1][0][11][4].text)/2),
                            ('MCP2R',float(root[0][1][0][12][1].text)/2+float(root[0][1][0][12][3].text)/2,
                            float(root[0][1][0][12][2].text)/2+float(root[0][1][0][12][4].text)/2),
                            ('MCP3R',float(root[0][1][0][13][1].text)/2+float(root[0][1][0][13][3].text)/2,
                            float(root[0][1][0][13][2].text)/2+float(root[0][1][0][13][4].text)/2),
                            ('MCP4R',float(root[0][1][0][14][1].text)/2+float(root[0][1][0][14][3].text)/2,
                            float(root[0][1][0][14][2].text)/2+float(root[0][1][0][14][4].text)/2),
                            ('MCP5R',float(root[0][1][0][15][1].text)/2+float(root[0][1][0][15][3].text)/2,
                            float(root[0][1][0][15][2].text)/2+float(root[0][1][0][15][4].text)/2)])
    
    ## read motor matrix xml
    # ms = p4[76:95]
    p = path.Path(dr+'\\'+ms+'\\Calibration')
    xml_left = p.files('left-motormatrix.xml')[0]
    xml_right = p.files('right-motormatrix.xml')[0]
    
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
    
    
    ## find best PD coordinate for joint
    mmListL = np.array([[20,80],
                        [100,80],
                        [40,50],
                        [60,50]])
    
    mmListR = np.array([[20,80],
                        [100,80],
                        [70,50],
                        [50,50]])
    
    
    ## the files
    p = path.Path(dr+'\\'+ms+'\\Raw data')
    imgfiles_left_nir = sorted(p.files('*left_low_nir_*.png'))
    imgfiles_right_nir = sorted(p.files('*right_low_nir_*.png'))
    imgfiles_left_vis = sorted(p.files('*left_low_vis_*.png'))
    imgfiles_right_vis = sorted(p.files('*right_low_vis_*.png'))
    mmRight = p.files('*right_mm_nir_motormatrix.png')
    mmRread = mpimg.imread(mmRight[0])
    p = path.Path(dr+'\\'+ms+'\\Photodiode')
    pdfiles_left_nir = sorted(p.files('left_mm_nir_photodiode_*.png'))
    pdfiles_right_nir = sorted(p.files('right_mm_nir_photodiode_*.png'))
    pdfiles_left_vis = sorted(p.files('left_mm_vis_photodiode_*.png'))
    pdfiles_right_vis = sorted(p.files('right_mm_vis_photodiode_*.png'))
    
    imgLnir=mpimg.imread(imgfiles_left_nir[0])
    imgRnir=mpimg.imread(imgfiles_right_nir[0])
    
    figure(num=10,figsize=(12,6))
    subplot(1,2,1)
    imshow(imgLnir,cmap='gray')
    for i in range(len(jointstructL)):
        plot(float(jointstructL[i,1])/2,float(jointstructL[i,2])/2,'r+')
    subplot(1,2,2)
    imshow(imgRnir,cmap='gray')
    for i in range(len(jointstructR)):
        plot(float(jointstructR[i,1])/2,float(jointstructR[i,2])/2,'r+')
    savefig(dr+'\\'+ms+'_joints.png')
    
    pdLnir=mpimg.imread(pdfiles_left_nir[0])
    pdRnir=mpimg.imread(pdfiles_right_nir[0])
    pdLvis=mpimg.imread(pdfiles_left_vis[0])
    pdRvis=mpimg.imread(pdfiles_right_vis[0])
    
    pdLnirAvg=mpimg.imread(pdfiles_left_nir[0])/200
    pdRnirAvg=mpimg.imread(pdfiles_right_nir[0])/200
    pdLvisAvg=mpimg.imread(pdfiles_left_vis[0])/200
    pdRvisAvg=mpimg.imread(pdfiles_right_vis[0])/200
    
    figure(num=11,figsize=(12,6))
    subplot(1,2,1)
    imshow(pdLnir,cmap='gray')
    for i in range(len(mmListL)):
        plot(mmListL[i,0],mmListL[i,1],'r+')
    subplot(1,2,2)
    imshow(pdRnir,cmap='gray')
    for i in range(len(mmListR)):
        plot(mmListR[i,0],mmListR[i,1],'r+')
    savefig(dr+'\\'+ms+'_pd.png')
    
    ## calculate response curves
    S = 2
    Spd = 1
    
    N = len(imgfiles_left_nir)
    responseLnir = np.zeros([len(jointstructL),N])
    responseRnir = np.zeros([len(jointstructR),N])
    responseLvis = np.zeros([len(jointstructL),N])
    responseRvis = np.zeros([len(jointstructR),N])
    responsePdLnir = np.zeros([len(mmListL),N])
    responsePdLvis = np.zeros([len(mmListL),N])
    responsePdRnir = np.zeros([len(mmListR),N])
    responsePdRvis = np.zeros([len(mmListR),N])
    
    diffPdLnir = np.zeros([N])
    diffPdRnir = np.zeros([N])
    diffPdLvis = np.zeros([N])
    diffPdRvis = np.zeros([N])
    
    for i in range(N):
        #print(i) #prints range for all photo's
        img=mpimg.imread(imgfiles_left_nir[i])
        for j in range(len(jointstructL)):
            tmpx = int(float(jointstructL[j,1])/2)
            tmpy = int(float(jointstructL[j,2])/2)
            imgcrop = img[tmpy-S:tmpy+S,tmpx-S:tmpx+S]
            responseLnir[j,i] = np.average(imgcrop)
        img=mpimg.imread(imgfiles_right_nir[i])
        for j in range(len(jointstructR)):
            tmpx = int(float(jointstructR[j,1])/2)
            tmpy = int(float(jointstructR[j,2])/2)
            imgcrop = img[tmpy-S:tmpy+S,tmpx-S:tmpx+S]
            responseRnir[j,i] = np.average(imgcrop)
        
        img=mpimg.imread(imgfiles_left_vis[i])
        for j in range(len(jointstructL)):
            tmpx = int(float(jointstructL[j,1])/2)
            tmpy = int(float(jointstructL[j,2])/2)
            imgcrop = img[tmpy-S:tmpy+S,tmpx-S:tmpx+S]
            responseLvis[j,i] = np.average(imgcrop)
        img=mpimg.imread(imgfiles_right_vis[i])
        for j in range(len(jointstructR)):
            tmpx = int(float(jointstructR[j,1])/2)
            tmpy = int(float(jointstructR[j,2])/2)
            imgcrop = img[tmpy-S:tmpy+S,tmpx-S:tmpx+S]
            responseRvis[j,i] = np.average(imgcrop)
            
        img=mpimg.imread(pdfiles_left_nir[i])
        for j in range(len(mmListL)):
            imgcrop = img[mmListL[j,1]-Spd:mmListL[j,1]+Spd,mmListL[j,0]-Spd:mmListL[j,0]+Spd]
            responsePdLnir[j,i] = np.average(imgcrop)
        diffPdLnir[i] = np.sum(img-pdLnir)
        pdLnirAvg=pdLnirAvg+img/200
        
        img=mpimg.imread(pdfiles_left_vis[i])
        for j in range(len(mmListL)):
            imgcrop = img[mmListL[j,1]-Spd:mmListL[j,1]+Spd,mmListL[j,0]-Spd:mmListL[j,0]+Spd]
            responsePdLvis[j,i] = np.average(imgcrop)
        diffPdLvis[i] = np.sum(img-pdLvis)
        pdLvisAvg=pdLvisAvg+img/200
        
        img=mpimg.imread(pdfiles_right_nir[i])
        for j in range(len(mmListR)):
            imgcrop = img[mmListR[j,1]-Spd:mmListR[j,1]+Spd,mmListR[j,0]-Spd:mmListR[j,0]+Spd]
            responsePdRnir[j,i] = np.average(imgcrop)
        diffPdRnir[i] = np.sum(img-pdRnir)
        pdRnirAvg=pdRnirAvg+img/200
        img=mpimg.imread(pdfiles_right_vis[i])
        for j in range(len(mmListR)):
            imgcrop = img[mmListR[j,1]-Spd:mmListR[j,1]+Spd,mmListR[j,0]-Spd:mmListR[j,0]+Spd]
            responsePdRvis[j,i] = np.average(imgcrop)
        diffPdRvis[i] = np.sum(img-pdRvis)
        pdRvisAvg=pdRvisAvg+img/200
    
    # print('Resp curve')
    ## plots
    
    figure(num=20,figsize=(12,6))
    subplot(1,2,1)
    title('Left NIR')
    ylim([0,0.7])
    xlabel('Image [-]')
    ylabel('Intensity [-]')
    for i in range(len(jointstructL)):
        plot(responseLnir[i,:])
    subplot(1,2,2)
    title('Right NIR')
    ylim([0,0.7])
    xlabel('Image [-]')
    for i in range(len(jointstructR)):
        plot(responseRnir[i,:])
    savefig(dr+'\\'+ms+'_responseNIR.png')
    
    figure(num=21,figsize=(12,6))
    subplot(1,2,1)
    title('Left VIS')
    ylim([0,0.7])
    ylabel('Intensity [-]')
    xlabel('Image [-]')
    for i in range(len(jointstructL)):
        plot(responseLvis[i,:])
    subplot(1,2,2)
    title('Right VIS')
    ylim([0,0.7])
    xlabel('Image [-]')
    for i in range(len(jointstructR)):
        plot(responseRvis[i,:])
    savefig(dr+'\\'+ms+'_responseVIS.png')
    
    figure(num=22,figsize=(12,6))
    subplot(1,2,1)
    title('PD Left NIR')
    xlabel('Image [-]')
    ylabel('PD intensity [-]')
    for i in range(len(mmListL)):
        plot(responsePdLnir[i,:])
    subplot(1,2,2)
    title('PD Right NIR')
    xlabel('Image [-]')
    ylabel('PD intensity [-]')
    for i in range(len(mmListR)):
        plot(responsePdRnir[i,:])
    savefig(dr+'\\'+ms+'_PDresponseNIR.png')
    
    
    figure(num=23,figsize=(12,6))
    subplot(1,2,1)
    title('PD Left VIS')
    xlabel('Image [-]')
    ylabel('PD intensity [-]')
    for i in range(len(mmListL)):
        plot(responsePdLvis[i,:])
    subplot(1,2,2)
    title('PD Right VIS')
    xlabel('Image [-]')
    ylabel('PD intensity [-]')
    for i in range(len(mmListR)):
        plot(responsePdRvis[i,:])
    savefig(dr+'\\'+ms+'_PDresponseVIS.png')
    
    # PD integrety
    figure(num=24,figsize=(12,6))
    title('PD integrety')
    plot(diffPdLnir,label='Lnir')
    plot(diffPdLvis,label='Lvis')
    plot(diffPdRnir,label='Rnir')
    plot(diffPdRvis,label='Rvis')
    xlabel('Image [-]')
    ylabel('Diff PD - PD0 [-]')
    legend()
    savefig(dr+'\\'+ms+'_diffPD.png')
    
    figure(num=25,figsize=(12,6))
    subplot(2,2,1)
    imshow(pdLnirAvg,cmap='gray')
    subplot(2,2,2)
    imshow(pdRnirAvg,cmap='gray')
    subplot(2,2,3)
    imshow(pdLvisAvg,cmap='gray')
    subplot(2,2,4)
    imshow(pdRvisAvg,cmap='gray')
    savefig(dr+'\\'+ms+'_sumPD.png')
    
    # laser calibration data
    p = path.Path(dr+'\\'+ms+'\\Calibration')
    laserdatafiles = sorted(p.files('*lasercalibrationdata.csv'))
    fig, axs = subplots(2,2)
    axs = axs.reshape(4)
    for i,file in enumerate(laserdatafiles):
        data = np.genfromtxt(file, delimiter='\t', skip_header=1)
        axs[i].plot(data[:,0],data[:,3], 'o')
        axs[i].set_title(file.split('\\')[-1])
    savefig(dr+'\\'+ms+'_LaserCalibration.png')
    
    # print('Plots')

    # show()
    
    ## Check if the joint measurement is valid using NIR and both hands
    
    #Moving average filter
    def movingaverage(interval, window_size):
        window = np.ones(int(window_size))/float(window_size)
        return np.convolve(interval, window, 'same')
        
    WS = 10 #window_size
    y_ma = np.zeros((11,199))
    figure(num = 30, figsize=(12,6))
    #file = open(r'stage\Python\Thresholds\20170907_joint_thr.txt', 'a')
    subplot(1,2,1)
    title('Left NIR')
    
    for i in range(len(jointstructL)):
        #Left hand
        y_LH = responseLnir[i,:] 
        x_LH = len(responseLnir[i])
        y_av_LH = movingaverage(y_LH,WS)
        y_av_LH[0:5] = y_av_LH[5]
        y_av_LH[194:199] = y_av_LH[194]
        subplot(1,2,1)
        title('Left NIR')
        plot(y_av_LH)
        
        #plot(responseLnir[i,:]) # Original values
        
        #Right hand
        y_RH = responseRnir[i,:]
        x_RH = len(responseRnir[i])
        y_av_RH = movingaverage(y_RH,WS)
        y_av_RH[0:5] = y_av_RH[5]
        y_av_RH[194:199] = y_av_RH[194]
        
        subplot(1,2,2)
        title('Right NIR')
        plot(y_av_RH) 
        #plot(responseRnir[i,:]) # Original values
        
        #Define thresholds (Values can be adjusted)
       
        #Left hand
        a_LH = abs(y_av_LH[10] - y_av_LH[190]) 
        b_LH = abs(y_av_LH[60] - y_av_LH[130])
        c_LH = abs(y_av_LH[150] - y_av_LH[190])
        
        #Right hand
        a_RH = abs(y_av_RH[10] - y_av_RH[190]) 
        b_RH = abs(y_av_RH[60] - y_av_RH[130])
        c_RH = abs(y_av_RH[150] - y_av_RH[190])
        
        #file is used to determine a, b and c parameter values.
        #file = open(r'stage\Python\Thresholds\joints.txt', 'a') 
        #file.write('%.5f' %a_LH + ',' + '%.5f' %b_LH + ',' + '%.5f' %c_LH + ',' + '%.5f' %a_RH + ',' + '%.5f' %b_RH + ',' + '%.5f\n' %c_RH)
        
        #Determined comparing different measurements from the same system.
        if a_LH < 0.1 and b_LH < 0.09 and c_LH < 0.06 and a_RH < 0.1 and b_RH < 0.09 and c_RH < 0.06:  
            logfile.write('%s/' %jointstructL[i,0] + '%s' %jointstructR[i,0] +':  pass/pass \n')    
        elif a_LH < 0.1 and b_LH < 0.06 and c_LH < 0.06:
            logfile.write('%s/' %jointstructL[i,0] + '%s' %jointstructR[i,0] +':  pass/fail \n')    
            k_hmsr = 1
            k_pass = 1
        elif a_RH < 0.1 and b_RH < 0.06 and c_RH < 0.06:
            logfile.write('%s/' %jointstructL[i,0] + '%s' %jointstructR[i,0] +':  fail/pass \n')    
        else:
            logfile.write('%s/' %jointstructL[i,0] + '%s' %jointstructR[i,0] +':  fail/fail \n')    
            k_hmsr = 1
            k_pass = 1
    #file.close()
    
    # print('Joint measurements')
 
         #A check could be implemented to detect a straight line during 
         #a test measurement

    ## Current (mA) minimum threshold
    fig, axs = subplots(2,2)
    axs = axs.reshape(4)
    for i,file in enumerate(laserdatafiles):
    # print(file)
        data = np.genfromtxt(file, delimiter='\t', skip_header=1)
        axs[i].plot(data[:,3],data[:,4], 'o')
        axs[i].set_title(file.split('\\')[-1])
        m_lc, c_lc = np.polyfit(data[8:,3],data[8:,4],1) #Make a trendline. Don't take PWM values into account
        x_lc = -c_lc/m_lc #Determine x for y = 0
        dir = ['Left ', 'Left ', 'Right', 'Right'] #Defined for print
        msr = ['NIR', 'VIS', 'NIR', 'VIS']
        if i == 0 or i == 2:    #Real current for NIR + threshold check
            Inir = (x_lc/4095)*(2.5/1.5)
            if Inir < 300:
                logfile.write('%s' %dir[i] + ' ' + '%s' %msr[i] + ' ' + 'Current threshold: pass \n')
            else:
                logfile.write('%s' %dir[i] + ' ' + '%s' %msr[i] + ' ' + 'Current threshold: fail \n' )
                k_cal = 1
                k_pass = 1
        else:                   #Real current for VIS + Treshold check
            Ivis = (x_lc/4095)*(2.5/1.0)
            if Ivis < 800:
                logfile.write('%s' %dir[i] + ' ' + '%s' %msr[i] + ' ' + 'Current threshold: pass \n')
            else:
                logfile.write('%s' %dir[i] + ' ' + '%s' %msr[i] + ' ' + 'Current threshold: fail \n')
                k_cal = 1
                k_pass = 1
    # print('min threshold')
    
    ## Maximum power threshold    
    #Define parameters to determine power output
    Vpd = data[63,4]/5*4095
    p = path.Path(dr+'\\'+ms+'\\Calibration\\')
    param = p.files('calibration.xml')[0]
    a_Lnir = float(linecache.getline(param, 117)[15:20])
    b_Lnir = int(linecache.getline(param, 118)[15:16])
    a_Rnir = float(linecache.getline(param, 133)[15:20])
    b_Rnir = int(linecache.getline(param, 134)[15:16])
    
    a_Lvis = float(linecache.getline(param, 124)[15:20])
    b_Lvis = int(linecache.getline(param, 125)[15:16])
    a_Rvis = float(linecache.getline(param, 140)[15:20])
    b_Rvis = int(linecache.getline(param, 141)[15:16])
    
    a = [a_Lvis, a_Rvis, a_Lnir, a_Rnir]
    b = [b_Lvis, b_Rvis, b_Lnir, b_Rnir]
    
    dir = ['Left ', 'Left ', 'Right', 'Right'] #Defined for print
    msr = ['NIR', 'VIS', 'NIR', 'VIS']
    
    for i in range(0, 2):
        Pvis = (Vpd-b[i])/a[i]
        if Pvis > 400:
            logfile.write('%s' %dir[i] + 'VIS maximum laser power: pass \n')
        else:
            logfile.write('%s' %dir[i] + 'VIS maximum laser power: fail \n')
            k_cal = 1
            k_pass = 1
    
    for i in range (2,4):
        Pnir = (Vpd-b[i])/a[i]
        if Pnir > 600:
            logfile.write('%s' %dir[i] + 'NIR maximum laser power: pass \n')
        else:
            logfile.write('%s' %dir[i] + 'NIR maximum laser power: fail \n')
            k_cal = 1
            k_pass = 1
            
    savefig(dr+'\\'+ms+'_LaserCalibration.png')
    # show()
    
    ## Laser calibration check. Laser current vs laser index
    
    
    k_lc = 0
    for j,file in enumerate(laserdatafiles):
    # print(file)
        data = np.genfromtxt(file, delimiter='\t', skip_header=1)
        for i in range(9,40):
            if data[i,3] >= data[i+1,3]: 
                k_lc = 1
        
        if k_lc == 0:
            logfile.write('%s' %dir[j] + ' ' + '%s' %msr[j] + ' ' + 'laser calibration: pass \n')
        else:
            logfile.write('%s' %dir[j] + ' ' + '%s' %msr[j] + ' ' + 'laser calibration: fail \n')
            k_cal = 1
            k_pass = 1
        
    ## Check wether the laser intensity decrease is not too high
    x = [j for j in range(199)]
    x = np.asarray(x)
    col1 = ['r', 'b', 'g', 'y']
    col2 = ['r--', 'b--', 'g--', 'y--']
    response = [responsePdLnir, responsePdRnir, responsePdLvis, responsePdRvis]
    PdLaser = ['Left NIR laser', 'Right NIR laser', 'Left VIS laser', 'Right VIS laser']
   
    # Root mean square fail (RMS)
    def rmse(predictions, targets):
        return np.sqrt(((predictions - targets) ** 2).mean())
    k_perdec = 0
    k_rms = 0
    #filePd = open(r'stage\Python\Thresholds\20170907_photodiode_threshold.txt', 'a')
    
    for j in range(len(mmListL)):
        resp = response[j]
        for i in range(len(mmListL)):
            y = resp[i,:]
            
            #Fit a line y = mx + c through the data of y
            m,c = np.polyfit(x,y,1)
            y_fit = m*x+c
            figure(num=40)
            plot(y, col1[i], label = ('Original %i' %i))
            plot(x,y_fit, col2[i], label = 'Fit %i' %i)
            title('Linear fit Photodiode') 
            
            # Percentage intensity decrease
            IntDec = y_fit[0]-y_fit[198]
            PerDec = abs(math.ceil(IntDec*100)) #Relative decrease
            
            # Root mean square fail
            rms = math.ceil(rmse(y_fit,y))
            
            #Write to text
            #filePd.write('%.5f' %rms + ',' + '%.3f\n' %PerDec)
            
            #print('RMS of line', i, '=', rms)
            if rms >= 0.01 and PerDec >= 10: 
                    k_perdec = 1
                    k_rms = 1
            
    if k_perdec == 0 and k_rms == 0:    
        logfile.write('Laser intensities: pass \n')
    else:
        logfile.write('fail : Laser intensity decrease too high, check PdLaser[j] \n')
        k_pass = 1
    #filePd.close()
    # print('laser int')
    
## Blanking lines check
    #Path direction
    p = path.Path(dr+'\\'+ms+'\\Calibration\\')
    xml_cal = p.files('calibration.xml')[0]
    
    tree = ET.parse(xml_cal)
    root = tree.getroot()
    
    xml_left = p.files('left-motormatrix.xml')[0]
    xml_right = p.files('right-motormatrix.xml')[0]
    
    #Check if BlankingStart and WristStart are the same
    for LeftLaserBlanking1SegmentStart in root.iter('LeftLaserBlanking1SegmentStart'):
        LeftBlanking = int(LeftLaserBlanking1SegmentStart.text)
        logfile.write('Left blanking: %i \n' %LeftBlanking )
    
    for RightLaserBlanking1SegmentStart in root.iter('RightLaserBlanking1SegmentStart'):
        RightBlanking = int(RightLaserBlanking1SegmentStart.text)
        logfile.write('Right blanking: %i \n' %RightBlanking)
        
    for LeftWristStartSegment in root.iter('LeftWristStartSegment'):
        LeftWrist = int(LeftWristStartSegment.text)
        logfile.write('Left wrist: %i \n' %LeftWrist)
        
    for RightWristStartSegment in root.iter('RightWristStartSegment'):
        RightWrist = int(RightWristStartSegment.text)
        logfile.write('Right wrist: %i \n' %RightWrist)
    
    if LeftBlanking == LeftWrist and RightBlanking == RightWrist:
        logfile.write('Blanking settings: pass \n')
    else:
        logfile.write('Blanking settings: fail \n')
        k_pass = 1
    
    #Left Wrist LUT check
    tree = ET.parse(xml_left)
    root = tree.getroot()
    
    MML = np.array([[0,0,0,0]])
    
    for elem in tree.iter('Coordinate'):
        #print(elem)
        tmprow = int(elem.get('Row'))
        tmpcol = int(elem.get('Column'))
        tmpx = int(elem.get('X'))
        tmpy = int(elem.get('Y'))
        MML = np.append(MML,np.array([[tmprow,tmpcol,tmpx,tmpy]]),axis=0)
    
    if MML[LeftWrist,2] > 0 and MML[LeftWrist,3] > 0:
        logfile.write('Left LUT: pass \n')
    else:
        logfile.write('Left LUT: fail \n')
        k_pass = 1
    
    #Right wrist LUT check
    tree = ET.parse(xml_right)
    root = tree.getroot()
    
    MML = np.array([[0,0,0,0]])
    
    for elem in tree.iter('Coordinate'):
        #print(elem)
        tmprow = int(elem.get('Row'))
        tmpcol = int(elem.get('Column'))
        tmpx = int(elem.get('X'))
        tmpy = int(elem.get('Y'))
        MML = np.append(MML,np.array([[tmprow,tmpcol,tmpx,tmpy]]),axis=0)
    
    if MML[RightWrist,2] > 0 and MML[RightWrist,3] > 0:
        logfile.write('Right LUT: pass \n')
    else:
        logfile.write('Right LUT: fail \n')
        k_pass = 1
    # print('bijna')
## Final check

    if k_pass == 0:
        # print('Volunteer Scan: \033[1;32;40m PASS \033[0;30;40m')
        print('PASS')
        logfile.write('Volunteer Scan: PASS \n')
    else:
        print('FAIL')
        # print('Volunteer Scan: \033[1;31;40m FAIL \033[0;30;40m')
        logfile.write('Volunteer Scan: FAIL \n')
    
    logfile.close()
    
## moves PNG
source = os.listdir(dr)
for files in source:
    if files.endswith('.png'):
        shutil.move(os.path.join(dr,files), os.path.join(savePath,files))
