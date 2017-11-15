import numpy as np
import matplotlib.image as mpimg
import matplotlib.pyplot as plt
from scipy import misc as img 
import scipy.ndimage.filters as filt
    
## Main Program
plt.close('all')

## Image Path
# intensity 01
im_01_RawLnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int1\\left_high_nir.png'
im_01_RawLvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int1\\left_high_vis.png'
im_01_PDLnir  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int1\\left_mm_nir_photodiode_001.png'
im_01_PDLvis  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int1\\left_mm_vis_photodiode_000.png'

# intensity 02
im_02_RawLnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int2\\left_high_nir.png'
im_02_RawLvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int2\\left_high_vis.png'
im_02_PDLnir  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int2\\left_mm_nir_photodiode_001.png'
im_02_PDLvis  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int2\\left_mm_vis_photodiode_000.png'

# intensity 04
im_04_RawLnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int4\\left_high_nir.png'
im_04_RawLvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int4\\left_high_vis.png'
im_04_PDLnir  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int4\\left_mm_nir_photodiode_001.png'
im_04_PDLvis  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int4\\left_mm_vis_photodiode_000.png'

# intensity 08
im_08_RawLnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int8\\left_high_nir.png'
im_08_RawLvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int8\\left_high_vis.png'
im_08_PDLnir  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int8\\left_mm_nir_photodiode_001.png'
im_08_PDLvis  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int8\\left_mm_vis_photodiode_000.png'

# intensity 16
im_16_RawLnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int16\\left_high_nir.png'
im_16_RawLvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int16\\left_high_vis.png'
im_16_PDLnir  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int16\\left_mm_nir_photodiode_001.png'
im_16_PDLvis  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int16\\left_mm_vis_photodiode_000.png'

# intensity 24
im_24_RawLnir = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int24\\left_high_nir.png'
im_24_RawLvis = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int24\\left_high_vis.png'
im_24_PDLnir  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int24\\left_mm_nir_photodiode_001.png'
im_24_PDLvis  = 'C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\4. Data Acquisition\\2. IntensityRatio [FR0113]\\source\\int24\\left_mm_vis_photodiode_000.png'

## Point Location
pRAWnir = np.array(([303,188],[341,264],[290,286],[316,345],[203,363],[189,328],[357,175],[417,290],[264,229],[253,339]))
pRAWvis = np.array(([315,214],[368,316],[228,275],[267,371],[371,197],[431,314],[252,200],[305,314],[166,259],[191,327]))
pPDnir  = np.array(([15,24],[15,94],[25,24],[25,94],[35,24],[35,94],[45,24],[45,94],[55,24],[55,94]))
pPDvis  = np.array(([15,24],[15,94],[25,24],[25,94],[35,24],[35,94],[45,24],[45,94],[55,24],[55,94]))

## Image Data
a1  = img.imread(im_01_RawLnir)
a2  = img.imread(im_02_RawLnir)
a4  = img.imread(im_04_RawLnir)
a8  = img.imread(im_08_RawLnir)
a16 = img.imread(im_16_RawLnir)
a24 = img.imread(im_24_RawLnir)

b1  = img.imread(im_01_RawLvis)
b2  = img.imread(im_02_RawLvis)
b4  = img.imread(im_04_RawLvis)
b8  = img.imread(im_08_RawLvis)
b16 = img.imread(im_16_RawLvis)
b24 = img.imread(im_24_RawLvis)

c1  = img.imread(im_01_PDLnir)
c2  = img.imread(im_02_PDLnir)
c4  = img.imread(im_04_PDLnir)
c8  = img.imread(im_08_PDLnir)
c16 = img.imread(im_16_PDLnir)
c24 = img.imread(im_24_PDLnir)

d1  = img.imread(im_01_PDLvis)
d2  = img.imread(im_02_PDLvis)
d4  = img.imread(im_04_PDLvis)
d8  = img.imread(im_08_PDLvis)
d16 = img.imread(im_16_PDLvis)
d24 = img.imread(im_24_PDLvis)

## Intensity Measurement
intRAWnir = np.array(([0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0]),dtype='float')
intRAWvis = np.array(([0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0]),dtype='float')
intPDnir  = np.array(([0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0]),dtype='float')
intPDvis  = np.array(([0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0],[0,0,0,0,0,0]),dtype='float')

for i in range (0,len(pRAWnir)):
    i1RAWnir  = np.round((a1[pRAWnir[i,0],pRAWnir[i,1]] / a8[pRAWnir[i,0],pRAWnir[i,1]]),decimals=2)
    i2RAWnir  = np.round((a2[pRAWnir[i,0],pRAWnir[i,1]] / a8[pRAWnir[i,0],pRAWnir[i,1]]),decimals=2)
    i4RAWnir  = np.round((a4[pRAWnir[i,0],pRAWnir[i,1]] / a8[pRAWnir[i,0],pRAWnir[i,1]]),decimals=2)
    i8RAWnir  = np.round((a8[pRAWnir[i,0],pRAWnir[i,1]] / a8[pRAWnir[i,0],pRAWnir[i,1]]),decimals=2)
    i16RAWnir = np.round((a16[pRAWnir[i,0],pRAWnir[i,1]]/ a8[pRAWnir[i,0],pRAWnir[i,1]]),decimals=2)
    i24RAWnir = np.round((a24[pRAWnir[i,0],pRAWnir[i,1]]/ a8[pRAWnir[i,0],pRAWnir[i,1]]),decimals=2)
    intRAWnir[i] = [i1RAWnir,i2RAWnir,i4RAWnir,i8RAWnir,i16RAWnir,i24RAWnir]
    
    i1RAWvis  = np.round((b1[pRAWvis[i,0],pRAWvis[i,1]] / b8[pRAWvis[i,0],pRAWvis[i,1]]),decimals=2)
    i2RAWvis  = np.round((b2[pRAWvis[i,0],pRAWvis[i,1]] / b8[pRAWvis[i,0],pRAWvis[i,1]]),decimals=2)
    i4RAWvis  = np.round((b4[pRAWvis[i,0],pRAWvis[i,1]] / b8[pRAWvis[i,0],pRAWvis[i,1]]),decimals=2)
    i8RAWvis  = np.round((b8[pRAWvis[i,0],pRAWvis[i,1]] / b8[pRAWvis[i,0],pRAWvis[i,1]]),decimals=2)
    i16RAWvis = np.round((b16[pRAWvis[i,0],pRAWvis[i,1]]/ b8[pRAWvis[i,0],pRAWvis[i,1]]),decimals=2)
    i24RAWvis = np.round((b24[pRAWvis[i,0],pRAWvis[i,1]]/ b8[pRAWvis[i,0],pRAWvis[i,1]]),decimals=2)
    intRAWvis[i] = [i1RAWvis,i2RAWvis,i4RAWvis,i8RAWvis,i16RAWvis,i24RAWvis]
    
    i1PDnir  = np.round((c1[pPDnir[i,0],pPDnir[i,1]] / c8[pPDnir[i,0],pPDnir[i,1]]),decimals=2)
    i2PDnir  = np.round((c2[pPDnir[i,0],pPDnir[i,1]] / c8[pPDnir[i,0],pPDnir[i,1]]),decimals=2)
    i4PDnir  = np.round((c4[pPDnir[i,0],pPDnir[i,1]] / c8[pPDnir[i,0],pPDnir[i,1]]),decimals=2)
    i8PDnir  = np.round((c8[pPDnir[i,0],pPDnir[i,1]] / c8[pPDnir[i,0],pPDnir[i,1]]),decimals=2)
    i16PDnir = np.round((c16[pPDnir[i,0],pPDnir[i,1]]/ c8[pPDnir[i,0],pPDnir[i,1]]),decimals=2)
    i24PDnir = np.round((c24[pPDnir[i,0],pPDnir[i,1]]/ c8[pPDnir[i,0],pPDnir[i,1]]),decimals=2)
    intPDnir[i] = [i1PDnir,i2PDnir,i4PDnir,i8PDnir,i16PDnir,i24PDnir]
    
    i1PDvis  = np.round((d1[pPDvis[i,0],pPDvis[i,1]] / d8[pPDvis[i,0],pPDvis[i,1]]),decimals=2)
    i2PDvis  = np.round((d2[pPDvis[i,0],pPDvis[i,1]] / d8[pPDvis[i,0],pPDvis[i,1]]),decimals=2)
    i4PDvis  = np.round((d4[pPDvis[i,0],pPDvis[i,1]] / d8[pPDvis[i,0],pPDvis[i,1]]),decimals=2)
    i8PDvis  = np.round((d8[pPDvis[i,0],pPDvis[i,1]] / d8[pPDvis[i,0],pPDvis[i,1]]),decimals=2)
    i16PDvis = np.round((d16[pPDvis[i,0],pPDvis[i,1]]/ d8[pPDvis[i,0],pPDvis[i,1]]),decimals=2)
    i24PDvis = np.round((d24[pPDvis[i,0],pPDvis[i,1]]/ d8[pPDvis[i,0],pPDvis[i,1]]),decimals=2)
    intPDvis[i] = [i1PDvis,i2PDvis,i4PDvis,i8PDvis,i16PDvis,i24PDvis]

expIntensity = np.array([0.125, 0.25, 0.5, 1, 1.519, 2.369], dtype='float')

## Plot NIR Image
plt.figure(0,figsize=(9,4.5))
plt.subplot(121)
plt.title('NIR Camera Intensity')
plt.xlabel('Expected Intensity')
plt.ylabel('Intensity')
plt.axis([0,2.5,0,3.0])
for i in range (0,len(pRAWnir)):
    plt.plot(expIntensity,intRAWnir[i])


plt.subplot(122)
plt.title('NIR PD Intensity')
plt.xlabel('Expected Intensity')
plt.ylabel('Intensity')
plt.axis([0,2.5,0,3.0])
for i in range (0,len(pRAWnir)):
    plt.plot(expIntensity,intPDnir[i])

plt.show()

## Plot VIS Image
plt.figure(1,figsize=(9,4.5))
plt.subplot(121)
plt.title('VIS Camera Intensity')
plt.xlabel('Expected Intensity')
plt.ylabel('Intensity')
plt.axis([0,2.5,0,3.0])
for i in range (0,len(pRAWnir)):
    plt.plot(expIntensity,intRAWvis[i])

plt.subplot(122)
plt.title('VIS PD Intensity')
plt.xlabel('Expected Intensity')
plt.ylabel('Intensity')
plt.axis([0,2.5,0,3.0])
for i in range (0,len(pRAWnir)):
    plt.plot(expIntensity,intPDvis[i])
plt.show()

## Relative Error NIR and VIS
errNIR = 1 + ((intRAWnir-intPDnir)/intRAWnir)
errVIS = 1 + ((intRAWvis-intPDvis)/intRAWvis)

plt.figure(2,figsize=(9,4.5))

plt.subplot(121)
plt.title('NIR Relative Error')
plt.xlabel('Expected Intensity')
plt.ylabel('Relative Error')
plt.axis([0,2.5,0.5,1.5])
for i in range (0,len(pRAWnir)):
    plt.plot(expIntensity,errNIR[i])

plt.subplot(122)
plt.title('VIS Relative Error')
plt.xlabel('Expected Intensity')
plt.ylabel('Relative Error')
plt.axis([0,2.5,0.5,1.5])
for i in range (0,len(pRAWnir)):
    plt.plot(expIntensity,errVIS[i])
plt.show()
