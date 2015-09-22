// Type: System.Security.Cryptography.Pkcs.Pkcs9ContentType
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType"/> class defines the type of the content of a CMS/PKCS #7 message.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class Pkcs9ContentType : Pkcs9AttributeObject
    {
        private Oid m_contentType;
        private bool m_decoded;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9ContentType.ContentType"/> property gets an <see cref="T:System.Security.Cryptography.Oid"/> object that contains the content type.
        /// </summary>
        /// 
        /// <returns>
        /// An  <see cref="T:System.Security.Cryptography.Oid"/> object that contains the content type.
        /// </returns>
        public Oid ContentType
        {
            get
            {
                if (!this.m_decoded && this.RawData != null)
                    this.Decode();
                return this.m_contentType;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9ContentType.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType"/> class.
        /// </summary>
        public Pkcs9ContentType()
            : base("1.2.840.113549.1.9.3")
        {
        }

        internal Pkcs9ContentType(byte[] encodedContentType)
            : base("1.2.840.113549.1.9.3", encodedContentType)
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
            if (this.RawData.Length < 2 || (int)this.RawData[1] != this.RawData.Length - 2)
                throw new CryptographicException(-2146885630);
            if ((int)this.RawData[0] != 6)
                throw new CryptographicException(-2146881269);
            this.m_contentType = new Oid(PkcsUtils.DecodeObjectIdentifier(this.RawData, 2));
            this.m_decoded = true;
        }
    }
}
