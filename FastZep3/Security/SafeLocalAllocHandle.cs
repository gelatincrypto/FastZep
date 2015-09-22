// Type: System.Security.Cryptography.SafeLocalAllocHandle
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
    internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeLocalAllocHandle InvalidHandle
        {
            get
            {
                return new SafeLocalAllocHandle(IntPtr.Zero);
            }
        }

        private SafeLocalAllocHandle()
            : base(true)
        {
        }

        internal SafeLocalAllocHandle(IntPtr handle)
            : base(true)
        {
            this.SetHandle(handle);
        }

        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll", SetLastError = true)]
        private extern static IntPtr LocalFree(IntPtr handle);

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return System.Security.Cryptography.SafeLocalAllocHandle.LocalFree(this.handle) == IntPtr.Zero;
        }
    }
}
