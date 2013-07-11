hl7-2-mongodb is a simple program to store HL7 messages in MongoDB for analysis. 


This library uses python-hl7 0.2.5 to parse messages of Health Level 7 (HL7) version 2.x into Python objects.

This library uses pymongo 2.5.2 for MongoDB operations.


This program has been tested on Windows 7 & Ubuntu 13.04 with python 2.7.5

To run the program type

install python-hl7 0.2.5 module
install pymongo 

Windows 
--------
command prompt>python hl7-2-mongodb.py -i <inputfilepath>

Linux
------

$./hl7-2-mongodb.py -i <inputfilepath>

<inputfilepath> is the file containing raw HL7 messages

For HL7 message analysis follow articles

http://www.j4jayant.com/articles/hl7/33-hl7-analysis-mongodb-2
http://www.j4jayant.com/articles/hl7/32-hl7-analysis-mongodb