namespace LipingShare.LCLib.Asn1Processor
{
    using Microsoft.Win32;
    using System;
    using System.IO;
    /*
Copyright (c) 2003 Liping Dai. All rights reserved.
Web: www.lipingshare.com
Email: lipingshare@yahoo.com
     */
    public class Asn1Util
    {
        private static char[] hexDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private const string PemEndStr = "-----END";
        private const string PemStartStr = "-----BEGIN";

        private Asn1Util()
        {
        }

        public static int BitPrecision(ulong ivalue)
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

        public static int BytePrecision(ulong value)
        {
            int num = 4;
            while (num > 0)
            {
                if ((value >> ((num - 1) * 8)) != 0L)
                {
                    return num;
                }
                num--;
            }
            return num;
        }

        public static long BytesToLong(byte[] bytes)
        {
            long num = 0L;
            for (int i = 0; i < bytes.Length; i++)
            {
                num = (num << 8) | bytes[i];
            }
            return num;
        }

        public static string BytesToPem(byte[] data)
        {
            return BytesToPem(data, "");
        }

        public static string BytesToPem(byte[] data, string pemHeader)
        {
            if ((pemHeader == null) || (pemHeader.Length < 1))
            {
                pemHeader = "ASN.1 Editor Generated PEM File";
            }
            string str = "";
            if ((pemHeader.Length > 0) && (pemHeader[0] != ' '))
            {
                pemHeader = " " + pemHeader;
            }
            str = FormatString(Convert.ToBase64String(data), 0x40, 0);
            return ("-----BEGIN" + pemHeader + "-----\r\n" + str + "\r\n-----END" + pemHeader + "-----\r\n");
        }

        public static string BytesToString(byte[] bytes)
        {
            string str = "";
            if ((bytes == null) || (bytes.Length < 1))
            {
                return str;
            }
            char[] chArray = new char[bytes.Length];
            int index = 0;
            int num2 = 0;
            while (index < bytes.Length)
            {
                if (bytes[index] != 0)
                {
                    chArray[num2++] = (char) bytes[index];
                }
                index++;
            }
            str = new string(chArray);
            return str.TrimEnd(new char[1]);
        }

        public static long DerLengthDecode(Stream bt, ref bool isIndefiniteLength)
        {
            isIndefiniteLength = false;
            long num = 0L;
            byte num2 = (byte) bt.ReadByte();
            if ((num2 & 0x80) == 0)
            {
                return (long) num2;
            }
            long num3 = num2 & 0x7f;
            if (num3 == 0L)
            {
                isIndefiniteLength = true;
                long position = bt.Position;
                return -2L;
            }
            num = 0L;
        Label_0054:
            num3 -= 1L;
            if (num3 > 0L)
            {
                if ((num >> 0x18) > 0L)
                {
                    return -1L;
                }
                num2 = (byte) bt.ReadByte();
                num = (num << 8) | num2;
                goto Label_0054;
            }
            return num;
        }

        public static int DERLengthEncode(Stream xdata, ulong length)
        {
            int num = 0;
            if (length <= 0x7fL)
            {
                xdata.WriteByte((byte) length);
                num++;
                return num;
            }
            xdata.WriteByte((byte) (BytePrecision(length) | 0x80));
            num++;
            for (int i = BytePrecision(length); i > 0; i--)
            {
                xdata.WriteByte((byte) (length >> ((i - 1) * 8)));
                num++;
            }
            return num;
        }

        public static string FormatString(string inStr, int lineLen, int groupLen)
        {
            char[] chArray = new char[inStr.Length * 2];
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < inStr.Length; i++)
            {
                chArray[num2++] = inStr[i];
                num4++;
                num3++;
                if ((num4 >= groupLen) && (groupLen > 0))
                {
                    chArray[num2++] = ' ';
                    num4 = 0;
                }
                if (num3 >= lineLen)
                {
                    chArray[num2++] = '\r';
                    chArray[num2++] = '\n';
                    num3 = 0;
                }
            }
            string str = new string(chArray);
            return str.TrimEnd(new char[1]).TrimEnd(new char[] { '\n' }).TrimEnd(new char[] { '\r' });
        }

        public static string GenStr(int len, char xch)
        {
            char[] chArray = new char[len];
            for (int i = 0; i < len; i++)
            {
                chArray[i] = xch;
            }
            return new string(chArray);
        }

        public static byte GetHexDigitsVal(char ch)
        {
            for (int i = 0; i < hexDigits.Length; i++)
            {
                if (hexDigits[i] == ch)
                {
                    return (byte) i;
                }
            }
            return 0;
        }

        public static string GetPemFileHeader(string fileName)
        {
            try
            {
                FileStream stream = new FileStream(fileName, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                return GetPemHeader(BytesToString(buffer));
            }
            catch
            {
                return "";
            }
        }

        public static string GetPemHeader(string pemStr)
        {
            string[] strArray = pemStr.Split(new char[] { '\n' });
            bool flag = false;
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                str = strArray[i].ToUpper().Replace("\r", "");
                if ((!(str == "") && (str.Length > "-----BEGIN".Length)) && (!flag && (str.Substring(0, "-----BEGIN".Length) == "-----BEGIN")))
                {
                    flag = true;
                    return strArray[i].Substring("-----BEGIN".Length, strArray[i].Length - "-----BEGIN".Length).Replace("-----", "").Replace("\r", "");
                }
            }
            return "";
        }

        public static string GetTagName(byte tag)
        {
            string str = "";
            if ((tag & 0xc0) == 0)
            {
                switch ((tag & 0x1f))
                {
                    case 1:
                        return (str + "BOOLEAN");

                    case 2:
                        return (str + "INTEGER");

                    case 3:
                        return (str + "BIT STRING");

                    case 4:
                        return (str + "OCTET STRING");

                    case 5:
                        return (str + "NULL");

                    case 6:
                        return (str + "OBJECT IDENTIFIER");

                    case 7:
                        return (str + "OBJECT DESCRIPTOR");

                    case 8:
                        return (str + "EXTERNAL");

                    case 9:
                        return (str + "REAL");

                    case 10:
                        return (str + "ENUMERATED");

                    case 11:
                    case 14:
                    case 15:
                    case 0x1d:
                        goto Label_0325;

                    case 12:
                        return (str + "UTF8 STRING");

                    case 13:
                        return (str + "RELATIVE-OID");

                    case 0x10:
                        return (str + "SEQUENCE");

                    case 0x11:
                        return (str + "SET");

                    case 0x12:
                        return (str + "NUMERIC STRING");

                    case 0x13:
                        return (str + "PRINTABLE STRING");

                    case 20:
                        return (str + "T61 STRING");

                    case 0x15:
                        return (str + "VIDEOTEXT STRING");

                    case 0x16:
                        return (str + "IA5 STRING");

                    case 0x17:
                        return (str + "UTC TIME");

                    case 0x18:
                        return (str + "GENERALIZED TIME");

                    case 0x19:
                        return (str + "GRAPHIC STRING");

                    case 0x1a:
                        return (str + "VISIBLE STRING");

                    case 0x1b:
                        return (str + "GENERAL STRING");

                    case 0x1c:
                        return (str + "UNIVERSAL STRING");

                    case 30:
                        return (str + "BMP STRING");
                }
            }
            else
            {
                int num = tag & 0xc0;
                if (num <= 0x20)
                {
                    switch (num)
                    {
                        case 0:
                        {
                            int num6 = tag & 0x1f;
                            return (str + "UNIVERSAL (" + num6.ToString() + ")");
                        }
                        case 0x20:
                        {
                            int num5 = tag & 0x1f;
                            return (str + "CONSTRUCTED (" + num5.ToString() + ")");
                        }
                    }
                    return str;
                }
                switch (num)
                {
                    case 0x40:
                    {
                        int num3 = tag & 0x1f;
                        return (str + "APPLICATION (" + num3.ToString() + ")");
                    }
                    case 0x80:
                    {
                        int num2 = tag & 0x1f;
                        return (str + "CONTEXT SPECIFIC (" + num2.ToString() + ")");
                    }
                    case 0xc0:
                    {
                        int num4 = tag & 0x1f;
                        return (str + "PRIVATE (" + num4.ToString() + ")");
                    }
                    default:
                        return str;
                }
            }
        Label_0325:
            return (str + "UNKNOWN TAG");
        }

        public static byte[] HexStrToBytes(string hexStr)
        {
            int num;
            hexStr = hexStr.Replace(" ", "");
            hexStr = hexStr.Replace("\r", "");
            hexStr = hexStr.Replace("\n", "");
            hexStr = hexStr.ToUpper();
            if ((hexStr.Length % 2) != 0)
            {
                throw new Exception("Invalid Hex string: odd length.");
            }
            for (num = 0; num < hexStr.Length; num++)
            {
                if (!IsValidHexDigits(hexStr[num]))
                {
                    throw new Exception("Invalid Hex string: included invalid character [" + hexStr[num] + "]");
                }
            }
            int num2 = hexStr.Length / 2;
            byte[] buffer = new byte[num2];
            for (num = 0; num < num2; num++)
            {
                int hexDigitsVal = GetHexDigitsVal(hexStr[num * 2]);
                int num4 = GetHexDigitsVal(hexStr[(num * 2) + 1]);
                int num5 = (hexDigitsVal << 4) | num4;
                buffer[num] = (byte) num5;
            }
            return buffer;
        }

        public static bool IsAsciiString(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < 0x20)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsAsn1EncodedHexStr(string dataStr)
        {
            bool flag = false;
            return flag;
        }

        public static bool IsEqual(byte[] source, byte[] target)
        {
            if (source == null)
            {
                return false;
            }
            if (target == null)
            {
                return false;
            }
            if (source.Length != target.Length)
            {
                return false;
            }
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != target[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsHexStr(string hexStr)
        {
            byte[] buffer = null;
            try
            {
                buffer = HexStrToBytes(hexStr);
            }
            catch
            {
                return false;
            }
            return ((buffer != null) && (buffer.Length >= 0));
        }

        public static bool IsPemFormated(string pemStr)
        {
            byte[] buffer = null;
            try
            {
                buffer = PemToBytes(pemStr);
            }
            catch
            {
                return false;
            }
            return (buffer.Length > 0);
        }

        public static bool IsPemFormatedFile(string fileName)
        {
            try
            {
                FileStream stream = new FileStream(fileName, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                return IsPemFormated(BytesToString(buffer));
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidHexDigits(char ch)
        {
            for (int i = 0; i < hexDigits.Length; i++)
            {
                if (hexDigits[i] == ch)
                {
                    return true;
                }
            }
            return false;
        }

        public static byte[] PemToBytes(string pemStr)
        {
            string[] strArray = pemStr.Split(new char[] { '\n' });
            string str = "";
            bool flag = false;
            bool flag2 = false;
            string str2 = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                str2 = strArray[i].ToUpper();
                if (str2 != "")
                {
                    if (((str2.Length > "-----BEGIN".Length) && !flag) && (str2.Substring(0, "-----BEGIN".Length) == "-----BEGIN"))
                    {
                        flag = true;
                    }
                    else
                    {
                        if ((str2.Length > "-----END".Length) && (str2.Substring(0, "-----END".Length) == "-----END"))
                        {
                            flag2 = true;
                            break;
                        }
                        if (flag)
                        {
                            str = str + strArray[i];
                        }
                    }
                }
            }
            if (!flag || !flag2)
            {
                throw new Exception("'BEGIN'/'END' line is missing.");
            }
            return Convert.FromBase64String(str.Replace("\r", "").Replace("\n", "").Replace("\n", " "));
        }

        public static Stream PemToStream(string pemStr)
        {
            MemoryStream stream = new MemoryStream(PemToBytes(pemStr));
            stream.Position = 0L;
            return stream;
        }

        public static object ReadRegInfo(string path, string name)
        {
            object obj2 = null;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, false);
            if (key != null)
            {
                obj2 = key.GetValue(name);
            }
            return obj2;
        }

        public static byte[] StringToBytes(string msg)
        {
            byte[] buffer = new byte[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                buffer[i] = (byte) msg[i];
            }
            return buffer;
        }

        public static string ToHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }
            char[] chArray = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int num = bytes[i];
                chArray[i * 2] = hexDigits[num >> 4];
                chArray[(i * 2) + 1] = hexDigits[num & 15];
            }
            return new string(chArray);
        }

    }
}

