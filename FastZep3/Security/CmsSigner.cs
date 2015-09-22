// Type: System.Security.Cryptography.Pkcs.CmsSigner
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Globalization;
using System.Runtime;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> class provides signing functionality.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class CmsSigner
    {
        private SubjectIdentifierType m_signerIdentifierType;
        private X509Certificate2 m_certificate;
        private Oid m_digestAlgorithm;
        private CryptographicAttributeObjectCollection m_signedAttributes;
        private CryptographicAttributeObjectCollection m_unsignedAttributes;
        private X509Certificate2Collection m_certificates;
        private X509IncludeOption m_includeOption;
        private bool m_dummyCert;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignerIdentifierType"/> property sets or retrieves the type of the identifier of the signer.
        /// </summary>
        /// 
        /// <returns>
        /// A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the type of the identifier of the signer.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
        public SubjectIdentifierType SignerIdentifierType
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_signerIdentifierType;
            }
            set
            {
                if (value != SubjectIdentifierType.IssuerAndSerialNumber && value != SubjectIdentifierType.SubjectKeyIdentifier && value != SubjectIdentifierType.NoSignature)
                    throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[1]
          {
            (object) "value"
          }));
                else if (this.m_dummyCert && value != SubjectIdentifierType.SubjectKeyIdentifier)
                    throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[1]
          {
            (object) "value"
          }));
                else
                    this.m_signerIdentifierType = value;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificate"/> property sets or retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that represents the signing certificate.
        /// </summary>
        /// 
        /// <returns>
        /// An  <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that represents the signing certificate.
        /// </returns>
        public X509Certificate2 Certificate
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_certificate;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.m_certificate = value;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.DigestAlgorithm"/> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid"/> that represents the hash algorithm used with the signature.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Oid"/> object that represents the hash algorithm used with the signature.
        /// </returns>
        public Oid DigestAlgorithm
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_digestAlgorithm;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.m_digestAlgorithm = value;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignedAttributes"/> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection of signed attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> content. Signed attributes are signed along with the specified content.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.
        /// </returns>
        public CryptographicAttributeObjectCollection SignedAttributes
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_signedAttributes;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.UnsignedAttributes"/> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection of unsigned PKCS #9 attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> content. Unsigned attributes can be modified without invalidating the signature.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection"/> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.
        /// </returns>
        public CryptographicAttributeObjectCollection UnsignedAttributes
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_unsignedAttributes;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificates"/> property retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> collection that contains certificates associated with the message to be signed.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> collection that represents the collection of  certificates associated with the message to be signed.
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
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption"/> property sets or retrieves the option that controls whether the root and entire chain associated with the signing certificate are included with the created CMS/PKCS #7 message.
        /// </summary>
        /// 
        /// <returns>
        /// A member of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption"/> enumeration that specifies how much of the X509 certificate chain should be included in the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> object. The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption"/> property can be one of the following <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption"/> members.NameValueMeaning<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.None"/>0The certificate chain is not included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.ExcludeRoot"/>1The certificate chain, except for the root certificate, is included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.EndCertOnly"/>2Only the end certificate is included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.WholeChain"/>3The certificate chain, including the root certificate, is included.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
        public X509IncludeOption IncludeOption
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_includeOption;
            }
            set
            {
                if (value < X509IncludeOption.None || value > X509IncludeOption.WholeChain)
                    throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[1]
          {
            (object) "value"
          }));
                else
                    this.m_includeOption = value;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> class by using a default subject identifier type.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public CmsSigner()
            : this(SubjectIdentifierType.IssuerAndSerialNumber, (X509Certificate2)null)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> class with the specified subject identifier type.
        /// </summary>
        /// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the signer identifier type.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public CmsSigner(SubjectIdentifierType signerIdentifierType)
            : this(signerIdentifierType, (X509Certificate2)null)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> class with the specified signing certificate.
        /// </summary>
        /// <param name="certificate">An    <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that represents the signing certificate.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public CmsSigner(X509Certificate2 certificate)
            : this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> class with the specified cryptographic service provider (CSP) parameters. <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)"/> is useful when you know the specific CSP and private key to use for signing.
        /// </summary>
        /// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters"/>  object that represents the set of CSP parameters to use.</param>
        [SecuritySafeCritical]
        public CmsSigner(CspParameters parameters)
            : this(SubjectIdentifierType.SubjectKeyIdentifier, PkcsUtils.CreateDummyCertificate(parameters))
        {
            this.m_dummyCert = true;
            this.IncludeOption = X509IncludeOption.None;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner"/> class with the specified signer identifier type and signing certificate.
        /// </summary>
        /// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the signer identifier type.</param><param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that represents the signing certificate.</param>
        public CmsSigner(SubjectIdentifierType signerIdentifierType, X509Certificate2 certificate)
        {
            switch (signerIdentifierType)
            {
                case SubjectIdentifierType.Unknown:
                    this.SignerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
                    this.IncludeOption = X509IncludeOption.ExcludeRoot;
                    break;
                case SubjectIdentifierType.IssuerAndSerialNumber:
                    this.SignerIdentifierType = signerIdentifierType;
                    this.IncludeOption = X509IncludeOption.ExcludeRoot;
                    break;
                case SubjectIdentifierType.SubjectKeyIdentifier:
                    this.SignerIdentifierType = signerIdentifierType;
                    this.IncludeOption = X509IncludeOption.ExcludeRoot;
                    break;
                case SubjectIdentifierType.NoSignature:
                    this.SignerIdentifierType = signerIdentifierType;
                    this.IncludeOption = X509IncludeOption.None;
                    break;
                default:
                    this.SignerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
                    this.IncludeOption = X509IncludeOption.ExcludeRoot;
                    break;
            }
            this.Certificate = certificate;
            this.DigestAlgorithm = new Oid("1.3.14.3.2.26");
            this.m_signedAttributes = new CryptographicAttributeObjectCollection();
            this.m_unsignedAttributes = new CryptographicAttributeObjectCollection();
            this.m_certificates = new X509Certificate2Collection();
        }
    }
}
