hl7-2-mongodb is a simple program to store HL7 messages in MongoDB for analysis. 

This program is release under MIT license

This program uses python-hl7 0.2.5 to parse messages of Health Level 7 (HL7) version 2.x into Python objects.

This program uses pymongo 2.5.2 for MongoDB operations.


This program has been tested on Windows 7 & Ubuntu 13.04 with python 2.7.5

To run the program install following prerequisites

python-hl7 0.2.5
pymongo 2.5.2

Windows 
--------
command prompt>python hl7-2-mongodb.py -f <HL7FeedName> -i <inputfilepath> [-d <dbServer> -p <dbPort>]


Linux
------

$./hl7-2-mongodb.py -f <HL7FeedName> -i <inputfilepath> [-d <dbServer> -p <dbPort>]

Note:
------
<HL7FeedName> required, is the name of message feed that you want to analyze i.e. ADT, MDM, ORU etc
<inputfilepath> required, is the file containing raw HL7 messages
<dbServer> optional, is the servername/ip of MongoDB server, if not provided default is localhost
<dbPort> optional, is the port on which MongoDB server is running, if not provided default is 27017



For HL7 message analysis follow articles

http://www.j4jayant.com/articles/hl7/33-hl7-analysis-mongodb-2
http://www.j4jayant.com/articles/hl7/32-hl7-analysis-mongodb