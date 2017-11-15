## Setting Work Directory
import os.path
os.chdir('C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\-. Python Working Dir\\')

## Import library
from library.lut_photo_processing import *
from library.lut_table_compilation import *

## import as CSV
# np.savetxt('row_dim.csv',np.array(row_ind,dtype='int'),delimiter=",")


## Setting Variable
thrIntensity = 0.4
thrMass = 500000
thrDistance = 50

## Path Variable
data4_path     = "C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\1. Matlab to Python Project\\20161205_094405_297_grid4\\"
data_path      = "C:\\Users\\Girindra\\Desktop\\Internship Hunter\\1. Internship DEMCON\\1. Matlab to Python Project\\20161205_094535_297_grid\\"
output_path    = "D:\\" 

## Program Sequence
print("Left hand photo to grid processing")
grid_left,grid4_left = lut_photo_processing('left', data4_path, data_path, True, thrIntensity, thrMass, thrDistance)

print("Right hand photo to grid processing")
grid_right,grid4_right = lut_photo_processing('right', data4_path, data_path, True, thrIntensity, thrMass, thrDistance)

print("Compiling the LUTs")
lut_table_compilation(grid_left, grid_right, output_path)

print("Finish")



