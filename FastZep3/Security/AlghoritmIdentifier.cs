// Type: System.Security.Cryptography.Pkcs.AlgorithmIdentifier
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> class defines the algorithm used for a cryptographic operation.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class AlgorithmIdentifier
    {
        private Oid m_oid;
        private int m_keyLength;
        private byte[] m_parameters;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Oid"/> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid"/>  object that specifies the object identifier for the algorithm.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Security.Cryptography.Oid"/> object that represents the algorithm.
        /// </returns>
        public Oid Oid
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_oid;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.m_oid = value;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.KeyLength"/> property sets or retrieves the key length, in bits. This property is not used for algorithms that use a fixed key length.
        /// </summary>
        /// 
        /// <returns>
        /// An int value that represents the key length, in bits.
        /// </returns>
        public int KeyLength
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_keyLength;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.m_keyLength = value;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Parameters"/> property sets or retrieves any parameters required by the algorithm.
        /// </summary>
        /// 
        /// <returns>
        /// An array of byte values that specifies any parameters required by the algorithm.
        /// </returns>
        public byte[] Parameters
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.m_parameters;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.m_parameters = value;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> class by using a set of default parameters.
        /// </summary>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        public AlgorithmIdentifier()
        {
            this.Reset(new Oid("1.2.840.113549.3.7"), 0, new byte[0]);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> class with the specified algorithm identifier.
        /// </summary>
        /// <param name="oid">An object identifier for the algorithm.</param><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        public AlgorithmIdentifier(Oid oid)
        {
            this.Reset(oid, 0, new byte[0]);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid,System.Int32)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier"/> class with the specified algorithm identifier and key length.
        /// </summary>
        /// <param name="oid">An object identifier for the algorithm.</param><param name="keyLength">The length, in bits, of the key.</param><exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
        public AlgorithmIdentifier(Oid oid, int keyLength)
        {
            this.Reset(oid, keyLength, new byte[0]);
        }

        internal AlgorithmIdentifier(string oidValue)
        {
            this.Reset(new Oid(oidValue), 0, new byte[0]);
        }

        [SecurityCritical]
        internal AlgorithmIdentifier(CAPI.CERT_PUBLIC_KEY_INFO keyInfo)
        {
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CERT_PUBLIC_KEY_INFO))));
            Marshal.StructureToPtr((object)keyInfo, localAllocHandle.DangerousGetHandle(), false);
            int keyLength = (int)CAPI.CAPISafe.CertGetPublicKeyLength(65537U, localAllocHandle.DangerousGetHandle());
            byte[] numArray = new byte[(IntPtr)keyInfo.Algorithm.Parameters.cbData];
            if (numArray.Length > 0)
                Marshal.Copy(keyInfo.Algorithm.Parameters.pbData, numArray, 0, numArray.Length);
            Marshal.DestroyStructure(localAllocHandle.DangerousGetHandle(), typeof(CAPI.CERT_PUBLIC_KEY_INFO));
            localAllocHandle.Dispose();
            this.Reset(new Oid(keyInfo.Algorithm.pszObjId), keyLength, numArray);
        }

        [SecurityCritical]
        internal AlgorithmIdentifier(CAPI.CRYPT_ALGORITHM_IDENTIFIER algorithmIdentifier)
        {
            int keyLength = 0;
            uint cbDecodedValue = 0U;
            SafeLocalAllocHandle decodedValue = SafeLocalAllocHandle.InvalidHandle;
            byte[] numArray = new byte[0];
            uint num = X509Utils.OidToAlgId(algorithmIdentifier.pszObjId);
            switch (num)
            {
                case 26114U:
                    if (algorithmIdentifier.Parameters.cbData > 0U)
                    {
                        if (!CAPI.DecodeObject(new IntPtr(41L), algorithmIdentifier.Parameters.pbData, algorithmIdentifier.Parameters.cbData, out decodedValue, out cbDecodedValue))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        CAPI.CRYPT_RC2_CBC_PARAMETERS rc2CbcParameters = (CAPI.CRYPT_RC2_CBC_PARAMETERS)Marshal.PtrToStructure(decodedValue.DangerousGetHandle(), typeof(CAPI.CRYPT_RC2_CBC_PARAMETERS));
                        switch (rc2CbcParameters.dwVersion)
                        {
                            case 52U:
                                keyLength = 56;
                                break;
                            case 58U:
                                keyLength = 128;
                                break;
                            case 160U:
                                keyLength = 40;
                                break;
                        }
                        if (rc2CbcParameters.fIV)
                        {
                            numArray = (byte[])rc2CbcParameters.rgbIV.Clone();
                            break;
                        }
                        else
                            break;
                    }
                    else
                        break;
                case 26625U:
                case 26113U:
                case 26115U:
                    if (algorithmIdentifier.Parameters.cbData > 0U)
                    {
                        if (!CAPI.DecodeObject(new IntPtr(25L), algorithmIdentifier.Parameters.pbData, algorithmIdentifier.Parameters.cbData, out decodedValue, out cbDecodedValue))
                            throw new CryptographicException(Marshal.GetLastWin32Error());
                        if (cbDecodedValue > 0U)
                        {
                            if ((int)num == 26625)
                            {
                                CAPI.CRYPTOAPI_BLOB cryptoapiBlob = (CAPI.CRYPTOAPI_BLOB)Marshal.PtrToStructure(decodedValue.DangerousGetHandle(), typeof(CAPI.CRYPTOAPI_BLOB));
                                if (cryptoapiBlob.cbData > 0U)
                                {
                                    numArray = new byte[(IntPtr)cryptoapiBlob.cbData];
                                    Marshal.Copy(cryptoapiBlob.pbData, numArray, 0, numArray.Length);
                                }
                            }
                            else
                            {
                                numArray = new byte[(IntPtr)cbDecodedValue];
                                Marshal.Copy(decodedValue.DangerousGetHandle(), numArray, 0, numArray.Length);
                            }
                        }
                    }
                    keyLength = (int)num != 26625 ? ((int)num != 26113 ? 192 : 64) : 128 - numArray.Length * 8;
                    break;
                default:
                    if (algorithmIdentifier.Parameters.cbData > 0U)
                    {
                        numArray = new byte[(IntPtr)algorithmIdentifier.Parameters.cbData];
                        Marshal.Copy(algorithmIdentifier.Parameters.pbData, numArray, 0, numArray.Length);
                        break;
                    }
                    else
                        break;
            }
            this.Reset(new Oid(algorithmIdentifier.pszObjId), keyLength, numArray);
            decodedValue.Dispose();
        }

        private void Reset(Oid oid, int keyLength, byte[] parameters)
        {
            this.m_oid = oid;
            this.m_keyLength = keyLength;
            this.m_parameters = parameters;
        }
    }
}
