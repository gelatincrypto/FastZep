// Type: System.Security.Cryptography.Pkcs.Pkcs9DocumentName
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName"/> class defines the name of a CMS/PKCS #7 message.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class Pkcs9DocumentName : Pkcs9AttributeObject
    {
        private string m_documentName;
        private bool m_decoded;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.DocumentName"/> property retrieves the document name.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> object that contains the document name.
        /// </returns>
        public string DocumentName
        {
            get
            {
                if (!this.m_decoded && this.RawData != null)
                    this.Decode();
                return this.m_documentName;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName"/> class.
        /// </summary>
        public Pkcs9DocumentName()
            : base("1.3.6.1.4.1.311.88.2.1")
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.String)"/> constructor creates an instance of the  <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName"/> class by using the specified name for the CMS/PKCS #7 message.
        /// </summary>
        /// <param name="documentName">A  <see cref="T:System.String"/>   object that specifies the name for the CMS/PKCS #7 message.</param>
        public Pkcs9DocumentName(string documentName)
            : base("1.3.6.1.4.1.311.88.2.1", Pkcs9DocumentName.Encode(documentName))
        {
            this.m_documentName = documentName;
            this.m_decoded = true;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.Byte[])"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName"/> class by using the specified array of byte values as the encoded name of the content of a CMS/PKCS #7 message.
        /// </summary>
        /// <param name="encodedDocumentName">An array of byte values that specifies the encoded name of the CMS/PKCS #7 message.</param>
        public Pkcs9DocumentName(byte[] encodedDocumentName)
            : base("1.3.6.1.4.1.311.88.2.1", encodedDocumentName)
        {
        }

        /// <summary>
        /// Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object.
        /// </summary>
        /// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object from which to copy information.</param>
        public override void CopyFrom(AsnEncodedData asnEncodedData)
        {
            base.CopyFrom(asnEncodedData);
            this.m_decoded = false;
        }

        private void Decode()
        {
            this.m_documentName = PkcsUtils.DecodeOctetString(this.RawData);
            this.m_decoded = true;
        }

        private static byte[] Encode(string documentName)
        {
            if (string.IsNullOrEmpty(documentName))
                throw new ArgumentNullException("documentName");
            else
                return PkcsUtils.EncodeOctetString(documentName);
        }
    }
}
