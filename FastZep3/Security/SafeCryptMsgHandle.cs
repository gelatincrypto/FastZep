// Type: System.Security.Cryptography.SafeCryptMsgHandle
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
    internal sealed class SafeCryptMsgHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeCryptMsgHandle InvalidHandle
        {
            get
            {
                return new SafeCryptMsgHandle(IntPtr.Zero);
            }
        }

        private SafeCryptMsgHandle()
            : base(true)
        {
        }

        internal SafeCryptMsgHandle(IntPtr handle)
            : base(true)
        {
            this.SetHandle(handle);
        }

        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("crypt32.dll", SetLastError = true)]
        private extern static bool CryptMsgClose(IntPtr handle);

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return SafeCryptMsgHandle.CryptMsgClose(this.handle);
        }
    }
}
