// Type: System.Security.Cryptography.Pkcs.KeyTransRecipientInfo
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
    /// The <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo"/> class defines key transport recipient information.        Key transport algorithms typically use the RSA algorithm, in which  an originator establishes a shared cryptographic key with a recipient by generating that key and  then transporting it to the recipient. This is in contrast to key agreement algorithms, in which the two parties that will be using a cryptographic key both take part in its generation, thereby mutually agreeing to that key.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class KeyTransRecipientInfo : RecipientInfo
    {
        private int m_version;
        private SubjectIdentifier m_recipientIdentifier;
        private AlgorithmIdentifier m_encryptionAlgorithm;
        private byte[] m_encryptedKey;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.Version"/> property retrieves the version of the key transport recipient. The version of the key transport recipient is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key transport algorithm.
        /// </summary>
        /// 
        /// <returns>
        /// An int value that represents the version of the key transport <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object.
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
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.RecipientIdentifier"/> property retrieves the subject identifier associated with the encrypted content.
        /// </summary>
        /// 
        /// <returns>
        /// A   <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier"/>  object that  stores the identifier of the recipient taking part in the key transport.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public override SubjectIdentifier RecipientIdentifier
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_recipientIdentifier == null)
                    this.m_recipientIdentifier = this.SubType != RecipientSubType.CmsKeyTransport ? new SubjectIdentifier((CAPI.CERT_INFO)this.CmsgRecipientInfo) : new SubjectIdentifier(((CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)this.CmsgRecipientInfo).RecipientId);
                return this.m_recipientIdentifier;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.KeyEncryptionAlgorithm"/> property retrieves the key encryption algorithm used to encrypt the content encryption key.
        /// </summary>
        /// 
        /// <returns>
        /// An  <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/>  object that stores the key encryption algorithm identifier.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public override AlgorithmIdentifier KeyEncryptionAlgorithm
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_encryptionAlgorithm == null)
                    this.m_encryptionAlgorithm = this.SubType != RecipientSubType.CmsKeyTransport ? new AlgorithmIdentifier(((CAPI.CERT_INFO)this.CmsgRecipientInfo).SignatureAlgorithm) : new AlgorithmIdentifier(((CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)this.CmsgRecipientInfo).KeyEncryptionAlgorithm);
                return this.m_encryptionAlgorithm;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.EncryptedKey"/> property retrieves the encrypted key for this key transport recipient.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values that represents the encrypted key.
        /// </returns>
        public override byte[] EncryptedKey
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_encryptedKey.Length == 0 && this.SubType == RecipientSubType.CmsKeyTransport)
                {
                    CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO transRecipientInfo = (CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)this.CmsgRecipientInfo;
                    if (transRecipientInfo.EncryptedKey.cbData > 0U)
                    {
                        this.m_encryptedKey = new byte[(IntPtr)transRecipientInfo.EncryptedKey.cbData];
                        Marshal.Copy(transRecipientInfo.EncryptedKey.pbData, this.m_encryptedKey, 0, this.m_encryptedKey.Length);
                    }
                }
                return this.m_encryptedKey;
            }
        }

        [SecurityCritical]
        internal KeyTransRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CERT_INFO certInfo, uint index)
            : base(RecipientInfoType.KeyTransport, RecipientSubType.Pkcs7KeyTransport, pRecipientInfo, (object)certInfo, index)
        {
            int version = 2;
            byte* numPtr = (byte*)(void*)certInfo.SerialNumber.pbData;
            for (int index1 = 0; (long)index1 < (long)certInfo.SerialNumber.cbData; ++index1)
            {
                if ((int)*numPtr++ != 0)
                {
                    version = 0;
                    break;
                }
            }
            this.Reset(version);
        }

        [SecurityCritical]
        internal KeyTransRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO keyTrans, uint index)
            : base(RecipientInfoType.KeyTransport, RecipientSubType.CmsKeyTransport, pRecipientInfo, (object)keyTrans, index)
        {
            this.Reset((int)keyTrans.dwVersion);
        }

        private void Reset(int version)
        {
            this.m_version = version;
            this.m_recipientIdentifier = (SubjectIdentifier)null;
            this.m_encryptionAlgorithm = (AlgorithmIdentifier)null;
            this.m_encryptedKey = new byte[0];
        }
    }
}
