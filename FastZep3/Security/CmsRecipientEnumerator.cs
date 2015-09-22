// Type: System.Security.Cryptography.Pkcs.CmsRecipientEnumerator
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System.Collections;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;
namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator"/> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator"/> implements the <see cref="T:System.Collections.IEnumerator"/> interface.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class CmsRecipientEnumerator : IEnumerator
    {
        private CmsRecipientCollection m_recipients;
        private int m_current;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Current"/> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
        public CmsRecipient Current
        {
            get
            {
                return this.m_recipients[this.m_current];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (object)this.m_recipients[this.m_current];
            }
        }

        private CmsRecipientEnumerator()
        {
        }

        internal CmsRecipientEnumerator(CmsRecipientCollection recipients)
        {
            this.m_recipients = recipients;
            this.m_current = -1;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.MoveNext"/> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// true if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object; false if the enumeration moved past the last item in the enumeration.
        /// </returns>
        public bool MoveNext()
        {
            if (this.m_current == this.m_recipients.Count - 1)
                return false;
            ++this.m_current;
            return true;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Reset"/> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        public void Reset()
        {
            this.m_current = -1;
        }
    }
}
