// Type: System.Security.Cryptography.Pkcs.Pkcs9SigningTime
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime"/> class defines the signing date and time of a signature. A <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime"/> object can  be used as an authenticated attribute of a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/>  object when an authenticated date and time are to accompany a digital signature.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class Pkcs9SigningTime : Pkcs9AttributeObject
    {
        private DateTime m_signingTime;
        private bool m_decoded;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.SigningTime"/> property retrieves a <see cref="T:System.DateTime"/> structure that represents the date and time that the message was signed.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.DateTime"/> structure that contains the date and time the document was signed.
        /// </returns>
        public DateTime SigningTime
        {
            get
            {
                if (!this.m_decoded && this.RawData != null)
                    this.Decode();
                return this.m_signingTime;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime"/> class.
        /// </summary>
        public Pkcs9SigningTime()
            : this(DateTime.Now)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.DateTime)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime"/> class by using the specified signing date and time.
        /// </summary>
        /// <param name="signingTime">A <see cref="T:System.DateTime"/>  structure that represents the signing date and time of the signature.</param>
        public Pkcs9SigningTime(DateTime signingTime)
            : base("1.2.840.113549.1.9.5", Pkcs9SigningTime.Encode(signingTime))
        {
            this.m_signingTime = signingTime;
            this.m_decoded = true;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.Byte[])"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime"/> class by using the specified array of byte values as the encoded signing date and time of the content of a CMS/PKCS #7 message.
        /// </summary>
        /// <param name="encodedSigningTime">An array of byte values that specifies the encoded signing date and time of the CMS/PKCS #7 message.</param>
        public Pkcs9SigningTime(byte[] encodedSigningTime)
            : base("1.2.840.113549.1.9.5", encodedSigningTime)
        {
        }

        /// <summary>
        /// Copies information from a <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object.
        /// </summary>
        /// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object from which to copy information.</param>
        public override void CopyFrom(AsnEncodedData asnEncodedData)
        {
            base.CopyFrom(asnEncodedData);
            this.m_decoded = false;
        }

        [SecuritySafeCritical]
        private void Decode()
        {
            uint cbDecodedValue = 0U;
            SafeLocalAllocHandle decodedValue = (SafeLocalAllocHandle)null;
            if (!CAPI.DecodeObject(new IntPtr(17L), this.RawData, out decodedValue, out cbDecodedValue))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            long fileTime = Marshal.ReadInt64(decodedValue.DangerousGetHandle());
            decodedValue.Dispose();
            this.m_signingTime = DateTime.FromFileTimeUtc(fileTime);
            this.m_decoded = true;
        }

        [SecuritySafeCritical]
        private static byte[] Encode(DateTime signingTime)
        {
            long val = signingTime.ToFileTimeUtc();
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(long))));
            Marshal.WriteInt64(localAllocHandle.DangerousGetHandle(), val);
            byte[] encodedData = new byte[0];
            if (!CAPI.EncodeObject("1.2.840.113549.1.9.5", localAllocHandle.DangerousGetHandle(), out encodedData))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            localAllocHandle.Dispose();
            return encodedData;
        }
    }
}
