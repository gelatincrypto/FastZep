// Type: System.Security.Cryptography.SafeCryptProvHandle
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
    internal sealed class SafeCryptProvHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeCryptProvHandle InvalidHandle
        {
            get
            {
                return new SafeCryptProvHandle(IntPtr.Zero);
            }
        }

        private SafeCryptProvHandle()
            : base(true)
        {
        }

        internal SafeCryptProvHandle(IntPtr handle)
            : base(true)
        {
            this.SetHandle(handle);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [DllImport("advapi32.dll", SetLastError = true)]
        private extern static bool CryptReleaseContext(IntPtr hCryptProv, uint dwFlags);

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return SafeCryptProvHandle.CryptReleaseContext(this.handle, 0U);
        }
    }
}
