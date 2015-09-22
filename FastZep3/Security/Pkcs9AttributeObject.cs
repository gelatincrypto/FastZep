// Type: System.Security.Cryptography.Pkcs.Pkcs9AttributeObject
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// Represents an attribute used for CMS/PKCS #7 and PKCS #9 operations.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public class Pkcs9AttributeObject : AsnEncodedData
    {
        /// <summary>
        /// Gets an <see cref="T:System.Security.Cryptography.Oid"/> object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject"/> object.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Oid"/> object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject"/> object.
        /// </returns>
        public new Oid Oid
        {
            get
            {
                return base.Oid;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject"/> class.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Pkcs9AttributeObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject"/> class using a specified string representation of an object identifier (OID) as the attribute type and a specified ASN.1 encoded data as the attribute value.
        /// </summary>
        /// <param name="oid">A <see cref="T:System.String"/> object that contains the string representation of an OID that represents the PKCS #9 attribute type.</param><param name="encodedData">An array of byte values that contains the PKCS #9 attribute value.</param>
        public Pkcs9AttributeObject(string oid, byte[] encodedData)
            : this(new AsnEncodedData(oid, encodedData))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject"/> class using a specified <see cref="T:System.Security.Cryptography.Oid"/> object as the attribute type and a specified ASN.1 encoded data as the attribute value.
        /// </summary>
        /// <param name="oid">An <see cref="T:System.Security.Cryptography.Oid"/> object that represents the PKCS #9 attribute type.</param><param name="encodedData">An array of byte values that represents the PKCS #9 attribute value.</param>
        public Pkcs9AttributeObject(Oid oid, byte[] encodedData)
            : this(new AsnEncodedData(oid, encodedData))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject"/> class using a specified <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object as its attribute type and value.
        /// </summary>
        /// <param name="asnEncodedData">An <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object that contains the PKCS #9 attribute type and value to use.</param>
        public Pkcs9AttributeObject(AsnEncodedData asnEncodedData)
            : base(asnEncodedData)
        {
            if (asnEncodedData.Oid == null)
                throw new ArgumentNullException("asnEncodedData.Oid");
            string str = base.Oid.Value;
            if (str == null)
                throw new ArgumentNullException("oid.Value");
            if (str.Length == 0)
                throw new ArgumentException(SecurityResources.GetResourceString("Arg_EmptyOrNullString"), "oid.Value");
        }

        internal Pkcs9AttributeObject(string oid)
        {
            this.Oid = new Oid(oid);
        }

        /// <summary>
        /// Copies a PKCS #9 attribute type and value for this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject"/> from the specified <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object.
        /// </summary>
        /// <param name="asnEncodedData">An <see cref="T:System.Security.Cryptography.AsnEncodedData"/> object that contains the PKCS #9 attribute type and value to use.</param>
        public override void CopyFrom(AsnEncodedData asnEncodedData)
        {
            if (asnEncodedData == null)
                throw new ArgumentNullException("asnEncodedData");
            if (!(asnEncodedData is Pkcs9AttributeObject))
                throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Pkcs9_AttributeMismatch"));
            base.CopyFrom(asnEncodedData);
        }
    }
}
