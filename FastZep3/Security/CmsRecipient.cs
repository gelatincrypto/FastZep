// Type: System.Security.Cryptography.Pkcs.CmsRecipient
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;
namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> class defines the recipient of a CMS/PKCS #7 message.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class CmsRecipient
    {
        private SubjectIdentifierType m_recipientIdentifierType;
        private X509Certificate2 m_certificate;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.RecipientIdentifierType"/> property retrieves the type of the identifier of the recipient.
        /// </summary>
        /// 
        /// <returns>
        /// A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the type of the identifier of the recipient.
        /// </returns>
        public SubjectIdentifierType RecipientIdentifierType
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_recipientIdentifierType;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.Certificate"/> property retrieves the certificate associated with the recipient.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that holds the certificate associated with the recipient.
        /// </returns>
        public X509Certificate2 Certificate
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_certificate;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)"/> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> class by using the specified recipient certificate.
        /// </summary>
        /// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that represents the recipient certificate.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public CmsRecipient(X509Certificate2 certificate)
            : this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
        {
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)"/> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> class by using the specified recipient identifier type and recipient certificate.
        /// </summary>
        /// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the type of the identifier of the recipient.</param><param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2"/> object that represents the recipient certificate.</param><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        public CmsRecipient(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
        {
            this.Reset(recipientIdentifierType, certificate);
        }

        private CmsRecipient()
        {
        }

        private void Reset(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException("certificate");
            switch (recipientIdentifierType)
            {
                case SubjectIdentifierType.Unknown:
                    recipientIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
                    goto case 1;
                case SubjectIdentifierType.IssuerAndSerialNumber:
                    this.m_recipientIdentifierType = recipientIdentifierType;
                    this.m_certificate = certificate;
                    break;
                case SubjectIdentifierType.SubjectKeyIdentifier:
                    if (!PkcsUtils.CmsSupported())
                        throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
                    else
                        goto case 1;
                default:
                    throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), ((object)recipientIdentifierType).ToString());
            }
        }
    }
}
