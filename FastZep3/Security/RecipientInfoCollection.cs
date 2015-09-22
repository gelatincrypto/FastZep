// Type: System.Security.Cryptography.Pkcs.RecipientInfoCollection
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> objects. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> implements the <see cref="T:System.Collections.ICollection"/> interface.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class RecipientInfoCollection : ICollection, IEnumerable
    {
        [SecurityCritical]
        private SafeCryptMsgHandle m_safeCryptMsgHandle;
        private ArrayList m_recipientInfos;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Item(System.Int32)"/> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object at the specified index in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> object at the specified index.
        /// </returns>
        /// <param name="index">An int value that represents the index in the collection. The index is zero based.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
        public RecipientInfo this[int index]
        {
            get
            {
                if (index < 0 || index >= this.m_recipientInfos.Count)
                    throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
                else
                    return (RecipientInfo)this.m_recipientInfos[index];
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Count"/> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// An int value that represents the number of items in the collection.
        /// </returns>
        public int Count
        {
            get
            {
                return this.m_recipientInfos.Count;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.IsSynchronized"/> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means the collection is not thread safe.
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
        /// The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.SyncRoot"/> property retrieves an <see cref="T:System.Object"/> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Object"/>  object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </returns>
        public object SyncRoot
        {
            get
            {
                return (object)this;
            }
        }

        [SecuritySafeCritical]
        internal RecipientInfoCollection()
        {
            this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
            this.m_recipientInfos = new ArrayList();
        }

        [SecuritySafeCritical]
        internal RecipientInfoCollection(RecipientInfo recipientInfo)
        {
            this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
            this.m_recipientInfos = new ArrayList(1);
            this.m_recipientInfos.Add((object)recipientInfo);
        }

        [SecurityCritical]
        internal RecipientInfoCollection(SafeCryptMsgHandle safeCryptMsgHandle)
        {
            bool flag = PkcsUtils.CmsSupported();
            uint num1 = 0U;
            uint num2 = (uint)Marshal.SizeOf(typeof(uint));
            if (flag)
            {
                if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 33U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                    throw new CryptographicException(Marshal.GetLastWin32Error());
            }
            else if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 17U, 0U, new IntPtr((void*)&num1), new IntPtr((void*)&num2)))
                throw new CryptographicException(Marshal.GetLastWin32Error());
            this.m_recipientInfos = new ArrayList();
            for (uint index = 0U; index < num1; ++index)
            {
                if (flag)
                {
                    SafeLocalAllocHandle pvData;
                    uint cbData;
                    PkcsUtils.GetParam(safeCryptMsgHandle, 36U, index, out pvData, out cbData);
                    CAPI.CMSG_CMS_RECIPIENT_INFO cmsRecipientInfo = (CAPI.CMSG_CMS_RECIPIENT_INFO)Marshal.PtrToStructure(pvData.DangerousGetHandle(), typeof(CAPI.CMSG_CMS_RECIPIENT_INFO));
                    switch (cmsRecipientInfo.dwRecipientChoice)
                    {
                        case 1U:
                            CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO keyTrans = (CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)Marshal.PtrToStructure(cmsRecipientInfo.pRecipientInfo, typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO));
                            this.m_recipientInfos.Add((object)new KeyTransRecipientInfo(pvData, keyTrans, index));
                            continue;
                        case 2U:
                            CAPI.CMSG_KEY_AGREE_RECIPIENT_INFO agreeRecipientInfo = (CAPI.CMSG_KEY_AGREE_RECIPIENT_INFO)Marshal.PtrToStructure(cmsRecipientInfo.pRecipientInfo, typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_INFO));
                            switch (agreeRecipientInfo.dwOriginatorChoice)
                            {
                                case 1U:
                                    CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO certIdRecipient = (CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)Marshal.PtrToStructure(cmsRecipientInfo.pRecipientInfo, typeof(CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO));
                                    for (uint subIndex = 0U; subIndex < certIdRecipient.cRecipientEncryptedKeys; ++subIndex)
                                        this.m_recipientInfos.Add((object)new KeyAgreeRecipientInfo(pvData, certIdRecipient, index, subIndex));
                                    continue;
                                case 2U:
                                    CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO publicKeyRecipient = (CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)Marshal.PtrToStructure(cmsRecipientInfo.pRecipientInfo, typeof(CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO));
                                    for (uint subIndex = 0U; subIndex < publicKeyRecipient.cRecipientEncryptedKeys; ++subIndex)
                                        this.m_recipientInfos.Add((object)new KeyAgreeRecipientInfo(pvData, publicKeyRecipient, index, subIndex));
                                    continue;
                                default:
                                    throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Originator_Identifier_Choice"), agreeRecipientInfo.dwOriginatorChoice.ToString((IFormatProvider)CultureInfo.CurrentCulture));
                            }
                        default:
                            throw new CryptographicException(-2147483647);
                    }
                }
                else
                {
                    SafeLocalAllocHandle pvData;
                    uint cbData;
                    PkcsUtils.GetParam(safeCryptMsgHandle, 19U, index, out pvData, out cbData);
                    CAPI.CERT_INFO certInfo = (CAPI.CERT_INFO)Marshal.PtrToStructure(pvData.DangerousGetHandle(), typeof(CAPI.CERT_INFO));
                    this.m_recipientInfos.Add((object)new KeyTransRecipientInfo(pvData, certInfo, index));
                }
            }
            this.m_safeCryptMsgHandle = safeCryptMsgHandle;
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.GetEnumerator"/> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator"/> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator"/> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection.
        /// </returns>
        public RecipientInfoEnumerator GetEnumerator()
        {
            return new RecipientInfoEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new RecipientInfoEnumerator(this);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Array,System.Int32)"/> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection to an array.
        /// </summary>
        /// <param name="array">An <see cref="T:System.Array"/> object to which  the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection is to be copied.</param><param name="index">The zero-based index in <paramref name="array"/> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection is copied.</param><exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
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
        /// The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.RecipientInfo[],System.Int32)"/> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection to a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> array.
        /// </summary>
        /// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo"/> objects where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection is to be copied.</param><param name="index">The zero-based index in <paramref name="array"/> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection"/> collection is copied.</param><exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception><exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
        public void CopyTo(RecipientInfo[] array, int index)
        {
            this.CopyTo((Array)array, index);
        }
    }
}
