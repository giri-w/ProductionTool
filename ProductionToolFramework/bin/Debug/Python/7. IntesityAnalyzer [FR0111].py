import numpy as np
import matplotlib.image as mpimg
import matplotlib.pyplot as plt
from scipy import misc as img 
import scipy.ndimage.filters as filt

def LeftHand_Data (im_pathL,i,j):
    ## Read Left Image Data
    imLeft = img.imread(im_pathL)
    plt.figure(i*10 + 1)
    plt.imshow(imLeft,cmap='gray')
    plt.title(j)
    data1_temp = np.array(imLeft[222:250,157:415])
    data1 = np.array((np.sum(data1_temp,axis=0)/len(data1_temp)),dtype='int')
    # data1 = np.array(imLeft[222,157:415])
    data1 = filt.gaussian_filter1d(data1, 1)
    
    Lindex_loc1 = 203
    Lindex_loc2 = 227
    # max index finger
    Lindex_int = max(data1[Lindex_loc1:Lindex_loc2])
    Lindex_ind = np.where(data1 == Lindex_int)[0]
    
    
    # max middle finger
    Lmiddle_loc1 = 144
    Lmiddle_loc2 = 166
    Lmiddle_int = max(data1[Lmiddle_loc1:Lmiddle_loc2])
    Lmiddle_ind = np.where(data1 == Lmiddle_int)[0]
    
    
    # max ring finger
    LRing_loc1 = 90
    LRing_loc2 = 107
    Lring_int = max(data1[LRing_loc1:LRing_loc2])
    Lring_ind = np.where(data1 == Lring_int)[0]
    
    
    # max little finger
    Llittle_loc1 = 30
    Llittle_loc2 = 50
    Llittle_int = max(data1[Llittle_loc1:Llittle_loc2])
    Llittle_ind = np.where(data1 == Llittle_int)[0]
    
    
    # between index and middle finger
    Linmi_int = min(data1[Lmiddle_loc2:Lindex_loc1])
    Linmi_ind = np.array(np.where(data1 == Linmi_int))
    Linmi_ind = Linmi_ind[Linmi_ind >= np.array(Lmiddle_loc2)]
    
    # between middle and ring finger
    Lmiri_int = min(data1[LRing_loc2:Lmiddle_loc1])
    Lmiri_ind = np.array(np.where(data1 == Lmiri_int))
    Lmiri_ind = Lmiri_ind[Lmiri_ind >= np.array(LRing_loc2)]
    
    # between ring and little finger
    Lrili_int = min(data1[Llittle_loc2:LRing_loc1])
    Lrili_ind = np.array(np.where(data1 == Lrili_int))
    Lrili_ind = Lrili_ind[Lrili_ind >= np.array(Llittle_loc2)]
    
    
    figL = plt.figure(i*10 + 2)
    plt.plot(data1)
    plt.title(j)
    # title('Left Hand NIR')
    # show image
    plt.show()  
    
    
    # maximum fingers intensity
    # index value
    Lindex_msg = 'index max : ' + str(Lindex_int)
    plt.text((Lindex_ind[0]),float(Lindex_int),Lindex_msg)
    # middle value
    Lmiddle_msg = 'middle max : ' + str(Lmiddle_int)
    plt.text((Lmiddle_ind[0]),float(Lmiddle_int),Lmiddle_msg)
    # ring value
    Lring_msg = 'ring max : ' + str(Lring_int)
    plt.text((Lring_ind[0]),float(Lring_int),Lring_msg)
    # index value
    Llittle_msg = 'little max : ' + str(Llittle_int)
    plt.text((Llittle_ind[0]),float(Llittle_int),Llittle_msg)
    
    # minimum between finger intensity
    # index to middle finger
    Linmi_msg = 'index to middle min. int: '+str(Linmi_int)
    plt.text((Linmi_ind[0]),float(Linmi_int),Linmi_msg)
    # middle to ring finger
    Lmiri_msg = 'middle to ring min. int: '+str(Lmiri_int)
    plt.text((Lmiri_ind[0]),float(Lmiri_int),Lmiri_msg)
    # ring to little finger
    Lrili_msg = 'ring to little min. int: '+str(Lrili_int)
    plt.text((Lrili_ind[0]),float(Lrili_int),Lrili_msg)
    
    LFinger_value = np.array((Lindex_int, Lmiddle_int, Lring_int, Llittle_int))
    Lbtw_value = np.array([Linmi_int,Lmiri_int,Lrili_int])
    Lint_value = np.array((0,0,0,0),dtype='float')
    Lint_value[0] = np.round((Lbtw_value[0]/(LFinger_value[0]-Lbtw_value[0])),decimals=3)*100
    Lint_value[1] = np.round((((Lbtw_value[0]+Lbtw_value[1])/2)/(LFinger_value[1]-((Lbtw_value[0]+Lbtw_value[1])/2))),decimals=3)*100
    Lint_value[2] = np.round((((Lbtw_value[1]+Lbtw_value[2])/2)/(LFinger_value[2]-((Lbtw_value[1]+Lbtw_value[2])/2))),decimals=3)*100
    Lint_value[3] = np.round((Lbtw_value[2]/(LFinger_value[3]-Lbtw_value[2])),decimals=3)*100
    
    return LFinger_value,Lbtw_value,Lint_value


def RightHand_Data(im_pathR,i,j):
    ## Read Right Image Data
    imRight = img.imread(im_pathR)
    plt.figure(i*10 + 1)
    plt.imshow(imRight,cmap='gray')
    plt.title(j)
    data2_temp = imRight[222:250,120:360]
    data2 = np.array((np.sum(data2_temp,axis=0)/len(data2_temp)),dtype='int')
    # data2 = imRight[250,120:360]
    data2 = filt.gaussian_filter1d(data2, 1)
 
    Rindex_loc1 = 36
    Rindex_loc2 = 60
    # max index finger
    index_int = max(data2[Rindex_loc1:Rindex_loc2])
    index_ind = np.where(data2 == index_int)[0]
    
    
    # max middle finger
    Rmiddle_loc1 = 109
    Rmiddle_loc2 = 127
    middle_int = max(data2[Rmiddle_loc1:Rmiddle_loc2])
    middle_ind = np.where(data2 == middle_int)[0]
    
    
    # max ring finger
    RRing_loc1 = 155
    RRing_loc2 = 175
    ring_int = max(data2[RRing_loc1:RRing_loc2])
    ring_ind = np.where(data2 == ring_int)[0]
    
    # max little finger
    Rlittle_loc1 = 210
    Rlittle_loc2 = 230
    little_int = max(data2[Rlittle_loc1:Rlittle_loc2])
    little_ind = np.where(data2 == little_int)[0]
    
    # between index and middle finger
    inmi_int = min(data2[Rindex_loc2:Rmiddle_loc1])
    inmi_ind = np.array(np.where(data2 == inmi_int))
    inmi_ind = inmi_ind[inmi_ind >= np.array(Rindex_loc2)]
    
    # between middle and ring finger
    miri_int = min(data2[Rmiddle_loc2:RRing_loc1])
    miri_ind = np.array(np.where(data2 == miri_int))
    miri_ind = miri_ind[miri_ind >= np.array(Rmiddle_loc2)]
    
    # between ring and little finger
    rili_int = min(data2[RRing_loc2:Rlittle_loc1])
    rili_ind = np.array(np.where(data2 == rili_int))
    rili_ind = rili_ind[rili_ind >= np.array(RRing_loc2)]
    
    figL = plt.figure(i*10 + 2)
    plt.plot(data2)
    plt.title(j)
    # title('Right Hand NIR')
    # show image
    plt.show()  
    
    # maximum fingers intensity
    # index value
    index_msg = 'index max : ' + str(index_int)
    plt.text((index_ind[0]),float(index_int),index_msg)
    # middle value
    middle_msg = 'middle max : ' + str(middle_int)
    plt.text((middle_ind[0]),float(middle_int),middle_msg)
    # ring value
    ring_msg = 'ring max : ' + str(ring_int)
    plt.text((ring_ind[0]),float(ring_int),ring_msg)
    # index value
    little_msg ='little max : ' + str(little_int)
    plt.text((little_ind[0]),float(little_int),little_msg)
    
    # minimum between finger intensity
    # index to middle finger
    inmi_msg = 'index to middle min. int: ' + str(inmi_int)
    plt.text((inmi_ind[0]),float(inmi_int),inmi_msg)
    # middle to ring finger
    miri_msg = 'middle to ring min. int: '+ str(miri_int)
    plt.text((miri_ind[0]),float(miri_int),miri_msg)
    # ring to little finger
    rili_msg = 'ring to little min. int: ' + str(rili_int)
    plt.text((rili_ind[0]),float(rili_int),rili_msg)
    
    RFinger_value = np.array((index_int, middle_int, ring_int, little_int))
    Rbtw_value = np.array([inmi_int,miri_int,rili_int])
    Rint_value = np.array((0,0,0,0),dtype='float')
    Rint_value[0] = np.round((Rbtw_value[0]/(RFinger_value[0]-Rbtw_value[0])),decimals=3) * 100
    Rint_value[1] = np.round((((Rbtw_value[0]+Rbtw_value[1])/2)/(RFinger_value[1]-((Rbtw_value[0]+Rbtw_value[1])/2))),decimals=3) * 100
    Rint_value[2] = np.round((((Rbtw_value[1]+Rbtw_value[2])/2)/(RFinger_value[2]-((Rbtw_value[1]+Rbtw_value[2])/2))),decimals=3) * 100
    Rint_value[3] = np.round((Rbtw_value[2]/(RFinger_value[3]-Rbtw_value[2])),decimals=3) * 100
    
    return RFinger_value,Rbtw_value,Rint_value
    
## Main Program
plt.close('all')
im_pathLnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\1. IntensityAnalyzer\\white paper\\intensity 31\\left_high_nir.png'
im_pathLvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\1. IntensityAnalyzer\\white paper\\intensity 31\\left_high_vis.png'
im_pathRnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\1. IntensityAnalyzer\\white paper\\intensity 31\\right_high_nir.png'
im_pathRvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\1. IntensityAnalyzer\\white paper\\intensity 31\\right_high_vis.png'


LnirFinger_value,Lnirbtw_value,Lnirint_value = LeftHand_Data(im_pathLnir,1,'Left NIR')
LvisFinger_value,Lvisbtw_value,Lvisint_value = LeftHand_Data(im_pathLvis,2,'Left VIS')

RnirFinger_value,Rnirbtw_value,Rnirint_value = RightHand_Data(im_pathRnir,3,'Right NIR')
RvisFinger_value,Rvisbtw_value,Rvisint_value = RightHand_Data(im_pathRvis,4,'Right VIS')

# print Value
print((' Left NIR value : [IND] '+str(Lnirint_value[0])+'% , [MID] '+str(Lnirint_value[1])+'% , [RNG] '+str(Lnirint_value[2])+'% , [LTL] '+str(Lnirint_value[3])+'%'))
print((' Left VIS value : [IND] '+str(Lvisint_value[0])+'% , [MID] '+str(Lvisint_value[1])+'% , [RNG] '+str(Lvisint_value[2])+'% , [LTL] '+str(Lvisint_value[3])+'%'))
print(('Right NIR value : [IND] '+str(Rnirint_value[0])+'% , [MID] '+str(Rnirint_value[1])+'% , [RNG] '+str(Rnirint_value[2])+'% , [LTL] '+str(Rnirint_value[3])+'%'))
print(('Right VIS value : [IND] '+str(Rvisint_value[0])+'% , [MID] '+str(Rvisint_value[1])+'% , [RNG] '+str(Rvisint_value[2])+'% , [LTL] '+str(Rvisint_value[3])+'%'))