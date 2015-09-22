// Type: System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo
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
    /// The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo"/> class defines key agreement recipient information.        Key agreement algorithms typically use the Diffie-Hellman key agreement algorithm, in which the two parties that establish a shared cryptographic key both take part in its generation and, by definition, agree on that key. This is in contrast to key transport algorithms, in which one party generates the key unilaterally and sends, or transports it, to the other party.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class KeyAgreeRecipientInfo : RecipientInfo
    {
        private CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO m_encryptedKeyInfo;
        private uint m_originatorChoice;
        private int m_version;
        private SubjectIdentifierOrKey m_originatorIdentifier;
        private byte[] m_userKeyMaterial;
        private AlgorithmIdentifier m_encryptionAlgorithm;
        private SubjectIdentifier m_recipientIdentifier;
        private byte[] m_encryptedKey;
        private DateTime m_date;
        private CryptographicAttributeObject m_otherKeyAttribute;
        private uint m_subIndex;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Version"/> property retrieves the version of the key agreement recipient. This is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key agreement algorithm.
        /// </summary>
        /// 
        /// <returns>
        /// An int  that represents the version of the  <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo"/>  object.
        /// </returns>
        public override int Version
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_version;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OriginatorIdentifierOrKey"/> property retrieves information about the originator of the key agreement for key agreement algorithms that warrant it.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey"/> object that contains information about the originator of the key agreement.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public SubjectIdentifierOrKey OriginatorIdentifierOrKey
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_originatorIdentifier == null)
                    this.m_originatorIdentifier = (int)this.m_originatorChoice != 1 ? new SubjectIdentifierOrKey(((CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)this.CmsgRecipientInfo).OriginatorPublicKeyInfo) : new SubjectIdentifierOrKey(((CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)this.CmsgRecipientInfo).OriginatorCertId);
                return this.m_originatorIdentifier;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.RecipientIdentifier"/> property retrieves the identifier of the recipient.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier"/> object that contains the identifier of the recipient.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public override SubjectIdentifier RecipientIdentifier
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_recipientIdentifier == null)
                    this.m_recipientIdentifier = new SubjectIdentifier(this.m_encryptedKeyInfo.RecipientId);
                return this.m_recipientIdentifier;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Date"/> property retrieves the date and time of the start of the key agreement protocol by the originator.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.DateTime"/> object that contains the value of the date and time of the start of the key agreement protocol by the originator.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public DateTime Date
        {
            get
            {
                if (this.m_date == DateTime.MinValue)
                {
                    if (this.RecipientIdentifier.Type != SubjectIdentifierType.SubjectKeyIdentifier)
                        throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_Key_Agree_Date_Not_Available"));
                    this.m_date = DateTime.FromFileTimeUtc((long)(uint)this.m_encryptedKeyInfo.Date.dwHighDateTime << 32 | (long)(uint)this.m_encryptedKeyInfo.Date.dwLowDateTime);
                }
                return this.m_date;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OtherKeyAttribute"/> property retrieves attributes of the keying material.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.CryptographicAttribute"/> object that contains attributes of the keying material.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public CryptographicAttributeObject OtherKeyAttribute
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_otherKeyAttribute == null)
                {
                    if (this.RecipientIdentifier.Type != SubjectIdentifierType.SubjectKeyIdentifier)
                        throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available"));
                    if (this.m_encryptedKeyInfo.pOtherAttr != IntPtr.Zero)
                        this.m_otherKeyAttribute = new CryptographicAttributeObject((CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE)Marshal.PtrToStructure(this.m_encryptedKeyInfo.pOtherAttr, typeof(CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE)));
                }
                return this.m_otherKeyAttribute;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.KeyEncryptionAlgorithm"/> property retrieves the algorithm used to perform the key agreement.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> object that contains the value of the algorithm used to perform the key agreement.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public override AlgorithmIdentifier KeyEncryptionAlgorithm
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_encryptionAlgorithm == null)
                    this.m_encryptionAlgorithm = (int)this.m_originatorChoice != 1 ? new AlgorithmIdentifier(((CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)this.CmsgRecipientInfo).KeyEncryptionAlgorithm) : new AlgorithmIdentifier(((CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)this.CmsgRecipientInfo).KeyEncryptionAlgorithm);
                return this.m_encryptionAlgorithm;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.EncryptedKey"/> property retrieves the encrypted recipient keying material.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values that contain the encrypted recipient keying material.
        /// </returns>
        public override byte[] EncryptedKey
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_encryptedKey.Length == 0 && this.m_encryptedKeyInfo.EncryptedKey.cbData > 0U)
                {
                    this.m_encryptedKey = new byte[(IntPtr)this.m_encryptedKeyInfo.EncryptedKey.cbData];
                    Marshal.Copy(this.m_encryptedKeyInfo.EncryptedKey.pbData, this.m_encryptedKey, 0, this.m_encryptedKey.Length);
                }
                return this.m_encryptedKey;
            }
        }

        internal CAPI.CERT_ID RecipientId
        {
            get
            {
                return this.m_encryptedKeyInfo.RecipientId;
            }
        }

        internal uint SubIndex
        {
            get
            {
                return this.m_subIndex;
            }
        }

        private KeyAgreeRecipientInfo()
        {
        }

        [SecurityCritical]
        internal KeyAgreeRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO certIdRecipient, uint index, uint subIndex)
            : base(RecipientInfoType.KeyAgreement, RecipientSubType.CertIdKeyAgreement, pRecipientInfo, (object)certIdRecipient, index)
        {
            CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO encryptedKeyInfo = (CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO)Marshal.PtrToStructure(Marshal.ReadIntPtr(new IntPtr((long)certIdRecipient.rgpRecipientEncryptedKeys + (long)subIndex * (long)Marshal.SizeOf(typeof(IntPtr)))), typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO));
            this.Reset(1U, certIdRecipient.dwVersion, encryptedKeyInfo, subIndex);
        }

        [SecurityCritical]
        internal KeyAgreeRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO publicKeyRecipient, uint index, uint subIndex)
            : base(RecipientInfoType.KeyAgreement, RecipientSubType.PublicKeyAgreement, pRecipientInfo, (object)publicKeyRecipient, index)
        {
            CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO encryptedKeyInfo = (CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO)Marshal.PtrToStructure(Marshal.ReadIntPtr(new IntPtr((long)publicKeyRecipient.rgpRecipientEncryptedKeys + (long)subIndex * (long)Marshal.SizeOf(typeof(IntPtr)))), typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO));
            this.Reset(2U, publicKeyRecipient.dwVersion, encryptedKeyInfo, subIndex);
        }

        private void Reset(uint originatorChoice, uint version, CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO encryptedKeyInfo, uint subIndex)
        {
            this.m_encryptedKeyInfo = encryptedKeyInfo;
            this.m_originatorChoice = originatorChoice;
            this.m_version = (int)version;
            this.m_originatorIdentifier = (SubjectIdentifierOrKey)null;
            this.m_userKeyMaterial = new byte[0];
            this.m_encryptionAlgorithm = (AlgorithmIdentifier)null;
            this.m_recipientIdentifier = (SubjectIdentifier)null;
            this.m_encryptedKey = new byte[0];
            this.m_date = DateTime.MinValue;
            this.m_otherKeyAttribute = (CryptographicAttributeObject)null;
            this.m_subIndex = subIndex;
        }
    }
}
