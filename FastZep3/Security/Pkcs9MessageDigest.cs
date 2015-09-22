// Type: System.Security.Cryptography.Pkcs.Pkcs9MessageDigest
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest"/> class defines the message digest of a CMS/PKCS #7 message.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class Pkcs9MessageDigest : Pkcs9AttributeObject
    {
        private byte[] m_messageDigest;
        private bool m_decoded;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.MessageDigest"/> property retrieves the message digest.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values that contains the message digest.
        /// </returns>
        public byte[] MessageDigest
        {
            get
            {
                if (!this.m_decoded && this.RawData != null)
                    this.Decode();
                return this.m_messageDigest;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest"/> class.
        /// </summary>
        public Pkcs9MessageDigest()
            : base("1.2.840.113549.1.9.4")
        {
        }

        internal Pkcs9MessageDigest(byte[] encodedMessageDigest)
            : base("1.2.840.113549.1.9.4", encodedMessageDigest)
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
            this.m_messageDigest = PkcsUtils.DecodeOctetBytes(this.RawData);
            this.m_decoded = true;
        }
    }
}
