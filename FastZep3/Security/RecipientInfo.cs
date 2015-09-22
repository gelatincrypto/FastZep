// Type: System.Security.Cryptography.Pkcs.RecipientInfo
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System.Runtime;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> class represents information about a CMS/PKCS #7 message recipient. The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> class is an abstract class inherited by the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo"/> and <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo"/> classes.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public abstract class RecipientInfo
    {
        private RecipientInfoType m_recipentInfoType;
        private RecipientSubType m_recipientSubType;
        [SecurityCritical]
        private SafeLocalAllocHandle m_pCmsgRecipientInfo;
        private object m_cmsgRecipientInfo;
        private uint m_index;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Type"/> property retrieves the type of the recipient. The type of the recipient determines which of two major protocols is used to establish a key between the originator and the recipient of a CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// A value of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType"/> enumeration that defines the type of the recipient.
        /// </returns>
        public RecipientInfoType Type
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_recipentInfoType;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Version"/> abstract property retrieves the version of the recipient information. Derived classes automatically set this property for their objects, and the value indicates whether it is using PKCS #7 or Cryptographic Message Syntax (CMS) to protect messages. The version also implies whether the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object establishes a cryptographic key by a key agreement algorithm or a key transport algorithm.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Int32"/> value that represents the version of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object.
        /// </returns>
        public abstract int Version { get; }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.RecipientIdentifier"/> abstract property retrieves the identifier of the recipient.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier"/> object that contains the identifier of the recipient.
        /// </returns>
        public abstract SubjectIdentifier RecipientIdentifier { get; }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.KeyEncryptionAlgorithm"/> abstract property retrieves the algorithm used to perform the key establishment.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> object that contains the value of the algorithm used to establish the key between the originator and recipient of the CMS/PKCS #7 message.
        /// </returns>
        public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.EncryptedKey"/> abstract property retrieves the encrypted recipient keying material.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values that contain the encrypted recipient keying material.
        /// </returns>
        public abstract byte[] EncryptedKey { get; }

        internal RecipientSubType SubType
        {
            get
            {
                return this.m_recipientSubType;
            }
        }

        internal SafeLocalAllocHandle pCmsgRecipientInfo
        {
            [SecurityCritical]
            get
            {
                return this.m_pCmsgRecipientInfo;
            }
        }

        internal object CmsgRecipientInfo
        {
            get
            {
                return this.m_cmsgRecipientInfo;
            }
        }

        internal uint Index
        {
            get
            {
                return this.m_index;
            }
        }

        internal RecipientInfo()
        {
        }

        [SecurityCritical]
        internal RecipientInfo(RecipientInfoType recipientInfoType, RecipientSubType recipientSubType, SafeLocalAllocHandle pCmsgRecipientInfo, object cmsgRecipientInfo, uint index)
        {
            if (recipientInfoType < RecipientInfoType.Unknown || recipientInfoType > RecipientInfoType.KeyAgreement)
                recipientInfoType = RecipientInfoType.Unknown;
            if (recipientSubType < RecipientSubType.Unknown || recipientSubType > RecipientSubType.PublicKeyAgreement)
                recipientSubType = RecipientSubType.Unknown;
            this.m_recipentInfoType = recipientInfoType;
            this.m_recipientSubType = recipientSubType;
            this.m_pCmsgRecipientInfo = pCmsgRecipientInfo;
            this.m_cmsgRecipientInfo = cmsgRecipientInfo;
            this.m_index = index;
        }
    }
}
