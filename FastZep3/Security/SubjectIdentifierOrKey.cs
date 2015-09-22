// Type: System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey
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
    /// The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey"/> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/>.  The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class SubjectIdentifierOrKey
    {
        private SubjectIdentifierOrKeyType m_type;
        private object m_value;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type"/> property retrieves the type of subject identifier or key. The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.
        /// </summary>
        /// 
        /// <returns>
        /// A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType"/>  enumeration that specifies the type of subject identifier.
        /// </returns>
        public SubjectIdentifierOrKeyType Type
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_type;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value"/> property retrieves the value of the subject identifier or  key. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type"/> property to determine the type of subject identifier or key, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value"/> property to retrieve the corresponding value.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Object"/> object that represents the value of the subject identifier or key. This <see cref="T:System.Object"/> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type"/> property.<see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type"/> propertyObjectIssuerAndSerialNumber<see cref="T:System.Security.Cryptographyxml.X509IssuerSerial"/>SubjectKeyIdentifier<see cref="T:System.String"/>PublicKeyInfo<see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo"/>
        /// </returns>
        public object Value
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_value;
            }
        }

        private SubjectIdentifierOrKey()
        {
        }

        internal SubjectIdentifierOrKey(SubjectIdentifierOrKeyType type, object value)
        {
            this.Reset(type, value);
        }

        [SecurityCritical]
        internal SubjectIdentifierOrKey(CAPI.CERT_ID certId)
        {
            switch (certId.dwIdChoice)
            {
                case 1U:
                    this.Reset(SubjectIdentifierOrKeyType.IssuerAndSerialNumber, (object)PkcsUtils.DecodeIssuerSerial(certId.Value.IssuerSerialNumber));
                    break;
                case 2U:
                    byte[] numArray = new byte[(IntPtr)certId.Value.KeyId.cbData];
                    Marshal.Copy(certId.Value.KeyId.pbData, numArray, 0, numArray.Length);
                    this.Reset(SubjectIdentifierOrKeyType.SubjectKeyIdentifier, (object)X509Utils.EncodeHexString(numArray));
                    break;
                default:
                    throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), certId.dwIdChoice.ToString((IFormatProvider)CultureInfo.InvariantCulture));
            }
        }

        [SecurityCritical]
        internal SubjectIdentifierOrKey(CAPI.CERT_PUBLIC_KEY_INFO publicKeyInfo)
        {
            this.Reset(SubjectIdentifierOrKeyType.PublicKeyInfo, (object)new PublicKeyInfo(publicKeyInfo));
        }

        internal void Reset(SubjectIdentifierOrKeyType type, object value)
        {
            switch (type)
            {
                case SubjectIdentifierOrKeyType.Unknown:
                    this.m_type = type;
                    this.m_value = value;
                    break;
                case SubjectIdentifierOrKeyType.IssuerAndSerialNumber:
                    if (value.GetType() != typeof(X509IssuerSerial))
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
                    else
                        goto case 0;
                case SubjectIdentifierOrKeyType.SubjectKeyIdentifier:
                    if (!PkcsUtils.CmsSupported())
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
                    if (value.GetType() != typeof(string))
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
                    else
                        goto case 0;
                case SubjectIdentifierOrKeyType.PublicKeyInfo:
                    if (!PkcsUtils.CmsSupported())
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
                    if (value.GetType() != typeof(PublicKeyInfo))
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
                    else
                        goto case 0;
                default:
                    throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), ((object)type).ToString());
            }
        }
    }
}
