// Type: System.Security.Cryptography.Pkcs.SignerInfoEnumerator
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System.Collections;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator"/> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator"/> implements the <see cref="T:System.Collections.IEnumerator"/> interface.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class SignerInfoEnumerator : IEnumerator
    {
        private SignerInfoCollection m_signerInfos;
        private int m_current;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Current"/> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public SignerInfo Current
        {
            get
            {
                return this.m_signerInfos[this.m_current];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (object)this.m_signerInfos[this.m_current];
            }
        }

        private SignerInfoEnumerator()
        {
        }

        internal SignerInfoEnumerator(SignerInfoCollection signerInfos)
        {
            this.m_signerInfos = signerInfos;
            this.m_current = -1;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.MoveNext"/> method advances the enumeration to the next   <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// This method returns a bool value that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object, the method returns true. If the enumeration moved past the last item in the enumeration, it returns false.
        /// </returns>
        public bool MoveNext()
        {
            if (this.m_current == this.m_signerInfos.Count - 1)
                return false;
            ++this.m_current;
            return true;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Reset"/> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </summary>
        public void Reset()
        {
            this.m_current = -1;
        }
    }
}
