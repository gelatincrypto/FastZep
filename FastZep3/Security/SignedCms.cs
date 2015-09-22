// Type: System.Security.Cryptography.Pkcs.SignedCms
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> class enables signing and verifying of CMS/PKCS #7 messages.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class SignedCms
    {
        [SecurityCritical]
        private SafeCryptMsgHandle m_safeCryptMsgHandle;
        private int m_version;
        private SubjectIdentifierType m_signerIdentifierType;
        private ContentInfo m_contentInfo;
        private bool m_detached;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Version"/> property retrieves the version of the CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// An int value that represents the CMS/PKCS #7 message version.
        /// </returns>
        public int Version
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                    return this.m_version;
                else
                    return (int)PkcsUtils.GetVersion(this.m_safeCryptMsgHandle);
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.ContentInfo"/> property retrieves the inner contents of the encoded CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that represents the contents of the encoded CMS/PKCS #7 message.
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
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Detached"/> property retrieves whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> object is for a detached signature.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Boolean"/> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> object is for a detached signature. If this property is true, the signature is detached. If this property is false, the signature is not detached.
        /// </returns>
        public bool Detached
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_detached;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Certificates"/> property retrieves the certificates associated with the encoded CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> collection that represents the set of certificates for the encoded CMS/PKCS #7 message.
        /// </returns>
        public X509Certificate2Collection Certificates
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                    return new X509Certificate2Collection();
                else
                    return PkcsUtils.GetCertificates(this.m_safeCryptMsgHandle);
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.SignerInfos"/> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection associated with the CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> object that represents the signer information for the CMS/PKCS #7 message.
        /// </returns>
        public SignerInfoCollection SignerInfos
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                    return new SignerInfoCollection();
                else
                    return new SignerInfoCollection(this);
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> class.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        public SignedCms()
            : this(SubjectIdentifierType.IssuerAndSerialNumber, new ContentInfo(new Oid("1.2.840.113549.1.7.1"), new byte[0]), false)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> class by using the specified subject identifier type as the default subject identifier type for signers.
        /// </summary>
        /// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> member that specifies the default subject identifier type for signers.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        public SignedCms(SubjectIdentifierType signerIdentifierType)
            : this(signerIdentifierType, new ContentInfo(new Oid("1.2.840.113549.1.7.1"), new byte[0]), false)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> class by using the specified content information as the inner content.
        /// </summary>
        /// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> message.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public SignedCms(ContentInfo contentInfo)
            : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, false)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> class by using the specified subject identifier type as the default subject identifier type for signers and content information as the inner content.
        /// </summary>
        /// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> member that specifies the default subject identifier type for signers.</param><param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> message.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo contentInfo)
            : this(signerIdentifierType, contentInfo, false)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Boolean)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> class by using the specified content information as the inner content and by using the detached state.
        /// </summary>
        /// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> message.</param><param name="detached">A <see cref="T:System.Boolean"/> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> object is for a detached signature. If <paramref name="detached"/> is true, the signature is detached. If <paramref name="detached"/> is false, the signature is not detached.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public SignedCms(ContentInfo contentInfo, bool detached)
            : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, detached)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Boolean)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> class by using the specified subject identifier type as the default subject identifier type for signers, the content information as the inner content, and by using the detached state.
        /// </summary>
        /// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> member that specifies the default subject identifier type for signers.</param><param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo"/> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> message.</param><param name="detached">A <see cref="T:System.Boolean"/> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> object is for a detached signature. If <paramref name="detached"/> is true, the signature is detached. If detached is false, the signature is not detached.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
        [SecuritySafeCritical]
        public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo contentInfo, bool detached)
        {
            if (contentInfo == null)
                throw new ArgumentNullException("contentInfo");
            if (contentInfo.Content == null)
                throw new ArgumentNullException("contentInfo.Content");
            if (signerIdentifierType != SubjectIdentifierType.SubjectKeyIdentifier && signerIdentifierType != SubjectIdentifierType.IssuerAndSerialNumber && signerIdentifierType != SubjectIdentifierType.NoSignature)
                signerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
            this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
            this.m_signerIdentifierType = signerIdentifierType;
            this.m_version = 0;
            this.m_contentInfo = contentInfo;
            this.m_detached = detached;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Encode"/> method encodes the information in the object into a CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values that represents the encoded message. The encoded message can be decoded by the <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Decode(System.Byte[])"/> method.
        /// </returns>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        [SecuritySafeCritical]
        public byte[] Encode()
        {
            if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
            else
                return PkcsUtils.GetMessage(this.m_safeCryptMsgHandle);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Decode(System.Byte[])"/> method decodes an encoded <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> message. Upon successful decoding, the decoded information can be retrieved from the properties of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> object.
        /// </summary>
        /// <param name="encodedMessage">Array of byte values that represents the encoded CMS/PKCS #7 message to be decoded.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        [SecuritySafeCritical]
        public void Decode(byte[] encodedMessage)
        {
            if (encodedMessage == null)
                throw new ArgumentNullException("encodedMessage");
            if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
                this.m_safeCryptMsgHandle.Dispose();
            this.m_safeCryptMsgHandle = SignedCms.OpenToDecode(encodedMessage, this.ContentInfo, this.Detached);
            if (this.Detached)
                return;
            this.m_contentInfo = new ContentInfo(PkcsUtils.GetContentType(this.m_safeCryptMsgHandle), PkcsUtils.GetContent(this.m_safeCryptMsgHandle));
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.ComputeSignature"/> method prompts the user to select a signing certificate, creates a signature, and adds the signature to the CMS/PKCS #7 message.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows"/><IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates"/></PermissionSet>
        public void ComputeSignature()
        {
            this.ComputeSignature(new CmsSigner(this.m_signerIdentifierType), true);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.ComputeSignature(System.Security.Cryptography.Pkcs.CmsSigner)"/> method creates a signature using the specified signer and adds the signature to the CMS/PKCS #7 message.
        /// </summary>
        /// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> object that represents the signer.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void ComputeSignature(CmsSigner signer)
        {
            this.ComputeSignature(signer, true);
        }

        /// <summary>
        /// Creates a signature using the specified signer and adds the signature to the CMS/PKCS #7 message. If the value of the silent parameter is false and the <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificate"/> property of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> object specified by the signer parameter is not set to a valid certificate, this method prompts the user to select a signing certificate.
        /// </summary>
        /// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> object that represents the signer.</param><param name="silent">false to prompt the user to select a signing certificate.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">The value of the <paramref name="silent"/> parameter is true and a signing certificate is not specified.</exception>
        [SecuritySafeCritical]
        public void ComputeSignature(CmsSigner signer, bool silent)
        {
            if (signer == null)
                throw new ArgumentNullException("signer");
            if (this.ContentInfo.Content.Length == 0)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Sign_Empty_Content"));
            if (SubjectIdentifierType.NoSignature == signer.SignerIdentifierType)
            {
                if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
                    throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Sign_No_Signature_First_Signer"));
                this.Sign(signer, silent);
            }
            else
            {
                if (signer.Certificate == null)
                {
                    if (silent)
                        throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_RecipientCertificateNotFound"));
                    signer.Certificate = PkcsUtils.SelectSignerCertificate();
                }
                if (!signer.Certificate.HasPrivateKey)
                    throw new CryptographicException(-2146893811);
                CspParameters parameters = new CspParameters();
                if (!X509Utils.GetPrivateKeyInfo(X509Utils.GetCertContext(signer.Certificate), ref parameters))
                    throw new CryptographicException(SignedCms.SafeGetLastWin32Error());
                KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
                KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Sign);
                containerPermission.AccessEntries.Add(accessEntry);
                containerPermission.Demand();
                if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                    this.Sign(signer, silent);
                else
                    this.CoSign(signer, silent);
            }
        }

        /// <summary>
        /// Removes the signature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.SignerInfos"/> collection.
        /// </summary>
        /// <param name="index">The zero-based index of the signature to remove.</param>
        [SecuritySafeCritical]
        public unsafe void RemoveSignature(int index)
        {
            if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
            uint num1 = 0U;
            uint num2 = (uint)Marshal.SizeOf(typeof(uint));
            if (!CAPI.CAPISafe.CryptMsgGetParam(this.m_safeCryptMsgHandle, 5U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if (index < 0 || index >= (int)num1)
                throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
            if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 7U, new IntPtr((void*)&index)))
                throw new CryptographicException(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.RemoveSignature(System.Security.Cryptography.Pkcs.SignerInfo)"/> method removes the signature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object.
        /// </summary>
        /// <param name="signerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object that represents the countersignature being removed.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        [SecuritySafeCritical]
        public void RemoveSignature(SignerInfo signerInfo)
        {
            if (signerInfo == null)
                throw new ArgumentNullException("signerInfo");
            this.RemoveSignature(PkcsUtils.GetSignerIndex(this.m_safeCryptMsgHandle, signerInfo, 0));
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)"/> method verifies the digital signatures on the signed CMS/PKCS #7 message and, optionally, validates the signers' certificates.
        /// </summary>
        /// <param name="verifySignatureOnly">A <see cref="T:System.Boolean"/> value that specifies whether only the digital signatures are verified without the signers' certificates being validated. If <paramref name="verifySignatureOnly"/> is true, only the digital signatures are verified. If it is false, the digital signatures are verified, the signers' certificates are validated, and the purposes of the certificates are validated. The purposes of a certificate are considered valid if the certificate has no key usage or if the key usage supports digital signatures or nonrepudiation.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        public void CheckSignature(bool verifySignatureOnly)
        {
            this.CheckSignature(new X509Certificate2Collection(), verifySignatureOnly);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)"/> method verifies the digital signatures on the signed CMS/PKCS #7 message by using the specified collection of certificates and, optionally, validates the signers' certificates.
        /// </summary>
        /// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> object that can be used to validate the certificate chain. If no additional certificates are to be used to validate the certificate chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)"/> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)"/>.</param><param name="verifySignatureOnly">A <see cref="T:System.Boolean"/> value that specifies whether only the digital signatures are verified without the signers' certificates being validated. If <paramref name="verifySignatureOnly"/> is true, only the digital signatures are verified. If it is false, the digital signatures are verified, the signers' certificates are validated, and the purposes of the certificates are validated. The purposes of a certificate are considered valid if the certificate has no key usage or if the key usage supports digital signatures or nonrepudiation.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        [SecuritySafeCritical]
        public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
        {
            if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
            if (extraStore == null)
                throw new ArgumentNullException("extraStore");
            SignedCms.CheckSignatures(this.SignerInfos, extraStore, verifySignatureOnly);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckHash"/> method verifies the data integrity of the CMS/PKCS #7 message. <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckHash"/> is a specialized method used in specific security infrastructure applications in which the subject uses the <see cref="F:System.Security.Cryptography.Pkcs.SubjectIdentifierType.HashOnly"/> enumeration member when setting up a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> object. <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckHash"/> does not authenticate the author nor sender of the message because this method does not involve verifying a digital signature. For general-purpose checking of the integrity and authenticity of a CMS/PKCS #7 message, use the <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)"/> or <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)"/> methods.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        [SecuritySafeCritical]
        public void CheckHash()
        {
            if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
            SignedCms.CheckHashes(this.SignerInfos);
        }

        [SecuritySafeCritical]
        private static int SafeGetLastWin32Error()
        {
            return Marshal.GetLastWin32Error();
        }

        [SecurityCritical]
        internal SafeCryptMsgHandle GetCryptMsgHandle()
        {
            return this.m_safeCryptMsgHandle;
        }

        [SecuritySafeCritical]
        internal void ReopenToDecode()
        {
            byte[] message = PkcsUtils.GetMessage(this.m_safeCryptMsgHandle);
            if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
                this.m_safeCryptMsgHandle.Dispose();
            this.m_safeCryptMsgHandle = SignedCms.OpenToDecode(message, this.ContentInfo, this.Detached);
        }

        [SecuritySafeCritical]
        private unsafe void Sign(CmsSigner signer, bool silent)
        {
            CAPI.CMSG_SIGNED_ENCODE_INFO signedEncodeInfo = new CAPI.CMSG_SIGNED_ENCODE_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNED_ENCODE_INFO)));
            CAPI.CMSG_SIGNER_ENCODE_INFO signerEncodeInfo = PkcsUtils.CreateSignerEncodeInfo(signer, silent);
            byte[] encodedMessage = (byte[])null;
            try
            {
                SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO))));
                try
                {
                    Marshal.StructureToPtr((object)signerEncodeInfo, localAllocHandle.DangerousGetHandle(), false);
                    X509Certificate2Collection bagOfCertificates = PkcsUtils.CreateBagOfCertificates(signer);
                    SafeLocalAllocHandle encodedCertBlob = PkcsUtils.CreateEncodedCertBlob(bagOfCertificates);
                    signedEncodeInfo.cSigners = 1U;
                    signedEncodeInfo.rgSigners = localAllocHandle.DangerousGetHandle();
                    signedEncodeInfo.cCertEncoded = (uint)bagOfCertificates.Count;
                    if (bagOfCertificates.Count > 0)
                        signedEncodeInfo.rgCertEncoded = encodedCertBlob.DangerousGetHandle();
                    SafeCryptMsgHandle safeCryptMsgHandle = string.Compare(this.ContentInfo.ContentType.Value, "1.2.840.113549.1.7.1", StringComparison.OrdinalIgnoreCase) != 0 ? CAPI.CryptMsgOpenToEncode(65537U, this.Detached ? 4U : 0U, 2U, new IntPtr((void*)&signedEncodeInfo), this.ContentInfo.ContentType.Value, IntPtr.Zero) : CAPI.CryptMsgOpenToEncode(65537U, this.Detached ? 4U : 0U, 2U, new IntPtr((void*)&signedEncodeInfo), IntPtr.Zero, IntPtr.Zero);
                    if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                    if (this.ContentInfo.Content.Length > 0 && !CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, this.ContentInfo.pContent, (uint)this.ContentInfo.Content.Length, true))
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                    encodedMessage = PkcsUtils.GetContent(safeCryptMsgHandle);
                    safeCryptMsgHandle.Dispose();
                    encodedCertBlob.Dispose();
                }
                finally
                {
                    Marshal.DestroyStructure(localAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_ENCODE_INFO));
                    localAllocHandle.Dispose();
                }
            }
            finally
            {
                signerEncodeInfo.Dispose();
            }
            SafeCryptMsgHandle safeCryptMsgHandle1 = SignedCms.OpenToDecode(encodedMessage, this.ContentInfo, this.Detached);
            if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
                this.m_safeCryptMsgHandle.Dispose();
            this.m_safeCryptMsgHandle = safeCryptMsgHandle1;
            GC.KeepAlive((object)signer);
        }

        [SecuritySafeCritical]
        private void CoSign(CmsSigner signer, bool silent)
        {
            CAPI.CMSG_SIGNER_ENCODE_INFO signerEncodeInfo = PkcsUtils.CreateSignerEncodeInfo(signer, silent);
            try
            {
                SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO))));
                try
                {
                    Marshal.StructureToPtr((object)signerEncodeInfo, localAllocHandle.DangerousGetHandle(), false);
                    if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 6U, localAllocHandle.DangerousGetHandle()))
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                }
                finally
                {
                    Marshal.DestroyStructure(localAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_ENCODE_INFO));
                    localAllocHandle.Dispose();
                }
            }
            finally
            {
                signerEncodeInfo.Dispose();
            }
            int num = (int)PkcsUtils.AddCertsToMessage(this.m_safeCryptMsgHandle, this.Certificates, PkcsUtils.CreateBagOfCertificates(signer));
        }

        [SecuritySafeCritical]
        private static SafeCryptMsgHandle OpenToDecode(byte[] encodedMessage, ContentInfo contentInfo, bool detached)
        {
            SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CAPISafe.CryptMsgOpenToDecode(65537U, detached ? 4U : 0U, 0U, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if (!CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, encodedMessage, (uint)encodedMessage.Length, true))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            if (2 != (int)PkcsUtils.GetMessageType(safeCryptMsgHandle))
                throw new CryptographicException(-2146889724);
            if (detached)
            {
                byte[] content = contentInfo.Content;
                if (content != null && content.Length > 0 && !CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, content, (uint)content.Length, true))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
            }
            return safeCryptMsgHandle;
        }

        private static void CheckSignatures(SignerInfoCollection signers, X509Certificate2Collection extraStore, bool verifySignatureOnly)
        {
            if (signers == null || signers.Count < 1)
                throw new CryptographicException(-2146885618);
            foreach (SignerInfo signerInfo in signers)
            {
                signerInfo.CheckSignature(extraStore, verifySignatureOnly);
                if (signerInfo.CounterSignerInfos.Count > 0)
                    SignedCms.CheckSignatures(signerInfo.CounterSignerInfos, extraStore, verifySignatureOnly);
            }
        }

        private static void CheckHashes(SignerInfoCollection signers)
        {
            if (signers == null || signers.Count < 1)
                throw new CryptographicException(-2146885618);
            foreach (SignerInfo signerInfo in signers)
            {
                if (signerInfo.SignerIdentifier.Type == SubjectIdentifierType.NoSignature)
                    signerInfo.CheckHash();
            }
        }
    }
}
