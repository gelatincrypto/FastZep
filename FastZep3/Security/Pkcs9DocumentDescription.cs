// Type: System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription"/> class defines the description of the content of a CMS/PKCS #7 message.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class Pkcs9DocumentDescription : Pkcs9AttributeObject
    {
        private string m_documentDescription;
        private bool m_decoded;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.DocumentDescription"/> property retrieves the document description.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> object that contains the document description.
        /// </returns>
        public string DocumentDescription
        {
            get
            {
                if (!this.m_decoded && this.RawData != null)
                    this.Decode();
                return this.m_documentDescription;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription"/> class.
        /// </summary>
        public Pkcs9DocumentDescription()
            : base("1.3.6.1.4.1.311.88.2.2")
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.String)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription"/> class by using the specified description of the content of a CMS/PKCS #7 message.
        /// </summary>
        /// <param name="documentDescription">An instance of the <see cref="T:System.String"/>  class that specifies the description for the CMS/PKCS #7 message.</param>
        public Pkcs9DocumentDescription(string documentDescription)
            : base("1.3.6.1.4.1.311.88.2.2", Pkcs9DocumentDescription.Encode(documentDescription))
        {
            this.m_documentDescription = documentDescription;
            this.m_decoded = true;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.Byte[])"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription"/> class by using the specified array of byte values as the encoded description of the content of a CMS/PKCS #7 message.
        /// </summary>
        /// <param name="encodedDocumentDescription">An array of byte values that specifies the encoded description of the CMS/PKCS #7 message.</param>
        public Pkcs9DocumentDescription(byte[] encodedDocumentDescription)
            : base("1.3.6.1.4.1.311.88.2.2", encodedDocumentDescription)
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
            this.m_documentDescription = PkcsUtils.DecodeOctetString(this.RawData);
            this.m_decoded = true;
        }

        private static byte[] Encode(string documentDescription)
        {
            if (string.IsNullOrEmpty(documentDescription))
                throw new ArgumentNullException("documentDescription");
            else
                return PkcsUtils.EncodeOctetString(documentDescription);
        }
    }
}
