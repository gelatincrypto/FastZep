﻿// Type: System.Security.Cryptography.Pkcs.CmsRecipientCollection
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Collections;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Cryptography.Pkcs;

namespace FastZep3
{
    /// <summary>
    /// The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> class represents a set of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> objects. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> implements the <see cref="T:System.Collections.ICollection"/> interface.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class CmsRecipientCollection : ICollection, IEnumerable
    {
        private ArrayList m_recipients;

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Item(System.Int32)"/> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object at the specified index in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object at the specified index.
        /// </returns>
        /// <param name="index">An <see cref="T:System.Int32"/> value that represents the index in the collection. The index is zero based.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
        public CmsRecipient this[int index]
        {
            get
            {
                if (index < 0 || index >= this.m_recipients.Count)
                    throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
                else
                    return (CmsRecipient)this.m_recipients[index];
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Count"/> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Int32"/> value that represents the number of items in the collection.
        /// </returns>
        public int Count
        {
            get
            {
                return this.m_recipients.Count;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.IsSynchronized"/> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means that the collection is not thread safe.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Boolean"/> value of false, which means that the collection is not thread safe.
        /// </returns>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.SyncRoot"/> property retrieves an <see cref="T:System.Object"/> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Object"/> object that is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </returns>
        public object SyncRoot
        {
            get
            {
                return (object)this;
            }
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> class.
        /// </summary>
        public CmsRecipientCollection()
        {
            this.m_recipients = new ArrayList();
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.CmsRecipient)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> class and adds the specified recipient.
        /// </summary>
        /// <param name="recipient">An instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> class that represents the specified CMS/PKCS #7 recipient.</param>
        public CmsRecipientCollection(CmsRecipient recipient)
        {
            this.m_recipients = new ArrayList(1);
            this.m_recipients.Add((object)recipient);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)"/> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> class and adds recipients based on the specified subject identifier and set of certificates that identify the recipients.
        /// </summary>
        /// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType"/> enumeration that specifies the type of subject identifier.</param><param name="certificates">An <see cref="T:System.Security.Cryptography.X509certificates.X509Certificate2Collection"/> collection that contains the certificates that identify the recipients.</param>
        public CmsRecipientCollection(SubjectIdentifierType recipientIdentifierType, X509Certificate2Collection certificates)
        {
            this.m_recipients = new ArrayList(certificates.Count);
            for (int index = 0; index < certificates.Count; ++index)
                this.m_recipients.Add((object)new CmsRecipient(recipientIdentifierType, certificates[index]));
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Add(System.Security.Cryptography.Pkcs.CmsRecipient)"/> method adds a recipient to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// If the method succeeds, the method returns an <see cref="T:System.Int32"/> value that represents the zero-based position where the recipient is to be inserted.If the method fails, it throws an exception.
        /// </returns>
        /// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object that represents the recipient to add to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.</param><exception cref="T:System.ArgumentNullException"><paramref name="recipient"/> is null.</exception>
        public int Add(CmsRecipient recipient)
        {
            if (recipient == null)
                throw new ArgumentNullException("recipient");
            else
                return this.m_recipients.Add((object)recipient);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Remove(System.Security.Cryptography.Pkcs.CmsRecipient)"/> method removes a recipient from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        /// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> object that represents the recipient to remove from the collection.</param><exception cref="T:System.ArgumentNullException"><paramref name="recipient"/> is null.</exception>
        public void Remove(CmsRecipient recipient)
        {
            if (recipient == null)
                throw new ArgumentNullException("recipient");
            this.m_recipients.Remove((object)recipient);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.GetEnumerator"/> method returns a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator"/> object for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator"/> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection.
        /// </returns>
        public CmsRecipientEnumerator GetEnumerator()
        {
            return new CmsRecipientEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new CmsRecipientEnumerator(this);
        }

        /// <summary>
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Array,System.Int32)"/> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection to an array.
        /// </summary>
        /// <param name="array">An <see cref="T:System.Array"/> object to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection is to be copied.</param><param name="index">The zero-based index in <paramref name="array"/> where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection is copied.</param><exception cref="T:System.ArgumentException"><paramref name="array"/> is not large enough to hold the specified elements.-or-<paramref name="array"/> does not contain the proper number of dimensions.</exception><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of elements in <paramref name="array"/>.</exception>
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
        /// The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Security.Cryptography.Pkcs.CmsRecipient[],System.Int32)"/> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection to a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> array.
        /// </summary>
        /// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> objects where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection is to be copied.</param><param name="index">The zero-based index for the array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient"/> objects in <paramref name="array"/> to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection"/> collection is copied.</param><exception cref="T:System.ArgumentException"><paramref name="array"/> is not large enough to hold the specified elements.-or-<paramref name="array"/> does not contain the proper number of dimensions.</exception><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of elements in <paramref name="array"/>.</exception>
        public void CopyTo(CmsRecipient[] array, int index)
        {
            this.CopyTo((Array)array, index);
        }
    }
}
