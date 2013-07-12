#!/usr/bin/python
"""hl7-2-mongodb is a simple program to store HL7 messages in MongoDB for analysis. 
This library uses python-hl7 0.2.5 to parse messages of Health Level 7 (HL7) version 2.x into Python objects.
This library uses pymongo 2.5.2 for MongoDB operations
This program has been tested on Windows 7 & Ubuntu 13.04 with python 2.7.5
"""

__version__ = '1.0.1'
__author__ = 'Jayant Singh'
__license__ = 'MIT'
__copyright__ = 'Copyright 2013, Jayant Singh <www.j4jayant.com>'
__url__ = 'http://www.j4jayant.com'

import sys, getopt
import hl7
from pymongo import *

def hl72MongoDocument(h):
    msh_10 = h.segment('MSH')[9][0];
    event = h.segment('MSH')[8][1];
    msgdt = h.segment('MSH')[6][0];

    _segments = [];
    segIndex = 1;
    segDic = {};
    for seg in h:
        #print (seg.separator)
        segName = unicode(seg[0])
        segVal = unicode(seg)
        fieldIndex = 1
        fieldCount = 1
        _fields = []
        seg.pop(0)
        if(segName == 'MSH'):
            fieldDoc = {'_id':'MSH_1','Val': seg.separator}
            _fields.append(fieldDoc)
            fieldCount += 1
            fieldIndex += 1
        for field in seg:
            fieldName = segName+'_'+unicode(fieldIndex)
            fieldVal = unicode(field)
            hasRepetitions = False;
            if fieldVal:
                fieldDoc = {'_id': fieldName,'Val': fieldVal}
                
                if ('~' in fieldVal and fieldName != 'MSH_2'):
                    hasRepetitions = True;
                    _repfields = []
                    repFields = fieldVal.split('~');
                    repIndex = 1;
                    for repField in repFields:
                        if repField:
                            repFieldVal = unicode(repField);
                            fieldName = segName+'_'+unicode(fieldIndex)
                            fieldDoc = {'_id': fieldName,'Val': repFieldVal, 'Rep': repIndex}
                            _repfields.append(fieldDoc)
                            
                            if('^' in repFieldVal):
                                repFieldComps = repFieldVal.split('^');
                                comIndex = 1;
                                for repFieldComp in repFieldComps:
                                    repFieldCompVal = unicode(repFieldComp);
                                    comName = segName+'_'+unicode(fieldIndex)+'_'+unicode(comIndex)
                                    if repFieldCompVal:
                                        fieldDoc = {'_id': comName,'Val': repFieldCompVal, 'Rep': repIndex}
                                        _repfields.append(fieldDoc)
                                    comIndex += 1
                        repIndex += 1;	
							
                    fieldDoc = {'_id': fieldName,'Val': fieldVal, 'Repetitions': _repfields}
					
                _fields.append(fieldDoc)
                fieldCount += 1
				
                if (hasRepetitions == False and len(field) > 1 and fieldName != 'MSH_2'):
                    comIndex = 1
                    for component in field:
                        comName = segName+'_'+unicode(fieldIndex)+'_'+unicode(comIndex)
                        comVal = unicode(component)
                        if comVal:
                            fieldDoc = {'_id': comName,'Val': comVal}
                            _fields.append(fieldDoc)
                        comIndex += 1
            fieldIndex += 1

        if segName in segDic:
            segDic[segName] = segDic[segName] + 1;
        else:
            segDic[segName] = 1;
		
        segDoc ={'_id': segName, 'Rep': segDic[segName], 'Seq': segIndex, 'Val': segVal, 'FC': fieldIndex-1, 'VF': fieldCount-1, 'Fields': _fields}
        _segments.append(segDoc)
        segIndex += 1

    hl7doc = {'_id': msh_10, 'Event': event, 'MsgDt': msgdt, 'Segments': _segments}
	
    return hl7doc

def readHL7File(ifile):
	firstLine = True;
	Messages = [];
	strMessage = '';
	strSegment = '';
	try:
		with open(ifile) as infile:
			for line in infile:
				line = line.strip();
				if(len(line) > 0):
					if(firstLine == False and line.startswith('MSH|^~\&')):
						Messages.append(strMessage);
						strMessage = line + "\r";
					else:
						strMessage += line + "\r";
					firstLine = False;
			if(len(strMessage) >= 4):
				Messages.append(strMessage);
		return Messages;
	except:
		print('File error'+ unicode(sys.exc_info()[0]) + "\n");
		return '';

		
#print (Messages);

def insertDocuments(Messages, feed, db, port):
    dbServer = 'localhost';
    dbPort = 27017;
    
    if len(db) > 0:
        dbServer = db;
    if len(port) > 0:
        try:
            dbPort = int(port);
        except:
            print("Invalid db port provided");
            return "";
    
    client = MongoClient(dbServer, dbPort);
    dbName = feed + '_Analysis';
    db = client[dbName];
    message_collection = db['Message'];
    totalMessages = len(Messages);
    msgCount = 1;
    print("Total Messages: " + str(totalMessages));
    for message in Messages:
        try:
            print("Processing Message " + str(msgCount) + " of " + str(totalMessages));
            h = hl7.parse(message);
            doc = hl72MongoDocument(h);
            doc_id = message_collection.insert(doc);
            print("Inserted Message " + str(msgCount) + " with _id: " + doc_id);
        except:
            print("Error inserting document " + str(msgCount) + " Error: " + unicode(sys.exc_info()[0]) + "\n");
        msgCount += 1;


total = len(sys.argv)
cmdargs = str(sys.argv)
ifile=''
feed=''
db=''
port=''

try:
    myopts, args = getopt.getopt(sys.argv[1:],"f:i:d:p:")
    if len(myopts) <= 0:
        print("Invalid number of arguments detected\n");
        print("Usage: %s -f feed -i input -d dbServer -p dbPort" % sys.argv[0]+"\n")
		
    for o, a in myopts:
        if o == '-i':
            ifile=a;
        elif o == '-f':
            feed=a;
        elif o == '-d':
            db=a;
        elif o == '-p':
            port=a;
        else:
            print("Usage: %s -f feed -i input -d dbServer -p dbPort" % sys.argv[0]+"\n")
except:
    print("Please enter valid arguments");
    print("Usage: %s -f feed -i input -d dbServer -p dbPort" % sys.argv[0]+"\n")
	
if (len(ifile) > 0 and len(feed) > 0):
    print('Feed Name: ' + feed);
    print('Input File: ' + ifile);
	
    Messages = readHL7File(ifile);
    try:
        if(isinstance(Messages,list)):
            insertDocuments(Messages, feed, db, port);
        else:
            print("No messages to process\n");
    except:
        print("Error inserting document"+ unicode(sys.exc_info()[0]) + "\n");
else:
	print("No Input file or Feed Name detected");


