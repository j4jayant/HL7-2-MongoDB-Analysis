/*
Author: Jayant Singh
Website: www.j4jayant.com
Description: This class extracts HL7 messages from text file & stores them in MongoDB for analysis
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace j4jayant.HL7toMongoDB
{
    public class ProcessMessage
    {
        public string MessagePath { get; set; }
        public string DBServer { get; set; }
        public string DBPort { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }
        public string Feed { get; set; }

        public ProcessMessage()
        {
            MessagePath = String.Empty;
            DBServer = String.Empty;
            DBPort = String.Empty;
            DBUser = String.Empty;
            DBPassword = String.Empty;
            Feed = String.Empty;
        }

        /// <summary>
        /// Extract HL7 File & insert messages into MongoDB
        /// This method expects an argument of type LoadData to write messages on Form
        /// </summary>
        /// <param name="_frm">LoadData</param>
        public void ProcessFile(LoadData _frm)
        {
            List<String> Messages = extractMessagesFromFile(MessagePath);
            int MsgCount = Messages.Count;

            _frm.UpdateLabelTotal(MsgCount);

            String connectionString = "mongodb://" + DBServer + ":" + DBPort;
            
            if(DBUser.Length > 0 && DBPassword.Length > 0)
                connectionString = "mongodb://" + DBUser + ":" + DBPassword + "@" +DBServer + ":" + DBPort;

            MongoUrl url = new MongoUrl(connectionString);

            MongoClient mc = new MongoClient(url);
            MongoServer server = mc.GetServer();

            String DBName = Feed + "_Analysis";

            MongoDatabase AnalysisDB = server.GetDatabase(DBName);

            var msgCollection = AnalysisDB.GetCollection<BsonDocument>("Message");

            for (int index = 0; index < MsgCount; ++index)
            {
                _frm.UpdateLabelCurrent(index + 1);

                string strMsg = Messages[index];

                try
                {
                    HL7Helper h = new HL7Helper();

                    BsonDocument doc = h.GetBsonFromMessage(strMsg);
                    msgCollection.Insert(doc);
                }
                catch (Exception ex)
                {
                    StringBuilder buf = new StringBuilder();
                    buf.Append("Origional Message: " + strMsg);
                    buf.Append("\n");
                    buf.Append("Error: " + ex.Message);
                    _frm.UpdateActivity_Error(buf.ToString());

                    System.Threading.Thread.Sleep(100);
                }
            }
            _frm.UpdateActivity_Info("Processed File : " + MessagePath);
        }

        /// <summary>
        /// This method extracts HL7 messages from file & creates a List of HL7 messages
        /// </summary>
        /// <param name="MessagePath">File Path</param>
        /// <returns>List of HL7 messages</returns>
        private List<String> extractMessagesFromFile(String MessagePath)
        {
            List<String> Messages = new List<string>();

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(MessagePath))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String _Message = String.Empty;
                    String _Segment = String.Empty;
                    bool firstLine = true;
                    while ((_Segment = streamReader.ReadLine()) != null)
                    {
                        _Segment = _Segment.Trim(new char[] { '\v', '\r', '\n', ' ', Convert.ToChar(28) });
                        if (_Segment.Length > 0)
                        {
                            if (!firstLine && _Segment.StartsWith("MSH"))
                            {
                                Messages.Add(_Message);
                                _Message = _Segment + "\r";
                            }
                            else
                            {
                                _Message += _Segment + "\r";
                            }

                            firstLine = false;
                        }
                    }

                    //Add Last Message to list
                    if (_Message.Length > 4)
                        Messages.Add(_Message);
                }
            }

            return Messages;
        }
    }
}
