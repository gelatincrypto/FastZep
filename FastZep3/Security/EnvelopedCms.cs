// Type: System.Security.Cryptography.Pkcs.EnvelopedCms
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> class represents a CMS/PKCS #7 structure for enveloped data.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class EnvelopedCms
    {
        [SecurityCritical]
        private SafeCryptMsgHandle m_safeCryptMsgHandle;
        private int m_version;
        private SubjectIdentifierType m_recipientIdentifierType;
        private ContentInfo m_contentInfo;
        private AlgorithmIdentifier m_encryptionAlgorithm;
        private X509Certificate2Collection m_certificates;
        private CryptographicAttributeObjectCollection m_unprotectedAttributes;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Version"/> property retrieves the version of the enveloped CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// An int value that represents the version of the enveloped CMS/PKCS #7 message.
        /// </returns>
        public int Version
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_version;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo"/> property retrieves the inner content information for the enveloped CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that represents the inner content information from the enveloped CMS/PKCS #7 message.
        /// </returns>
        public ContentInfo ContentInfo
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_contentInfo;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentEncryptionAlgorithm"/> property retrieves the identifier of the algorithm used to encrypt the content.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> object that represents the algorithm identifier.
        /// </returns>
        public AlgorithmIdentifier ContentEncryptionAlgorithm
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_encryptionAlgorithm;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Certificates"/> property retrieves the set of certificates associated with the enveloped CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> collection that represents the X.509 certificates used with the enveloped CMS/PKCS #7 message. If no certificates exist, the property value is an empty collection.
        /// </returns>
        public X509Certificate2Collection Certificates
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_certificates;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.UnprotectedAttributes"/> property retrieves the unprotected (unencrypted) attributes associated with the enveloped CMS/PKCS #7 message. Unprotected attributes are not encrypted, and so do not have data confidentiality within an <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> object.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection that represents the unprotected attributes. If no unprotected attributes exist, the property value is an empty collection.
        /// </returns>
        public CryptographicAttributeObjectCollection UnprotectedAttributes
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_unprotectedAttributes;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.RecipientInfos"/> property retrieves the recipient information associated with the enveloped CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection that represents the recipient information. If no recipients exist, the property value is an empty collection.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public RecipientInfoCollection RecipientInfos
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                    return new RecipientInfoCollection();
                else
                    return new RecipientInfoCollection(this.m_safeCryptMsgHandle);
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> class.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        public EnvelopedCms()
            : this(SubjectIdentifierType.IssuerAndSerialNumber, new ContentInfo("1.2.840.113549.1.7.1", new byte[0]), new AlgorithmIdentifier("1.2.840.113549.3.7"))
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> class by using the specified content information as the inner content type.
        /// </summary>
        /// <param name="contentInfo">An instance of the <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo"/> class that represents the content and its type.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        public EnvelopedCms(ContentInfo contentInfo)
            : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, new AlgorithmIdentifier("1.2.840.113549.3.7"))
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> class by using the specified subject identifier type and content information. The specified content information is to be used as the inner content type.
        /// </summary>
        /// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the means of identifying the recipient.</param><param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that represents the content and its type.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo)
            : this(recipientIdentifierType, contentInfo, new AlgorithmIdentifier("1.2.840.113549.3.7"))
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> class by using the specified content information and encryption algorithm. The specified content information is to be used as the inner content type.
        /// </summary>
        /// <param name="contentInfo">A  <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that represents the content and its type.</param><param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> object that specifies the encryption algorithm.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public EnvelopedCms(ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
            : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, encryptionAlgorithm)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> class by using the specified subject identifier type, content information, and encryption algorithm. The specified content information is to be used as the inner content type.
        /// </summary>
        /// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the means of identifying the recipient.</param><param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that represents the content and its type.</param><param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> object that specifies the encryption algorithm.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
        [SecuritySafeCritical]
        public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
        {
            if (contentInfo == null)
                throw new ArgumentNullException("contentInfo");
            if (contentInfo.Content == null)
                throw new ArgumentNullException("contentInfo.Content");
            if (encryptionAlgorithm == null)
                throw new ArgumentNullException("encryptionAlgorithm");
            this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
            this.m_version = recipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier ? 2 : 0;
            this.m_recipientIdentifierType = recipientIdentifierType;
            this.m_contentInfo = contentInfo;
            this.m_encryptionAlgorithm = encryptionAlgorithm;
            this.m_encryptionAlgorithm.Parameters = new byte[0];
            this.m_certificates = new X509Certificate2Collection();
            this.m_unprotectedAttributes = new CryptographicAttributeObjectCollection();
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encode"/> method encodes the contents of the enveloped CMS/PKCS #7 message and returns it as an array of byte values. Encryption must be done before encoding.
        /// </summary>
        /// 
        /// <returns>
        /// If the method succeeds, the method returns an array of byte values that represent the encoded information.If the method fails, it throws an exception.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        [SecuritySafeCritical]
        public byte[] Encode()
        {
            if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotEncrypted"));
            else
                return PkcsUtils.GetContent(this.m_safeCryptMsgHandle);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decode(System.Byte[])"/> method decodes the specified enveloped CMS/PKCS #7 message and resets all member variables in the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms"/> object.
        /// </summary>
        /// <param name="encodedMessage">An array of byte values that represent the information to be decoded.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
        [SecuritySafeCritical]
        public void Decode(byte[] encodedMessage)
        {
            if (encodedMessage == null)
                throw new ArgumentNullException("encodedMessage");
            if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
                this.m_safeCryptMsgHandle.Dispose();
            this.m_safeCryptMsgHandle = EnvelopedCms.OpenToDecode(encodedMessage);
            this.m_version = (int)PkcsUtils.GetVersion(this.m_safeCryptMsgHandle);
            this.m_contentInfo = new ContentInfo(PkcsUtils.GetContentType(this.m_safeCryptMsgHandle), PkcsUtils.GetContent(this.m_safeCryptMsgHandle));
            this.m_encryptionAlgorithm = PkcsUtils.GetAlgorithmIdentifier(this.m_safeCryptMsgHandle);
            this.m_certificates = PkcsUtils.GetCertificates(this.m_safeCryptMsgHandle);
            this.m_unprotectedAttributes = PkcsUtils.GetUnprotectedAttributes(this.m_safeCryptMsgHandle);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt"/> method encrypts the contents of the CMS/PKCS #7 message.
        /// </summary>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows"/><IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates"/></PermissionSet>
        public void Encrypt()
        {
            this.Encrypt(new CmsRecipientCollection());
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipient)"/> method encrypts the contents of the CMS/PKCS #7 message by using the specified recipient information.
        /// </summary>
        /// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object that represents the recipient information.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        public void Encrypt(CmsRecipient recipient)
        {
            if (recipient == null)
                throw new ArgumentNullException("recipient");
            this.Encrypt(new CmsRecipientCollection(recipient));
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipientCollection)"/> method encrypts the contents of the CMS/PKCS #7 message by using the information for the specified list of recipients. The message is encrypted by using a message encryption key with a symmetric encryption algorithm such as triple DES. The message encryption key is then encrypted with the public key of each recipient.
        /// </summary>
        /// <param name="recipients">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection that represents the information for the list of recipients.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        public void Encrypt(CmsRecipientCollection recipients)
        {
            if (recipients == null)
                throw new ArgumentNullException("recipients");
            if (this.ContentInfo.Content.Length == 0)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Envelope_Empty_Content"));
            if (recipients.Count == 0)
                recipients = PkcsUtils.SelectRecipients(this.m_recipientIdentifierType);
            this.EncryptContent(recipients);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt"/> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt"/> method searches the current user and computer My stores for the appropriate certificate and private key.
        /// </summary>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy"/><IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates"/></PermissionSet>
        public void Decrypt()
        {
            this.DecryptContent(this.RecipientInfos, (X509Certificate2Collection)null);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo)"/> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information.
        /// </summary>
        /// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object that represents the recipient information that identifies the certificate associated with the private key to use for the decryption.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        public void Decrypt(RecipientInfo recipientInfo)
        {
            if (recipientInfo == null)
                throw new ArgumentNullException("recipientInfo");
            this.DecryptContent(new RecipientInfoCollection(recipientInfo), (X509Certificate2Collection)null);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)"/> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the specified certificate collection. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)"/> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.
        /// </summary>
        /// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)"/> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        public void Decrypt(X509Certificate2Collection extraStore)
        {
            if (extraStore == null)
                throw new ArgumentNullException("extraStore");
            this.DecryptContent(this.RecipientInfos, extraStore);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)"/> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information and by using the specified certificate collection.  The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)"/> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.
        /// </summary>
        /// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object that represents the recipient information to use for the decryption.</param><param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)"/> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        public void Decrypt(RecipientInfo recipientInfo, X509Certificate2Collection extraStore)
        {
            if (recipientInfo == null)
                throw new ArgumentNullException("recipientInfo");
            if (extraStore == null)
                throw new ArgumentNullException("extraStore");
            this.DecryptContent(new RecipientInfoCollection(recipientInfo), extraStore);
        }

        [SecuritySafeCritical]
        private unsafe void DecryptContent(RecipientInfoCollection recipientInfos, X509Certificate2Collection extraStore)
        {
            int hr = -2146889717;
            if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_NoEncryptedMessageToEncode"));
            for (int index = 0; index < recipientInfos.Count; ++index)
            {
                RecipientInfo recipientInfo = recipientInfos[index];
                EnvelopedCms.CMSG_DECRYPT_PARAM cmsgDecryptParam = new EnvelopedCms.CMSG_DECRYPT_PARAM();
                int num = EnvelopedCms.GetCspParams(recipientInfo, extraStore, ref cmsgDecryptParam);
                if (num == 0)
                {
                    CspParameters parameters = new CspParameters();
                    if (!X509Utils.GetPrivateKeyInfo(cmsgDecryptParam.safeCertContextHandle, ref parameters))
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                    KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
                    KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Decrypt);
                    containerPermission.AccessEntries.Add(accessEntry);
                    containerPermission.Demand();
                    switch (recipientInfo.Type)
                    {
                        case RecipientInfoType.KeyTransport:
                            CAPI.CMSG_CTRL_DECRYPT_PARA cmsgCtrlDecryptPara = new CAPI.CMSG_CTRL_DECRYPT_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_DECRYPT_PARA)));
                            cmsgCtrlDecryptPara.hCryptProv = cmsgDecryptParam.safeCryptProvHandle.DangerousGetHandle();
                            cmsgCtrlDecryptPara.dwKeySpec = cmsgDecryptParam.keySpec;
                            cmsgCtrlDecryptPara.dwRecipientIndex = recipientInfo.Index;
                            if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 2U, new IntPtr((void*)&cmsgCtrlDecryptPara)))
                                num = Marshal.GetHRForLastWin32Error();
                            GC.KeepAlive((object)cmsgCtrlDecryptPara);
                            break;
                        case RecipientInfoType.KeyAgreement:
                            System.Security.Cryptography.SafeCertContextHandle certContextHandle = System.Security.Cryptography.SafeCertContextHandle.InvalidHandle;
                            KeyAgreeRecipientInfo agreeRecipientInfo = (KeyAgreeRecipientInfo)recipientInfo;
                            CAPI.CMSG_CMS_RECIPIENT_INFO cmsRecipientInfo = (CAPI.CMSG_CMS_RECIPIENT_INFO)Marshal.PtrToStructure(agreeRecipientInfo.pCmsgRecipientInfo.DangerousGetHandle(), typeof(CAPI.CMSG_CMS_RECIPIENT_INFO));
                            CAPI.CMSG_CTRL_KEY_AGREE_DECRYPT_PARA agreeDecryptPara = new CAPI.CMSG_CTRL_KEY_AGREE_DECRYPT_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_KEY_AGREE_DECRYPT_PARA)));
                            agreeDecryptPara.hCryptProv = cmsgDecryptParam.safeCryptProvHandle.DangerousGetHandle();
                            agreeDecryptPara.dwKeySpec = cmsgDecryptParam.keySpec;
                            agreeDecryptPara.pKeyAgree = cmsRecipientInfo.pRecipientInfo;
                            agreeDecryptPara.dwRecipientIndex = agreeRecipientInfo.Index;
                            agreeDecryptPara.dwRecipientEncryptedKeyIndex = agreeRecipientInfo.SubIndex;
                            if (agreeRecipientInfo.SubType == RecipientSubType.CertIdKeyAgreement)
                            {
                                CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO certIdRecipientInfo = (CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)agreeRecipientInfo.CmsgRecipientInfo;
                                certContextHandle = CAPI.CertFindCertificateInStore(EnvelopedCms.BuildOriginatorStore(this.Certificates, extraStore), 65537U, 0U, 1048576U, new IntPtr((void*)&certIdRecipientInfo.OriginatorCertId), System.Security.Cryptography.SafeCertContextHandle.InvalidHandle);
                                if (certContextHandle == null || certContextHandle.IsInvalid)
                                {
                                    num = -2146885628;
                                    break;
                                }
                                else
                                {
                                    CAPI.CERT_INFO certInfo = (CAPI.CERT_INFO)Marshal.PtrToStructure(((CAPI.CERT_CONTEXT)Marshal.PtrToStructure(certContextHandle.DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT))).pCertInfo, typeof(CAPI.CERT_INFO));
                                    agreeDecryptPara.OriginatorPublicKey = certInfo.SubjectPublicKeyInfo.PublicKey;
                                }
                            }
                            else
                            {
                                CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO keyRecipientInfo = (CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)agreeRecipientInfo.CmsgRecipientInfo;
                                agreeDecryptPara.OriginatorPublicKey = keyRecipientInfo.OriginatorPublicKeyInfo.PublicKey;
                            }
                            if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 17U, new IntPtr((void*)&agreeDecryptPara)))
                                num = Marshal.GetHRForLastWin32Error();
                            GC.KeepAlive((object)agreeDecryptPara);
                            GC.KeepAlive((object)certContextHandle);
                            break;
                        default:
                            throw new CryptographicException(-2147483647);
                    }
                    GC.KeepAlive((object)cmsgDecryptParam);
                }
                if (num == 0)
                {
                    uint cbData = 0U;
                    SafeLocalAllocHandle pvData = SafeLocalAllocHandle.InvalidHandle;
                    PkcsUtils.GetParam(this.m_safeCryptMsgHandle, 2U, 0U, out pvData, out cbData);
                    if (cbData > 0U)
                    {
                        Oid contentType = PkcsUtils.GetContentType(this.m_safeCryptMsgHandle);
                        byte[] numArray = new byte[(IntPtr)cbData];
                        Marshal.Copy(pvData.DangerousGetHandle(), numArray, 0, (int)cbData);
                        this.m_contentInfo = new ContentInfo(contentType, numArray);
                    }
                    pvData.Dispose();
                    hr = 0;
                    break;
                }
                else
                    hr = num;
            }
            if (hr != 0)
                throw new CryptographicException(hr);
        }

        [SecuritySafeCritical]
        private unsafe void EncryptContent(CmsRecipientCollection recipients)
        {
            EnvelopedCms.CMSG_ENCRYPT_PARAM encryptParam = new EnvelopedCms.CMSG_ENCRYPT_PARAM();
            if (recipients.Count < 1)
                throw new CryptographicException(-2146889717);
            foreach (CmsRecipient cmsRecipient in recipients)
            {
                if (cmsRecipient.Certificate == null)
                    throw new ArgumentNullException(SecurityResources.GetResourceString("Cryptography_Cms_RecipientCertificateNotFound"));
                if (PkcsUtils.GetRecipientInfoType(cmsRecipient.Certificate) == RecipientInfoType.KeyAgreement || cmsRecipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
                    encryptParam.useCms = true;
            }
            if (!encryptParam.useCms && (this.Certificates.Count > 0 || this.UnprotectedAttributes.Count > 0))
                encryptParam.useCms = true;
            if (encryptParam.useCms && !PkcsUtils.CmsSupported())
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
            CAPI.CMSG_ENVELOPED_ENCODE_INFO envelopedEncodeInfo = new CAPI.CMSG_ENVELOPED_ENCODE_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_ENVELOPED_ENCODE_INFO)));
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_ENVELOPED_ENCODE_INFO))));
            EnvelopedCms.SetCspParams(this.ContentEncryptionAlgorithm, ref encryptParam);
            envelopedEncodeInfo.ContentEncryptionAlgorithm.pszObjId = this.ContentEncryptionAlgorithm.Oid.Value;
            if (encryptParam.pvEncryptionAuxInfo != null && !encryptParam.pvEncryptionAuxInfo.IsInvalid)
                envelopedEncodeInfo.pvEncryptionAuxInfo = encryptParam.pvEncryptionAuxInfo.DangerousGetHandle();
            envelopedEncodeInfo.cRecipients = (uint)recipients.Count;
            if (encryptParam.useCms)
            {
                EnvelopedCms.SetCmsRecipientParams(recipients, this.Certificates, this.UnprotectedAttributes, this.ContentEncryptionAlgorithm, ref encryptParam);
                envelopedEncodeInfo.rgCmsRecipients = encryptParam.rgpRecipients.DangerousGetHandle();
                if (encryptParam.rgCertEncoded != null && !encryptParam.rgCertEncoded.IsInvalid)
                {
                    envelopedEncodeInfo.cCertEncoded = (uint)this.Certificates.Count;
                    envelopedEncodeInfo.rgCertEncoded = encryptParam.rgCertEncoded.DangerousGetHandle();
                }
                if (encryptParam.rgUnprotectedAttr != null && !encryptParam.rgUnprotectedAttr.IsInvalid)
                {
                    envelopedEncodeInfo.cUnprotectedAttr = (uint)this.UnprotectedAttributes.Count;
                    envelopedEncodeInfo.rgUnprotectedAttr = encryptParam.rgUnprotectedAttr.DangerousGetHandle();
                }
            }
            else
            {
                EnvelopedCms.SetPkcs7RecipientParams(recipients, ref encryptParam);
                envelopedEncodeInfo.rgpRecipients = encryptParam.rgpRecipients.DangerousGetHandle();
            }
            Marshal.StructureToPtr((object)envelopedEncodeInfo, localAllocHandle.DangerousGetHandle(), false);
            try
            {
                SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CryptMsgOpenToEncode(65537U, 0U, 3U, localAllocHandle.DangerousGetHandle(), this.ContentInfo.ContentType.Value, IntPtr.Zero);
                if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
                    this.m_safeCryptMsgHandle.Dispose();
                this.m_safeCryptMsgHandle = safeCryptMsgHandle;
            }
            finally
            {
                Marshal.DestroyStructure(localAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_ENVELOPED_ENCODE_INFO));
                localAllocHandle.Dispose();
            }
            byte[] encodedData = new byte[0];
            if (string.Compare(this.ContentInfo.ContentType.Value, "1.2.840.113549.1.7.1", StringComparison.OrdinalIgnoreCase) == 0)
            {
                byte[] content = this.ContentInfo.Content;
                fixed (byte* numPtr = content)
                {
                    if (!CAPI.EncodeObject(new IntPtr(25L), new IntPtr((void*)&new CAPI.CRYPTOAPI_BLOB()
                    {
                        cbData = (uint)content.Length,
                        pbData = new IntPtr((void*)numPtr)
                    }), out encodedData))
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                }
            }
            else
                encodedData = this.ContentInfo.Content;
            if (encodedData.Length > 0 && !CAPI.CAPISafe.CryptMsgUpdate(this.m_safeCryptMsgHandle, encodedData, (uint)encodedData.Length, true))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            GC.KeepAlive((object)encryptParam);
            GC.KeepAlive((object)recipients);
        }

        [SecuritySafeCritical]
        private static SafeCryptMsgHandle OpenToDecode(byte[] encodedMessage)
        {
            SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CAPISafe.CryptMsgOpenToDecode(65537U, 0U, 0U, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if (!CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, encodedMessage, (uint)encodedMessage.Length, true))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if (3 != (int)PkcsUtils.GetMessageType(safeCryptMsgHandle))
                throw new CryptographicException(-2146889724);
            else
                return safeCryptMsgHandle;
        }

        [SecurityCritical]
        private static unsafe int GetCspParams(RecipientInfo recipientInfo, X509Certificate2Collection extraStore, ref EnvelopedCms.CMSG_DECRYPT_PARAM cmsgDecryptParam)
        {
            int num = -2146889717;
            System.Security.Cryptography.SafeCertContextHandle certContextHandle = System.Security.Cryptography.SafeCertContextHandle.InvalidHandle;
            System.Security.Cryptography.SafeCertStoreHandle hCertStore = EnvelopedCms.BuildDecryptorStore(extraStore);
            switch (recipientInfo.Type)
            {
                case RecipientInfoType.KeyTransport:
                    if (recipientInfo.SubType == RecipientSubType.Pkcs7KeyTransport)
                    {
                        certContextHandle = CAPI.CertFindCertificateInStore(hCertStore, 65537U, 0U, 720896U, recipientInfo.pCmsgRecipientInfo.DangerousGetHandle(), System.Security.Cryptography.SafeCertContextHandle.InvalidHandle);
                        break;
                    }
                    else
                    {
                        CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO transRecipientInfo = (CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)recipientInfo.CmsgRecipientInfo;
                        certContextHandle = CAPI.CertFindCertificateInStore(hCertStore, 65537U, 0U, 1048576U, new IntPtr((void*)&transRecipientInfo.RecipientId), System.Security.Cryptography.SafeCertContextHandle.InvalidHandle);
                        break;
                    }
                case RecipientInfoType.KeyAgreement:
                    CAPI.CERT_ID recipientId = ((KeyAgreeRecipientInfo)recipientInfo).RecipientId;
                    certContextHandle = CAPI.CertFindCertificateInStore(hCertStore, 65537U, 0U, 1048576U, new IntPtr((void*)&recipientId), System.Security.Cryptography.SafeCertContextHandle.InvalidHandle);
                    break;
                default:
                    num = -2147483647;
                    break;
            }
            if (certContextHandle != null && !certContextHandle.IsInvalid)
            {
                SafeCryptProvHandle invalidHandle = SafeCryptProvHandle.InvalidHandle;
                uint pdwKeySpec = 0U;
                bool pfCallerFreeProv = false;
                CspParameters parameters = new CspParameters();
                if (!X509Utils.GetPrivateKeyInfo(certContextHandle, ref parameters))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                if (string.Compare(parameters.ProviderName, "Microsoft Base Cryptographic Provider v1.0", StringComparison.OrdinalIgnoreCase) == 0 && (CAPI.CryptAcquireContext(out invalidHandle, parameters.KeyContainerName, "Microsoft Enhanced Cryptographic Provider v1.0", 1U, 0U) || CAPI.CryptAcquireContext(out invalidHandle, parameters.KeyContainerName, "Microsoft Strong Cryptographic Provider", 1U, 0U)))
                    cmsgDecryptParam.safeCryptProvHandle = invalidHandle;
                cmsgDecryptParam.safeCertContextHandle = certContextHandle;
                cmsgDecryptParam.keySpec = (uint)parameters.KeyNumber;
                num = 0;
                if (invalidHandle == null || invalidHandle.IsInvalid)
                {
                    if (CAPI.CAPISafe.CryptAcquireCertificatePrivateKey(certContextHandle, 6U, IntPtr.Zero, out invalidHandle, out pdwKeySpec, out pfCallerFreeProv))
                    {
                        if (!pfCallerFreeProv)
                            GC.SuppressFinalize((object)invalidHandle);
                        cmsgDecryptParam.safeCryptProvHandle = invalidHandle;
                    }
                    else
                        num = Marshal.GetHRForLastWin32Error();
                }
            }
            return num;
        }

        [SecurityCritical]
        private static void SetCspParams(AlgorithmIdentifier contentEncryptionAlgorithm, ref EnvelopedCms.CMSG_ENCRYPT_PARAM encryptParam)
        {
            encryptParam.safeCryptProvHandle = SafeCryptProvHandle.InvalidHandle;
            encryptParam.pvEncryptionAuxInfo = SafeLocalAllocHandle.InvalidHandle;
            SafeCryptProvHandle invalidHandle = SafeCryptProvHandle.InvalidHandle;
            if (!CAPI.CryptAcquireContext(ref invalidHandle, IntPtr.Zero, IntPtr.Zero, 1U, 4026531840U))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            uint algId = X509Utils.OidToAlgId(contentEncryptionAlgorithm.Oid.Value);
            switch (algId)
            {
                case 26114U:
                case 26625U:
                    CAPI.CMSG_RC2_AUX_INFO cmsgRc2AuxInfo = new CAPI.CMSG_RC2_AUX_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_RC2_AUX_INFO)));
                    uint num = (uint)contentEncryptionAlgorithm.KeyLength;
                    if ((int)num == 0)
                        num = (uint)PkcsUtils.GetMaxKeyLength(invalidHandle, algId);
                    cmsgRc2AuxInfo.dwBitLen = num;
                    SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_RC2_AUX_INFO))));
                    Marshal.StructureToPtr((object)cmsgRc2AuxInfo, localAllocHandle.DangerousGetHandle(), false);
                    encryptParam.pvEncryptionAuxInfo = localAllocHandle;
                    break;
            }
            encryptParam.safeCryptProvHandle = invalidHandle;
        }

        [SecurityCritical]
        private static unsafe void SetCmsRecipientParams(CmsRecipientCollection recipients, X509Certificate2Collection certificates, CryptographicAttributeObjectCollection unprotectedAttributes, AlgorithmIdentifier contentEncryptionAlgorithm, ref EnvelopedCms.CMSG_ENCRYPT_PARAM encryptParam)
        {
            uint[] numArray = new uint[recipients.Count];
            int length = 0;
            int num1 = recipients.Count * Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCODE_INFO));
            int num2 = num1;
            for (int index = 0; index < recipients.Count; ++index)
            {
                numArray[index] = (uint)PkcsUtils.GetRecipientInfoType(recipients[index].Certificate);
                if ((int)numArray[index] == 1)
                {
                    num2 += Marshal.SizeOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO));
                }
                else
                {
                    if ((int)numArray[index] != 2)
                        throw new CryptographicException(-2146889726);
                    ++length;
                    num2 += Marshal.SizeOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO));
                }
            }
            encryptParam.rgpRecipients = CAPI.LocalAlloc(64U, new IntPtr(num2));
            encryptParam.rgCertEncoded = SafeLocalAllocHandle.InvalidHandle;
            encryptParam.rgUnprotectedAttr = SafeLocalAllocHandle.InvalidHandle;
            encryptParam.rgSubjectKeyIdentifier = new SafeLocalAllocHandle[recipients.Count];
            encryptParam.rgszObjId = new SafeLocalAllocHandle[recipients.Count];
            if (length > 0)
            {
                encryptParam.rgszKeyWrapObjId = new SafeLocalAllocHandle[length];
                encryptParam.rgKeyWrapAuxInfo = new SafeLocalAllocHandle[length];
                encryptParam.rgEphemeralIdentifier = new SafeLocalAllocHandle[length];
                encryptParam.rgszEphemeralObjId = new SafeLocalAllocHandle[length];
                encryptParam.rgUserKeyingMaterial = new SafeLocalAllocHandle[length];
                encryptParam.prgpEncryptedKey = new SafeLocalAllocHandle[length];
                encryptParam.rgpEncryptedKey = new SafeLocalAllocHandle[length];
            }
            if (certificates.Count > 0)
            {
                encryptParam.rgCertEncoded = CAPI.LocalAlloc(64U, new IntPtr(certificates.Count * Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB))));
                for (int index = 0; index < certificates.Count; ++index)
                {
                    CAPI.CERT_CONTEXT certContext = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificates[index]).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
                    CAPI.CRYPTOAPI_BLOB* cryptoapiBlobPtr = (CAPI.CRYPTOAPI_BLOB*)(void*)new IntPtr((long)encryptParam.rgCertEncoded.DangerousGetHandle() + (long)(index * Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB))));
                    cryptoapiBlobPtr->cbData = certContext.cbCertEncoded;
                    cryptoapiBlobPtr->pbData = certContext.pbCertEncoded;
                }
            }
            if (unprotectedAttributes.Count > 0)
                encryptParam.rgUnprotectedAttr = new SafeLocalAllocHandle(PkcsUtils.CreateCryptAttributes(unprotectedAttributes));
            int index1 = 0;
            IntPtr num3 = new IntPtr((long)encryptParam.rgpRecipients.DangerousGetHandle() + (long)num1);
            for (int index2 = 0; index2 < recipients.Count; ++index2)
            {
                CmsRecipient cmsRecipient = recipients[index2];
                X509Certificate2 certificate = cmsRecipient.Certificate;
                CAPI.CERT_INFO certInfo = (CAPI.CERT_INFO)Marshal.PtrToStructure(((CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificate).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT))).pCertInfo, typeof(CAPI.CERT_INFO));
                CAPI.CMSG_RECIPIENT_ENCODE_INFO* recipientEncodeInfoPtr = (CAPI.CMSG_RECIPIENT_ENCODE_INFO*)(void*)new IntPtr((long)encryptParam.rgpRecipients.DangerousGetHandle() + (long)(index2 * Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCODE_INFO))));
                recipientEncodeInfoPtr->dwRecipientChoice = numArray[index2];
                recipientEncodeInfoPtr->pRecipientInfo = num3;
                if ((int)numArray[index2] == 1)
                {
                    Marshal.WriteInt32(new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "cbSize")), Marshal.SizeOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO)));
                    IntPtr num4 = new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "KeyEncryptionAlgorithm"));
                    byte[] bytes = Encoding.ASCII.GetBytes(certInfo.SubjectPublicKeyInfo.Algorithm.pszObjId);
                    encryptParam.rgszObjId[index2] = CAPI.LocalAlloc(64U, new IntPtr(bytes.Length + 1));
                    Marshal.Copy(bytes, 0, encryptParam.rgszObjId[index2].DangerousGetHandle(), bytes.Length);
                    Marshal.WriteIntPtr(new IntPtr((long)num4 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId")), encryptParam.rgszObjId[index2].DangerousGetHandle());
                    IntPtr num5 = new IntPtr((long)num4 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "Parameters"));
                    IntPtr ptr1 = new IntPtr((long)num5 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                    Marshal.WriteInt32(ptr1, (int)certInfo.SubjectPublicKeyInfo.Algorithm.Parameters.cbData);
                    IntPtr ptr2 = new IntPtr((long)num5 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                    Marshal.WriteIntPtr(ptr2, certInfo.SubjectPublicKeyInfo.Algorithm.Parameters.pbData);
                    IntPtr num6 = new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "RecipientPublicKey"));
                    ptr1 = new IntPtr((long)num6 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cbData"));
                    Marshal.WriteInt32(ptr1, (int)certInfo.SubjectPublicKeyInfo.PublicKey.cbData);
                    ptr2 = new IntPtr((long)num6 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "pbData"));
                    Marshal.WriteIntPtr(ptr2, certInfo.SubjectPublicKeyInfo.PublicKey.pbData);
                    Marshal.WriteInt32(new IntPtr((long)num6 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cUnusedBits")), (int)certInfo.SubjectPublicKeyInfo.PublicKey.cUnusedBits);
                    IntPtr num7 = new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "RecipientId"));
                    if (cmsRecipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
                    {
                        uint pcbData = 0U;
                        SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
                        if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, invalidHandle, out pcbData))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        SafeLocalAllocHandle pvData = CAPI.LocalAlloc(64U, new IntPtr((long)pcbData));
                        if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, pvData, out pcbData))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        encryptParam.rgSubjectKeyIdentifier[index2] = pvData;
                        Marshal.WriteInt32(new IntPtr((long)num7 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "dwIdChoice")), 2);
                        IntPtr num8 = new IntPtr((long)num7 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
                        ptr1 = new IntPtr((long)num8 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                        Marshal.WriteInt32(ptr1, (int)pcbData);
                        ptr2 = new IntPtr((long)num8 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                        Marshal.WriteIntPtr(ptr2, pvData.DangerousGetHandle());
                    }
                    else
                    {
                        Marshal.WriteInt32(new IntPtr((long)num7 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "dwIdChoice")), 1);
                        IntPtr num8 = new IntPtr((long)num7 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
                        IntPtr num9 = new IntPtr((long)num8 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "Issuer"));
                        ptr1 = new IntPtr((long)num9 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                        Marshal.WriteInt32(ptr1, (int)certInfo.Issuer.cbData);
                        ptr2 = new IntPtr((long)num9 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                        Marshal.WriteIntPtr(ptr2, certInfo.Issuer.pbData);
                        IntPtr num10 = new IntPtr((long)num8 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "SerialNumber"));
                        ptr1 = new IntPtr((long)num10 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                        Marshal.WriteInt32(ptr1, (int)certInfo.SerialNumber.cbData);
                        ptr2 = new IntPtr((long)num10 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                        Marshal.WriteIntPtr(ptr2, certInfo.SerialNumber.pbData);
                    }
                    num3 = new IntPtr((long)num3 + (long)Marshal.SizeOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO)));
                }
                else if ((int)numArray[index2] == 2)
                {
                    IntPtr ptr1 = new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "cbSize"));
                    Marshal.WriteInt32(ptr1, Marshal.SizeOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO)));
                    IntPtr num4 = new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "KeyEncryptionAlgorithm"));
                    byte[] bytes1 = Encoding.ASCII.GetBytes("1.2.840.113549.1.9.16.3.5");
                    encryptParam.rgszObjId[index2] = CAPI.LocalAlloc(64U, new IntPtr(bytes1.Length + 1));
                    Marshal.Copy(bytes1, 0, encryptParam.rgszObjId[index2].DangerousGetHandle(), bytes1.Length);
                    IntPtr ptr2 = new IntPtr((long)num4 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId"));
                    Marshal.WriteIntPtr(ptr2, encryptParam.rgszObjId[index2].DangerousGetHandle());
                    IntPtr num5 = new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "KeyWrapAlgorithm"));
                    uint num6 = X509Utils.OidToAlgId(contentEncryptionAlgorithm.Oid.Value);
                    byte[] source = (int)num6 != 26114 ? Encoding.ASCII.GetBytes("1.2.840.113549.1.9.16.3.6") : Encoding.ASCII.GetBytes("1.2.840.113549.1.9.16.3.7");
                    encryptParam.rgszKeyWrapObjId[index1] = CAPI.LocalAlloc(64U, new IntPtr(source.Length + 1));
                    Marshal.Copy(source, 0, encryptParam.rgszKeyWrapObjId[index1].DangerousGetHandle(), source.Length);
                    ptr2 = new IntPtr((long)num5 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId"));
                    Marshal.WriteIntPtr(ptr2, encryptParam.rgszKeyWrapObjId[index1].DangerousGetHandle());
                    if ((int)num6 == 26114)
                        Marshal.WriteIntPtr(new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "pvKeyWrapAuxInfo")), encryptParam.pvEncryptionAuxInfo.DangerousGetHandle());
                    Marshal.WriteInt32(new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "dwKeyChoice")), 1);
                    IntPtr ptr3 = new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "pEphemeralAlgorithmOrSenderId"));
                    encryptParam.rgEphemeralIdentifier[index1] = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER))));
                    Marshal.WriteIntPtr(ptr3, encryptParam.rgEphemeralIdentifier[index1].DangerousGetHandle());
                    byte[] bytes2 = Encoding.ASCII.GetBytes(certInfo.SubjectPublicKeyInfo.Algorithm.pszObjId);
                    encryptParam.rgszEphemeralObjId[index1] = CAPI.LocalAlloc(64U, new IntPtr(bytes2.Length + 1));
                    Marshal.Copy(bytes2, 0, encryptParam.rgszEphemeralObjId[index1].DangerousGetHandle(), bytes2.Length);
                    ptr2 = new IntPtr((long)encryptParam.rgEphemeralIdentifier[index1].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId"));
                    Marshal.WriteIntPtr(ptr2, encryptParam.rgszEphemeralObjId[index1].DangerousGetHandle());
                    IntPtr num7 = new IntPtr((long)encryptParam.rgEphemeralIdentifier[index1].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "Parameters"));
                    IntPtr ptr4 = new IntPtr((long)num7 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                    Marshal.WriteInt32(ptr4, (int)certInfo.SubjectPublicKeyInfo.Algorithm.Parameters.cbData);
                    IntPtr ptr5 = new IntPtr((long)num7 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                    Marshal.WriteIntPtr(ptr5, certInfo.SubjectPublicKeyInfo.Algorithm.Parameters.pbData);
                    Marshal.WriteInt32(new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "cRecipientEncryptedKeys")), 1);
                    encryptParam.prgpEncryptedKey[index1] = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(IntPtr))));
                    Marshal.WriteIntPtr(new IntPtr((long)num3 + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "rgpRecipientEncryptedKeys")), encryptParam.prgpEncryptedKey[index1].DangerousGetHandle());
                    encryptParam.rgpEncryptedKey[index1] = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO))));
                    Marshal.WriteIntPtr(encryptParam.prgpEncryptedKey[index1].DangerousGetHandle(), encryptParam.rgpEncryptedKey[index1].DangerousGetHandle());
                    ptr1 = new IntPtr((long)encryptParam.rgpEncryptedKey[index1].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO), "cbSize"));
                    Marshal.WriteInt32(ptr1, Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO)));
                    IntPtr num8 = new IntPtr((long)encryptParam.rgpEncryptedKey[index1].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO), "RecipientPublicKey"));
                    ptr4 = new IntPtr((long)num8 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cbData"));
                    Marshal.WriteInt32(ptr4, (int)certInfo.SubjectPublicKeyInfo.PublicKey.cbData);
                    ptr5 = new IntPtr((long)num8 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "pbData"));
                    Marshal.WriteIntPtr(ptr5, certInfo.SubjectPublicKeyInfo.PublicKey.pbData);
                    Marshal.WriteInt32(new IntPtr((long)num8 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cUnusedBits")), (int)certInfo.SubjectPublicKeyInfo.PublicKey.cUnusedBits);
                    IntPtr num9 = new IntPtr((long)encryptParam.rgpEncryptedKey[index1].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO), "RecipientId"));
                    IntPtr ptr6 = new IntPtr((long)num9 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "dwIdChoice"));
                    if (cmsRecipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
                    {
                        Marshal.WriteInt32(ptr6, 2);
                        IntPtr num10 = new IntPtr((long)num9 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
                        uint pcbData = 0U;
                        SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
                        if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, invalidHandle, out pcbData))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        SafeLocalAllocHandle pvData = CAPI.LocalAlloc(64U, new IntPtr((long)pcbData));
                        if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, pvData, out pcbData))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        encryptParam.rgSubjectKeyIdentifier[index1] = pvData;
                        ptr4 = new IntPtr((long)num10 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                        Marshal.WriteInt32(ptr4, (int)pcbData);
                        ptr5 = new IntPtr((long)num10 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                        Marshal.WriteIntPtr(ptr5, pvData.DangerousGetHandle());
                    }
                    else
                    {
                        Marshal.WriteInt32(ptr6, 1);
                        IntPtr num10 = new IntPtr((long)num9 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
                        IntPtr num11 = new IntPtr((long)num10 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "Issuer"));
                        ptr4 = new IntPtr((long)num11 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                        Marshal.WriteInt32(ptr4, (int)certInfo.Issuer.cbData);
                        ptr5 = new IntPtr((long)num11 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                        Marshal.WriteIntPtr(ptr5, certInfo.Issuer.pbData);
                        IntPtr num12 = new IntPtr((long)num10 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "SerialNumber"));
                        ptr4 = new IntPtr((long)num12 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                        Marshal.WriteInt32(ptr4, (int)certInfo.SerialNumber.cbData);
                        ptr5 = new IntPtr((long)num12 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                        Marshal.WriteIntPtr(ptr5, certInfo.SerialNumber.pbData);
                    }
                    ++index1;
                    num3 = new IntPtr((long)num3 + (long)Marshal.SizeOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO)));
                }
            }
        }

        [SecurityCritical]
        private static void SetPkcs7RecipientParams(CmsRecipientCollection recipients, ref EnvelopedCms.CMSG_ENCRYPT_PARAM encryptParam)
        {
            uint num = (uint)(recipients.Count * Marshal.SizeOf(typeof(IntPtr)));
            encryptParam.rgpRecipients = CAPI.LocalAlloc(64U, new IntPtr((long)num));
            IntPtr ptr = encryptParam.rgpRecipients.DangerousGetHandle();
            for (int index = 0; index < recipients.Count; ++index)
            {
                CAPI.CERT_CONTEXT certContext = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(recipients[index].Certificate).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
                Marshal.WriteIntPtr(ptr, certContext.pCertInfo);
                ptr = new IntPtr((long)ptr + (long)Marshal.SizeOf(typeof(IntPtr)));
            }
        }

        [SecurityCritical]
        private static SafeCertStoreHandle BuildDecryptorStore(X509Certificate2Collection extraStore)
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();
            try
            {
                X509Store x509Store = new X509Store("MY", StoreLocation.CurrentUser);
                x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
                collection.AddRange(x509Store.Certificates);
            }
            catch (SecurityException ex)
            {
            }
            try
            {
                X509Store x509Store = new X509Store("MY", StoreLocation.LocalMachine);
                x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
                collection.AddRange(x509Store.Certificates);
            }
            catch (SecurityException ex)
            {
            }
            if (extraStore != null)
                collection.AddRange(extraStore);
            if (collection.Count == 0)
                throw new CryptographicException(-2146889717);
            else
                return X509Utils.ExportToMemoryStore(collection);
        }

        [SecurityCritical]
        private static SafeCertStoreHandle BuildOriginatorStore(X509Certificate2Collection bagOfCerts, X509Certificate2Collection extraStore)
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();
            try
            {
                X509Store x509Store = new X509Store("AddressBook", StoreLocation.CurrentUser);
                x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
                collection.AddRange(x509Store.Certificates);
            }
            catch (SecurityException ex)
            {
            }
            try
            {
                X509Store x509Store = new X509Store("AddressBook", StoreLocation.LocalMachine);
                x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
                collection.AddRange(x509Store.Certificates);
            }
            catch (SecurityException ex)
            {
            }
            if (bagOfCerts != null)
                collection.AddRange(bagOfCerts);
            if (extraStore != null)
                collection.AddRange(extraStore);
            if (collection.Count == 0)
                throw new CryptographicException(-2146885628);
            else
                return X509Utils.ExportToMemoryStore(collection);
        }

        [SecurityCritical]
        private struct CMSG_DECRYPT_PARAM
        {
            internal System.Security.Cryptography.SafeCertContextHandle safeCertContextHandle;
            internal SafeCryptProvHandle safeCryptProvHandle;
            internal uint keySpec;
        }

        [SecurityCritical]
        private struct CMSG_ENCRYPT_PARAM
        {
            internal bool useCms;
            internal SafeCryptProvHandle safeCryptProvHandle;
            internal SafeLocalAllocHandle pvEncryptionAuxInfo;
            internal SafeLocalAllocHandle rgpRecipients;
            internal SafeLocalAllocHandle rgCertEncoded;
            internal SafeLocalAllocHandle rgUnprotectedAttr;
            internal SafeLocalAllocHandle[] rgSubjectKeyIdentifier;
            internal SafeLocalAllocHandle[] rgszObjId;
            internal SafeLocalAllocHandle[] rgszKeyWrapObjId;
            internal SafeLocalAllocHandle[] rgKeyWrapAuxInfo;
            internal SafeLocalAllocHandle[] rgEphemeralIdentifier;
            internal SafeLocalAllocHandle[] rgszEphemeralObjId;
            internal SafeLocalAllocHandle[] rgUserKeyingMaterial;
            internal SafeLocalAllocHandle[] prgpEncryptedKey;
            internal SafeLocalAllocHandle[] rgpEncryptedKey;
        }
    }
}
