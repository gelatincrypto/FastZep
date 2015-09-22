// Type: System.Security.Cryptography.Pkcs.PublicKeyInfo
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
    /// The <see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo"/> class represents information associated with a public key.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class PublicKeyInfo
    {
        private AlgorithmIdentifier m_algorithm;
        private byte[] m_keyValue;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.Algorithm"/> property retrieves the algorithm identifier associated with the public key.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/>  object that represents the algorithm.
        /// </returns>
        public AlgorithmIdentifier Algorithm
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_algorithm;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.KeyValue"/> property retrieves the value of the encoded public component of the public key pair.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values  that represents the encoded public component of the public key pair.
        /// </returns>
        public byte[] KeyValue
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_keyValue;
            }
        }

        private PublicKeyInfo()
        {
        }

        [SecurityCritical]
        internal PublicKeyInfo(CAPI.CERT_PUBLIC_KEY_INFO keyInfo)
        {
            this.m_algorithm = new AlgorithmIdentifier(keyInfo);
            this.m_keyValue = new byte[(IntPtr)keyInfo.PublicKey.cbData];
            if (this.m_keyValue.Length <= 0)
                return;
            Marshal.Copy(keyInfo.PublicKey.pbData, this.m_keyValue, 0, this.m_keyValue.Length);
        }
    }
}
