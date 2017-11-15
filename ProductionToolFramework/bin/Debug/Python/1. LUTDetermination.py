# Debug
a = "C:\\Users\\GWA\\Documents\\GitHub\\Demcon\\ProductionToolFramework\\ProductionToolFramework\\bin\\Debug\\Python"
b = "C:\\Users\\GWA\\Desktop\\Internship DEMCON\\1. Matlab to Python Project\\20161205_094405_297_grid4"
c = "C:\\Users\\GWA\\Desktop\\Internship DEMCON\\1. Matlab to Python Project\\20161205_094535_297_grid"
d = 0.4
e = 500000
f = 50
g = "YES"



# import sys
# # Python output directory
# # a = str(sys.argv[1])  
# a0 = str(sys.argv[1])  
# # a0 = a0.replace("\\","\\\\")
# a1 = a0.rsplit("\\",1)
# # a2 = a1[0]+"\\figure"
# a = a1[0];
# # print(a)

# # GRID 4 location
# b = str(sys.argv[2])   
# b = b.replace("\\","\\\\")
# # GRID location
# c = str(sys.argv[3])   
# c = c.replace("\\","\\\\")

# # Measurement variables
# d = float(sys.argv[4]) # Intensity
# e = float(sys.argv[5]) # Mass
# f = float(sys.argv[6]) # Distance
# g = str(sys.argv[7]) #print XML


## Setting Work Directory
import os.path
os.chdir(a)

## Import library
from library.lut_photo_processing import *
from library.lut_table_compilation import *
from library.lut_file_organizer import *

## import as CSV
# np.savetxt('row_dim.csv',np.array(row_ind,dtype='int'),delimiter=",")

## Path Variable
output_path    = a + "\\figure"
data4_path     = b
data_path      = c

## Setting Variable
thrIntensity = d
thrMass		 = e
thrDistance	 = f

## Program Sequence
print("30,Left hand photo to grid processing")
grid_left,grid4_left = lut_photo_processing('left', data4_path, data_path, True, thrIntensity, thrMass, thrDistance)

print("40,Right hand photo to grid processing")
grid_right,grid4_right = lut_photo_processing('right', data4_path, data_path, True, thrIntensity, thrMass, thrDistance)

# organize the output file
print("60,Compressing the result")
lut_file_organizer(a,output_path)

print("80,Compiling the LUTs")
lut_table_compilation(grid_left, grid_right, output_path)

print("100,LUT Determination: PASS")



