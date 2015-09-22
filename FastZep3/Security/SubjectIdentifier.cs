// Type: System.Security.Cryptography.Pkcs.SubjectIdentifier
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Globalization;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier"/> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/>.  The subject can be identified by the certificate issuer and serial number or the subject key.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class SubjectIdentifier
    {
        private SubjectIdentifierType m_type;
        private object m_value;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type"/> property retrieves the type of subject identifier. The subject can be identified by the certificate issuer and serial number or the subject key.
        /// </summary>
        /// 
        /// <returns>
        /// A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/>  enumeration that identifies the type of subject.
        /// </returns>
        public SubjectIdentifierType Type
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_type;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value"/> property retrieves the value of the subject identifier. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type"/> property to determine the type of subject identifier, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value"/> property to retrieve the corresponding value.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Object"/> object that represents the value of the subject identifier. This <see cref="T:System.Object"/> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type"/> property.<see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type"/> propertyObjectIssuerAndSerialNumber<see cref="T:System.Security.Cryptographyxml.X509IssuerSerial"/>SubjectKeyIdentifier<see cref="T:System.String"/>
        /// </returns>
        public object Value
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_value;
            }
        }

        private SubjectIdentifier()
        {
        }

        [SecurityCritical]
        internal SubjectIdentifier(CAPI.CERT_INFO certInfo)
            : this(certInfo.Issuer, certInfo.SerialNumber)
        {
        }

        [SecurityCritical]
        internal SubjectIdentifier(CAPI.CMSG_SIGNER_INFO signerInfo)
            : this(signerInfo.Issuer, signerInfo.SerialNumber)
        {
        }

        internal SubjectIdentifier(SubjectIdentifierType type, object value)
        {
            this.Reset(type, value);
        }

        [SecurityCritical]
        internal SubjectIdentifier(CAPI.CRYPTOAPI_BLOB issuer, CAPI.CRYPTOAPI_BLOB serialNumber)
        {
            bool flag = true;
            byte* numPtr = (byte*)(void*)serialNumber.pbData;
            for (uint index = 0U; index < serialNumber.cbData; ++index)
            {
                if ((int)*numPtr++ != 0)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                byte[] numArray = new byte[(IntPtr)issuer.cbData];
                Marshal.Copy(issuer.pbData, numArray, 0, numArray.Length);
                if (string.Compare("CN=Dummy Signer", new X500DistinguishedName(numArray).Name, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.Reset(SubjectIdentifierType.NoSignature, (object)null);
                    return;
                }
            }
            if (flag)
            {
                this.m_type = SubjectIdentifierType.SubjectKeyIdentifier;
                this.m_value = (object)string.Empty;
                uint cbDecodedValue = 0U;
                SafeLocalAllocHandle decodedValue = SafeLocalAllocHandle.InvalidHandle;
                if (!CAPI.DecodeObject(new IntPtr(7L), issuer.pbData, issuer.cbData, out decodedValue, out cbDecodedValue))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                using (decodedValue)
                {
                    CAPI.CERT_NAME_INFO certNameInfo = (CAPI.CERT_NAME_INFO)Marshal.PtrToStructure(decodedValue.DangerousGetHandle(), typeof(CAPI.CERT_NAME_INFO));
                    for (uint index1 = 0U; index1 < certNameInfo.cRDN; ++index1)
                    {
                        CAPI.CERT_RDN certRdn = (CAPI.CERT_RDN)Marshal.PtrToStructure(new IntPtr((long)certNameInfo.rgRDN + (long)index1 * (long)Marshal.SizeOf(typeof(CAPI.CERT_RDN))), typeof(CAPI.CERT_RDN));
                        for (uint index2 = 0U; index2 < certRdn.cRDNAttr; ++index2)
                        {
                            CAPI.CERT_RDN_ATTR certRdnAttr = (CAPI.CERT_RDN_ATTR)Marshal.PtrToStructure(new IntPtr((long)certRdn.rgRDNAttr + (long)index2 * (long)Marshal.SizeOf(typeof(CAPI.CERT_RDN_ATTR))), typeof(CAPI.CERT_RDN_ATTR));
                            if (string.Compare("1.3.6.1.4.1.311.10.7.1", certRdnAttr.pszObjId, StringComparison.OrdinalIgnoreCase) == 0 && (int)certRdnAttr.dwValueType == 2)
                            {
                                byte[] numArray = new byte[(IntPtr)certRdnAttr.Value.cbData];
                                Marshal.Copy(certRdnAttr.Value.pbData, numArray, 0, numArray.Length);
                                this.Reset(SubjectIdentifierType.SubjectKeyIdentifier, (object)X509Utils.EncodeHexString(numArray));
                                return;
                            }
                        }
                    }
                }
                throw new CryptographicException(-2146889715);
            }
            else
            {
                CAPI.CERT_ISSUER_SERIAL_NUMBER pIssuerAndSerial;
                pIssuerAndSerial.Issuer = issuer;
                pIssuerAndSerial.SerialNumber = serialNumber;
                this.Reset(SubjectIdentifierType.IssuerAndSerialNumber, (object)PkcsUtils.DecodeIssuerSerial(pIssuerAndSerial));
            }
        }

        [SecurityCritical]
        internal SubjectIdentifier(CAPI.CERT_ID certId)
        {
            switch (certId.dwIdChoice)
            {
                case 1U:
                    this.Reset(SubjectIdentifierType.IssuerAndSerialNumber, (object)PkcsUtils.DecodeIssuerSerial(certId.Value.IssuerSerialNumber));
                    break;
                case 2U:
                    byte[] numArray = new byte[(IntPtr)certId.Value.KeyId.cbData];
                    Marshal.Copy(certId.Value.KeyId.pbData, numArray, 0, numArray.Length);
                    this.Reset(SubjectIdentifierType.SubjectKeyIdentifier, (object)X509Utils.EncodeHexString(numArray));
                    break;
                default:
                    throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), certId.dwIdChoice.ToString((IFormatProvider)CultureInfo.InvariantCulture));
            }
        }

        internal void Reset(SubjectIdentifierType type, object value)
        {
            switch (type)
            {
                case SubjectIdentifierType.Unknown:
                case SubjectIdentifierType.NoSignature:
                    this.m_type = type;
                    this.m_value = value;
                    break;
                case SubjectIdentifierType.IssuerAndSerialNumber:
                    if (value.GetType() != typeof(X509IssuerSerial))
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
                    else
                        goto case 0;
                case SubjectIdentifierType.SubjectKeyIdentifier:
                    if (!PkcsUtils.CmsSupported())
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
                    if (value.GetType() != typeof(string))
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
                    else
                        goto case 0;
                default:
                    throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), ((object)type).ToString());
            }
        }
    }
}
