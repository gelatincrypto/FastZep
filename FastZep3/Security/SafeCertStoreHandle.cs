// Type: System.Security.Cryptography.SafeCertStoreHandle
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace FastZep3
{
    [SecurityCritical]
    internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeCertStoreHandle InvalidHandle
        {
            get
            {
                return new SafeCertStoreHandle(IntPtr.Zero);
            }
        }

        private SafeCertStoreHandle()
            : base(true)
        {
        }

        internal SafeCertStoreHandle(IntPtr handle)
            : base(true)
        {
            this.SetHandle(handle);
        }

        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("crypt32.dll", SetLastError = true)]
        private extern static bool CertCloseStore(IntPtr hCertStore, uint dwFlags);

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return SafeCertStoreHandle.CertCloseStore(this.handle, 0U);
        }
    }
}
