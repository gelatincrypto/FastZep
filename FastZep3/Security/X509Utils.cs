// Type: System.Security.Cryptography.X509Certificates.X509Utils
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace FastZep3
{
    internal class X509Utils
    {
        private static readonly char[] hexValues = new char[16]
    {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      'A',
      'B',
      'C',
      'D',
      'E',
      'F'
    };

        static X509Utils()
        {
        }

        private X509Utils()
        {
        }

        internal static uint MapRevocationFlags(X509RevocationMode revocationMode, X509RevocationFlag revocationFlag)
        {
            uint num = 0U;
            if (revocationMode == X509RevocationMode.NoCheck)
                return num;
            if (revocationMode == X509RevocationMode.Offline)
                num |= (uint)int.MinValue;
            return revocationFlag != X509RevocationFlag.EndCertificateOnly ? (revocationFlag != X509RevocationFlag.EntireChain ? num | 1073741824U : num | 536870912U) : num | 268435456U;
        }

        internal static string EncodeHexString(byte[] sArray)
        {
            return X509Utils.EncodeHexString(sArray, 0U, (uint)sArray.Length);
        }

        internal static string EncodeHexString(byte[] sArray, uint start, uint end)
        {
            string str = (string)null;
            if (sArray != null)
            {
                char[] chArray1 = new char[(IntPtr)(uint)(((int)end - (int)start) * 2)];
                uint num1 = start;
                uint num2 = 0U;
                for (; num1 < end; ++num1)
                {
                    uint num3 = (uint)(((int)sArray[(IntPtr)num1] & 240) >> 4);
                    char[] chArray2 = chArray1;
                    int num4 = (int)num2;
                    int num5 = 1;
                    uint num6 = (uint)(num4 + num5);
                    IntPtr index1 = (IntPtr)(uint)num4;
                    int num7 = (int)X509Utils.hexValues[(IntPtr)num3];
                    chArray2[index1] = (char)num7;
                    uint num8 = (uint)sArray[(IntPtr)num1] & 15U;
                    char[] chArray3 = chArray1;
                    int num9 = (int)num6;
                    int num10 = 1;
                    num2 = (uint)(num9 + num10);
                    IntPtr index2 = (IntPtr)(uint)num9;
                    int num11 = (int)X509Utils.hexValues[(IntPtr)num8];
                    chArray3[index2] = (char)num11;
                }
                str = new string(chArray1);
            }
            return str;
        }

        internal static string EncodeHexStringFromInt(byte[] sArray)
        {
            return X509Utils.EncodeHexStringFromInt(sArray, 0U, (uint)sArray.Length);
        }

        internal static string EncodeHexStringFromInt(byte[] sArray, uint start, uint end)
        {
            string str = (string)null;
            if (sArray != null)
            {
                char[] chArray1 = new char[(IntPtr)(uint)(((int)end - (int)start) * 2)];
                uint num1 = end;
                uint num2 = 0U;
                while (num1-- > start)
                {
                    uint num3 = ((uint)sArray[(IntPtr)num1] & 240U) >> 4;
                    char[] chArray2 = chArray1;
                    int num4 = (int)num2;
                    int num5 = 1;
                    uint num6 = (uint)(num4 + num5);
                    IntPtr index1 = (IntPtr)(uint)num4;
                    int num7 = (int)X509Utils.hexValues[(IntPtr)num3];
                    chArray2[index1] = (char)num7;
                    uint num8 = (uint)sArray[(IntPtr)num1] & 15U;
                    char[] chArray3 = chArray1;
                    int num9 = (int)num6;
                    int num10 = 1;
                    num2 = (uint)(num9 + num10);
                    IntPtr index2 = (IntPtr)(uint)num9;
                    int num11 = (int)X509Utils.hexValues[(IntPtr)num8];
                    chArray3[index2] = (char)num11;
                }
                str = new string(chArray1);
            }
            return str;
        }

        internal static byte HexToByte(char val)
        {
            if ((int)val <= 57 && (int)val >= 48)
                return (byte)((uint)val - 48U);
            if ((int)val >= 97 && (int)val <= 102)
                return (byte)((int)val - 97 + 10);
            if ((int)val >= 65 && (int)val <= 70)
                return (byte)((int)val - 65 + 10);
            else
                return byte.MaxValue;
        }

        internal static byte[] DecodeHexString(string s)
        {
            string str = System.Security.Cryptography.Xml.Utils.DiscardWhiteSpaces(s);
            uint num = (uint)str.Length / 2U;
            byte[] numArray = new byte[(IntPtr)num];
            int index1 = 0;
            for (int index2 = 0; (long)index2 < (long)num; ++index2)
            {
                numArray[index2] = (byte)((uint)X509Utils.HexToByte(str[index1]) << 4 | (uint)X509Utils.HexToByte(str[index1 + 1]));
                index1 += 2;
            }
            return numArray;
        }

        [SecurityCritical]
        internal static unsafe bool MemEqual(byte* pbBuf1, uint cbBuf1, byte* pbBuf2, uint cbBuf2)
        {
            if ((int)cbBuf1 != (int)cbBuf2)
                return false;
            while (cbBuf1-- > 0U)
            {
                if ((int)*pbBuf1++ != (int)*pbBuf2++)
                    return false;
            }
            return true;
        }

        [SecurityCritical]
        internal static SafeLocalAllocHandle StringToAnsiPtr(string s)
        {
            byte[] numArray = new byte[s.Length + 1];
            Encoding.ASCII.GetBytes(s, 0, s.Length, numArray, 0);
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(numArray.Length));
            Marshal.Copy(numArray, 0, localAllocHandle.DangerousGetHandle(), numArray.Length);
            return localAllocHandle;
        }

        [SecurityCritical]
        internal static System.Security.Cryptography.SafeCertContextHandle GetCertContext(X509Certificate2 certificate)
        {
            System.Security.Cryptography.SafeCertContextHandle certContextHandle = CAPI.CertDuplicateCertificateContext(certificate.Handle);
            GC.KeepAlive((object)certificate);
            return certContextHandle;
        }

        [SecurityCritical]
        internal static bool GetPrivateKeyInfo(System.Security.Cryptography.SafeCertContextHandle safeCertContext, ref CspParameters parameters)
        {
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            uint pcbData = 0U;
            if (!CAPI.CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, invalidHandle, out pcbData))
            {
                if (Marshal.GetLastWin32Error() == -2146885628)
                    return false;
                else
                    throw new CryptographicException(Marshal.GetLastWin32Error());
            }
            else
            {
                SafeLocalAllocHandle pvData = CAPI.LocalAlloc(0U, new IntPtr((long)pcbData));
                if (!CAPI.CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, pvData, out pcbData))
                {
                    if (Marshal.GetLastWin32Error() == -2146885628)
                        return false;
                    else
                        throw new CryptographicException(Marshal.GetLastWin32Error());
                }
                else
                {
                    CAPI.CRYPT_KEY_PROV_INFO cryptKeyProvInfo = (CAPI.CRYPT_KEY_PROV_INFO)Marshal.PtrToStructure(pvData.DangerousGetHandle(), typeof(CAPI.CRYPT_KEY_PROV_INFO));
                    parameters.ProviderName = cryptKeyProvInfo.pwszProvName;
                    parameters.KeyContainerName = cryptKeyProvInfo.pwszContainerName;
                    parameters.ProviderType = (int)cryptKeyProvInfo.dwProvType;
                    parameters.KeyNumber = (int)cryptKeyProvInfo.dwKeySpec;
                    parameters.Flags = ((int)cryptKeyProvInfo.dwFlags & 32) == 32 ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
                    pvData.Dispose();
                    return true;
                }
            }
        }

        [SecurityCritical]
        internal static SafeCertStoreHandle ExportToMemoryStore(X509Certificate2Collection collection)
        {
            new StorePermission(StorePermissionFlags.CreateStore | StorePermissionFlags.DeleteStore | StorePermissionFlags.EnumerateStores | StorePermissionFlags.OpenStore | StorePermissionFlags.AddToStore | StorePermissionFlags.RemoveFromStore | StorePermissionFlags.EnumerateCertificates).Assert();
            SafeCertStoreHandle invalidHandle = SafeCertStoreHandle.InvalidHandle;
            SafeCertStoreHandle hCertStore = CAPI.CertOpenStore(new IntPtr(2L), 65537U, IntPtr.Zero, 8704U, (string)null);
            if (hCertStore == null || hCertStore.IsInvalid)
                throw new CryptographicException(Marshal.GetLastWin32Error());
            foreach (X509Certificate2 certificate in collection)
            {
                if (!CAPI.CertAddCertificateLinkToStore(hCertStore, X509Utils.GetCertContext(certificate), 4U, System.Security.Cryptography.SafeCertContextHandle.InvalidHandle))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
            }
            return hCertStore;
        }

        [SecuritySafeCritical]
        internal static uint OidToAlgId(string value)
        {
            return CAPI.CryptFindOIDInfo(1U, X509Utils.StringToAnsiPtr(value), 0U).Algid;
        }

        internal static bool IsSelfSigned(X509Chain chain)
        {
            X509ChainElementCollection chainElements = chain.ChainElements;
            if (chainElements.Count != 1)
                return false;
            X509Certificate2 certificate = chainElements[0].Certificate;
            return string.Compare(certificate.SubjectName.Name, certificate.IssuerName.Name, StringComparison.OrdinalIgnoreCase) == 0;
        }

        [SecurityCritical]
        internal static SafeLocalAllocHandle CopyOidsToUnmanagedMemory(OidCollection oids)
        {
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            if (oids == null || oids.Count == 0)
                return invalidHandle;
            int num1 = oids.Count * Marshal.SizeOf(typeof(IntPtr));
            int num2 = 0;
            foreach (Oid oid in oids)
                num2 += oid.Value.Length + 1;
            SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)(uint)(num1 + num2)));
            IntPtr num3 = new IntPtr((long)localAllocHandle.DangerousGetHandle() + (long)num1);
            for (int index = 0; index < oids.Count; ++index)
            {
                Marshal.WriteIntPtr(new IntPtr((long)localAllocHandle.DangerousGetHandle() + (long)(index * Marshal.SizeOf(typeof(IntPtr)))), num3);
                byte[] bytes = Encoding.ASCII.GetBytes(oids[index].Value);
                Marshal.Copy(bytes, 0, num3, bytes.Length);
                num3 = new IntPtr((long)num3 + (long)oids[index].Value.Length + 1L);
            }
            return localAllocHandle;
        }

        [SecurityCritical]
        internal static X509Certificate2Collection GetCertificates(SafeCertStoreHandle safeCertStoreHandle)
        {
            X509Certificate2Collection certificate2Collection = new X509Certificate2Collection();
            for (IntPtr index = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, IntPtr.Zero); index != IntPtr.Zero; index = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, index))
            {
                X509Certificate2 certificate = new X509Certificate2(index);
                certificate2Collection.Add(certificate);
            }
            return certificate2Collection;
        }

        [SecurityCritical]
        internal static unsafe int BuildChain(IntPtr hChainEngine, System.Security.Cryptography.SafeCertContextHandle pCertContext, X509Certificate2Collection extraStore, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, ref SafeCertChainHandle ppChainContext)
        {
            if (pCertContext == null || pCertContext.IsInvalid)
                throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_InvalidContextHandle"), "pCertContext");
            SafeCertStoreHandle hAdditionalStore = SafeCertStoreHandle.InvalidHandle;
            if (extraStore != null && extraStore.Count > 0)
                hAdditionalStore = X509Utils.ExportToMemoryStore(extraStore);
            CAPI.CERT_CHAIN_PARA pChainPara = new CAPI.CERT_CHAIN_PARA();
            pChainPara.cbSize = (uint)Marshal.SizeOf((object)pChainPara);
            SafeLocalAllocHandle localAllocHandle1 = SafeLocalAllocHandle.InvalidHandle;
            if (applicationPolicy != null && applicationPolicy.Count > 0)
            {
                pChainPara.RequestedUsage.dwType = 0U;
                pChainPara.RequestedUsage.Usage.cUsageIdentifier = (uint)applicationPolicy.Count;
                localAllocHandle1 = X509Utils.CopyOidsToUnmanagedMemory(applicationPolicy);
                pChainPara.RequestedUsage.Usage.rgpszUsageIdentifier = localAllocHandle1.DangerousGetHandle();
            }
            SafeLocalAllocHandle localAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
            if (certificatePolicy != null && certificatePolicy.Count > 0)
            {
                pChainPara.RequestedIssuancePolicy.dwType = 0U;
                pChainPara.RequestedIssuancePolicy.Usage.cUsageIdentifier = (uint)certificatePolicy.Count;
                localAllocHandle2 = X509Utils.CopyOidsToUnmanagedMemory(certificatePolicy);
                pChainPara.RequestedIssuancePolicy.Usage.rgpszUsageIdentifier = localAllocHandle2.DangerousGetHandle();
            }
            pChainPara.dwUrlRetrievalTimeout = (uint)timeout.Milliseconds;
            System.Runtime.InteropServices.ComTypes.FILETIME pTime = new System.Runtime.InteropServices.ComTypes.FILETIME();
            *(long*)&pTime = verificationTime.ToFileTime();
            uint dwFlags = X509Utils.MapRevocationFlags(revocationMode, revocationFlag);
            if (!CAPI.CAPISafe.CertGetCertificateChain(hChainEngine, pCertContext, ref pTime, hAdditionalStore, ref pChainPara, dwFlags, IntPtr.Zero, out ppChainContext))
                return Marshal.GetHRForLastWin32Error();
            localAllocHandle1.Dispose();
            localAllocHandle2.Dispose();
            return 0;
        }

        [SecurityCritical]
        internal static unsafe int VerifyCertificate(System.Security.Cryptography.SafeCertContextHandle pCertContext, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, X509Certificate2Collection extraStore, IntPtr pszPolicy, IntPtr pdwErrorStatus)
        {
            if (pCertContext == null || pCertContext.IsInvalid)
                throw new ArgumentException("pCertContext");
            CAPI.CERT_CHAIN_POLICY_PARA pPolicyPara = new CAPI.CERT_CHAIN_POLICY_PARA(Marshal.SizeOf(typeof(CAPI.CERT_CHAIN_POLICY_PARA)));
            CAPI.CERT_CHAIN_POLICY_STATUS pPolicyStatus = new CAPI.CERT_CHAIN_POLICY_STATUS(Marshal.SizeOf(typeof(CAPI.CERT_CHAIN_POLICY_STATUS)));
            SafeCertChainHandle invalidHandle = SafeCertChainHandle.InvalidHandle;
            int num = X509Utils.BuildChain(new IntPtr(0L), pCertContext, extraStore, applicationPolicy, certificatePolicy, revocationMode, revocationFlag, verificationTime, timeout, ref invalidHandle);
            if (num != 0)
                return num;
            if (!CAPI.CAPISafe.CertVerifyCertificateChainPolicy(pszPolicy, invalidHandle, ref pPolicyPara, out pPolicyStatus))
                return Marshal.GetHRForLastWin32Error();
            if (pdwErrorStatus != IntPtr.Zero)
                *(int*)(void*)pdwErrorStatus = (int)pPolicyStatus.dwError;
            return (int)pPolicyStatus.dwError != 0 ? 1 : 0;
        }
    }
}
