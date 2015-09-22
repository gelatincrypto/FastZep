// Type: System.Security.Cryptography.Pkcs.RecipientInfoEnumerator
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System.Collections;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator"/> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator"/> implements the <see cref="T:System.Collections.IEnumerator"/> interface.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class RecipientInfoEnumerator : IEnumerator
    {
        private RecipientInfoCollection m_recipientInfos;
        private int m_current;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Current"/> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object from the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object that represents the current recipient information structure in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public RecipientInfo Current
        {
            get
            {
                return this.m_recipientInfos[this.m_current];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (object)this.m_recipientInfos[this.m_current];
            }
        }

        private RecipientInfoEnumerator()
        {
        }

        internal RecipientInfoEnumerator(RecipientInfoCollection RecipientInfos)
        {
            this.m_recipientInfos = RecipientInfos;
            this.m_current = -1;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.MoveNext"/> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// This method returns a bool that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object, the method returns true. If the enumeration moved past the last item in the enumeration, it returns false.
        /// </returns>
        public bool MoveNext()
        {
            if (this.m_current == this.m_recipientInfos.Count - 1)
                return false;
            ++this.m_current;
            return true;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Reset"/> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </summary>
        public void Reset()
        {
            this.m_current = -1;
        }
    }
}
