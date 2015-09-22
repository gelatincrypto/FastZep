// Type: System.Security.Cryptography.SafeCertChainHandle
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
    internal sealed class SafeCertChainHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeCertChainHandle InvalidHandle
        {
            get
            {
                return new SafeCertChainHandle(IntPtr.Zero);
            }
        }

        private SafeCertChainHandle()
            : base(true)
        {
        }

        internal SafeCertChainHandle(IntPtr handle)
            : base(true)
        {
            this.SetHandle(handle);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [DllImport("crypt32.dll", SetLastError = true)]
        private extern static void CertFreeCertificateChain(IntPtr handle);

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            SafeCertChainHandle.CertFreeCertificateChain(this.handle);
            return true;
        }
    }
}
