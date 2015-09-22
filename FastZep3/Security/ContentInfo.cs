// Type: System.Security.Cryptography.Pkcs.ContentInfo
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> class represents the CMS/PKCS #7 ContentInfo data structure as defined in the CMS/PKCS #7 standards document. This data structure is the basis for all CMS/PKCS #7 messages.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class ContentInfo
    {
        private IntPtr m_pContent = IntPtr.Zero;
        private Oid m_contentType;
        private byte[] m_content;
        private GCHandle m_gcHandle;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.ContentType"/> property  retrieves the <see cref="T:System.Security.Cryptography.Oid"/>   object that contains the <paramref name="object identifier"/> (OID)  of the content type of the inner content of the CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Oid"/>  object that contains the OID value that represents the content type.
        /// </returns>
        public Oid ContentType
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_contentType;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.Content"/> property  retrieves the content of the CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values that represents the content data.
        /// </returns>
        public byte[] Content
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_content;
            }
        }

        internal IntPtr pContent
        {
            [SecurityCritical]
            get
            {
                if (IntPtr.Zero == this.m_pContent && this.m_content != null && this.m_content.Length != 0)
                {
                    this.m_gcHandle = GCHandle.Alloc((object)this.m_content, GCHandleType.Pinned);
                    this.m_pContent = Marshal.UnsafeAddrOfPinnedArrayElement((Array)this.m_content, 0);
                }
                return this.m_pContent;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Byte[])"/> constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> class by using an array of byte values as the data and a default <paramref name="object identifier"/> (OID) that represents the content type.
        /// </summary>
        /// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object.</param><exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument. </exception>
        public ContentInfo(byte[] content)
            : this(new Oid("1.2.840.113549.1.7.1"), content)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Security.Cryptography.Oid,System.Byte[])"/>  constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> class by using the specified content type and an array of byte values as the data.
        /// </summary>
        /// <param name="contentType">An <see cref="T:System.Security.Cryptography.Oid"/> object that contains an <paramref name="object identifier"/> (OID) that specifies the content type of the content. This can be data, digestedData, encryptedData, envelopedData, hashedData, signedAndEnvelopedData, or signedData.  For more information, see  Remarks.</param><param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object.</param><exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument. </exception>
        public ContentInfo(Oid contentType, byte[] content)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (content == null)
                throw new ArgumentNullException("content");
            this.m_contentType = contentType;
            this.m_content = content;
        }

        private ContentInfo()
            : this(new Oid("1.2.840.113549.1.7.1"), new byte[0])
        {
        }

        internal ContentInfo(string contentType, byte[] content)
            : this(new Oid(contentType), content)
        {
        }

        [SecuritySafeCritical]
        ~ContentInfo()
        {
            if (!this.m_gcHandle.IsAllocated)
                return;
            this.m_gcHandle.Free();
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.GetContentType(System.Byte[])"/> static method  retrieves the outer content type of the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> message represented by an array of byte values.
        /// </summary>
        /// 
        /// <returns>
        /// If the method succeeds, the method returns an <see cref="T:System.Security.Cryptography.Oid"/> object that contains the outer content type of the specified encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> message.If the method fails, it throws an exception.
        /// </returns>
        /// <param name="encodedMessage">An array of byte values that represents the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> message from which to retrieve the outer content type.</param><exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred during a cryptographic operation.</exception>
        [SecuritySafeCritical]
        public static Oid GetContentType(byte[] encodedMessage)
        {
            if (encodedMessage == null)
                throw new ArgumentNullException("encodedMessage");
            SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CAPISafe.CryptMsgOpenToDecode(65537U, 0U, 0U, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if (!CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, encodedMessage, (uint)encodedMessage.Length, true))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            Oid oid;
            switch (PkcsUtils.GetMessageType(safeCryptMsgHandle))
            {
                case 1U:
                    oid = new Oid("1.2.840.113549.1.7.1");
                    break;
                case 2U:
                    oid = new Oid("1.2.840.113549.1.7.2");
                    break;
                case 3U:
                    oid = new Oid("1.2.840.113549.1.7.3");
                    break;
                case 4U:
                    oid = new Oid("1.2.840.113549.1.7.4");
                    break;
                case 5U:
                    oid = new Oid("1.2.840.113549.1.7.5");
                    break;
                case 6U:
                    oid = new Oid("1.2.840.113549.1.7.6");
                    break;
                default:
                    throw new CryptographicException(-2146889724);
            }
            safeCryptMsgHandle.Dispose();
            return oid;
        }
    }
}
