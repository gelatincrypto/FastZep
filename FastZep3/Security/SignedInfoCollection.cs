// Type: System.Security.Cryptography.Pkcs.SignerInfoCollection
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> objects. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> implements the <see cref="T:System.Collections.ICollection"/> interface.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class SignerInfoCollection : ICollection, IEnumerable
    {
        private SignerInfo[] m_signerInfos;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Item(System.Int32)"/> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object at the specified index in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> object  at the specified index.
        /// </returns>
        /// <param name="index">An int value that represents the index in the collection. The index is zero based.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
        public SignerInfo this[int index]
        {
            get
            {
                if (index < 0 || index >= this.m_signerInfos.Length)
                    throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
                else
                    return this.m_signerInfos[index];
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Count"/> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// An int value that represents the number of items in the collection.
        /// </returns>
        public int Count
        {
            get
            {
                return this.m_signerInfos.Length;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.IsSynchronized"/> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means the collection is not thread safe.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Boolean"/> value of false, which means the collection is not thread safe.
        /// </returns>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.SyncRoot"/> property retrieves an <see cref="T:System.Object"/> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Object"/> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </returns>
        public object SyncRoot
        {
            get
            {
                return (object)this;
            }
        }

        internal SignerInfoCollection()
        {
            this.m_signerInfos = new SignerInfo[0];
        }

        [SecuritySafeCritical]
        internal SignerInfoCollection(SignedCms signedCms)
        {
            uint num1 = 0U;
            uint num2 = (uint)Marshal.SizeOf(typeof(uint));
            SafeCryptMsgHandle cryptMsgHandle = signedCms.GetCryptMsgHandle();
            if (!CAPI.CAPISafe.CryptMsgGetParam(cryptMsgHandle, 5U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            SignerInfo[] signerInfoArray = new SignerInfo[(IntPtr)num1];
            for (int index = 0; (long)index < (long)num1; ++index)
            {
                uint num3 = 0U;
                if (!CAPI.CAPISafe.CryptMsgGetParam(cryptMsgHandle, 6U, (uint)index, IntPtr.Zero, new IntPtr((void*)&num3)))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                SafeLocalAllocHandle localAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)num3));
                if (!CAPI.CAPISafe.CryptMsgGetParam(cryptMsgHandle, 6U, (uint)index, localAllocHandle, new IntPtr((void*)&num3)))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
                signerInfoArray[index] = new SignerInfo(signedCms, localAllocHandle);
            }
            this.m_signerInfos = signerInfoArray;
        }

        [SecuritySafeCritical]
        internal SignerInfoCollection(SignedCms signedCms, SignerInfo signerInfo)
        {
            SignerInfo[] signerInfoArray1 = new SignerInfo[0];
            int length = 0;
            int num = 0;
            foreach (CryptographicAttributeObject cryptographicAttributeObject in signerInfo.UnsignedAttributes)
            {
                if (cryptographicAttributeObject.Oid.Value == "1.2.840.113549.1.9.6")
                    length += cryptographicAttributeObject.Values.Count;
            }
            SignerInfo[] signerInfoArray2 = new SignerInfo[length];
            foreach (CryptographicAttributeObject cryptographicAttributeObject in signerInfo.UnsignedAttributes)
            {
                if (cryptographicAttributeObject.Oid.Value == "1.2.840.113549.1.9.6")
                {
                    for (int index = 0; index < cryptographicAttributeObject.Values.Count; ++index)
                    {
                        AsnEncodedData asnEncodedData = cryptographicAttributeObject.Values[index];
                        signerInfoArray2[num++] = new SignerInfo(signedCms, signerInfo, asnEncodedData.RawData);
                    }
                }
            }
            this.m_signerInfos = signerInfoArray2;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.GetEnumerator"/> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator"/> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator"/> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection.
        /// </returns>
        public SignerInfoEnumerator GetEnumerator()
        {
            return new SignerInfoEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new SignerInfoEnumerator(this);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Array,System.Int32)"/> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection to an array.
        /// </summary>
        /// <param name="array">An <see cref="T:System.Array"/> object to which the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection is to be copied.</param><param name="index">The zero-based index in <paramref name="array"/> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection is copied.</param><exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Rank != 1)
                throw new ArgumentException(SecurityResources.GetResourceString("Arg_RankMultiDimNotSupported"));
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
            if (index + this.Count > array.Length)
                throw new ArgumentException(SecurityResources.GetResourceString("Argument_InvalidOffLen"));
            for (int index1 = 0; index1 < this.Count; ++index1)
            {
                array.SetValue((object)this[index1], index);
                ++index;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.SignerInfo[],System.Int32)"/> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection to a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> array.
        /// </summary>
        /// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo"/> objects where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection is to be copied.</param><param name="index">The zero-based index in <paramref name="array"/> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection"/> collection is copied.</param><exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
        public void CopyTo(SignerInfo[] array, int index)
        {
            this.CopyTo((Array)array, index);
        }
    }
}
