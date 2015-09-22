namespace LipingShare.LCLib.Asn1Processor
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.IO;
    using System.Windows.Forms;

    /*
Copyright (c) 2003 Liping Dai. All rights reserved.
Web: www.lipingshare.com
Email: lipingshare@yahoo.com
     */

    public class Oid
    {
        private static StringDictionary oidDictionary;

        public virtual string Decode(Stream bt)
        {
            string str = "";
            ulong v = 0L;
            byte num = (byte) bt.ReadByte();
            str = (str + Convert.ToString((int) (num / 40))) + "." + Convert.ToString((int) (num % 40));
            while (bt.Position < bt.Length)
            {
                try
                {
                    this.DecodeValue(bt, ref v);
                    str = str + "." + v.ToString();
                    continue;
                }
                catch (Exception exception)
                {
                    throw new Exception("Failed to decode OID value: " + exception.Message);
                }
            }
            return str;
        }

        public string Decode(byte[] data)
        {
            MemoryStream bt = new MemoryStream(data);
            bt.Position = 0L;
            string str = this.Decode(bt);
            bt.Close();
            return str;
        }

        protected int DecodeValue(Stream bt, ref ulong v)
        {
            byte num;
            int num2 = 0;
            v = 0L;
            do
            {
                num = (byte) bt.ReadByte();
                num2++;
                v = v << 7;
                v += (ulong) num & 0x7f;
            }
            while ((num & 0x80) != 0);
            return num2;
        }

        public byte[] Encode(string oidStr)
        {
            MemoryStream bt = new MemoryStream();
            this.Encode(bt, oidStr);
            bt.Position = 0L;
            byte[] buffer = new byte[bt.Length];
            bt.Read(buffer, 0, buffer.Length);
            bt.Close();
            return buffer;
        }

        public virtual void Encode(Stream bt, string oidStr)
        {
            string[] strArray = oidStr.Split(new char[] { '.' });
            if (strArray.Length < 2)
            {
                throw new Exception("Invalid OID string.");
            }
            ulong[] numArray = new ulong[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                numArray[i] = Convert.ToUInt64(strArray[i]);
            }
            bt.WriteByte((byte) ((numArray[0] * ((ulong) 40L)) + numArray[1]));
            for (int j = 2; j < numArray.Length; j++)
            {
                this.EncodeValue(bt, numArray[j]);
            }
        }

        protected void EncodeValue(Stream bt, ulong v)
        {
            for (int i = (Asn1Util.BitPrecision(v) - 1) / 7; i > 0; i--)
            {
                bt.WriteByte((byte) (((ulong) 0x80L) | ((v >> (i * 7)) & ((ulong) 0x7fL))));
            }
            bt.WriteByte((byte) (v & ((ulong) 0x7fL)));
        }

        public string GetOidName(string inOidStr)
        {
            if (oidDictionary == null)
            {
                oidDictionary = new StringDictionary();
                string executablePath = Application.ExecutablePath;
                string path = Path.GetDirectoryName(executablePath) + @"\OID.txt";
                string str3 = Path.GetDirectoryName(executablePath) + @"\OID.Backup.txt";
                string key = "";
                string str5 = "";
                bool flag = false;
                int num = 0;
                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string str6;
                        while ((str6 = reader.ReadLine()) != null)
                        {
                            string[] strArray = str6.Split(new char[] { ',' });
                            if (strArray.Length >= 2)
                            {
                                key = strArray[0].Trim();
                                str5 = strArray[1].Trim();
                                try
                                {
                                    oidDictionary.Add(key, str5);
                                    continue;
                                }
                                catch (Exception exception)
                                {
                                    flag = true;
                                    string message = exception.Message;
                                    num++;
                                    continue;
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        using (StreamWriter writer = new StreamWriter(str3))
                        {
                            using (StreamReader reader2 = new StreamReader(path))
                            {
                                string str7;
                                while ((str7 = reader2.ReadLine()) != null)
                                {
                                    writer.Write(str7 + "\r\n");
                                }
                            }
                        }
                        SortedList list = new SortedList();
                        using (StreamWriter writer2 = new StreamWriter(path))
                        {
                            string str8 = "";
                            foreach (DictionaryEntry entry in oidDictionary)
                            {
                                if (!list.ContainsKey(entry.Key))
                                {
                                    list.Add(entry.Key, entry.Value);
                                }
                            }
                            for (int i = 0; i < list.Count; i++)
                            {
                                str8 = string.Format("{0}, {1}\r\n", list.GetKey(i), list.GetByIndex(i));
                                writer2.Write(str8);
                            }
                        }
                        MessageBox.Show(string.Format("Duplicated OIDs were found in the OID table: {0}.\r\nThe duplicate has been removed; the table is sorted.\r\nThe original OID file is copied as: {1}\r\n", path, str3));
                    }
                }
                catch (Exception exception2)
                {
                    MessageBox.Show("Failed to read OID values from file." + exception2.Message);
                }
            }
            return oidDictionary[inOidStr];
        }
    }
}

