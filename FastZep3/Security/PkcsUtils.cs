// Type: System.Security.Cryptography.Pkcs.PkcsUtils
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;

namespace FastZep3
{
    internal static class PkcsUtils
    {
        private static int m_cmsSupported = -1;

        static PkcsUtils()
        {
        }

        internal static uint AlignedLength(uint length)
        {
            return (uint)((int)length + 7 & -8);
        }

        [SecuritySafeCritical]
        internal static bool CmsSupported()
        {
            if (PkcsUtils.m_cmsSupported == -1)
            {
                using (SafeLibraryHandle hModule = CAPI.CAPISafe.LoadLibrary("Crypt32.dll"))
                {
                    if (!hModule.IsInvalid)
                        PkcsUtils.m_cmsSupported = CAPI.CAPISafe.GetProcAddress(hModule, "CryptMsgVerifyCountersignatureEncodedEx") == IntPtr.Zero ? 0 : 1;
                }
            }
            return PkcsUtils.m_cmsSupported != 0;
        }

        [SecuritySafeCritical]
        internal static RecipientInfoType GetRecipientInfoType(X509Certificate2 certificate)
        {
            RecipientInfoType recipientInfoType = RecipientInfoType.Unknown;
            if (certificate != null)
            {
                switch (X509Utils.OidToAlgId(((CAPI.CERT_INFO)Marshal.PtrToStructure(((CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificate).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT))).pCertInfo, typeof(CAPI.CERT_INFO))).SubjectPublicKeyInfo.Algorithm.pszObjId))
                {
                    case 41984U:
                        recipientInfoType = RecipientInfoType.KeyTransport;
                        break;
                    case 43521U:
                    case 43522U:
                        recipientInfoType = RecipientInfoType.KeyAgreement;
                        break;
                    default:
                        recipientInfoType = RecipientInfoType.Unknown;
                        break;
                }
            }
            return recipientInfoType;
        }

        [SecurityCritical]
        internal static unsafe int GetMaxKeyLength(SafeCryptProvHandle safeCryptProvHandle, uint algId)
        {
            uint dwFlags = 1U;
            uint num = (uint)Marshal.SizeOf(typeof(CAPI.PROV_ENUMALGS_EX));
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.PROV_ENUMALGS_EX))));
            using (localAllocHandle)
            {
                for (; CAPI.CAPISafe.CryptGetProvParam(safeCryptProvHandle, 22U, localAllocHandle.DangerousGetHandle(), new IntPtr((void*)&num), dwFlags); dwFlags = 0U)
                {
                    CAPI.PROV_ENUMALGS_EX provEnumalgsEx = (CAPI.PROV_ENUMALGS_EX)Marshal.PtrToStructure(localAllocHandle.DangerousGetHandle(), typeof(CAPI.PROV_ENUMALGS_EX));
                    if ((int)provEnumalgsEx.aiAlgid == (int)algId)
                        return (int)provEnumalgsEx.dwMaxLen;
                }
            }
            throw new CryptographicException(-2146889726);
        }

        [SecurityCritical]
        internal static unsafe uint GetVersion(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            uint num1 = 0U;
            uint num2 = (uint)Marshal.SizeOf(typeof(uint));
            if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 30U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                PkcsUtils.checkErr(Marshal.GetLastWin32Error());
            return num1;
        }

        [SecurityCritical]
        internal static unsafe uint GetMessageType(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            uint num1 = 0U;
            uint num2 = (uint)Marshal.SizeOf(typeof(uint));
            if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 1U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                PkcsUtils.checkErr(Marshal.GetLastWin32Error());
            return num1;
        }

        [SecurityCritical]
        internal static unsafe AlgorithmIdentifier GetAlgorithmIdentifier(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier();
            uint num = 0U;
            if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 15U, 0U, IntPtr.Zero, new IntPtr((void*)&num)))
                PkcsUtils.checkErr(Marshal.GetLastWin32Error());
            if (num > 0U)
            {
                SafeLocalAllocHandle pvData = CAPI.LocalAlloc(0U, new IntPtr((long)num));
                if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 15U, 0U, pvData, new IntPtr((void*)&num)))
                    PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                algorithmIdentifier = new AlgorithmIdentifier((CAPI.CRYPT_ALGORITHM_IDENTIFIER)Marshal.PtrToStructure(pvData.DangerousGetHandle(), typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER)));
                pvData.Dispose();
            }
            return algorithmIdentifier;
        }

        [SecurityCritical]
        internal static unsafe void GetParam(SafeCryptMsgHandle safeCryptMsgHandle, uint paramType, uint index, out SafeLocalAllocHandle pvData, out uint cbData)
        {
            cbData = 0U;
            pvData = SafeLocalAllocHandle.InvalidHandle;
            fixed (uint* numPtr = &cbData)
            {
                if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, pvData, new IntPtr((void*)numPtr)))
                    PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                if (cbData > 0U)
                {
                    pvData = CAPI.LocalAlloc(64U, new IntPtr((long)cbData));
                    if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, pvData, new IntPtr((void*)numPtr)))
                        PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                }
            }
        }

        [SecurityCritical]
        internal static unsafe void GetParam(SafeCryptMsgHandle safeCryptMsgHandle, uint paramType, uint index, out byte[] pvData, out uint cbData)
        {
            cbData = 0U;
            pvData = new byte[0];
            fixed (uint* numPtr1 = &cbData)
            {
                if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, IntPtr.Zero, new IntPtr((void*)numPtr1)))
                    PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                if (cbData > 0U)
                {
                    pvData = new byte[(IntPtr)cbData];
                    fixed (byte* numPtr2 = &pvData[0])
                    {
                        if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, new IntPtr((void*)numPtr2), new IntPtr((void*)numPtr1)))
                            PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                    }
                }
            }
        }

        [SecurityCritical]
        internal static unsafe X509Certificate2Collection GetCertificates(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            uint num1 = 0U;
            uint num2 = (uint)Marshal.SizeOf(typeof(uint));
            X509Certificate2Collection certificate2Collection = new X509Certificate2Collection();
            if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 11U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                PkcsUtils.checkErr(Marshal.GetLastWin32Error());
            for (uint index = 0U; index < num1; ++index)
            {
                uint cbData = 0U;
                SafeLocalAllocHandle pvData = SafeLocalAllocHandle.InvalidHandle;
                PkcsUtils.GetParam(safeCryptMsgHandle, 12U, index, out pvData, out cbData);
                if (cbData > 0U)
                {
                    System.Security.Cryptography.SafeCertContextHandle certificateContext = CAPI.CAPISafe.CertCreateCertificateContext(65537U, pvData, cbData);
                    if (certificateContext == null || certificateContext.IsInvalid)
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                    certificate2Collection.Add(new X509Certificate2(certificateContext.DangerousGetHandle()));
                    certificateContext.Dispose();
                }
            }
            return certificate2Collection;
        }

        [SecurityCritical]
        internal static byte[] GetContent(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            uint cbData = 0U;
            byte[] pvData = new byte[0];
            PkcsUtils.GetParam(safeCryptMsgHandle, 2U, 0U, out pvData, out cbData);
            return pvData;
        }

        [SecurityCritical]
        internal static Oid GetContentType(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            uint cbData = 0U;
            byte[] pvData = new byte[0];
            PkcsUtils.GetParam(safeCryptMsgHandle, 4U, 0U, out pvData, out cbData);
            if (pvData.Length > 0 && (int)pvData[pvData.Length - 1] == 0)
            {
                byte[] numArray = new byte[pvData.Length - 1];
                Array.Copy((Array)pvData, 0, (Array)numArray, 0, numArray.Length);
                pvData = numArray;
            }
            return new Oid(Encoding.ASCII.GetString(pvData));
        }

        [SecurityCritical]
        internal static byte[] GetMessage(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            uint cbData = 0U;
            byte[] pvData = new byte[0];
            PkcsUtils.GetParam(safeCryptMsgHandle, 29U, 0U, out pvData, out cbData);
            return pvData;
        }

        [SecurityCritical]
        internal static unsafe int GetSignerIndex(SafeCryptMsgHandle safeCrytpMsgHandle, SignerInfo signerInfo, int startIndex)
        {
            uint num1 = 0U;
            uint num2 = (uint)Marshal.SizeOf(typeof(uint));
            if (!CAPI.CAPISafe.CryptMsgGetParam(safeCrytpMsgHandle, 5U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                PkcsUtils.checkErr(Marshal.GetLastWin32Error());
            for (int index = startIndex; index < (int)num1; ++index)
            {
                uint num3 = 0U;
                if (!CAPI.CAPISafe.CryptMsgGetParam(safeCrytpMsgHandle, 6U, (uint)index, IntPtr.Zero, new IntPtr((void*)&num3)))
                    PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                if (num3 > 0U)
                {
                    SafeLocalAllocHandle pvData = CAPI.LocalAlloc(0U, new IntPtr((long)num3));
                    if (!CAPI.CAPISafe.CryptMsgGetParam(safeCrytpMsgHandle, 6U, (uint)index, pvData, new IntPtr((void*)&num3)))
                        PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                    CAPI.CMSG_SIGNER_INFO cmsgSignerInfo1 = signerInfo.GetCmsgSignerInfo();
                    CAPI.CMSG_SIGNER_INFO cmsgSignerInfo2 = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(pvData.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
                    if (X509Utils.MemEqual((byte*)(void*)cmsgSignerInfo1.Issuer.pbData, cmsgSignerInfo1.Issuer.cbData, (byte*)(void*)cmsgSignerInfo2.Issuer.pbData, cmsgSignerInfo2.Issuer.cbData) && X509Utils.MemEqual((byte*)(void*)cmsgSignerInfo1.SerialNumber.pbData, cmsgSignerInfo1.SerialNumber.cbData, (byte*)(void*)cmsgSignerInfo2.SerialNumber.pbData, cmsgSignerInfo2.SerialNumber.cbData))
                        return index;
                    pvData.Dispose();
                }
            }
            throw new CryptographicException(-2146889714);
        }

        [SecurityCritical]
        internal static unsafe CryptographicAttributeObjectCollection GetUnprotectedAttributes(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            uint num = 0U;
            CryptographicAttributeObjectCollection objectCollection = new CryptographicAttributeObjectCollection();
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 37U, 0U, invalidHandle, new IntPtr((void*)&num)) && Marshal.GetLastWin32Error() != -2146889713)
                PkcsUtils.checkErr(Marshal.GetLastWin32Error());
            if (num > 0U)
            {
                SafeLocalAllocHandle localAllocHandle;
                using (localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)num)))
                {
                    if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 37U, 0U, localAllocHandle, new IntPtr((void*)&num)))
                        PkcsUtils.checkErr(Marshal.GetLastWin32Error());
                    objectCollection = new CryptographicAttributeObjectCollection(localAllocHandle);
                }
            }
            return objectCollection;
        }

        [SecurityCritical]
        internal static unsafe X509IssuerSerial DecodeIssuerSerial(CAPI.CERT_ISSUER_SERIAL_NUMBER pIssuerAndSerial)
        {
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            uint csz = CAPI.CAPISafe.CertNameToStrW(65537U, new IntPtr((void*)&pIssuerAndSerial.Issuer), 33554435U, invalidHandle, 0U);
            if (csz <= 1U)
                throw new CryptographicException(Marshal.GetLastWin32Error());
            SafeLocalAllocHandle psz = CAPI.LocalAlloc(0U, new IntPtr((long)(2U * csz)));
            if (CAPI.CAPISafe.CertNameToStrW(65537U, new IntPtr((void*)&pIssuerAndSerial.Issuer), 33554435U, psz, csz) <= 1U)
                throw new CryptographicException(Marshal.GetLastWin32Error());
            X509IssuerSerial x509IssuerSerial = new X509IssuerSerial();
            x509IssuerSerial.IssuerName = Marshal.PtrToStringUni(psz.DangerousGetHandle());
            byte[] numArray = new byte[(IntPtr)pIssuerAndSerial.SerialNumber.cbData];
            Marshal.Copy(pIssuerAndSerial.SerialNumber.pbData, numArray, 0, numArray.Length);
            x509IssuerSerial.SerialNumber = X509Utils.EncodeHexStringFromInt(numArray);
            psz.Dispose();
            return x509IssuerSerial;
        }

        [SecuritySafeCritical]
        internal static string DecodeOctetString(byte[] encodedOctetString)
        {
            uint cbDecodedValue = 0U;
            SafeLocalAllocHandle decodedValue = (SafeLocalAllocHandle)null;
            if (!CAPI.DecodeObject(new IntPtr(25L), encodedOctetString, out decodedValue, out cbDecodedValue))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if ((int)cbDecodedValue == 0)
                return string.Empty;
            CAPI.CRYPTOAPI_BLOB cryptoapiBlob = (CAPI.CRYPTOAPI_BLOB)Marshal.PtrToStructure(decodedValue.DangerousGetHandle(), typeof(CAPI.CRYPTOAPI_BLOB));
            if ((int)cryptoapiBlob.cbData == 0)
                return string.Empty;
            string str = Marshal.PtrToStringUni(cryptoapiBlob.pbData);
            decodedValue.Dispose();
            return str;
        }

        [SecuritySafeCritical]
        internal static byte[] DecodeOctetBytes(byte[] encodedOctetString)
        {
            uint cbDecodedValue = 0U;
            SafeLocalAllocHandle decodedValue = (SafeLocalAllocHandle)null;
            if (!CAPI.DecodeObject(new IntPtr(25L), encodedOctetString, out decodedValue, out cbDecodedValue))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if ((int)cbDecodedValue == 0)
                return new byte[0];
            using (decodedValue)
                return CAPI.BlobToByteArray(decodedValue.DangerousGetHandle());
        }

        internal static byte[] EncodeOctetString(string octetString)
        {
            byte[] numArray = new byte[2 * (octetString.Length + 1)];
            Encoding.Unicode.GetBytes(octetString, 0, octetString.Length, numArray, 0);
            return PkcsUtils.EncodeOctetString(numArray);
        }

        [SecuritySafeCritical]
        internal static unsafe byte[] EncodeOctetString(byte[] octets)
        {
            fixed (byte* numPtr = octets)
            {
                CAPI.CRYPTOAPI_BLOB cryptoapiBlob = new CAPI.CRYPTOAPI_BLOB();
                cryptoapiBlob.cbData = (uint)octets.Length;
                cryptoapiBlob.pbData = new IntPtr((void*)numPtr);
                byte[] encodedData = new byte[0];
                if (!CAPI.EncodeObject(new IntPtr(25L), new IntPtr((long)&cryptoapiBlob), out encodedData))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                else
                    return encodedData;
            }
        }

        internal static string DecodeObjectIdentifier(byte[] encodedObjId, int offset)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            if (0 < encodedObjId.Length - offset)
            {
                byte num1 = encodedObjId[offset];
                byte num2 = (byte)((uint)num1 / 40U);
                stringBuilder.Append(num2.ToString((string)null, (IFormatProvider)null));
                stringBuilder.Append(".");
                byte num3 = (byte)((uint)num1 % 40U);
                stringBuilder.Append(num3.ToString((string)null, (IFormatProvider)null));
                ulong num4 = 0UL;
                for (int index = offset + 1; index < encodedObjId.Length; ++index)
                {
                    byte num5 = encodedObjId[index];
                    num4 = (num4 << 7) + (ulong)((int)num5 & (int)sbyte.MaxValue);
                    if (((int)num5 & 128) == 0)
                    {
                        stringBuilder.Append(".");
                        stringBuilder.Append(num4.ToString((string)null, (IFormatProvider)null));
                        num4 = 0UL;
                    }
                }
                if (0L != (long)num4)
                    throw new CryptographicException(-2146885630);
            }
            return ((object)stringBuilder).ToString();
        }

        internal static CmsRecipientCollection SelectRecipients(SubjectIdentifierType recipientIdentifierType)
        {
            X509Store x509Store = new X509Store("AddressBook");
            x509Store.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certificates1 = new X509Certificate2Collection(x509Store.Certificates);
            foreach (X509Certificate2 certificate in x509Store.Certificates)
            {
                if (certificate.NotBefore <= DateTime.Now && certificate.NotAfter >= DateTime.Now)
                {
                    bool flag = true;
                    foreach (X509Extension x509Extension in certificate.Extensions)
                    {
                        if (string.Compare(x509Extension.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            X509KeyUsageExtension keyUsageExtension = new X509KeyUsageExtension();
                            keyUsageExtension.CopyFrom((AsnEncodedData)x509Extension);
                            if ((keyUsageExtension.KeyUsages & X509KeyUsageFlags.KeyEncipherment) == X509KeyUsageFlags.None && (keyUsageExtension.KeyUsages & X509KeyUsageFlags.KeyAgreement) == X509KeyUsageFlags.None)
                            {
                                flag = false;
                                break;
                            }
                            else
                                break;
                        }
                    }
                    if (flag)
                        certificates1.Add(certificate);
                }
            }
            if (certificates1.Count < 1)
                throw new CryptographicException(-2146889717);
            X509Certificate2Collection certificates2 = X509Certificate2UI.SelectFromCollection(certificates1, (string)null, (string)null, X509SelectionFlag.MultiSelection);
            if (certificates2.Count < 1)
                throw new CryptographicException(1223);
            else
                return new CmsRecipientCollection(recipientIdentifierType, certificates2);
        }

        internal static X509Certificate2 SelectSignerCertificate()
        {
            X509Store x509Store = new X509Store();
            x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
            X509Certificate2Collection certificates = new X509Certificate2Collection();
            foreach (X509Certificate2 certificate in x509Store.Certificates)
            {
                if (certificate.HasPrivateKey && certificate.NotBefore <= DateTime.Now && certificate.NotAfter >= DateTime.Now)
                {
                    bool flag = true;
                    foreach (X509Extension x509Extension in certificate.Extensions)
                    {
                        if (string.Compare(x509Extension.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            X509KeyUsageExtension keyUsageExtension = new X509KeyUsageExtension();
                            keyUsageExtension.CopyFrom((AsnEncodedData)x509Extension);
                            if ((keyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.None && (keyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) == X509KeyUsageFlags.None)
                            {
                                flag = false;
                                break;
                            }
                            else
                                break;
                        }
                    }
                    if (flag)
                        certificates.Add(certificate);
                }
            }
            if (certificates.Count < 1)
                throw new CryptographicException(-2146889714);
            X509Certificate2Collection certificate2Collection = X509Certificate2UI.SelectFromCollection(certificates, (string)null, (string)null, X509SelectionFlag.SingleSelection);
            if (certificate2Collection.Count < 1)
                throw new CryptographicException(1223);
            else
                return certificate2Collection[0];
        }

        [SecuritySafeCritical]
        internal static AsnEncodedDataCollection GetAsnEncodedDataCollection(CAPI.CRYPT_ATTRIBUTE cryptAttribute)
        {
            AsnEncodedDataCollection encodedDataCollection = new AsnEncodedDataCollection();
            Oid oid = new Oid(cryptAttribute.pszObjId);
            string name = oid.Value;
            for (uint index = 0U; index < cryptAttribute.cValue; ++index)
            {
                IntPtr pBlob = new IntPtr((long)cryptAttribute.rgValue + (long)index * (long)Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB)));
                Pkcs9AttributeObject pkcs9AttributeObject1 = new Pkcs9AttributeObject(oid, CAPI.BlobToByteArray(pBlob));
                Pkcs9AttributeObject pkcs9AttributeObject2 = CryptoConfig.CreateFromName(name) as Pkcs9AttributeObject;
                if (pkcs9AttributeObject2 != null)
                {
                    pkcs9AttributeObject2.CopyFrom((AsnEncodedData)pkcs9AttributeObject1);
                    pkcs9AttributeObject1 = pkcs9AttributeObject2;
                }
                encodedDataCollection.Add((AsnEncodedData)pkcs9AttributeObject1);
            }
            return encodedDataCollection;
        }

        [SecurityCritical]
        internal static AsnEncodedDataCollection GetAsnEncodedDataCollection(CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE cryptAttribute)
        {
            return new AsnEncodedDataCollection()
      {
        (AsnEncodedData) new Pkcs9AttributeObject(new Oid(cryptAttribute.pszObjId), CAPI.BlobToByteArray(cryptAttribute.Value))
      };
        }

        [SecurityCritical]
        internal static unsafe IntPtr CreateCryptAttributes(CryptographicAttributeObjectCollection attributes)
        {
            if (attributes.Count == 0)
                return IntPtr.Zero;
            uint num1 = 0U;
            uint num2 = PkcsUtils.AlignedLength((uint)Marshal.SizeOf(typeof(PkcsUtils.I_CRYPT_ATTRIBUTE)));
            uint num3 = PkcsUtils.AlignedLength((uint)Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB)));
            foreach (CryptographicAttributeObject cryptographicAttributeObject in attributes)
            {
                num1 = num1 + num2 + PkcsUtils.AlignedLength((uint)(cryptographicAttributeObject.Oid.Value.Length + 1));
                foreach (AsnEncodedData asnEncodedData in cryptographicAttributeObject.Values)
                    num1 = num1 + num3 + PkcsUtils.AlignedLength((uint)asnEncodedData.RawData.Length);
            }
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)num1));
            PkcsUtils.I_CRYPT_ATTRIBUTE* iCryptAttributePtr = (PkcsUtils.I_CRYPT_ATTRIBUTE*)(void*)localAllocHandle.DangerousGetHandle();
            IntPtr num4 = new IntPtr((long)localAllocHandle.DangerousGetHandle() + (long)num2 * (long)attributes.Count);
            foreach (CryptographicAttributeObject cryptographicAttributeObject in attributes)
            {
                byte* numPtr = (byte*)(void*)num4;
                byte[] numArray = new byte[cryptographicAttributeObject.Oid.Value.Length + 1];
                CAPI.CRYPTOAPI_BLOB* cryptoapiBlobPtr = (CAPI.CRYPTOAPI_BLOB*)(numPtr + (int)PkcsUtils.AlignedLength((uint)numArray.Length));
                iCryptAttributePtr->pszObjId = (IntPtr)((void*)numPtr);
                iCryptAttributePtr->cValue = (uint)cryptographicAttributeObject.Values.Count;
                iCryptAttributePtr->rgValue = (IntPtr)((void*)cryptoapiBlobPtr);
                Encoding.ASCII.GetBytes(cryptographicAttributeObject.Oid.Value, 0, cryptographicAttributeObject.Oid.Value.Length, numArray, 0);
                Marshal.Copy(numArray, 0, iCryptAttributePtr->pszObjId, numArray.Length);
                IntPtr destination = new IntPtr((long)cryptoapiBlobPtr + (long)cryptographicAttributeObject.Values.Count * (long)num3);
                foreach (AsnEncodedData asnEncodedData in cryptographicAttributeObject.Values)
                {
                    byte[] rawData = asnEncodedData.RawData;
                    if (rawData.Length > 0)
                    {
                        cryptoapiBlobPtr->cbData = (uint)rawData.Length;
                        cryptoapiBlobPtr->pbData = destination;
                        Marshal.Copy(rawData, 0, destination, rawData.Length);
                        destination = new IntPtr((long)destination + (long)PkcsUtils.AlignedLength((uint)rawData.Length));
                    }
                    ++cryptoapiBlobPtr;
                }
                ++iCryptAttributePtr;
                num4 = destination;
            }
            GC.SuppressFinalize((object)localAllocHandle);
            return localAllocHandle.DangerousGetHandle();
        }

        internal static CAPI.CMSG_SIGNER_ENCODE_INFO CreateSignerEncodeInfo(CmsSigner signer)
        {
            return PkcsUtils.CreateSignerEncodeInfo(signer, false);
        }

        [SecuritySafeCritical]
        internal static unsafe CAPI.CMSG_SIGNER_ENCODE_INFO CreateSignerEncodeInfo(CmsSigner signer, bool silent)
        {
            CAPI.CMSG_SIGNER_ENCODE_INFO signerEncodeInfo = new CAPI.CMSG_SIGNER_ENCODE_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO)));
            SafeCryptProvHandle invalidHandle1 = SafeCryptProvHandle.InvalidHandle;
            uint pdwKeySpec = 0U;
            bool pfCallerFreeProv = false;
            signerEncodeInfo.HashAlgorithm.pszObjId = signer.DigestAlgorithm.Value;
            if (string.Compare(signer.Certificate.PublicKey.Oid.Value, "1.2.840.10040.4.1", StringComparison.Ordinal) == 0)
                signerEncodeInfo.HashEncryptionAlgorithm.pszObjId = "1.2.840.10040.4.3";
            signerEncodeInfo.cAuthAttr = (uint)signer.SignedAttributes.Count;
            signerEncodeInfo.rgAuthAttr = PkcsUtils.CreateCryptAttributes(signer.SignedAttributes);
            signerEncodeInfo.cUnauthAttr = (uint)signer.UnsignedAttributes.Count;
            signerEncodeInfo.rgUnauthAttr = PkcsUtils.CreateCryptAttributes(signer.UnsignedAttributes);
            if (signer.SignerIdentifierType == SubjectIdentifierType.NoSignature)
            {
                signerEncodeInfo.HashEncryptionAlgorithm.pszObjId = "1.3.6.1.5.5.7.6.2";
                signerEncodeInfo.pCertInfo = IntPtr.Zero;
                signerEncodeInfo.dwKeySpec = pdwKeySpec;
                if (!CAPI.CryptAcquireContext(out invalidHandle1, (string)null, (string)null, 1U, 4026531840U))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                signerEncodeInfo.hCryptProv = invalidHandle1.DangerousGetHandle();
                GC.SuppressFinalize((object)invalidHandle1);
                signerEncodeInfo.SignerId.dwIdChoice = 1U;
                X500DistinguishedName distinguishedName = new X500DistinguishedName("CN=Dummy Signer");
                distinguishedName.Oid = new Oid("1.3.6.1.4.1.311.21.9");
                signerEncodeInfo.SignerId.Value.IssuerSerialNumber.Issuer.cbData = (uint)distinguishedName.RawData.Length;
                SafeLocalAllocHandle localAllocHandle1 = CAPI.LocalAlloc(64U, new IntPtr((long)signerEncodeInfo.SignerId.Value.IssuerSerialNumber.Issuer.cbData));
                Marshal.Copy(distinguishedName.RawData, 0, localAllocHandle1.DangerousGetHandle(), distinguishedName.RawData.Length);
                signerEncodeInfo.SignerId.Value.IssuerSerialNumber.Issuer.pbData = localAllocHandle1.DangerousGetHandle();
                GC.SuppressFinalize((object)localAllocHandle1);
                signerEncodeInfo.SignerId.Value.IssuerSerialNumber.SerialNumber.cbData = 1U;
                SafeLocalAllocHandle localAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr((long)signerEncodeInfo.SignerId.Value.IssuerSerialNumber.SerialNumber.cbData));
                *(sbyte*)(void*)localAllocHandle2.DangerousGetHandle() = (sbyte)0;
                signerEncodeInfo.SignerId.Value.IssuerSerialNumber.SerialNumber.pbData = localAllocHandle2.DangerousGetHandle();
                GC.SuppressFinalize((object)localAllocHandle2);
                return signerEncodeInfo;
            }
            else
            {
                System.Security.Cryptography.SafeCertContextHandle certContext1 = X509Utils.GetCertContext(signer.Certificate);
                if (!CAPI.CAPISafe.CryptAcquireCertificatePrivateKey(certContext1, silent ? 70U : 6U, IntPtr.Zero, out invalidHandle1, out pdwKeySpec, out pfCallerFreeProv))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                signerEncodeInfo.dwKeySpec = pdwKeySpec;
                signerEncodeInfo.hCryptProv = invalidHandle1.DangerousGetHandle();
                GC.SuppressFinalize((object)invalidHandle1);
                CAPI.CERT_CONTEXT certContext2 = *(CAPI.CERT_CONTEXT*)(void*)certContext1.DangerousGetHandle();
                signerEncodeInfo.pCertInfo = certContext2.pCertInfo;
                if (signer.SignerIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
                {
                    uint pcbData = 0U;
                    SafeLocalAllocHandle invalidHandle2 = SafeLocalAllocHandle.InvalidHandle;
                    if (!CAPI.CAPISafe.CertGetCertificateContextProperty(certContext1, 20U, invalidHandle2, out pcbData))
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                    if (pcbData > 0U)
                    {
                        SafeLocalAllocHandle pvData = CAPI.LocalAlloc(64U, new IntPtr((long)pcbData));
                        if (!CAPI.CAPISafe.CertGetCertificateContextProperty(certContext1, 20U, pvData, out pcbData))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        signerEncodeInfo.SignerId.dwIdChoice = 2U;
                        signerEncodeInfo.SignerId.Value.KeyId.cbData = pcbData;
                        signerEncodeInfo.SignerId.Value.KeyId.pbData = pvData.DangerousGetHandle();
                        GC.SuppressFinalize((object)pvData);
                    }
                }
                return signerEncodeInfo;
            }
        }

        [SecuritySafeCritical]
        internal static X509Certificate2Collection CreateBagOfCertificates(CmsSigner signer)
        {
            X509Certificate2Collection certificate2Collection = new X509Certificate2Collection();
            certificate2Collection.AddRange(signer.Certificates);
            if (signer.IncludeOption != X509IncludeOption.None)
            {
                if (signer.IncludeOption == X509IncludeOption.EndCertOnly)
                {
                    certificate2Collection.Add(signer.Certificate);
                }
                else
                {
                    int num = 1;
                    X509Chain x509Chain = new X509Chain();
                    x509Chain.Build(signer.Certificate);
                    if (x509Chain.ChainStatus.Length > 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
                        throw new CryptographicException(-2146762486);
                    if (signer.IncludeOption == X509IncludeOption.WholeChain)
                        num = x509Chain.ChainElements.Count;
                    else if (x509Chain.ChainElements.Count > 1)
                        num = x509Chain.ChainElements.Count - 1;
                    for (int index = 0; index < num; ++index)
                        certificate2Collection.Add(x509Chain.ChainElements[index].Certificate);
                }
            }
            return certificate2Collection;
        }

        [SecurityCritical]
        internal static unsafe SafeLocalAllocHandle CreateEncodedCertBlob(X509Certificate2Collection certificates)
        {
            SafeLocalAllocHandle localAllocHandle = SafeLocalAllocHandle.InvalidHandle;
            if (certificates.Count > 0)
            {
                localAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(certificates.Count * Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB))));
                CAPI.CRYPTOAPI_BLOB* cryptoapiBlobPtr = (CAPI.CRYPTOAPI_BLOB*)(void*)localAllocHandle.DangerousGetHandle();
                foreach (X509Certificate2 certificate in certificates)
                {
                    CAPI.CERT_CONTEXT certContext = *(CAPI.CERT_CONTEXT*)(void*)X509Utils.GetCertContext(certificate).DangerousGetHandle();
                    cryptoapiBlobPtr->cbData = certContext.cbCertEncoded;
                    cryptoapiBlobPtr->pbData = certContext.pbCertEncoded;
                    ++cryptoapiBlobPtr;
                }
            }
            return localAllocHandle;
        }

        [SecuritySafeCritical]
        internal static unsafe uint AddCertsToMessage(SafeCryptMsgHandle safeCryptMsgHandle, X509Certificate2Collection bagOfCerts, X509Certificate2Collection chainOfCerts)
        {
            uint num = 0U;
            foreach (X509Certificate2 certificate in chainOfCerts)
            {
                if (bagOfCerts.Find(X509FindType.FindByThumbprint, (object)certificate.Thumbprint, false).Count == 0)
                {
                    CAPI.CERT_CONTEXT certContext = *(CAPI.CERT_CONTEXT*)(void*)X509Utils.GetCertContext(certificate).DangerousGetHandle();
                    if (!CAPI.CryptMsgControl(safeCryptMsgHandle, 0U, 10U, new IntPtr((long)&new CAPI.CRYPTOAPI_BLOB()
                    {
                        cbData = certContext.cbCertEncoded,
                        pbData = certContext.pbCertEncoded
                    })))
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                    ++num;
                }
            }
            return num;
        }

        internal static X509Certificate2 FindCertificate(SubjectIdentifier identifier, X509Certificate2Collection certificates)
        {
            X509Certificate2 x509Certificate2 = (X509Certificate2)null;
            if (certificates != null && certificates.Count > 0)
            {
                switch (identifier.Type)
                {
                    case SubjectIdentifierType.IssuerAndSerialNumber:
                        X509Certificate2Collection certificate2Collection1 = certificates.Find(X509FindType.FindByIssuerDistinguishedName, (object)((X509IssuerSerial)identifier.Value).IssuerName, false);
                        if (certificate2Collection1.Count > 0)
                        {
                            X509Certificate2Collection certificate2Collection2 = certificate2Collection1.Find(X509FindType.FindBySerialNumber, (object)((X509IssuerSerial)identifier.Value).SerialNumber, false);
                            if (certificate2Collection2.Count > 0)
                            {
                                x509Certificate2 = certificate2Collection2[0];
                                break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    case SubjectIdentifierType.SubjectKeyIdentifier:
                        X509Certificate2Collection certificate2Collection3 = certificates.Find(X509FindType.FindBySubjectKeyIdentifier, identifier.Value, false);
                        if (certificate2Collection3.Count > 0)
                        {
                            x509Certificate2 = certificate2Collection3[0];
                            break;
                        }
                        else
                            break;
                }
            }
            return x509Certificate2;
        }

        private static void checkErr(int err)
        {
            if (-2146889724 != err)
                throw new CryptographicException(err);
        }

        [SecuritySafeCritical]
        internal static unsafe X509Certificate2 CreateDummyCertificate(CspParameters parameters)
        {
            System.Security.Cryptography.SafeCertContextHandle invalidHandle = System.Security.Cryptography.SafeCertContextHandle.InvalidHandle;
            SafeCryptProvHandle hCryptProv = SafeCryptProvHandle.InvalidHandle;
            uint dwFlags = 0U;
            if ((parameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags)
                dwFlags |= 32U;
            if ((parameters.Flags & CspProviderFlags.UseDefaultKeyContainer) != CspProviderFlags.NoFlags)
                dwFlags |= 4026531840U;
            if ((parameters.Flags & CspProviderFlags.NoPrompt) != CspProviderFlags.NoFlags)
                dwFlags |= 64U;
            if (!CAPI.CryptAcquireContext(ref hCryptProv, parameters.KeyContainerName, parameters.ProviderName, (uint)parameters.ProviderType, dwFlags))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            CAPI.CRYPT_KEY_PROV_INFO cryptKeyProvInfo = new CAPI.CRYPT_KEY_PROV_INFO();
            cryptKeyProvInfo.pwszProvName = parameters.ProviderName;
            cryptKeyProvInfo.pwszContainerName = parameters.KeyContainerName;
            cryptKeyProvInfo.dwProvType = (uint)parameters.ProviderType;
            cryptKeyProvInfo.dwKeySpec = (uint)parameters.KeyNumber;
            cryptKeyProvInfo.dwFlags = (parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore ? 32U : 0U;
            SafeLocalAllocHandle localAllocHandle1 = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_KEY_PROV_INFO))));
            Marshal.StructureToPtr((object)cryptKeyProvInfo, localAllocHandle1.DangerousGetHandle(), false);
            CAPI.CRYPT_ALGORITHM_IDENTIFIER algorithmIdentifier = new CAPI.CRYPT_ALGORITHM_IDENTIFIER();
            algorithmIdentifier.pszObjId = "1.3.14.3.2.29";
            SafeLocalAllocHandle localAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER))));
            Marshal.StructureToPtr((object)algorithmIdentifier, localAllocHandle2.DangerousGetHandle(), false);
            X500DistinguishedName distinguishedName = new X500DistinguishedName("cn=CMS Signer Dummy Certificate");
            System.Security.Cryptography.SafeCertContextHandle selfSignCertificate;
            fixed (byte* numPtr = distinguishedName.RawData)
                selfSignCertificate = CAPI.CAPIUnsafe.CertCreateSelfSignCertificate(hCryptProv, new IntPtr((void*)&new CAPI.CRYPTOAPI_BLOB()
                {
                    cbData = (uint)distinguishedName.RawData.Length,
                    pbData = new IntPtr((void*)numPtr)
                }), 1U, localAllocHandle1.DangerousGetHandle(), localAllocHandle2.DangerousGetHandle(), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            Marshal.DestroyStructure(localAllocHandle1.DangerousGetHandle(), typeof(CAPI.CRYPT_KEY_PROV_INFO));
            localAllocHandle1.Dispose();
            Marshal.DestroyStructure(localAllocHandle2.DangerousGetHandle(), typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER));
            localAllocHandle2.Dispose();
            if (selfSignCertificate == null || selfSignCertificate.IsInvalid)
                throw new CryptographicException(Marshal.GetLastWin32Error());
            X509Certificate2 x509Certificate2 = new X509Certificate2(selfSignCertificate.DangerousGetHandle());
            selfSignCertificate.Dispose();
            return x509Certificate2;
        }

        private struct I_CRYPT_ATTRIBUTE
        {
            internal IntPtr pszObjId;
            internal uint cValue;
            internal IntPtr rgValue;
        }
    }
}
