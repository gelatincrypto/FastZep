using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace FastZep3
{
    /*
     Copyright Ludovit Scholtz 2009-2010
     */
    class ASNTree
    {
        ASNNode root = new ASNNode();
        public ASNTree(byte Type) {
            root.Type = Type;
        }
        public byte[] GetBytes(){
            return root.get();
        }
    }
    class ASNNode {
        public ASNNode[] getChilds() {
            ASNNode[] ch = new ASNNode[childs.Count];
            for (int i = 0; i < childs.Count; i++) {
                ch[i] = (ASNNode) childs[i];
            }
            return ch;
        }
        public byte[] getValue() {
            return this.data;
        }
        public static ArrayList parse(byte[] data)
        {
            ArrayList ret = new ArrayList();
            bool reachedEnd = false;
            int start = 0;
            while (!reachedEnd) {
                
                ASNNode node = new ASNNode();
                node.Type = data[start];
                int len = Convert.ToInt32(data[start + 1]);
                int offset = 1;
                int length = 0;
                if ((len & 0x80) == 0x80)
                {
                    len = len - 0x80;
                    offset += 1 + len;
                    for (int i = 0; i < len; i++)
                    {
                        length *= 0x100;
                        length += Convert.ToInt32(data[start + 2 + i]);
                    }
                }
                else
                {
                    offset += 1;
                    length = Convert.ToInt32(data[start + 1]);
                }

                byte[] tmp = new byte[length];
                MemoryStream st = new MemoryStream();
                st.Write(data, start + offset, length);
                st.Position = 0;
                st.Read(tmp, 0, length);

                node.setValue(tmp);
                if(tmp.Length > 0)
                    if (node.Type == AsnTag.SEQUENCE ||
                        node.Type == AsnTag.CONTEXT_SPECIFIC1 ||
                        node.Type == AsnTag.SET ||
                        node.Type == AsnTag.CONTEXT_SPECIFIC)
                    {
                        node.childs = ASNNode.parse(tmp);
                    }
                ret.Add(node);
                start += offset + tmp.Length;
                if (start >= data.Length) {
                    reachedEnd = true;
                }
            }
            return ret;



        }

        byte[] rawData;

        public ASNNode()
        {
        }
        public ASNNode(int d)
        {
            this.Type = AsnTag.INTEGER;
            setValue(d);
        }
        public ASNNode(byte[] d, byte Type) {
            this.Type = Type;
            if (Type == AsnTag.RAW_DATA) {
                rawData = d;
            }
            setValue(d);
        }
        public ASNNode(byte[] d)
        {
            this.Type = AsnTag.BIT_STRING;
            setValue(d);
        }
        
        public ASNNode(Oid d)
        {
            this.Type = AsnTag.OBJECT_IDENTIFIER;
            setValue(d);
        }
        public ASNNode(string d)
        {
            this.Type = AsnTag.OCTET_STRING;
            setValue(d);
        }
        public ASNNode(string d, byte Type) {
            if (Type == AsnTag.UTF8_STRING)
            {
                this.Type = Type;
                UTF8Encoding u8 = new UTF8Encoding(false);
                var val = u8.GetBytes(d);
                setValue(val);
            }
            else {
                this.Type = AsnTag.OCTET_STRING;
                setValue(d);
            }
        }
        public ASNNode(byte Type)
        {
            this.Type = Type;
        }
        public void ReverseData() {
            Array.Reverse(data);
        }
        public byte[] get() {
            if (rawData != null) return rawData;
            int innerLength = this.getLength(false);
            byte[] len = ASNNode.getLengthBytes(innerLength);
            if (childs.Count > 0)
            {
                MemoryStream data2 = new MemoryStream();
                foreach (ASNNode child in childs)
                {
                    byte[] childBytes = child.get();
                    data2.Write(childBytes, 0, childBytes.Length);
                }

                MemoryStream ret = new MemoryStream();
                ret.WriteByte(Type);
                ret.Write(len, 0, len.Length);
                data = new byte[data2.Length];
                data2.Position = 0;
                data2.Read(data, 0, (int)data2.Length);
                ret.Write(data, 0, (int)data2.Length);

                byte[] ret1 = new byte[ret.Length];
                ret.Position = 0;
                ret.Read(ret1, 0, (int)ret1.Length);

                return ret1;
            }
            else
            {
                if (Type == AsnTag.UTF8_STRING) {
                    int xx = 1;
                }
                MemoryStream ret = new MemoryStream();
                ret.WriteByte(Type);
                ret.Write(len, 0, len.Length);
                if (data != null)
                {
                    ret.Write(data, 0, (int)data.Length);

                }

                byte[] ret1 = new byte[ret.Length];
                ret.Position = 0;
                ret.Read(ret1, 0, (int)ret1.Length);
                return ret1;

            }
        }
        private ArrayList childs = new ArrayList();
        public byte Type;
        private byte[] data;
        public byte[] getData() {
            return data;
        }
        public void AppendChild(ASNNode child) {
            childs.Add(child);
        }
        public void setValue(int d) {
            if (d < 256)
            {
                data = new byte[1];
                data[0] = (byte)((d >> 0) & 0x000000FF);
            }
            else
            {
                data = new byte[4];
                for (int i = 0; i <= 3; i++) data[i] = (byte)((d >> (i * 8)) & 0x000000FF);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(data);
                }
            }
        }
        public void setValue(byte[] d)
        {
            data = d;
        }
        public void setValue(string d)
        {
            UTF8Encoding text = new UTF8Encoding();
            data = text.GetBytes(d);
        }
        public void setValue(Oid d)
        { 
            data = MyOid.getName(d);
        }
        public int getLength() {
            return getLength(true);
        }
        public int getLength(bool includeHeader)
        {
            if (rawData != null) return rawData.Length;

            int innerLength = 0;
            int ret = 0;
            if (data != null)
            {
                innerLength += data.Length;
            }
            foreach (ASNNode node in childs) {
                int l = node.getLength();
                innerLength += l;
                if (innerLength == 0x0000011d) {
                    int xx = 1;
                }
            }
            if (includeHeader)
            {
                ret += 1;
                byte[] len = ASNNode.getLengthBytes(innerLength);
                ret += len.Length;
            }
            ret += innerLength;
            if (ret == 199) {
                int zz = 1;
            }
            return ret;
        }
        public static byte[] getLengthBytes(int length)
        {
            /*
            Stream xdata = new MemoryStream();
            LipingShare.LCLib.Asn1Processor.Asn1Util.DERLengthEncode(xdata, (ulong) length);
            xdata.Position = 0;
            byte[] ret = new byte[xdata.Length];
            xdata.Read(ret, 0, (int) xdata.Length);
            return ret;
            /**/

            byte[] ret = new byte[1];

            byte zero = (byte) 0;
            byte[] len = BitConverter.GetBytes(length);
            if (length < 0x80) {
                ret = new byte[1];
                ret[0] = len[0];
            }
            else if (len[1] == zero && len[2] == zero && len[3] == zero)
            {
                ret = new byte[2];
                ret[0] = 0x81;
                ret[1] = len[0];
            }
            else if (len[2] == zero && len[3] == zero)
            {
                ret = new byte[3];
                ret[0] = 0x82;
                ret[1] = len[1];
                ret[2] = len[0];
            }
            else if (len[3] == zero)
            {
                ret = new byte[4];
                ret[0] = 0x83;
                ret[1] = len[2];
                ret[2] = len[1];
                ret[3] = len[0];
            }
            else {
                ret = new byte[5];
                ret[0] = 0x84;
                ret[1] = len[3];
                ret[2] = len[2];
                ret[3] = len[1];
                ret[4] = len[0];
            
            }
            return ret;
            /**/
        }
    }
    class MyOid{
        public static byte[] getName(Oid oid) {

            MemoryStream ret1 = new MemoryStream();
            MyOid.Encode(ret1, oid.Value);

            byte[] ret = new byte[ret1.Length];
            ret1.Position = 0;
            ret1.Read(ret, 0, (int) ret1.Length);
            return ret;
            
        }
        public static void Encode(Stream bt, string oidStr)
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
            bt.WriteByte((byte)((numArray[0] * ((ulong)40L)) + numArray[1]));
            for (int j = 2; j < numArray.Length; j++)
            {
                MyOid.EncodeValue(bt, numArray[j]);
            }
        }
        protected static void EncodeValue(Stream bt, ulong v)
        {
            for (int i = (BitPrecision(v) - 1) / 7; i > 0; i--)
            {
                bt.WriteByte((byte)(((ulong)0x80L) | ((v >> (i * 7)) & ((ulong)0x7fL))));
            }
            bt.WriteByte((byte)(v & ((ulong)0x7fL)));
        }
        private static int BitPrecision(ulong ivalue)
        {
            if (ivalue == 0L)
            {
                return 0;
            }
            int num = 0;
            int num2 = 0x20;
            while ((num2 - num) > 1)
            {
                int num3 = (num + num2) / 2;
                if ((ivalue >> num3) != 0L)
                {
                    num = num3;
                }
                else
                {
                    num2 = num3;
                }
            }
            return num2;
        }
    }
    public class AsnTag
    {
        public const byte BOOLEAN = 1;
        public const byte INTEGER = 0x02;
        public const byte OCTET_STRING = 4;
        public const byte ENUMERATED = 0x0A;
        public const byte SEQUENCE = 0x30;
        public const byte SET = 0x31;
        public const byte NULL = 0x05;
        public const byte BIT_STRING = 3;
        public const byte BMPSTRING = 30;
        public const byte EXTERNAL = 8;
        public const byte GENERAL_STRING = 0x1b;
        public const byte GENERALIZED_TIME = 0x18;
        public const byte GRAPHIC_STRING = 0x19;
        public const byte IA5_STRING = 0x16;
        public const byte NUMERIC_STRING = 0x12;
        public const byte OBJECT_DESCRIPTOR = 7;
        public const byte OBJECT_IDENTIFIER = 6;
        public const byte PRINTABLE_STRING = 0x13;
        public const byte REAL = 9;
        public const byte RELATIVE_OID = 13;
        public const byte T61_STRING = 20;
        public const byte TAG_MASK = 0x1f;
        public const byte TAG_NULL = 5;
        public const byte UNIVERSAL_STRING = 0x1c;
        public const byte UTC_TIME = 0x17;
        public const byte UTF8_STRING = 12;
        public const byte VIDEOTEXT_STRING = 0x15;
        public const byte VISIBLE_STRING = 0x1a;
        public const byte CONTEXT_SPECIFIC1 = 0xA0;
        public const byte CONTEXT_SPECIFIC = 0xA4;
        public const byte RAW_DATA = 0xD3;
    }
}
