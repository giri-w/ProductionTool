def write_xml(table,name_of_file):
  from lxml import etree
  import os.path
  motorMatrix = etree.Element('MotorMatrix')
  coordinates = etree.SubElement(motorMatrix,'Coordinates')
  for i in range (0,len(table)):
    child2 = etree.SubElement(coordinates, 'Coordinate')
    child2.set('Column',str(table[i].Column))
    child2.set('Row',str(table[i].Row))
    child2.set('X',str(table[i].X))
    child2.set('Y',str(table[i].Y))

  #Save location
  save_path = 'D:/'
  #name_of_file = ("MotorMatrix")
  completeName = os.path.join(save_path,name_of_file)

  tree = etree.ElementTree(motorMatrix)
  tree.write(completeName, pretty_print=True)
  return