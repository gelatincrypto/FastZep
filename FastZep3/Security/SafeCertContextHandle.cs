// Type: System.Security.Cryptography.SafeCertContextHandle
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Security.Cryptography
{
    [SecurityCritical]
    internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeCertContextHandle InvalidHandle
        {
            get
            {
                return new SafeCertContextHandle(IntPtr.Zero);
            }
        }

        private SafeCertContextHandle()
            : base(true)
        {
        }

        internal SafeCertContextHandle(IntPtr handle)
            : base(true)
        {
            this.SetHandle(handle);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [DllImport("crypt32.dll", SetLastError = true)]
        private extern static bool CertFreeCertificateContext(IntPtr pCertContext);

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return SafeCertContextHandle.CertFreeCertificateContext(this.handle);
        }
    }
}
