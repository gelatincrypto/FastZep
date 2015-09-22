// Type: System.Security.Cryptography.Pkcs.SignerInfo
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> class represents a signer associated with a <see cref="T:System.Security.Cryptography.Pkcs.SignedCms"/> object that represents a CMS/PKCS #7 message.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class SignerInfo
    {
        private X509Certificate2 m_certificate;
        private SubjectIdentifier m_signerIdentifier;
        private CryptographicAttributeObjectCollection m_signedAttributes;
        private CryptographicAttributeObjectCollection m_unsignedAttributes;
        private SignedCms m_signedCms;
        private SignerInfo m_parentSignerInfo;
        private byte[] m_encodedSignerInfo;
        [SecurityCritical]
        private SafeLocalAllocHandle m_pbCmsgSignerInfo;
        private CAPI.CMSG_SIGNER_INFO m_cmsgSignerInfo;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Version"/> property retrieves the signer information version.
        /// </summary>
        /// 
        /// <returns>
        /// An int value that specifies the signer information version.
        /// </returns>
        public int Version
        {
            get
            {
                return (int)this.m_cmsgSignerInfo.dwVersion;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Certificate"/> property retrieves the signing certificate associated with the signer information.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that represents the signing certificate.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy"/></PermissionSet>
        public X509Certificate2 Certificate
        {
            get
            {
                if (this.m_certificate == null)
                    this.m_certificate = PkcsUtils.FindCertificate(this.SignerIdentifier, this.m_signedCms.Certificates);
                return this.m_certificate;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignerIdentifier"/> property retrieves the certificate identifier of the signer associated with the signer information.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier"/> object that uniquely identifies the certificate associated with the signer information.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public SubjectIdentifier SignerIdentifier
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_signerIdentifier == null)
                    this.m_signerIdentifier = new SubjectIdentifier(this.m_cmsgSignerInfo);
                return this.m_signerIdentifier;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.DigestAlgorithm"/> property retrieves the <see cref="T:System.Security.Cryptography.Oid"/> object that represents the hash algorithm used in the computation of the signatures.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Oid"/> object that represents the hash algorithm used with the signature.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public Oid DigestAlgorithm
        {
            get
            {
                return new Oid(this.m_cmsgSignerInfo.HashAlgorithm.pszObjId);
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignedAttributes"/> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection of signed attributes that is associated with the signer information. Signed attributes are signed along with the rest of the message content.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public CryptographicAttributeObjectCollection SignedAttributes
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_signedAttributes == null)
                    this.m_signedAttributes = new CryptographicAttributeObjectCollection(this.m_cmsgSignerInfo.AuthAttrs);
                return this.m_signedAttributes;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.UnsignedAttributes"/> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection of unsigned attributes that is associated with the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> content. Unsigned attributes can be modified without invalidating the signature.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public CryptographicAttributeObjectCollection UnsignedAttributes
        {
            [SecuritySafeCritical]
            get
            {
                if (this.m_unsignedAttributes == null)
                    this.m_unsignedAttributes = new CryptographicAttributeObjectCollection(this.m_cmsgSignerInfo.UnauthAttrs);
                return this.m_unsignedAttributes;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos"/> property retrieves the set of counter signers associated with the signer information.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection that represents the counter signers for the signer information. If there are no counter signers, the property is an empty collection.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public SignerInfoCollection CounterSignerInfos
        {
            get
            {
                if (this.m_parentSignerInfo != null)
                    return new SignerInfoCollection();
                else
                    return new SignerInfoCollection(this.m_signedCms, this);
            }
        }

        private SignerInfo()
        {
        }

        [SecurityCritical]
        internal SignerInfo(SignedCms signedCms, SafeLocalAllocHandle pbCmsgSignerInfo)
        {
            this.m_signedCms = signedCms;
            this.m_parentSignerInfo = (SignerInfo)null;
            this.m_encodedSignerInfo = (byte[])null;
            this.m_pbCmsgSignerInfo = pbCmsgSignerInfo;
            this.m_cmsgSignerInfo = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(pbCmsgSignerInfo.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
        }

        [SecuritySafeCritical]
        internal SignerInfo(SignedCms signedCms, SignerInfo parentSignerInfo, byte[] encodedSignerInfo)
        {
            uint cbDecodedValue = 0U;
            SafeLocalAllocHandle decodedValue = SafeLocalAllocHandle.InvalidHandle;
            fixed (byte* numPtr = &encodedSignerInfo[0])
            {
                if (!CAPI.DecodeObject(new IntPtr(500L), new IntPtr((void*)numPtr), (uint)encodedSignerInfo.Length, out decodedValue, out cbDecodedValue))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
            }
            this.m_signedCms = signedCms;
            this.m_parentSignerInfo = parentSignerInfo;
            this.m_encodedSignerInfo = (byte[])encodedSignerInfo.Clone();
            this.m_pbCmsgSignerInfo = decodedValue;
            this.m_cmsgSignerInfo = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(decodedValue.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature"/> method prompts the user to select a signing certificate, creates a countersignature, and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows"/><IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates"/></PermissionSet>
        public void ComputeCounterSignature()
        {
            this.ComputeCounterSignature(new CmsSigner(this.m_signedCms.Version == 2 ? SubjectIdentifierType.SubjectKeyIdentifier : SubjectIdentifierType.IssuerAndSerialNumber));
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature(System.Security.Cryptography.Pkcs.CmsSigner)"/> method creates a countersignature by using the specified signer and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.
        /// </summary>
        /// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> object that represents the counter signer.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        public void ComputeCounterSignature(CmsSigner signer)
        {
            if (this.m_parentSignerInfo != null)
                throw new CryptographicException(-2147483647);
            if (signer == null)
                throw new ArgumentNullException("signer");
            if (signer.Certificate == null)
                signer.Certificate = PkcsUtils.SelectSignerCertificate();
            if (!signer.Certificate.HasPrivateKey)
                throw new CryptographicException(-2146893811);
            this.CounterSign(signer);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Int32)"/> method removes the countersignature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos"/> collection.
        /// </summary>
        /// <param name="index">The zero-based index of the countersignature to remove.</param>
        [SecuritySafeCritical]
        public void RemoveCounterSignature(int index)
        {
            if (this.m_parentSignerInfo != null)
                throw new CryptographicException(-2147483647);
            this.RemoveCounterSignature(PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0), index);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Security.Cryptography.Pkcs.SignerInfo)"/> method removes the countersignature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object.
        /// </summary>
        /// <param name="counterSignerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object that represents the countersignature being removed.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        [SecuritySafeCritical]
        public void RemoveCounterSignature(SignerInfo counterSignerInfo)
        {
            if (this.m_parentSignerInfo != null)
                throw new CryptographicException(-2147483647);
            if (counterSignerInfo == null)
                throw new ArgumentNullException("counterSignerInfo");
            foreach (CryptographicAttributeObject cryptographicAttributeObject in this.UnsignedAttributes)
            {
                if (string.Compare(cryptographicAttributeObject.Oid.Value, "1.2.840.113549.1.9.6", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    for (int childIndex = 0; childIndex < cryptographicAttributeObject.Values.Count; ++childIndex)
                    {
                        SignerInfo signerInfo = new SignerInfo(this.m_signedCms, this.m_parentSignerInfo, cryptographicAttributeObject.Values[childIndex].RawData);
                        if (counterSignerInfo.SignerIdentifier.Type == SubjectIdentifierType.IssuerAndSerialNumber && signerInfo.SignerIdentifier.Type == SubjectIdentifierType.IssuerAndSerialNumber)
                        {
                            X509IssuerSerial x509IssuerSerial1 = (X509IssuerSerial)counterSignerInfo.SignerIdentifier.Value;
                            X509IssuerSerial x509IssuerSerial2 = (X509IssuerSerial)signerInfo.SignerIdentifier.Value;
                            if (string.Compare(x509IssuerSerial1.IssuerName, x509IssuerSerial2.IssuerName, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(x509IssuerSerial1.SerialNumber, x509IssuerSerial2.SerialNumber, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                this.RemoveCounterSignature(PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0), childIndex);
                                return;
                            }
                        }
                        else if (counterSignerInfo.SignerIdentifier.Type == SubjectIdentifierType.SubjectKeyIdentifier && signerInfo.SignerIdentifier.Type == SubjectIdentifierType.SubjectKeyIdentifier && string.Compare(counterSignerInfo.SignerIdentifier.Value as string, signerInfo.SignerIdentifier.Value as string, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            this.RemoveCounterSignature(PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0), childIndex);
                            return;
                        }
                    }
                }
            }
            throw new CryptographicException(-2146889714);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)"/> method verifies the digital signature of the message and, optionally, validates the certificate.
        /// </summary>
        /// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly"/> is true, only the signature is verified. If <paramref name="verifySignatureOnly"/> is false, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        public void CheckSignature(bool verifySignatureOnly)
        {
            this.CheckSignature(new X509Certificate2Collection(), verifySignatureOnly);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)"/> method verifies the digital signature of the message by using the specified collection of certificates and, optionally, validates the certificate.
        /// </summary>
        /// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> object that can be used to validate the chain. If no additional certificates are to be used to validate the chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)"/> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)"/>.</param><param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly"/> is true, only the signature is verified. If <paramref name="verifySignatureOnly"/> is false, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception><exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
        public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
        {
            if (extraStore == null)
                throw new ArgumentNullException("extraStore");
            X509Certificate2 certificate = this.Certificate;
            if (certificate == null)
            {
                certificate = PkcsUtils.FindCertificate(this.SignerIdentifier, extraStore);
                if (certificate == null)
                    throw new CryptographicException(-2146889714);
            }
            this.Verify(extraStore, certificate, verifySignatureOnly);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash"/> method verifies the data integrity of the CMS/PKCS #7 message signer information. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash"/> is a specialized method used in specific security infrastructure applications in which the subject uses the HashOnly member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration when setting up a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> object. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash"/> does not authenticate the signer information because this method does not involve verifying a digital signature. For general-purpose checking of the integrity and authenticity of CMS/PKCS #7 message signer information and countersignatures, use the <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)"/> or <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)"/> methods.
        /// </summary>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        [SecuritySafeCritical]
        public unsafe void CheckHash()
        {
            if (!CAPI.CryptMsgControl(this.m_signedCms.GetCryptMsgHandle(), 0U, 19U, new IntPtr((void*)&new CAPI.CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA)))
            {
                dwSignerType = 4U,
                dwSignerIndex = (uint)PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0)
            })))
                throw new CryptographicException(Marshal.GetLastWin32Error());
        }

        internal CAPI.CMSG_SIGNER_INFO GetCmsgSignerInfo()
        {
            return this.m_cmsgSignerInfo;
        }

        [SecuritySafeCritical]
        private void CounterSign(CmsSigner signer)
        {
            CspParameters parameters = new CspParameters();
            if (!X509Utils.GetPrivateKeyInfo(X509Utils.GetCertContext(signer.Certificate), ref parameters))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
            KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Sign);
            containerPermission.AccessEntries.Add(accessEntry);
            containerPermission.Demand();
            uint dwIndex = (uint)PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0);
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO))));
            CAPI.CMSG_SIGNER_ENCODE_INFO signerEncodeInfo = PkcsUtils.CreateSignerEncodeInfo(signer);
            try
            {
                Marshal.StructureToPtr((object)signerEncodeInfo, localAllocHandle.DangerousGetHandle(), false);
                if (!CAPI.CryptMsgCountersign(this.m_signedCms.GetCryptMsgHandle(), dwIndex, 1U, localAllocHandle.DangerousGetHandle()))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                this.m_signedCms.ReopenToDecode();
            }
            finally
            {
                Marshal.DestroyStructure(localAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_ENCODE_INFO));
                localAllocHandle.Dispose();
                signerEncodeInfo.Dispose();
            }
            int num = (int)PkcsUtils.AddCertsToMessage(this.m_signedCms.GetCryptMsgHandle(), this.m_signedCms.Certificates, PkcsUtils.CreateBagOfCertificates(signer));
        }

        [SecuritySafeCritical]
        private unsafe void Verify(X509Certificate2Collection extraStore, X509Certificate2 certificate, bool verifySignatureOnly)
        {
            SafeLocalAllocHandle pvData1 = SafeLocalAllocHandle.InvalidHandle;
            CAPI.CERT_CONTEXT certContext = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificate).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
            IntPtr ptr1 = new IntPtr((long)new IntPtr((long)certContext.pCertInfo + (long)Marshal.OffsetOf(typeof(CAPI.CERT_INFO), "SubjectPublicKeyInfo")) + (long)Marshal.OffsetOf(typeof(CAPI.CERT_PUBLIC_KEY_INFO), "Algorithm"));
            IntPtr num1 = new IntPtr((long)ptr1 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "Parameters"));
            if ((int)CAPI.CryptFindOIDInfo(1U, Marshal.ReadIntPtr(ptr1), 3U).Algid == 8704)
            {
                bool flag = false;
                IntPtr ptr2 = new IntPtr((long)num1 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
                IntPtr ptr3 = new IntPtr((long)num1 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
                if (Marshal.ReadInt32(ptr2) == 0)
                    flag = true;
                else if (Marshal.ReadIntPtr(ptr3) == IntPtr.Zero)
                    flag = true;
                else if (Marshal.ReadInt32(Marshal.ReadIntPtr(ptr3)) == 5)
                    flag = true;
                if (flag)
                {
                    SafeCertChainHandle invalidHandle = SafeCertChainHandle.InvalidHandle;
                    X509Utils.BuildChain(new IntPtr(0L), X509Utils.GetCertContext(certificate), (X509Certificate2Collection)null, (OidCollection)null, (OidCollection)null, X509RevocationMode.NoCheck, X509RevocationFlag.ExcludeRoot, DateTime.Now, new TimeSpan(0, 0, 0), ref invalidHandle);
                    invalidHandle.Dispose();
                    uint pcbData = 0U;
                    if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 22U, pvData1, out pcbData))
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                    if (pcbData > 0U)
                    {
                        pvData1 = CAPI.LocalAlloc(64U, new IntPtr((long)pcbData));
                        if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 22U, pvData1, out pcbData))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        Marshal.WriteInt32(ptr2, (int)pcbData);
                        Marshal.WriteIntPtr(ptr3, pvData1.DangerousGetHandle());
                    }
                }
            }
            if (this.m_parentSignerInfo == null)
            {
                if (!CAPI.CryptMsgControl(this.m_signedCms.GetCryptMsgHandle(), 0U, 1U, certContext.pCertInfo))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
            }
            else
            {
                int num2 = -1;
                int hr = 0;
                SafeLocalAllocHandle pvData2;
                while (true)
                {
                    try
                    {
                        num2 = PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this.m_parentSignerInfo, num2 + 1);
                    }
                    catch (CryptographicException ex)
                    {
                        if (hr != 0)
                            throw new CryptographicException(hr);
                        throw;
                    }
                    uint cbData = 0U;
                    pvData2 = SafeLocalAllocHandle.InvalidHandle;
                    PkcsUtils.GetParam(this.m_signedCms.GetCryptMsgHandle(), 28U, (uint)num2, out pvData2, out cbData);
                    if ((int)cbData == 0)
                    {
                        hr = -2146885618;
                    }
                    else
                    {
                        fixed (byte* numPtr = this.m_encodedSignerInfo)
                        {
                            if (!CAPI.CAPISafe.CryptMsgVerifyCountersignatureEncoded(IntPtr.Zero, 65537U, pvData2.DangerousGetHandle(), cbData, new IntPtr((void*)numPtr), (uint)this.m_encodedSignerInfo.Length, certContext.pCertInfo))
                                hr = Marshal.GetLastWin32Error();
                            else
                                break;
                        }
                    }
                }
                // ISSUE: fixed variable is out of scope
                // ISSUE: __unpin statement
                __unpin(numPtr);
                pvData2.Dispose();
            }
            if (!verifySignatureOnly)
            {
                int hr = SignerInfo.VerifyCertificate(certificate, extraStore);
                if (hr != 0)
                    throw new CryptographicException(hr);
            }
            pvData1.Dispose();
        }

        [SecuritySafeCritical]
        private unsafe void RemoveCounterSignature(int parentIndex, int childIndex)
        {
            if (parentIndex < 0)
                throw new ArgumentOutOfRangeException("parentIndex");
            if (childIndex < 0)
                throw new ArgumentOutOfRangeException("childIndex");
            uint cbData1 = 0U;
            SafeLocalAllocHandle pvData1 = SafeLocalAllocHandle.InvalidHandle;
            uint cbData2 = 0U;
            SafeLocalAllocHandle pvData2 = SafeLocalAllocHandle.InvalidHandle;
            IntPtr num1 = IntPtr.Zero;
            SafeCryptMsgHandle cryptMsgHandle = this.m_signedCms.GetCryptMsgHandle();
            uint num2;
            if (PkcsUtils.CmsSupported())
            {
                PkcsUtils.GetParam(cryptMsgHandle, 39U, (uint)parentIndex, out pvData1, out cbData1);
                CAPI.CMSG_CMS_SIGNER_INFO cmsgCmsSignerInfo = (CAPI.CMSG_CMS_SIGNER_INFO)Marshal.PtrToStructure(pvData1.DangerousGetHandle(), typeof(CAPI.CMSG_CMS_SIGNER_INFO));
                num2 = cmsgCmsSignerInfo.UnauthAttrs.cAttr;
                num1 = new IntPtr((long)cmsgCmsSignerInfo.UnauthAttrs.rgAttr);
            }
            else
            {
                PkcsUtils.GetParam(cryptMsgHandle, 6U, (uint)parentIndex, out pvData2, out cbData2);
                CAPI.CMSG_SIGNER_INFO cmsgSignerInfo = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(pvData2.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
                num2 = cmsgSignerInfo.UnauthAttrs.cAttr;
                num1 = new IntPtr((long)cmsgSignerInfo.UnauthAttrs.rgAttr);
            }
            for (uint index = 0U; index < num2; ++index)
            {
                CAPI.CRYPT_ATTRIBUTE cryptAttribute1 = (CAPI.CRYPT_ATTRIBUTE)Marshal.PtrToStructure(num1, typeof(CAPI.CRYPT_ATTRIBUTE));
                if (string.Compare(cryptAttribute1.pszObjId, "1.2.840.113549.1.9.6", StringComparison.OrdinalIgnoreCase) == 0 && cryptAttribute1.cValue > 0U)
                {
                    if (childIndex < (int)cryptAttribute1.cValue)
                    {
                        if (!CAPI.CryptMsgControl(cryptMsgHandle, 0U, 9U, new IntPtr((void*)&new CAPI.CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA)))
                        {
                            dwSignerIndex = (uint)parentIndex,
                            dwUnauthAttrIndex = index
                        })))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        if (cryptAttribute1.cValue <= 1U)
                            return;
                        try
                        {
                            SafeLocalAllocHandle localAllocHandle1 = CAPI.LocalAlloc(64U, new IntPtr((long)(uint)((ulong)(cryptAttribute1.cValue - 1U) * (ulong)Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB)))));
                            CAPI.CRYPTOAPI_BLOB* cryptoapiBlobPtr1 = (CAPI.CRYPTOAPI_BLOB*)(void*)cryptAttribute1.rgValue;
                            CAPI.CRYPTOAPI_BLOB* cryptoapiBlobPtr2 = (CAPI.CRYPTOAPI_BLOB*)(void*)localAllocHandle1.DangerousGetHandle();
                            int num3 = 0;
                            while (num3 < (int)cryptAttribute1.cValue)
                            {
                                if (num3 != childIndex)
                                    *cryptoapiBlobPtr2 = *cryptoapiBlobPtr1;
                                ++num3;
                                ++cryptoapiBlobPtr1;
                                ++cryptoapiBlobPtr2;
                            }
                            CAPI.CRYPT_ATTRIBUTE cryptAttribute2 = new CAPI.CRYPT_ATTRIBUTE();
                            cryptAttribute2.pszObjId = cryptAttribute1.pszObjId;
                            cryptAttribute2.cValue = cryptAttribute1.cValue - 1U;
                            cryptAttribute2.rgValue = localAllocHandle1.DangerousGetHandle();
                            SafeLocalAllocHandle localAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_ATTRIBUTE))));
                            Marshal.StructureToPtr((object)cryptAttribute2, localAllocHandle2.DangerousGetHandle(), false);
                            byte[] encodedData;
                            try
                            {
                                if (!CAPI.EncodeObject(new IntPtr(22L), localAllocHandle2.DangerousGetHandle(), out encodedData))
                                    throw new CryptographicException(Marshal.GetLastWin32Error());
                            }
                            finally
                            {
                                Marshal.DestroyStructure(localAllocHandle2.DangerousGetHandle(), typeof(CAPI.CRYPT_ATTRIBUTE));
                                localAllocHandle2.Dispose();
                            }
                            fixed (byte* numPtr = &encodedData[0])
                            {
                                if (!CAPI.CryptMsgControl(cryptMsgHandle, 0U, 8U, new IntPtr((void*)&new CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA)))
                                {
                                    dwSignerIndex = (uint)parentIndex,
                                    blob =
                                    {
                                        cbData = (uint)encodedData.Length,
                                        pbData = new IntPtr((void*)numPtr)
                                    }
                                })))
                                    throw new CryptographicException(Marshal.GetLastWin32Error());
                            }
                            localAllocHandle1.Dispose();
                            return;
                        }
                        catch (CryptographicException ex)
                        {
                            byte[] encodedData;
                            if (CAPI.EncodeObject(new IntPtr(22L), num1, out encodedData))
                            {
                                fixed (byte* numPtr = &encodedData[0])
                                    CAPI.CryptMsgControl(cryptMsgHandle, 0U, 8U, new IntPtr((void*)&new CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA)))
                                    {
                                        dwSignerIndex = (uint)parentIndex,
                                        blob =
                                        {
                                            cbData = (uint)encodedData.Length,
                                            pbData = new IntPtr((void*)numPtr)
                                        }
                                    }));
                            }
                            throw;
                        }
                    }
                    else
                        childIndex -= (int)cryptAttribute1.cValue;
                }
                num1 = new IntPtr((long)num1 + (long)Marshal.SizeOf(typeof(CAPI.CRYPT_ATTRIBUTE)));
            }
            if (pvData1 != null && !pvData1.IsInvalid)
                pvData1.Dispose();
            if (pvData2 != null && !pvData2.IsInvalid)
                pvData2.Dispose();
            throw new CryptographicException(-2146885618);
        }

        [SecuritySafeCritical]
        private static unsafe int VerifyCertificate(X509Certificate2 certificate, X509Certificate2Collection extraStore)
        {
            int num1;
            int num2 = X509Utils.VerifyCertificate(X509Utils.GetCertContext(certificate), (OidCollection)null, (OidCollection)null, X509RevocationMode.Online, X509RevocationFlag.ExcludeRoot, DateTime.Now, new TimeSpan(0, 0, 0), extraStore, new IntPtr(1L), new IntPtr((void*)&num1));
            if (num2 != 0)
                return num1;
            foreach (X509Extension x509Extension in certificate.Extensions)
            {
                if (string.Compare(x509Extension.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    X509KeyUsageExtension keyUsageExtension = new X509KeyUsageExtension();
                    keyUsageExtension.CopyFrom((AsnEncodedData)x509Extension);
                    if ((keyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.None && (keyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) == X509KeyUsageFlags.None)
                    {
                        num2 = -2146762480;
                        break;
                    }
                }
            }
            return num2;
        }
    }
}
