/*
Author: Jayant Singh
Website: www.j4jayant.com
Description: This class parses HL7 messages to access individual elements
 * This class also provides function to convert HL7 message into BsonDocument
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace j4jayant.HL7toMongoDB
{
    /// <summary>
    /// HL7Helper class to parse HL7 message & convert it to BsonDocument
    /// </summary>
    public class HL7Helper
    {
        /// <summary>
        /// Parse HL7 message
        /// </summary>
        /// <param name="strMessage">HL7 message in text format</param>
        /// <returns>Custom type Message</returns>
        private Message Parse(string strMessage)
        {
            Message msg = new Message();

            msg.Value = strMessage;

            List<Segment> lstSegments = new List<Segment>();
            List<String> tmpSegs = new List<String>();

            tmpSegs = strMessage.Split(new Char[2] { '\r', '\n' }, StringSplitOptions.None).ToList<String>();

            foreach (String strSeg in tmpSegs)
            {
                String segment = strSeg;
                segment = strSeg.Trim(new Char[3] { '\r', '\n', ' ' });
                if (segment.Length > 4) // 3 chars for seg name & 1 for field separator
                {
                    List<Field> lstFields = new List<Field>();
                    List<String> tmpFields = new List<String>();

                    tmpFields = strSeg.Split(new Char[1] { '|' }, StringSplitOptions.None).ToList<String>();

                    foreach (String strField in tmpFields)
                    {
                        if (!strField.Contains("~") || strField.Equals("^~\\&"))
                        {
                            List<Component> lstComps = new List<Component>();
                            List<String> tmpComps = new List<String>();

                            tmpComps = strField.Split(new Char[1] { '^' }, StringSplitOptions.None).ToList<String>();

                            if (tmpComps.Count > 1 && !strField.Equals("^~\\&"))
                            {
                                foreach (String strVal in tmpComps)
                                {
                                    Component cmp = new Component();
                                    cmp.Value = strVal;
                                    lstComps.Add(cmp);
                                }
                            }

                            Field field = new Field();
                            field.Value = strField;
                            field.Components = lstComps;
                            lstFields.Add(field);

                            if (strField.Equals("MSH"))
                            {
                                Field field1 = new Field();
                                field1.Value = "|";
                                lstFields.Add(field1);
                            }
                        }
                        else
                        {
                            List<Field> _RepetitionList = new List<Field>();
                            List<String> InduvidualFields = strField.Split(new Char[1] { '~' }, StringSplitOptions.None).ToList<String>();

                            for (int index = 0; index < InduvidualFields.Count; index++)
                            {
                                String strFieldVal = InduvidualFields[index];
                                ///
                                List<Component> lstComps = new List<Component>();
                                List<String> tmpComps = new List<String>();

                                tmpComps = strFieldVal.Split(new Char[1] { '^' }, StringSplitOptions.None).ToList<String>();

                                if (tmpComps.Count > 1 && !strField.Equals("^~\\&"))
                                {
                                    foreach (String strVal in tmpComps)
                                    {
                                        Component cmp = new Component();
                                        cmp.Value = strVal;
                                        lstComps.Add(cmp);
                                    }
                                }
                                ///
                                Field field = new Field();
                                field.Value = strFieldVal;
                                field.Components = lstComps;
                                //lstFields.Add(field);

                                _RepetitionList.Add(field);
                            }

                            Field fieldWithRep = new Field();
                            fieldWithRep.Value = strField;
                            fieldWithRep.Repetitions = _RepetitionList;
                            lstFields.Add(fieldWithRep);
                        }
                    }
                    Segment seg = new Segment();
                    seg.Value = strSeg;
                    seg.Fields = lstFields;
                    lstSegments.Add(seg);
                }
            }
            msg.Segments = lstSegments;
            return msg;
        }

        /// <summary>
        /// Converts HL7 message to BsonDocument format to store in MongoDB
        /// </summary>
        /// <param name="strHL7">HL7 message in text format</param>
        /// <returns>BsonDocument</returns>
        public BsonDocument GetBsonFromMessage(String strHL7)
        {
            Message msg = Parse(strHL7);

            BsonDocument hl7Doc = new BsonDocument();
            BsonDocument hl7Seg;

            BsonElement _id = new BsonElement("_id", msg.Segments[0].Fields[10].Value);
            hl7Doc.Add(_id);

            BsonElement MsgEvent = new BsonElement("Event", msg.Segments[0].Fields[9].Components[1].Value);
            hl7Doc.Add(MsgEvent);

            BsonElement MsgDt = new BsonElement("MsgDt", msg.Segments[0].Fields[7].Value);
            hl7Doc.Add(MsgDt);

            int segIndex = 1;

            BsonArray segArray = new BsonArray();

            Dictionary<String, int> segDic = new Dictionary<String, int>();

            foreach (Segment Seg in msg.Segments)
            {
                String segName = Seg.Fields[0].Value;

                hl7Seg = new BsonDocument();

                BsonElement SegName = new BsonElement("_id", segName);
                hl7Seg.Add(SegName);

                if (segDic.ContainsKey(segName))
                    segDic[segName] = segDic[segName] + 1;
                else
                    segDic[segName] = 1;

                BsonElement SegIndex = new BsonElement("Rep", segDic[segName]);
                hl7Seg.Add(SegIndex);

                BsonElement SeqNo = new BsonElement("Seq", segIndex++);
                hl7Seg.Add(SeqNo);

                BsonElement RawValue = new BsonElement("Val", Seg.Value);
                hl7Seg.Add(RawValue);

                BsonArray fieldArray = new BsonArray();
                BsonDocument segField = new BsonDocument();

                int fieldIndex = 0;
                int fieldCount = 0;
                foreach (Field field in Seg.Fields)
                {
                    if (field.Value == segName)
                        continue;
                    fieldIndex++;
                    if (!String.IsNullOrEmpty(field.Value))
                    {
                        segField = new BsonDocument();

                        String strFieldName = segName + "_" + (fieldIndex).ToString();// +"_" + seg.RepetitionNo.ToString();
                        segField.Add("_id", strFieldName);
                        segField.Add("Val", field.Value);
                        fieldArray.Add(segField);
                        fieldCount++;
                        if (field.Repetitions.Count > 0)
                        {
                            BsonArray repFieldArray = new BsonArray();
                            BsonDocument repField = new BsonDocument();
                            int repIndex = 0;
                            foreach (Field rep in field.Repetitions)
                            {
                                repIndex++;
                                if (!String.IsNullOrEmpty(rep.Value))
                                {
                                    repField = new BsonDocument();

                                    strFieldName = segName + "_" + (fieldIndex).ToString();// +"_" + seg.RepetitionNo.ToString();
                                    repField.Add("_id", strFieldName);
                                    repField.Add("Val", rep.Value);
                                    repField.Add("Rep", repIndex);
                                    repFieldArray.Add(repField);

                                    if (rep.Components.Count > 0)
                                    {
                                        int comIndex = 0;
                                        foreach (Component com in rep.Components)
                                        {
                                            comIndex++;
                                            if (!String.IsNullOrEmpty(com.Value))
                                            {
                                                repField = new BsonDocument();

                                                strFieldName = segName + "_" + (fieldIndex).ToString() + "_" + (comIndex).ToString();// +"_" + seg.RepetitionNo.ToString();
                                                repField.Add("_id", strFieldName);
                                                repField.Add("Val", com.Value);
                                                repField.Add("Rep", repIndex);
                                                repFieldArray.Add(repField);
                                            }
                                        }
                                    }
                                }
                            }
                            segField.Add("Repetitions", repFieldArray);
                        }
                        if (field.Components.Count > 0)
                        {
                            int comIndex = 0;
                            foreach (Component com in field.Components)
                            {
                                comIndex++;
                                if (!String.IsNullOrEmpty(com.Value))
                                {
                                    segField = new BsonDocument();

                                    strFieldName = segName + "_" + (fieldIndex).ToString() + "_" + (comIndex).ToString();// +"_" + seg.RepetitionNo.ToString();
                                    segField.Add("_id", strFieldName);
                                    segField.Add("Val", com.Value);
                                    fieldArray.Add(segField);

                                }
                            }
                        }
                    }
                }
                BsonElement FC = new BsonElement("FC", fieldIndex);
                hl7Seg.Add(FC);

                BsonElement VF = new BsonElement("VF", fieldCount);
                hl7Seg.Add(VF);

                hl7Seg.Add("Fields", fieldArray);

                segArray.Add(hl7Seg);
            }

            hl7Doc.Add("Segments", segArray);

            return hl7Doc;
        }
    }

    /// <summary>
    /// Custom Message Class to store HL7 message & list of segments
    /// </summary>
    public class Message
    {
        public String Value = String.Empty;
        public List<Segment> Segments = new List<Segment>();
    }

    /// <summary>
    /// Custom Segment class to store each segment with list of fields
    /// </summary>
    public class Segment
    {
        public String Value = String.Empty;
        public List<Field> Fields = new List<Field>();
    }

    /// <summary>
    /// Custom Field class to store each field with list of components
    /// This class also stores list of repetitions in case of repeated field
    /// </summary>
    public class Field
    {
        public String Value = String.Empty;
        public List<Component> Components = new List<Component>();
        public List<Field> Repetitions = new List<Field>();
    }

    /// <summary>
    /// Custom Component class to store value of individual component
    /// </summary>
    public class Component
    {
        public String Value = String.Empty;
    }

}
