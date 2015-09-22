// Type: System.Security.Cryptography.CAPI
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Security.Cryptography;

namespace FastZep3
{
    internal static class CAPI
    {
        internal const string ADVAPI32 = "advapi32.dll";
        internal const string CRYPT32 = "crypt32.dll";
        internal const string CRYPTUI = "cryptui.dll";
        internal const string KERNEL32 = "kernel32.dll";
        internal const uint LMEM_FIXED = 0U;
        internal const uint LMEM_ZEROINIT = 64U;
        internal const uint LPTR = 64U;
        internal const int S_OK = 0;
        internal const int S_FALSE = 1;
        internal const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096U;
        internal const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512U;
        internal const uint VER_PLATFORM_WIN32s = 0U;
        internal const uint VER_PLATFORM_WIN32_WINDOWS = 1U;
        internal const uint VER_PLATFORM_WIN32_NT = 2U;
        internal const uint VER_PLATFORM_WINCE = 3U;
        internal const uint ASN_TAG_NULL = 5U;
        internal const uint ASN_TAG_OBJID = 6U;
        internal const uint CERT_QUERY_OBJECT_FILE = 1U;
        internal const uint CERT_QUERY_OBJECT_BLOB = 2U;
        internal const uint CERT_QUERY_CONTENT_CERT = 1U;
        internal const uint CERT_QUERY_CONTENT_CTL = 2U;
        internal const uint CERT_QUERY_CONTENT_CRL = 3U;
        internal const uint CERT_QUERY_CONTENT_SERIALIZED_STORE = 4U;
        internal const uint CERT_QUERY_CONTENT_SERIALIZED_CERT = 5U;
        internal const uint CERT_QUERY_CONTENT_SERIALIZED_CTL = 6U;
        internal const uint CERT_QUERY_CONTENT_SERIALIZED_CRL = 7U;
        internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED = 8U;
        internal const uint CERT_QUERY_CONTENT_PKCS7_UNSIGNED = 9U;
        internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED_EMBED = 10U;
        internal const uint CERT_QUERY_CONTENT_PKCS10 = 11U;
        internal const uint CERT_QUERY_CONTENT_PFX = 12U;
        internal const uint CERT_QUERY_CONTENT_CERT_PAIR = 13U;
        internal const uint CERT_QUERY_CONTENT_FLAG_CERT = 2U;
        internal const uint CERT_QUERY_CONTENT_FLAG_CTL = 4U;
        internal const uint CERT_QUERY_CONTENT_FLAG_CRL = 8U;
        internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_STORE = 16U;
        internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CERT = 32U;
        internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CTL = 64U;
        internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CRL = 128U;
        internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED = 256U;
        internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_UNSIGNED = 512U;
        internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED_EMBED = 1024U;
        internal const uint CERT_QUERY_CONTENT_FLAG_PKCS10 = 2048U;
        internal const uint CERT_QUERY_CONTENT_FLAG_PFX = 4096U;
        internal const uint CERT_QUERY_CONTENT_FLAG_CERT_PAIR = 8192U;
        internal const uint CERT_QUERY_CONTENT_FLAG_ALL = 16382U;
        internal const uint CERT_QUERY_FORMAT_BINARY = 1U;
        internal const uint CERT_QUERY_FORMAT_BASE64_ENCODED = 2U;
        internal const uint CERT_QUERY_FORMAT_ASN_ASCII_HEX_ENCODED = 3U;
        internal const uint CERT_QUERY_FORMAT_FLAG_BINARY = 2U;
        internal const uint CERT_QUERY_FORMAT_FLAG_BASE64_ENCODED = 4U;
        internal const uint CERT_QUERY_FORMAT_FLAG_ASN_ASCII_HEX_ENCODED = 8U;
        internal const uint CERT_QUERY_FORMAT_FLAG_ALL = 14U;
        internal const uint CRYPTPROTECT_UI_FORBIDDEN = 1U;
        internal const uint CRYPTPROTECT_LOCAL_MACHINE = 4U;
        internal const uint CRYPTPROTECT_CRED_SYNC = 8U;
        internal const uint CRYPTPROTECT_AUDIT = 16U;
        internal const uint CRYPTPROTECT_NO_RECOVERY = 32U;
        internal const uint CRYPTPROTECT_VERIFY_PROTECTION = 64U;
        internal const uint CRYPTPROTECTMEMORY_BLOCK_SIZE = 16U;
        internal const uint CRYPTPROTECTMEMORY_SAME_PROCESS = 0U;
        internal const uint CRYPTPROTECTMEMORY_CROSS_PROCESS = 1U;
        internal const uint CRYPTPROTECTMEMORY_SAME_LOGON = 2U;
        internal const uint CRYPT_OID_INFO_OID_KEY = 1U;
        internal const uint CRYPT_OID_INFO_NAME_KEY = 2U;
        internal const uint CRYPT_OID_INFO_ALGID_KEY = 3U;
        internal const uint CRYPT_OID_INFO_SIGN_KEY = 4U;
        internal const uint CRYPT_HASH_ALG_OID_GROUP_ID = 1U;
        internal const uint CRYPT_ENCRYPT_ALG_OID_GROUP_ID = 2U;
        internal const uint CRYPT_PUBKEY_ALG_OID_GROUP_ID = 3U;
        internal const uint CRYPT_SIGN_ALG_OID_GROUP_ID = 4U;
        internal const uint CRYPT_RDN_ATTR_OID_GROUP_ID = 5U;
        internal const uint CRYPT_EXT_OR_ATTR_OID_GROUP_ID = 6U;
        internal const uint CRYPT_ENHKEY_USAGE_OID_GROUP_ID = 7U;
        internal const uint CRYPT_POLICY_OID_GROUP_ID = 8U;
        internal const uint CRYPT_TEMPLATE_OID_GROUP_ID = 9U;
        internal const uint CRYPT_LAST_OID_GROUP_ID = 9U;
        internal const uint CRYPT_FIRST_ALG_OID_GROUP_ID = 1U;
        internal const uint CRYPT_LAST_ALG_OID_GROUP_ID = 4U;
        internal const uint CRYPT_ASN_ENCODING = 1U;
        internal const uint CRYPT_NDR_ENCODING = 2U;
        internal const uint X509_ASN_ENCODING = 1U;
        internal const uint X509_NDR_ENCODING = 2U;
        internal const uint PKCS_7_ASN_ENCODING = 65536U;
        internal const uint PKCS_7_NDR_ENCODING = 131072U;
        internal const uint PKCS_7_OR_X509_ASN_ENCODING = 65537U;
        internal const uint CERT_STORE_PROV_MSG = 1U;
        internal const uint CERT_STORE_PROV_MEMORY = 2U;
        internal const uint CERT_STORE_PROV_FILE = 3U;
        internal const uint CERT_STORE_PROV_REG = 4U;
        internal const uint CERT_STORE_PROV_PKCS7 = 5U;
        internal const uint CERT_STORE_PROV_SERIALIZED = 6U;
        internal const uint CERT_STORE_PROV_FILENAME_A = 7U;
        internal const uint CERT_STORE_PROV_FILENAME_W = 8U;
        internal const uint CERT_STORE_PROV_FILENAME = 8U;
        internal const uint CERT_STORE_PROV_SYSTEM_A = 9U;
        internal const uint CERT_STORE_PROV_SYSTEM_W = 10U;
        internal const uint CERT_STORE_PROV_SYSTEM = 10U;
        internal const uint CERT_STORE_PROV_COLLECTION = 11U;
        internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY_A = 12U;
        internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY_W = 13U;
        internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY = 13U;
        internal const uint CERT_STORE_PROV_PHYSICAL_W = 14U;
        internal const uint CERT_STORE_PROV_PHYSICAL = 14U;
        internal const uint CERT_STORE_PROV_SMART_CARD_W = 15U;
        internal const uint CERT_STORE_PROV_SMART_CARD = 15U;
        internal const uint CERT_STORE_PROV_LDAP_W = 16U;
        internal const uint CERT_STORE_PROV_LDAP = 16U;
        internal const uint CERT_STORE_NO_CRYPT_RELEASE_FLAG = 1U;
        internal const uint CERT_STORE_SET_LOCALIZED_NAME_FLAG = 2U;
        internal const uint CERT_STORE_DEFER_CLOSE_UNTIL_LAST_FREE_FLAG = 4U;
        internal const uint CERT_STORE_DELETE_FLAG = 16U;
        internal const uint CERT_STORE_SHARE_STORE_FLAG = 64U;
        internal const uint CERT_STORE_SHARE_CONTEXT_FLAG = 128U;
        internal const uint CERT_STORE_MANIFOLD_FLAG = 256U;
        internal const uint CERT_STORE_ENUM_ARCHIVED_FLAG = 512U;
        internal const uint CERT_STORE_UPDATE_KEYID_FLAG = 1024U;
        internal const uint CERT_STORE_BACKUP_RESTORE_FLAG = 2048U;
        internal const uint CERT_STORE_READONLY_FLAG = 32768U;
        internal const uint CERT_STORE_OPEN_EXISTING_FLAG = 16384U;
        internal const uint CERT_STORE_CREATE_NEW_FLAG = 8192U;
        internal const uint CERT_STORE_MAXIMUM_ALLOWED_FLAG = 4096U;
        internal const uint CERT_SYSTEM_STORE_UNPROTECTED_FLAG = 1073741824U;
        internal const uint CERT_SYSTEM_STORE_LOCATION_MASK = 16711680U;
        internal const uint CERT_SYSTEM_STORE_LOCATION_SHIFT = 16U;
        internal const uint CERT_SYSTEM_STORE_CURRENT_USER_ID = 1U;
        internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ID = 2U;
        internal const uint CERT_SYSTEM_STORE_CURRENT_SERVICE_ID = 4U;
        internal const uint CERT_SYSTEM_STORE_SERVICES_ID = 5U;
        internal const uint CERT_SYSTEM_STORE_USERS_ID = 6U;
        internal const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID = 7U;
        internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID = 8U;
        internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID = 9U;
        internal const uint CERT_SYSTEM_STORE_CURRENT_USER = 65536U;
        internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072U;
        internal const uint CERT_SYSTEM_STORE_CURRENT_SERVICE = 262144U;
        internal const uint CERT_SYSTEM_STORE_SERVICES = 327680U;
        internal const uint CERT_SYSTEM_STORE_USERS = 393216U;
        internal const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY = 458752U;
        internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY = 524288U;
        internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE = 589824U;
        internal const uint CERT_NAME_EMAIL_TYPE = 1U;
        internal const uint CERT_NAME_RDN_TYPE = 2U;
        internal const uint CERT_NAME_ATTR_TYPE = 3U;
        internal const uint CERT_NAME_SIMPLE_DISPLAY_TYPE = 4U;
        internal const uint CERT_NAME_FRIENDLY_DISPLAY_TYPE = 5U;
        internal const uint CERT_NAME_DNS_TYPE = 6U;
        internal const uint CERT_NAME_URL_TYPE = 7U;
        internal const uint CERT_NAME_UPN_TYPE = 8U;
        internal const uint CERT_SIMPLE_NAME_STR = 1U;
        internal const uint CERT_OID_NAME_STR = 2U;
        internal const uint CERT_X500_NAME_STR = 3U;
        internal const uint CERT_NAME_STR_SEMICOLON_FLAG = 1073741824U;
        internal const uint CERT_NAME_STR_NO_PLUS_FLAG = 536870912U;
        internal const uint CERT_NAME_STR_NO_QUOTING_FLAG = 268435456U;
        internal const uint CERT_NAME_STR_CRLF_FLAG = 134217728U;
        internal const uint CERT_NAME_STR_COMMA_FLAG = 67108864U;
        internal const uint CERT_NAME_STR_REVERSE_FLAG = 33554432U;
        internal const uint CERT_NAME_ISSUER_FLAG = 1U;
        internal const uint CERT_NAME_STR_DISABLE_IE4_UTF8_FLAG = 65536U;
        internal const uint CERT_NAME_STR_ENABLE_T61_UNICODE_FLAG = 131072U;
        internal const uint CERT_NAME_STR_ENABLE_UTF8_UNICODE_FLAG = 262144U;
        internal const uint CERT_NAME_STR_FORCE_UTF8_DIR_STR_FLAG = 524288U;
        internal const uint CERT_KEY_PROV_HANDLE_PROP_ID = 1U;
        internal const uint CERT_KEY_PROV_INFO_PROP_ID = 2U;
        internal const uint CERT_SHA1_HASH_PROP_ID = 3U;
        internal const uint CERT_MD5_HASH_PROP_ID = 4U;
        internal const uint CERT_HASH_PROP_ID = 3U;
        internal const uint CERT_KEY_CONTEXT_PROP_ID = 5U;
        internal const uint CERT_KEY_SPEC_PROP_ID = 6U;
        internal const uint CERT_IE30_RESERVED_PROP_ID = 7U;
        internal const uint CERT_PUBKEY_HASH_RESERVED_PROP_ID = 8U;
        internal const uint CERT_ENHKEY_USAGE_PROP_ID = 9U;
        internal const uint CERT_CTL_USAGE_PROP_ID = 9U;
        internal const uint CERT_NEXT_UPDATE_LOCATION_PROP_ID = 10U;
        internal const uint CERT_FRIENDLY_NAME_PROP_ID = 11U;
        internal const uint CERT_PVK_FILE_PROP_ID = 12U;
        internal const uint CERT_DESCRIPTION_PROP_ID = 13U;
        internal const uint CERT_ACCESS_STATE_PROP_ID = 14U;
        internal const uint CERT_SIGNATURE_HASH_PROP_ID = 15U;
        internal const uint CERT_SMART_CARD_DATA_PROP_ID = 16U;
        internal const uint CERT_EFS_PROP_ID = 17U;
        internal const uint CERT_FORTEZZA_DATA_PROP_ID = 18U;
        internal const uint CERT_ARCHIVED_PROP_ID = 19U;
        internal const uint CERT_KEY_IDENTIFIER_PROP_ID = 20U;
        internal const uint CERT_AUTO_ENROLL_PROP_ID = 21U;
        internal const uint CERT_PUBKEY_ALG_PARA_PROP_ID = 22U;
        internal const uint CERT_CROSS_CERT_DIST_POINTS_PROP_ID = 23U;
        internal const uint CERT_ISSUER_PUBLIC_KEY_MD5_HASH_PROP_ID = 24U;
        internal const uint CERT_SUBJECT_PUBLIC_KEY_MD5_HASH_PROP_ID = 25U;
        internal const uint CERT_ENROLLMENT_PROP_ID = 26U;
        internal const uint CERT_DATE_STAMP_PROP_ID = 27U;
        internal const uint CERT_ISSUER_SERIAL_NUMBER_MD5_HASH_PROP_ID = 28U;
        internal const uint CERT_SUBJECT_NAME_MD5_HASH_PROP_ID = 29U;
        internal const uint CERT_EXTENDED_ERROR_INFO_PROP_ID = 30U;
        internal const uint CERT_RENEWAL_PROP_ID = 64U;
        internal const uint CERT_ARCHIVED_KEY_HASH_PROP_ID = 65U;
        internal const uint CERT_FIRST_RESERVED_PROP_ID = 66U;
        internal const uint CERT_DELETE_KEYSET_PROP_ID = 101U;
        internal const uint CERT_INFO_VERSION_FLAG = 1U;
        internal const uint CERT_INFO_SERIAL_NUMBER_FLAG = 2U;
        internal const uint CERT_INFO_SIGNATURE_ALGORITHM_FLAG = 3U;
        internal const uint CERT_INFO_ISSUER_FLAG = 4U;
        internal const uint CERT_INFO_NOT_BEFORE_FLAG = 5U;
        internal const uint CERT_INFO_NOT_AFTER_FLAG = 6U;
        internal const uint CERT_INFO_SUBJECT_FLAG = 7U;
        internal const uint CERT_INFO_SUBJECT_PUBLIC_KEY_INFO_FLAG = 8U;
        internal const uint CERT_INFO_ISSUER_UNIQUE_ID_FLAG = 9U;
        internal const uint CERT_INFO_SUBJECT_UNIQUE_ID_FLAG = 10U;
        internal const uint CERT_INFO_EXTENSION_FLAG = 11U;
        internal const uint CERT_COMPARE_MASK = 65535U;
        internal const uint CERT_COMPARE_SHIFT = 16U;
        internal const uint CERT_COMPARE_ANY = 0U;
        internal const uint CERT_COMPARE_SHA1_HASH = 1U;
        internal const uint CERT_COMPARE_NAME = 2U;
        internal const uint CERT_COMPARE_ATTR = 3U;
        internal const uint CERT_COMPARE_MD5_HASH = 4U;
        internal const uint CERT_COMPARE_PROPERTY = 5U;
        internal const uint CERT_COMPARE_PUBLIC_KEY = 6U;
        internal const uint CERT_COMPARE_HASH = 1U;
        internal const uint CERT_COMPARE_NAME_STR_A = 7U;
        internal const uint CERT_COMPARE_NAME_STR_W = 8U;
        internal const uint CERT_COMPARE_KEY_SPEC = 9U;
        internal const uint CERT_COMPARE_ENHKEY_USAGE = 10U;
        internal const uint CERT_COMPARE_CTL_USAGE = 10U;
        internal const uint CERT_COMPARE_SUBJECT_CERT = 11U;
        internal const uint CERT_COMPARE_ISSUER_OF = 12U;
        internal const uint CERT_COMPARE_EXISTING = 13U;
        internal const uint CERT_COMPARE_SIGNATURE_HASH = 14U;
        internal const uint CERT_COMPARE_KEY_IDENTIFIER = 15U;
        internal const uint CERT_COMPARE_CERT_ID = 16U;
        internal const uint CERT_COMPARE_CROSS_CERT_DIST_POINTS = 17U;
        internal const uint CERT_COMPARE_PUBKEY_MD5_HASH = 18U;
        internal const uint CERT_FIND_ANY = 0U;
        internal const uint CERT_FIND_SHA1_HASH = 65536U;
        internal const uint CERT_FIND_MD5_HASH = 262144U;
        internal const uint CERT_FIND_SIGNATURE_HASH = 917504U;
        internal const uint CERT_FIND_KEY_IDENTIFIER = 983040U;
        internal const uint CERT_FIND_HASH = 65536U;
        internal const uint CERT_FIND_PROPERTY = 327680U;
        internal const uint CERT_FIND_PUBLIC_KEY = 393216U;
        internal const uint CERT_FIND_SUBJECT_NAME = 131079U;
        internal const uint CERT_FIND_SUBJECT_ATTR = 196615U;
        internal const uint CERT_FIND_ISSUER_NAME = 131076U;
        internal const uint CERT_FIND_ISSUER_ATTR = 196612U;
        internal const uint CERT_FIND_SUBJECT_STR_A = 458759U;
        internal const uint CERT_FIND_SUBJECT_STR_W = 524295U;
        internal const uint CERT_FIND_SUBJECT_STR = 524295U;
        internal const uint CERT_FIND_ISSUER_STR_A = 458756U;
        internal const uint CERT_FIND_ISSUER_STR_W = 524292U;
        internal const uint CERT_FIND_ISSUER_STR = 524292U;
        internal const uint CERT_FIND_KEY_SPEC = 589824U;
        internal const uint CERT_FIND_ENHKEY_USAGE = 655360U;
        internal const uint CERT_FIND_CTL_USAGE = 655360U;
        internal const uint CERT_FIND_SUBJECT_CERT = 720896U;
        internal const uint CERT_FIND_ISSUER_OF = 786432U;
        internal const uint CERT_FIND_EXISTING = 851968U;
        internal const uint CERT_FIND_CERT_ID = 1048576U;
        internal const uint CERT_FIND_CROSS_CERT_DIST_POINTS = 1114112U;
        internal const uint CERT_FIND_PUBKEY_MD5_HASH = 1179648U;
        internal const uint CERT_ENCIPHER_ONLY_KEY_USAGE = 1U;
        internal const uint CERT_CRL_SIGN_KEY_USAGE = 2U;
        internal const uint CERT_KEY_CERT_SIGN_KEY_USAGE = 4U;
        internal const uint CERT_KEY_AGREEMENT_KEY_USAGE = 8U;
        internal const uint CERT_DATA_ENCIPHERMENT_KEY_USAGE = 16U;
        internal const uint CERT_KEY_ENCIPHERMENT_KEY_USAGE = 32U;
        internal const uint CERT_NON_REPUDIATION_KEY_USAGE = 64U;
        internal const uint CERT_DIGITAL_SIGNATURE_KEY_USAGE = 128U;
        internal const uint CERT_DECIPHER_ONLY_KEY_USAGE = 32768U;
        internal const uint CERT_STORE_ADD_NEW = 1U;
        internal const uint CERT_STORE_ADD_USE_EXISTING = 2U;
        internal const uint CERT_STORE_ADD_REPLACE_EXISTING = 3U;
        internal const uint CERT_STORE_ADD_ALWAYS = 4U;
        internal const uint CERT_STORE_ADD_REPLACE_EXISTING_INHERIT_PROPERTIES = 5U;
        internal const uint CERT_STORE_ADD_NEWER = 6U;
        internal const uint CERT_STORE_ADD_NEWER_INHERIT_PROPERTIES = 7U;
        internal const uint CERT_STORE_SAVE_AS_STORE = 1U;
        internal const uint CERT_STORE_SAVE_AS_PKCS7 = 2U;
        internal const uint CERT_STORE_SAVE_TO_FILE = 1U;
        internal const uint CERT_STORE_SAVE_TO_MEMORY = 2U;
        internal const uint CERT_STORE_SAVE_TO_FILENAME_A = 3U;
        internal const uint CERT_STORE_SAVE_TO_FILENAME_W = 4U;
        internal const uint CERT_STORE_SAVE_TO_FILENAME = 4U;
        internal const uint CERT_CA_SUBJECT_FLAG = 128U;
        internal const uint CERT_END_ENTITY_SUBJECT_FLAG = 64U;
        internal const uint RSA_CSP_PUBLICKEYBLOB = 19U;
        internal const uint X509_MULTI_BYTE_UINT = 38U;
        internal const uint X509_DSS_PUBLICKEY = 38U;
        internal const uint X509_DSS_PARAMETERS = 39U;
        internal const uint X509_DSS_SIGNATURE = 40U;
        internal const uint X509_EXTENSIONS = 5U;
        internal const uint X509_NAME_VALUE = 6U;
        internal const uint X509_NAME = 7U;
        internal const uint X509_AUTHORITY_KEY_ID = 9U;
        internal const uint X509_KEY_USAGE_RESTRICTION = 11U;
        internal const uint X509_BASIC_CONSTRAINTS = 13U;
        internal const uint X509_KEY_USAGE = 14U;
        internal const uint X509_BASIC_CONSTRAINTS2 = 15U;
        internal const uint X509_CERT_POLICIES = 16U;
        internal const uint PKCS_UTC_TIME = 17U;
        internal const uint PKCS_ATTRIBUTE = 22U;
        internal const uint X509_UNICODE_NAME_VALUE = 24U;
        internal const uint X509_OCTET_STRING = 25U;
        internal const uint X509_BITS = 26U;
        internal const uint X509_ANY_STRING = 6U;
        internal const uint X509_UNICODE_ANY_STRING = 24U;
        internal const uint X509_ENHANCED_KEY_USAGE = 36U;
        internal const uint PKCS_RC2_CBC_PARAMETERS = 41U;
        internal const uint X509_CERTIFICATE_TEMPLATE = 64U;
        internal const uint PKCS7_SIGNER_INFO = 500U;
        internal const uint CMS_SIGNER_INFO = 501U;
        internal const string szOID_AUTHORITY_KEY_IDENTIFIER = "2.5.29.1";
        internal const string szOID_KEY_USAGE_RESTRICTION = "2.5.29.4";
        internal const string szOID_KEY_USAGE = "2.5.29.15";
        internal const string szOID_KEYID_RDN = "1.3.6.1.4.1.311.10.7.1";
        internal const string szOID_RDN_DUMMY_SIGNER = "1.3.6.1.4.1.311.21.9";
        internal const uint CERT_CHAIN_POLICY_BASE = 1U;
        internal const uint CERT_CHAIN_POLICY_AUTHENTICODE = 2U;
        internal const uint CERT_CHAIN_POLICY_AUTHENTICODE_TS = 3U;
        internal const uint CERT_CHAIN_POLICY_SSL = 4U;
        internal const uint CERT_CHAIN_POLICY_BASIC_CONSTRAINTS = 5U;
        internal const uint CERT_CHAIN_POLICY_NT_AUTH = 6U;
        internal const uint CERT_CHAIN_POLICY_MICROSOFT_ROOT = 7U;
        internal const uint USAGE_MATCH_TYPE_AND = 0U;
        internal const uint USAGE_MATCH_TYPE_OR = 1U;
        internal const uint CERT_CHAIN_REVOCATION_CHECK_END_CERT = 268435456U;
        internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN = 536870912U;
        internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 1073741824U;
        internal const uint CERT_CHAIN_REVOCATION_CHECK_CACHE_ONLY = 2147483648U;
        internal const uint CERT_CHAIN_REVOCATION_ACCUMULATIVE_TIMEOUT = 134217728U;
        internal const uint CERT_TRUST_NO_ERROR = 0U;
        internal const uint CERT_TRUST_IS_NOT_TIME_VALID = 1U;
        internal const uint CERT_TRUST_IS_NOT_TIME_NESTED = 2U;
        internal const uint CERT_TRUST_IS_REVOKED = 4U;
        internal const uint CERT_TRUST_IS_NOT_SIGNATURE_VALID = 8U;
        internal const uint CERT_TRUST_IS_NOT_VALID_FOR_USAGE = 16U;
        internal const uint CERT_TRUST_IS_UNTRUSTED_ROOT = 32U;
        internal const uint CERT_TRUST_REVOCATION_STATUS_UNKNOWN = 64U;
        internal const uint CERT_TRUST_IS_CYCLIC = 128U;
        internal const uint CERT_TRUST_INVALID_EXTENSION = 256U;
        internal const uint CERT_TRUST_INVALID_POLICY_CONSTRAINTS = 512U;
        internal const uint CERT_TRUST_INVALID_BASIC_CONSTRAINTS = 1024U;
        internal const uint CERT_TRUST_INVALID_NAME_CONSTRAINTS = 2048U;
        internal const uint CERT_TRUST_HAS_NOT_SUPPORTED_NAME_CONSTRAINT = 4096U;
        internal const uint CERT_TRUST_HAS_NOT_DEFINED_NAME_CONSTRAINT = 8192U;
        internal const uint CERT_TRUST_HAS_NOT_PERMITTED_NAME_CONSTRAINT = 16384U;
        internal const uint CERT_TRUST_HAS_EXCLUDED_NAME_CONSTRAINT = 32768U;
        internal const uint CERT_TRUST_IS_OFFLINE_REVOCATION = 16777216U;
        internal const uint CERT_TRUST_NO_ISSUANCE_CHAIN_POLICY = 33554432U;
        internal const uint CERT_TRUST_IS_PARTIAL_CHAIN = 65536U;
        internal const uint CERT_TRUST_CTL_IS_NOT_TIME_VALID = 131072U;
        internal const uint CERT_TRUST_CTL_IS_NOT_SIGNATURE_VALID = 262144U;
        internal const uint CERT_TRUST_CTL_IS_NOT_VALID_FOR_USAGE = 524288U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_VALID_FLAG = 1U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_NOT_TIME_VALID_FLAG = 2U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_NESTED_FLAG = 4U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_BASIC_CONSTRAINTS_FLAG = 8U;
        internal const uint CERT_CHAIN_POLICY_ALLOW_UNKNOWN_CA_FLAG = 16U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_WRONG_USAGE_FLAG = 32U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_NAME_FLAG = 64U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_POLICY_FLAG = 128U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_END_REV_UNKNOWN_FLAG = 256U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_SIGNER_REV_UNKNOWN_FLAG = 512U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_CA_REV_UNKNOWN_FLAG = 1024U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_ROOT_REV_UNKNOWN_FLAG = 2048U;
        internal const uint CERT_CHAIN_POLICY_IGNORE_ALL_REV_UNKNOWN_FLAGS = 3840U;
        internal const uint CERT_TRUST_HAS_EXACT_MATCH_ISSUER = 1U;
        internal const uint CERT_TRUST_HAS_KEY_MATCH_ISSUER = 2U;
        internal const uint CERT_TRUST_HAS_NAME_MATCH_ISSUER = 4U;
        internal const uint CERT_TRUST_IS_SELF_SIGNED = 8U;
        internal const uint CERT_TRUST_HAS_PREFERRED_ISSUER = 256U;
        internal const uint CERT_TRUST_HAS_ISSUANCE_CHAIN_POLICY = 512U;
        internal const uint CERT_TRUST_HAS_VALID_NAME_CONSTRAINTS = 1024U;
        internal const uint CERT_TRUST_IS_COMPLEX_CHAIN = 65536U;
        internal const string szOID_PKIX_NO_SIGNATURE = "1.3.6.1.5.5.7.6.2";
        internal const string szOID_PKIX_KP_SERVER_AUTH = "1.3.6.1.5.5.7.3.1";
        internal const string szOID_PKIX_KP_CLIENT_AUTH = "1.3.6.1.5.5.7.3.2";
        internal const string szOID_PKIX_KP_CODE_SIGNING = "1.3.6.1.5.5.7.3.3";
        internal const string szOID_PKIX_KP_EMAIL_PROTECTION = "1.3.6.1.5.5.7.3.4";
        internal const string SPC_INDIVIDUAL_SP_KEY_PURPOSE_OBJID = "1.3.6.1.4.1.311.2.1.21";
        internal const string SPC_COMMERCIAL_SP_KEY_PURPOSE_OBJID = "1.3.6.1.4.1.311.2.1.22";
        internal const uint HCCE_CURRENT_USER = 0U;
        internal const uint HCCE_LOCAL_MACHINE = 1U;
        internal const string szOID_PKCS_1 = "1.2.840.113549.1.1";
        internal const string szOID_PKCS_2 = "1.2.840.113549.1.2";
        internal const string szOID_PKCS_3 = "1.2.840.113549.1.3";
        internal const string szOID_PKCS_4 = "1.2.840.113549.1.4";
        internal const string szOID_PKCS_5 = "1.2.840.113549.1.5";
        internal const string szOID_PKCS_6 = "1.2.840.113549.1.6";
        internal const string szOID_PKCS_7 = "1.2.840.113549.1.7";
        internal const string szOID_PKCS_8 = "1.2.840.113549.1.8";
        internal const string szOID_PKCS_9 = "1.2.840.113549.1.9";
        internal const string szOID_PKCS_10 = "1.2.840.113549.1.10";
        internal const string szOID_PKCS_12 = "1.2.840.113549.1.12";
        internal const string szOID_RSA_data = "1.2.840.113549.1.7.1";
        internal const string szOID_RSA_signedData = "1.2.840.113549.1.7.2";
        internal const string szOID_RSA_envelopedData = "1.2.840.113549.1.7.3";
        internal const string szOID_RSA_signEnvData = "1.2.840.113549.1.7.4";
        internal const string szOID_RSA_digestedData = "1.2.840.113549.1.7.5";
        internal const string szOID_RSA_hashedData = "1.2.840.113549.1.7.5";
        internal const string szOID_RSA_encryptedData = "1.2.840.113549.1.7.6";
        internal const string szOID_RSA_emailAddr = "1.2.840.113549.1.9.1";
        internal const string szOID_RSA_unstructName = "1.2.840.113549.1.9.2";
        internal const string szOID_RSA_contentType = "1.2.840.113549.1.9.3";
        internal const string szOID_RSA_messageDigest = "1.2.840.113549.1.9.4";
        internal const string szOID_RSA_signingTime = "1.2.840.113549.1.9.5";
        internal const string szOID_RSA_counterSign = "1.2.840.113549.1.9.6";
        internal const string szOID_RSA_challengePwd = "1.2.840.113549.1.9.7";
        internal const string szOID_RSA_unstructAddr = "1.2.840.113549.1.9.8";
        internal const string szOID_RSA_extCertAttrs = "1.2.840.113549.1.9.9";
        internal const string szOID_RSA_SMIMECapabilities = "1.2.840.113549.1.9.15";
        internal const string szOID_CAPICOM = "1.3.6.1.4.1.311.88";
        internal const string szOID_CAPICOM_version = "1.3.6.1.4.1.311.88.1";
        internal const string szOID_CAPICOM_attribute = "1.3.6.1.4.1.311.88.2";
        internal const string szOID_CAPICOM_documentName = "1.3.6.1.4.1.311.88.2.1";
        internal const string szOID_CAPICOM_documentDescription = "1.3.6.1.4.1.311.88.2.2";
        internal const string szOID_CAPICOM_encryptedData = "1.3.6.1.4.1.311.88.3";
        internal const string szOID_CAPICOM_encryptedContent = "1.3.6.1.4.1.311.88.3.1";
        internal const string szOID_OIWSEC_sha1 = "1.3.14.3.2.26";
        internal const string szOID_RSA_MD5 = "1.2.840.113549.2.5";
        internal const string szOID_OIWSEC_SHA256 = "2.16.840.1.101.3.4.1";
        internal const string szOID_OIWSEC_SHA384 = "2.16.840.1.101.3.4.2";
        internal const string szOID_OIWSEC_SHA512 = "2.16.840.1.101.3.4.3";
        internal const string szOID_RSA_RC2CBC = "1.2.840.113549.3.2";
        internal const string szOID_RSA_RC4 = "1.2.840.113549.3.4";
        internal const string szOID_RSA_DES_EDE3_CBC = "1.2.840.113549.3.7";
        internal const string szOID_OIWSEC_desCBC = "1.3.14.3.2.7";
        internal const string szOID_RSA_SMIMEalg = "1.2.840.113549.1.9.16.3";
        internal const string szOID_RSA_SMIMEalgESDH = "1.2.840.113549.1.9.16.3.5";
        internal const string szOID_RSA_SMIMEalgCMS3DESwrap = "1.2.840.113549.1.9.16.3.6";
        internal const string szOID_RSA_SMIMEalgCMSRC2wrap = "1.2.840.113549.1.9.16.3.7";
        internal const string szOID_X957_DSA = "1.2.840.10040.4.1";
        internal const string szOID_X957_sha1DSA = "1.2.840.10040.4.3";
        internal const string szOID_OIWSEC_sha1RSASign = "1.3.14.3.2.29";
        internal const uint CERT_ALT_NAME_OTHER_NAME = 1U;
        internal const uint CERT_ALT_NAME_RFC822_NAME = 2U;
        internal const uint CERT_ALT_NAME_DNS_NAME = 3U;
        internal const uint CERT_ALT_NAME_X400_ADDRESS = 4U;
        internal const uint CERT_ALT_NAME_DIRECTORY_NAME = 5U;
        internal const uint CERT_ALT_NAME_EDI_PARTY_NAME = 6U;
        internal const uint CERT_ALT_NAME_URL = 7U;
        internal const uint CERT_ALT_NAME_IP_ADDRESS = 8U;
        internal const uint CERT_ALT_NAME_REGISTERED_ID = 9U;
        internal const uint CERT_RDN_ANY_TYPE = 0U;
        internal const uint CERT_RDN_ENCODED_BLOB = 1U;
        internal const uint CERT_RDN_OCTET_STRING = 2U;
        internal const uint CERT_RDN_NUMERIC_STRING = 3U;
        internal const uint CERT_RDN_PRINTABLE_STRING = 4U;
        internal const uint CERT_RDN_TELETEX_STRING = 5U;
        internal const uint CERT_RDN_T61_STRING = 5U;
        internal const uint CERT_RDN_VIDEOTEX_STRING = 6U;
        internal const uint CERT_RDN_IA5_STRING = 7U;
        internal const uint CERT_RDN_GRAPHIC_STRING = 8U;
        internal const uint CERT_RDN_VISIBLE_STRING = 9U;
        internal const uint CERT_RDN_ISO646_STRING = 9U;
        internal const uint CERT_RDN_GENERAL_STRING = 10U;
        internal const uint CERT_RDN_UNIVERSAL_STRING = 11U;
        internal const uint CERT_RDN_INT4_STRING = 11U;
        internal const uint CERT_RDN_BMP_STRING = 12U;
        internal const uint CERT_RDN_UNICODE_STRING = 12U;
        internal const uint CERT_RDN_UTF8_STRING = 13U;
        internal const uint CERT_RDN_TYPE_MASK = 255U;
        internal const uint CERT_RDN_FLAGS_MASK = 4278190080U;
        internal const uint CERT_STORE_CTRL_RESYNC = 1U;
        internal const uint CERT_STORE_CTRL_NOTIFY_CHANGE = 2U;
        internal const uint CERT_STORE_CTRL_COMMIT = 3U;
        internal const uint CERT_STORE_CTRL_AUTO_RESYNC = 4U;
        internal const uint CERT_STORE_CTRL_CANCEL_NOTIFY = 5U;
        internal const uint CERT_ID_ISSUER_SERIAL_NUMBER = 1U;
        internal const uint CERT_ID_KEY_IDENTIFIER = 2U;
        internal const uint CERT_ID_SHA1_HASH = 3U;
        internal const string MS_ENHANCED_PROV = "Microsoft Enhanced Cryptographic Provider v1.0";
        internal const string MS_STRONG_PROV = "Microsoft Strong Cryptographic Provider";
        internal const string MS_DEF_PROV = "Microsoft Base Cryptographic Provider v1.0";
        internal const string MS_DEF_DSS_DH_PROV = "Microsoft Base DSS and Diffie-Hellman Cryptographic Provider";
        internal const string MS_ENH_DSS_DH_PROV = "Microsoft Enhanced DSS and Diffie-Hellman Cryptographic Provider";
        internal const string DummySignerCommonName = "CN=Dummy Signer";
        internal const uint PROV_RSA_FULL = 1U;
        internal const uint PROV_DSS_DH = 13U;
        internal const uint ALG_TYPE_ANY = 0U;
        internal const uint ALG_TYPE_DSS = 512U;
        internal const uint ALG_TYPE_RSA = 1024U;
        internal const uint ALG_TYPE_BLOCK = 1536U;
        internal const uint ALG_TYPE_STREAM = 2048U;
        internal const uint ALG_TYPE_DH = 2560U;
        internal const uint ALG_TYPE_SECURECHANNEL = 3072U;
        internal const uint ALG_CLASS_ANY = 0U;
        internal const uint ALG_CLASS_SIGNATURE = 8192U;
        internal const uint ALG_CLASS_MSG_ENCRYPT = 16384U;
        internal const uint ALG_CLASS_DATA_ENCRYPT = 24576U;
        internal const uint ALG_CLASS_HASH = 32768U;
        internal const uint ALG_CLASS_KEY_EXCHANGE = 40960U;
        internal const uint ALG_CLASS_ALL = 57344U;
        internal const uint ALG_SID_ANY = 0U;
        internal const uint ALG_SID_RSA_ANY = 0U;
        internal const uint ALG_SID_RSA_PKCS = 1U;
        internal const uint ALG_SID_RSA_MSATWORK = 2U;
        internal const uint ALG_SID_RSA_ENTRUST = 3U;
        internal const uint ALG_SID_RSA_PGP = 4U;
        internal const uint ALG_SID_DSS_ANY = 0U;
        internal const uint ALG_SID_DSS_PKCS = 1U;
        internal const uint ALG_SID_DSS_DMS = 2U;
        internal const uint ALG_SID_DES = 1U;
        internal const uint ALG_SID_3DES = 3U;
        internal const uint ALG_SID_DESX = 4U;
        internal const uint ALG_SID_IDEA = 5U;
        internal const uint ALG_SID_CAST = 6U;
        internal const uint ALG_SID_SAFERSK64 = 7U;
        internal const uint ALG_SID_SAFERSK128 = 8U;
        internal const uint ALG_SID_3DES_112 = 9U;
        internal const uint ALG_SID_CYLINK_MEK = 12U;
        internal const uint ALG_SID_RC5 = 13U;
        internal const uint ALG_SID_AES_128 = 14U;
        internal const uint ALG_SID_AES_192 = 15U;
        internal const uint ALG_SID_AES_256 = 16U;
        internal const uint ALG_SID_AES = 17U;
        internal const uint ALG_SID_SKIPJACK = 10U;
        internal const uint ALG_SID_TEK = 11U;
        internal const uint ALG_SID_RC2 = 2U;
        internal const uint ALG_SID_RC4 = 1U;
        internal const uint ALG_SID_SEAL = 2U;
        internal const uint ALG_SID_DH_SANDF = 1U;
        internal const uint ALG_SID_DH_EPHEM = 2U;
        internal const uint ALG_SID_AGREED_KEY_ANY = 3U;
        internal const uint ALG_SID_KEA = 4U;
        internal const uint ALG_SID_MD2 = 1U;
        internal const uint ALG_SID_MD4 = 2U;
        internal const uint ALG_SID_MD5 = 3U;
        internal const uint ALG_SID_SHA = 4U;
        internal const uint ALG_SID_SHA1 = 4U;
        internal const uint ALG_SID_MAC = 5U;
        internal const uint ALG_SID_RIPEMD = 6U;
        internal const uint ALG_SID_RIPEMD160 = 7U;
        internal const uint ALG_SID_SSL3SHAMD5 = 8U;
        internal const uint ALG_SID_HMAC = 9U;
        internal const uint ALG_SID_TLS1PRF = 10U;
        internal const uint ALG_SID_HASH_REPLACE_OWF = 11U;
        internal const uint ALG_SID_SSL3_MASTER = 1U;
        internal const uint ALG_SID_SCHANNEL_MASTER_HASH = 2U;
        internal const uint ALG_SID_SCHANNEL_MAC_KEY = 3U;
        internal const uint ALG_SID_PCT1_MASTER = 4U;
        internal const uint ALG_SID_SSL2_MASTER = 5U;
        internal const uint ALG_SID_TLS1_MASTER = 6U;
        internal const uint ALG_SID_SCHANNEL_ENC_KEY = 7U;
        internal const uint CALG_MD2 = 32769U;
        internal const uint CALG_MD4 = 32770U;
        internal const uint CALG_MD5 = 32771U;
        internal const uint CALG_SHA = 32772U;
        internal const uint CALG_SHA1 = 32772U;
        internal const uint CALG_MAC = 32773U;
        internal const uint CALG_RSA_SIGN = 9216U;
        internal const uint CALG_DSS_SIGN = 8704U;
        internal const uint CALG_NO_SIGN = 8192U;
        internal const uint CALG_RSA_KEYX = 41984U;
        internal const uint CALG_DES = 26113U;
        internal const uint CALG_3DES_112 = 26121U;
        internal const uint CALG_3DES = 26115U;
        internal const uint CALG_DESX = 26116U;
        internal const uint CALG_RC2 = 26114U;
        internal const uint CALG_RC4 = 26625U;
        internal const uint CALG_SEAL = 26626U;
        internal const uint CALG_DH_SF = 43521U;
        internal const uint CALG_DH_EPHEM = 43522U;
        internal const uint CALG_AGREEDKEY_ANY = 43523U;
        internal const uint CALG_KEA_KEYX = 43524U;
        internal const uint CALG_HUGHES_MD5 = 40963U;
        internal const uint CALG_SKIPJACK = 26122U;
        internal const uint CALG_TEK = 26123U;
        internal const uint CALG_CYLINK_MEK = 26124U;
        internal const uint CALG_SSL3_SHAMD5 = 32776U;
        internal const uint CALG_SSL3_MASTER = 19457U;
        internal const uint CALG_SCHANNEL_MASTER_HASH = 19458U;
        internal const uint CALG_SCHANNEL_MAC_KEY = 19459U;
        internal const uint CALG_SCHANNEL_ENC_KEY = 19463U;
        internal const uint CALG_PCT1_MASTER = 19460U;
        internal const uint CALG_SSL2_MASTER = 19461U;
        internal const uint CALG_TLS1_MASTER = 19462U;
        internal const uint CALG_RC5 = 26125U;
        internal const uint CALG_HMAC = 32777U;
        internal const uint CALG_TLS1PRF = 32778U;
        internal const uint CALG_HASH_REPLACE_OWF = 32779U;
        internal const uint CALG_AES_128 = 26126U;
        internal const uint CALG_AES_192 = 26127U;
        internal const uint CALG_AES_256 = 26128U;
        internal const uint CALG_AES = 26129U;
        internal const uint CRYPT_FIRST = 1U;
        internal const uint CRYPT_NEXT = 2U;
        internal const uint PP_ENUMALGS_EX = 22U;
        internal const uint CRYPT_VERIFYCONTEXT = 4026531840U;
        internal const uint CRYPT_NEWKEYSET = 8U;
        internal const uint CRYPT_DELETEKEYSET = 16U;
        internal const uint CRYPT_MACHINE_KEYSET = 32U;
        internal const uint CRYPT_SILENT = 64U;
        internal const uint CRYPT_USER_KEYSET = 4096U;
        internal const uint CRYPT_EXPORTABLE = 1U;
        internal const uint CRYPT_USER_PROTECTED = 2U;
        internal const uint CRYPT_CREATE_SALT = 4U;
        internal const uint CRYPT_UPDATE_KEY = 8U;
        internal const uint CRYPT_NO_SALT = 16U;
        internal const uint CRYPT_PREGEN = 64U;
        internal const uint CRYPT_RECIPIENT = 16U;
        internal const uint CRYPT_INITIATOR = 64U;
        internal const uint CRYPT_ONLINE = 128U;
        internal const uint CRYPT_SF = 256U;
        internal const uint CRYPT_CREATE_IV = 512U;
        internal const uint CRYPT_KEK = 1024U;
        internal const uint CRYPT_DATA_KEY = 2048U;
        internal const uint CRYPT_VOLATILE = 4096U;
        internal const uint CRYPT_SGCKEY = 8192U;
        internal const uint CRYPT_ARCHIVABLE = 16384U;
        internal const byte CUR_BLOB_VERSION = (byte)2;
        internal const byte SIMPLEBLOB = (byte)1;
        internal const byte PUBLICKEYBLOB = (byte)6;
        internal const byte PRIVATEKEYBLOB = (byte)7;
        internal const byte PLAINTEXTKEYBLOB = (byte)8;
        internal const byte OPAQUEKEYBLOB = (byte)9;
        internal const byte PUBLICKEYBLOBEX = (byte)10;
        internal const byte SYMMETRICWRAPKEYBLOB = (byte)11;
        internal const uint DSS_MAGIC = 827544388U;
        internal const uint DSS_PRIVATE_MAGIC = 844321604U;
        internal const uint DSS_PUB_MAGIC_VER3 = 861098820U;
        internal const uint DSS_PRIV_MAGIC_VER3 = 877876036U;
        internal const uint RSA_PUB_MAGIC = 826364754U;
        internal const uint RSA_PRIV_MAGIC = 843141970U;
        internal const uint CRYPT_ACQUIRE_CACHE_FLAG = 1U;
        internal const uint CRYPT_ACQUIRE_USE_PROV_INFO_FLAG = 2U;
        internal const uint CRYPT_ACQUIRE_COMPARE_KEY_FLAG = 4U;
        internal const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
        internal const uint CMSG_BARE_CONTENT_FLAG = 1U;
        internal const uint CMSG_LENGTH_ONLY_FLAG = 2U;
        internal const uint CMSG_DETACHED_FLAG = 4U;
        internal const uint CMSG_AUTHENTICATED_ATTRIBUTES_FLAG = 8U;
        internal const uint CMSG_CONTENTS_OCTETS_FLAG = 16U;
        internal const uint CMSG_MAX_LENGTH_FLAG = 32U;
        internal const uint CMSG_TYPE_PARAM = 1U;
        internal const uint CMSG_CONTENT_PARAM = 2U;
        internal const uint CMSG_BARE_CONTENT_PARAM = 3U;
        internal const uint CMSG_INNER_CONTENT_TYPE_PARAM = 4U;
        internal const uint CMSG_SIGNER_COUNT_PARAM = 5U;
        internal const uint CMSG_SIGNER_INFO_PARAM = 6U;
        internal const uint CMSG_SIGNER_CERT_INFO_PARAM = 7U;
        internal const uint CMSG_SIGNER_HASH_ALGORITHM_PARAM = 8U;
        internal const uint CMSG_SIGNER_AUTH_ATTR_PARAM = 9U;
        internal const uint CMSG_SIGNER_UNAUTH_ATTR_PARAM = 10U;
        internal const uint CMSG_CERT_COUNT_PARAM = 11U;
        internal const uint CMSG_CERT_PARAM = 12U;
        internal const uint CMSG_CRL_COUNT_PARAM = 13U;
        internal const uint CMSG_CRL_PARAM = 14U;
        internal const uint CMSG_ENVELOPE_ALGORITHM_PARAM = 15U;
        internal const uint CMSG_RECIPIENT_COUNT_PARAM = 17U;
        internal const uint CMSG_RECIPIENT_INDEX_PARAM = 18U;
        internal const uint CMSG_RECIPIENT_INFO_PARAM = 19U;
        internal const uint CMSG_HASH_ALGORITHM_PARAM = 20U;
        internal const uint CMSG_HASH_DATA_PARAM = 21U;
        internal const uint CMSG_COMPUTED_HASH_PARAM = 22U;
        internal const uint CMSG_ENCRYPT_PARAM = 26U;
        internal const uint CMSG_ENCRYPTED_DIGEST = 27U;
        internal const uint CMSG_ENCODED_SIGNER = 28U;
        internal const uint CMSG_ENCODED_MESSAGE = 29U;
        internal const uint CMSG_VERSION_PARAM = 30U;
        internal const uint CMSG_ATTR_CERT_COUNT_PARAM = 31U;
        internal const uint CMSG_ATTR_CERT_PARAM = 32U;
        internal const uint CMSG_CMS_RECIPIENT_COUNT_PARAM = 33U;
        internal const uint CMSG_CMS_RECIPIENT_INDEX_PARAM = 34U;
        internal const uint CMSG_CMS_RECIPIENT_ENCRYPTED_KEY_INDEX_PARAM = 35U;
        internal const uint CMSG_CMS_RECIPIENT_INFO_PARAM = 36U;
        internal const uint CMSG_UNPROTECTED_ATTR_PARAM = 37U;
        internal const uint CMSG_SIGNER_CERT_ID_PARAM = 38U;
        internal const uint CMSG_CMS_SIGNER_INFO_PARAM = 39U;
        internal const uint CMSG_CTRL_VERIFY_SIGNATURE = 1U;
        internal const uint CMSG_CTRL_DECRYPT = 2U;
        internal const uint CMSG_CTRL_VERIFY_HASH = 5U;
        internal const uint CMSG_CTRL_ADD_SIGNER = 6U;
        internal const uint CMSG_CTRL_DEL_SIGNER = 7U;
        internal const uint CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR = 8U;
        internal const uint CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR = 9U;
        internal const uint CMSG_CTRL_ADD_CERT = 10U;
        internal const uint CMSG_CTRL_DEL_CERT = 11U;
        internal const uint CMSG_CTRL_ADD_CRL = 12U;
        internal const uint CMSG_CTRL_DEL_CRL = 13U;
        internal const uint CMSG_CTRL_ADD_ATTR_CERT = 14U;
        internal const uint CMSG_CTRL_DEL_ATTR_CERT = 15U;
        internal const uint CMSG_CTRL_KEY_TRANS_DECRYPT = 16U;
        internal const uint CMSG_CTRL_KEY_AGREE_DECRYPT = 17U;
        internal const uint CMSG_CTRL_MAIL_LIST_DECRYPT = 18U;
        internal const uint CMSG_CTRL_VERIFY_SIGNATURE_EX = 19U;
        internal const uint CMSG_CTRL_ADD_CMS_SIGNER_INFO = 20U;
        internal const uint CMSG_VERIFY_SIGNER_PUBKEY = 1U;
        internal const uint CMSG_VERIFY_SIGNER_CERT = 2U;
        internal const uint CMSG_VERIFY_SIGNER_CHAIN = 3U;
        internal const uint CMSG_VERIFY_SIGNER_NULL = 4U;
        internal const uint CMSG_DATA = 1U;
        internal const uint CMSG_SIGNED = 2U;
        internal const uint CMSG_ENVELOPED = 3U;
        internal const uint CMSG_SIGNED_AND_ENVELOPED = 4U;
        internal const uint CMSG_HASHED = 5U;
        internal const uint CMSG_ENCRYPTED = 6U;
        internal const uint CMSG_KEY_TRANS_RECIPIENT = 1U;
        internal const uint CMSG_KEY_AGREE_RECIPIENT = 2U;
        internal const uint CMSG_MAIL_LIST_RECIPIENT = 3U;
        internal const uint CMSG_KEY_AGREE_ORIGINATOR_CERT = 1U;
        internal const uint CMSG_KEY_AGREE_ORIGINATOR_PUBLIC_KEY = 2U;
        internal const uint CMSG_KEY_AGREE_EPHEMERAL_KEY_CHOICE = 1U;
        internal const uint CMSG_KEY_AGREE_STATIC_KEY_CHOICE = 2U;
        internal const uint CMSG_ENVELOPED_RECIPIENT_V0 = 0U;
        internal const uint CMSG_ENVELOPED_RECIPIENT_V2 = 2U;
        internal const uint CMSG_ENVELOPED_RECIPIENT_V3 = 3U;
        internal const uint CMSG_ENVELOPED_RECIPIENT_V4 = 4U;
        internal const uint CMSG_KEY_TRANS_PKCS_1_5_VERSION = 0U;
        internal const uint CMSG_KEY_TRANS_CMS_VERSION = 2U;
        internal const uint CMSG_KEY_AGREE_VERSION = 3U;
        internal const uint CMSG_MAIL_LIST_VERSION = 4U;
        internal const uint CRYPT_RC2_40BIT_VERSION = 160U;
        internal const uint CRYPT_RC2_56BIT_VERSION = 52U;
        internal const uint CRYPT_RC2_64BIT_VERSION = 120U;
        internal const uint CRYPT_RC2_128BIT_VERSION = 58U;
        internal const int E_NOTIMPL = -2147483647;
        internal const int E_FILENOTFOUND = -2147024894;
        internal const int E_OUTOFMEMORY = -2147024882;
        internal const int NTE_NO_KEY = -2146893811;
        internal const int NTE_BAD_PUBLIC_KEY = -2146893803;
        internal const int NTE_BAD_KEYSET = -2146893802;
        internal const int CRYPT_E_MSG_ERROR = -2146889727;
        internal const int CRYPT_E_UNKNOWN_ALGO = -2146889726;
        internal const int CRYPT_E_INVALID_MSG_TYPE = -2146889724;
        internal const int CRYPT_E_RECIPIENT_NOT_FOUND = -2146889717;
        internal const int CRYPT_E_ISSUER_SERIALNUMBER = -2146889715;
        internal const int CRYPT_E_SIGNER_NOT_FOUND = -2146889714;
        internal const int CRYPT_E_ATTRIBUTES_MISSING = -2146889713;
        internal const int CRYPT_E_BAD_ENCODE = -2146885630;
        internal const int CRYPT_E_NOT_FOUND = -2146885628;
        internal const int CRYPT_E_NO_MATCH = -2146885623;
        internal const int CRYPT_E_NO_SIGNER = -2146885618;
        internal const int CRYPT_E_REVOKED = -2146885616;
        internal const int CRYPT_E_NO_REVOCATION_CHECK = -2146885614;
        internal const int CRYPT_E_REVOCATION_OFFLINE = -2146885613;
        internal const int CRYPT_E_ASN1_BADTAG = -2146881269;
        internal const int TRUST_E_CERT_SIGNATURE = -2146869244;
        internal const int TRUST_E_BASIC_CONSTRAINTS = -2146869223;
        internal const int CERT_E_EXPIRED = -2146762495;
        internal const int CERT_E_VALIDITYPERIODNESTING = -2146762494;
        internal const int CERT_E_UNTRUSTEDROOT = -2146762487;
        internal const int CERT_E_CHAINING = -2146762486;
        internal const int TRUST_E_FAIL = -2146762485;
        internal const int CERT_E_REVOKED = -2146762484;
        internal const int CERT_E_UNTRUSTEDTESTROOT = -2146762483;
        internal const int CERT_E_REVOCATION_FAILURE = -2146762482;
        internal const int CERT_E_WRONG_USAGE = -2146762480;
        internal const int CERT_E_INVALID_POLICY = -2146762477;
        internal const int CERT_E_INVALID_NAME = -2146762476;
        internal const int ERROR_SUCCESS = 0;
        internal const int ERROR_FILE_NOT_FOUND = 2;
        internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;
        internal const int ERROR_CANCELLED = 1223;

        [SecurityCritical]
        internal static byte[] BlobToByteArray(IntPtr pBlob)
        {
            CAPI.CRYPTOAPI_BLOB blob = (CAPI.CRYPTOAPI_BLOB)Marshal.PtrToStructure(pBlob, typeof(CAPI.CRYPTOAPI_BLOB));
            if ((int)blob.cbData == 0)
                return new byte[0];
            else
                return CAPI.BlobToByteArray(blob);
        }

        [SecurityCritical]
        internal static byte[] BlobToByteArray(CAPI.CRYPTOAPI_BLOB blob)
        {
            if ((int)blob.cbData == 0)
                return new byte[0];
            byte[] destination = new byte[blob.cbData];
            Marshal.Copy(blob.pbData, destination, 0, destination.Length);
            return destination;
        }

        [SecurityCritical]
        internal static unsafe bool DecodeObject(IntPtr pszStructType, IntPtr pbEncoded, uint cbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
        {
            decodedValue = SafeLocalAllocHandle.InvalidHandle;
            cbDecodedValue = 0U;
            uint num = 0U;
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, invalidHandle, new IntPtr((void*)&num)))
                return false;
            SafeLocalAllocHandle pvStructInfo = CAPI.LocalAlloc(0U, new IntPtr((long)num));
            if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, pvStructInfo, new IntPtr((void*)&num)))
                return false;
            decodedValue = pvStructInfo;
            cbDecodedValue = num;
            return true;
        }

        [SecurityCritical]
        internal static unsafe bool DecodeObject(IntPtr pszStructType, byte[] pbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
        {
            decodedValue = SafeLocalAllocHandle.InvalidHandle;
            cbDecodedValue = 0U;
            uint num = 0U;
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, invalidHandle, new IntPtr((void*)&num)))
                return false;
            SafeLocalAllocHandle pvStructInfo = CAPI.LocalAlloc(0U, new IntPtr((long)num));
            if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, pvStructInfo, new IntPtr((void*)&num)))
                return false;
            decodedValue = pvStructInfo;
            cbDecodedValue = num;
            return true;
        }

        [SecuritySafeCritical]
        internal static unsafe bool EncodeObject(IntPtr lpszStructType, IntPtr pvStructInfo, out byte[] encodedData)
        {
            encodedData = new byte[0];
            uint num = 0U;
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, invalidHandle, new IntPtr((void*)&num)))
                return false;
            SafeLocalAllocHandle pbEncoded = CAPI.LocalAlloc(0U, new IntPtr((long)num));
            if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, pbEncoded, new IntPtr((void*)&num)))
                return false;
            encodedData = new byte[(IntPtr)num];
            Marshal.Copy(pbEncoded.DangerousGetHandle(), encodedData, 0, (int)num);
            pbEncoded.Dispose();
            return true;
        }

        [SecurityCritical]
        internal static unsafe bool EncodeObject(string lpszStructType, IntPtr pvStructInfo, out byte[] encodedData)
        {
            encodedData = new byte[0];
            uint num = 0U;
            SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
            if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, invalidHandle, new IntPtr((void*)&num)))
                return false;
            SafeLocalAllocHandle pbEncoded = CAPI.LocalAlloc(0U, new IntPtr((long)num));
            if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, pbEncoded, new IntPtr((void*)&num)))
                return false;
            encodedData = new byte[(IntPtr)num];
            Marshal.Copy(pbEncoded.DangerousGetHandle(), encodedData, 0, (int)num);
            pbEncoded.Dispose();
            return true;
        }

        internal static bool ErrorMayBeCausedByUnloadedProfile(int errorCode)
        {
            if (errorCode != -2147024894)
                return errorCode == 2;
            else
                return true;
        }

        [SecurityCritical]
        internal static SafeLocalAllocHandle LocalAlloc(uint uFlags, IntPtr sizetdwBytes)
        {
            SafeLocalAllocHandle localAllocHandle = CAPI.CAPISafe.LocalAlloc(uFlags, sizetdwBytes);
            if (localAllocHandle == null || localAllocHandle.IsInvalid)
                throw new OutOfMemoryException();
            else
                return localAllocHandle;
        }

        [SecurityCritical]
        internal static bool CryptAcquireContext([In, Out] ref SafeCryptProvHandle hCryptProv, [MarshalAs(UnmanagedType.LPStr), In] string pwszContainer, [MarshalAs(UnmanagedType.LPStr), In] string pwszProvider, [In] uint dwProvType, [In] uint dwFlags)
        {
            CspParameters parameters = new CspParameters();
            parameters.ProviderName = pwszProvider;
            parameters.KeyContainerName = pwszContainer;
            parameters.ProviderType = (int)dwProvType;
            parameters.KeyNumber = -1;
            parameters.Flags = ((int)dwFlags & 32) == 32 ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
            KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
            KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open);
            containerPermission.AccessEntries.Add(accessEntry);
            containerPermission.Demand();
            bool flag = CAPI.CAPIUnsafe.CryptAcquireContext(out hCryptProv, pwszContainer, pwszProvider, dwProvType, dwFlags);
            if (!flag && Marshal.GetLastWin32Error() == -2146893802)
                flag = CAPI.CAPIUnsafe.CryptAcquireContext(out hCryptProv, pwszContainer, pwszProvider, dwProvType, dwFlags | 8U);
            return flag;
        }

        [SecurityCritical]
        internal static bool CryptAcquireContext(ref SafeCryptProvHandle hCryptProv, IntPtr pwszContainer, IntPtr pwszProvider, uint dwProvType, uint dwFlags)
        {
            string pwszContainer1 = (string)null;
            if (pwszContainer != IntPtr.Zero)
                pwszContainer1 = Marshal.PtrToStringUni(pwszContainer);
            string pwszProvider1 = (string)null;
            if (pwszProvider != IntPtr.Zero)
                pwszProvider1 = Marshal.PtrToStringUni(pwszProvider);
            return CAPI.CryptAcquireContext(out hCryptProv, pwszContainer1, pwszProvider1, dwProvType, dwFlags);
        }

        [SecurityCritical]
        internal static CAPI.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] uint dwGroupId)
        {
            if (pvKey == IntPtr.Zero)
                throw new ArgumentNullException("pvKey");
            CAPI.CRYPT_OID_INFO cryptOidInfo = new CAPI.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CAPI.CRYPT_OID_INFO)));
            IntPtr oidInfo = CAPI.CAPISafe.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
            if (oidInfo != IntPtr.Zero)
                cryptOidInfo = (CAPI.CRYPT_OID_INFO)Marshal.PtrToStructure(oidInfo, typeof(CAPI.CRYPT_OID_INFO));
            return cryptOidInfo;
        }

        [SecurityCritical]
        internal static CAPI.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] uint dwGroupId)
        {
            if (pvKey == null)
                throw new ArgumentNullException("pvKey");
            if (pvKey.IsInvalid)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "pvKey");
            CAPI.CRYPT_OID_INFO cryptOidInfo = new CAPI.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CAPI.CRYPT_OID_INFO)));
            IntPtr oidInfo = CAPI.CAPISafe.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
            if (oidInfo != IntPtr.Zero)
                cryptOidInfo = (CAPI.CRYPT_OID_INFO)Marshal.PtrToStructure(oidInfo, typeof(CAPI.CRYPT_OID_INFO));
            return cryptOidInfo;
        }

        [SecurityCritical]
        internal static bool CryptMsgControl([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara)
        {
            return CAPI.CAPIUnsafe.CryptMsgControl(hCryptMsg, dwFlags, dwCtrlType, pvCtrlPara);
        }

        [SecurityCritical]
        internal static bool CryptMsgCountersign([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwIndex, [In] uint cCountersigners, [In] IntPtr rgCountersigners)
        {
            return CAPI.CAPIUnsafe.CryptMsgCountersign(hCryptMsg, dwIndex, cCountersigners, rgCountersigners);
        }

        [SecurityCritical]
        internal static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] IntPtr pszInnerContentObjID, [In] IntPtr pStreamInfo)
        {
            return CAPI.CAPIUnsafe.CryptMsgOpenToEncode(dwMsgEncodingType, dwFlags, dwMsgType, pvMsgEncodeInfo, pszInnerContentObjID, pStreamInfo);
        }

        [SecurityCritical]
        internal static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] string pszInnerContentObjID, [In] IntPtr pStreamInfo)
        {
            return CAPI.CAPIUnsafe.CryptMsgOpenToEncode(dwMsgEncodingType, dwFlags, dwMsgType, pvMsgEncodeInfo, pszInnerContentObjID, pStreamInfo);
        }

        [SecurityCritical]
        internal static SafeCertContextHandle CertDuplicateCertificateContext([In] IntPtr pCertContext)
        {
            if (pCertContext == IntPtr.Zero)
                return SafeCertContextHandle.InvalidHandle;
            else
                return CAPI.CAPISafe.CertDuplicateCertificateContext(pCertContext);
        }

        [SecurityCritical]
        internal static IntPtr CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pPrevCertContext)
        {
            if (hCertStore == null)
                throw new ArgumentNullException("hCertStore");
            if (hCertStore.IsInvalid)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
            if (pPrevCertContext == IntPtr.Zero)
                new StorePermission(StorePermissionFlags.EnumerateCertificates).Demand();
            IntPtr pCertContext = CAPI.CAPIUnsafe.CertEnumCertificatesInStore(hCertStore, pPrevCertContext);
            if (pCertContext == IntPtr.Zero)
            {
                int lastWin32Error = Marshal.GetLastWin32Error();
                if (lastWin32Error != -2146885628)
                {
                    CAPI.CAPISafe.CertFreeCertificateContext(pCertContext);
                    throw new CryptographicException(lastWin32Error);
                }
            }
            return pCertContext;
        }

        [SecurityCritical]
        internal static bool CertAddCertificateContextToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In, Out] SafeCertContextHandle ppStoreContext)
        {
            if (hCertStore == null)
                throw new ArgumentNullException("hCertStore");
            if (hCertStore.IsInvalid)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
            if (pCertContext == null)
                throw new ArgumentNullException("pCertContext");
            if (pCertContext.IsInvalid)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "pCertContext");
            new StorePermission(StorePermissionFlags.AddToStore).Demand();
            return CAPI.CAPIUnsafe.CertAddCertificateContextToStore(hCertStore, pCertContext, dwAddDisposition, ppStoreContext);
        }

        [SecurityCritical]
        internal static bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In, Out] SafeCertContextHandle ppStoreContext)
        {
            if (hCertStore == null)
                throw new ArgumentNullException("hCertStore");
            if (hCertStore.IsInvalid)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
            if (pCertContext == null)
                throw new ArgumentNullException("pCertContext");
            if (pCertContext.IsInvalid)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "pCertContext");
            new StorePermission(StorePermissionFlags.AddToStore).Demand();
            return CAPI.CAPIUnsafe.CertAddCertificateLinkToStore(hCertStore, pCertContext, dwAddDisposition, ppStoreContext);
        }

        [SecurityCritical]
        internal static SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara)
        {
            if (lpszStoreProvider != new IntPtr(2L) && lpszStoreProvider != new IntPtr(10L))
                throw new ArgumentException(SecurityResources.GetResourceString("Argument_InvalidValue"), "lpszStoreProvider");
            if ((((int)dwFlags & 131072) == 131072 || ((int)dwFlags & 524288) == 524288 || ((int)dwFlags & 589824) == 589824) && (pvPara != null && pvPara.StartsWith("\\\\", StringComparison.Ordinal)))
                new PermissionSet(PermissionState.Unrestricted).Demand();
            if (((int)dwFlags & 16) == 16)
                new StorePermission(StorePermissionFlags.DeleteStore).Demand();
            else
                new StorePermission(StorePermissionFlags.OpenStore).Demand();
            if (((int)dwFlags & 8192) == 8192)
                new StorePermission(StorePermissionFlags.CreateStore).Demand();
            if (((int)dwFlags & 16384) == 0)
                new StorePermission(StorePermissionFlags.CreateStore).Demand();
            return CAPI.CAPIUnsafe.CertOpenStore(lpszStoreProvider, dwMsgAndCertEncodingType, hCryptProv, dwFlags | 4U, pvPara);
        }

        [SecurityCritical]
        internal static bool CryptProtectData([In] IntPtr pDataIn, [In] string szDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In, Out] IntPtr pDataBlob)
        {
            new DataProtectionPermission(DataProtectionPermissionFlags.ProtectData).Demand();
            return CAPI.CAPIUnsafe.CryptProtectData(pDataIn, szDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, pDataBlob);
        }

        [SecurityCritical]
        internal static bool CryptUnprotectData([In] IntPtr pDataIn, [In] IntPtr ppszDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In, Out] IntPtr pDataBlob)
        {
            new DataProtectionPermission(DataProtectionPermissionFlags.UnprotectData).Demand();
            return CAPI.CAPIUnsafe.CryptUnprotectData(pDataIn, ppszDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, pDataBlob);
        }

        [SecurityCritical]
        internal static int SystemFunction040([In, Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags)
        {
            new DataProtectionPermission(DataProtectionPermissionFlags.ProtectMemory).Demand();
            return CAPI.CAPIUnsafe.SystemFunction040(pDataIn, cbDataIn, dwFlags);
        }

        [SecurityCritical]
        internal static int SystemFunction041([In, Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags)
        {
            new DataProtectionPermission(DataProtectionPermissionFlags.UnprotectMemory).Demand();
            return CAPI.CAPIUnsafe.SystemFunction041(pDataIn, cbDataIn, dwFlags);
        }

        [SecurityCritical]
        internal static SafeCertContextHandle CryptUIDlgSelectCertificateW([MarshalAs(UnmanagedType.LPStruct), In, Out] CAPI.CRYPTUI_SELECTCERTIFICATE_STRUCTW csc)
        {
            if (!Environment.UserInteractive)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Environment_NotInteractive"));
            new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
            return CAPI.CAPIUnsafe.CryptUIDlgSelectCertificateW(csc);
        }

        [SecurityCritical]
        internal static bool CryptUIDlgViewCertificateW([MarshalAs(UnmanagedType.LPStruct), In] CAPI.CRYPTUI_VIEWCERTIFICATE_STRUCTW ViewInfo, [In, Out] IntPtr pfPropertiesChanged)
        {
            if (!Environment.UserInteractive)
                throw new InvalidOperationException(SecurityResources.GetResourceString("Environment_NotInteractive"));
            new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
            return CAPI.CAPIUnsafe.CryptUIDlgViewCertificateW(ViewInfo, pfPropertiesChanged);
        }

        [SecurityCritical]
        internal static SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] IntPtr pvFindPara, [In] SafeCertContextHandle pPrevCertContext)
        {
            if (hCertStore == null)
                throw new ArgumentNullException("hCertStore");
            if (hCertStore.IsInvalid)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
            else
                return CAPI.CAPIUnsafe.CertFindCertificateInStore(hCertStore, dwCertEncodingType, dwFindFlags, dwFindType, pvFindPara, pPrevCertContext);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_ID
        {
            internal uint dwIdChoice;
            internal CAPI.CERT_ID_UNION Value;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        internal struct CERT_ID_UNION
        {
            [FieldOffset(0)]
            internal CAPI.CERT_ISSUER_SERIAL_NUMBER IssuerSerialNumber;
            [FieldOffset(0)]
            internal CAPI.CRYPTOAPI_BLOB KeyId;
            [FieldOffset(0)]
            internal CAPI.CRYPTOAPI_BLOB HashId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_ISSUER_SERIAL_NUMBER
        {
            internal CAPI.CRYPTOAPI_BLOB Issuer;
            internal CAPI.CRYPTOAPI_BLOB SerialNumber;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_RECIPIENT_ENCRYPTED_KEY_INFO
        {
            internal CAPI.CERT_ID RecipientId;
            internal CAPI.CRYPTOAPI_BLOB EncryptedKey;
            internal System.Runtime.InteropServices.ComTypes.FILETIME Date;
            internal IntPtr pOtherAttr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_SIGNER_INFO
        {
            internal uint dwVersion;
            internal CAPI.CRYPTOAPI_BLOB Issuer;
            internal CAPI.CRYPTOAPI_BLOB SerialNumber;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;
            internal CAPI.CRYPTOAPI_BLOB EncryptedHash;
            internal CAPI.CRYPT_ATTRIBUTES AuthAttrs;
            internal CAPI.CRYPT_ATTRIBUTES UnauthAttrs;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszObjId;
            internal CAPI.CRYPTOAPI_BLOB Parameters;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_ATTRIBUTES
        {
            internal uint cAttr;
            internal IntPtr rgAttr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPTOAPI_BLOB
        {
            internal uint cbData;
            internal IntPtr pbData;
        }

        [SuppressUnmanagedCodeSecurity]
        [SecurityCritical]
        internal static class CAPISafe
        {
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport("kernel32.dll", SetLastError = true)]
            internal extern static IntPtr LocalFree(IntPtr handle);

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport("kernel32.dll", SetLastError = true)]
            internal extern static void ZeroMemory(IntPtr handle, uint length);

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport("advapi32.dll", SetLastError = true)]
            internal extern static int LsaNtStatusToWinError([In] int status);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
            internal extern static IntPtr GetProcAddress([In] SafeLibraryHandle hModule, [MarshalAs(UnmanagedType.LPStr), In] string lpProcName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static SafeLocalAllocHandle LocalAlloc([In] uint uFlags, [In] IntPtr sizetdwBytes);

            [DllImport("kernel32.dll", EntryPoint = "LoadLibraryA", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
            internal extern static SafeLibraryHandle LoadLibrary([MarshalAs(UnmanagedType.LPStr), In] string lpFileName);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static SafeCertContextHandle CertCreateCertificateContext([In] uint dwCertEncodingType, [In] SafeLocalAllocHandle pbCertEncoded, [In] uint cbCertEncoded);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static SafeCertContextHandle CertDuplicateCertificateContext([In] IntPtr pCertContext);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CertFreeCertificateContext([In] IntPtr pCertContext);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CertGetCertificateChain([In] IntPtr hChainEngine, [In] SafeCertContextHandle pCertContext, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME pTime, [In] SafeCertStoreHandle hAdditionalStore, [In] ref CAPI.CERT_CHAIN_PARA pChainPara, [In] uint dwFlags, [In] IntPtr pvReserved, [In, Out] ref SafeCertChainHandle ppChainContext);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CertGetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In, Out] SafeLocalAllocHandle pvData, [In, Out] ref uint pcbData);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static uint CertGetPublicKeyLength([In] uint dwCertEncodingType, [In] IntPtr pPublicKey);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static uint CertNameToStrW([In] uint dwCertEncodingType, [In] IntPtr pName, [In] uint dwStrType, [In, Out] SafeLocalAllocHandle psz, [In] uint csz);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CertVerifyCertificateChainPolicy([In] IntPtr pszPolicyOID, [In] SafeCertChainHandle pChainContext, [In] ref CAPI.CERT_CHAIN_POLICY_PARA pPolicyPara, [In, Out] ref CAPI.CERT_CHAIN_POLICY_STATUS pPolicyStatus);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptAcquireCertificatePrivateKey([In] SafeCertContextHandle pCert, [In] uint dwFlags, [In] IntPtr pvReserved, [In, Out] ref SafeCryptProvHandle phCryptProv, [In, Out] ref uint pdwKeySpec, [In, Out] ref bool pfCallerFreeProv);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] IntPtr pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In, Out] SafeLocalAllocHandle pvStructInfo, [In, Out] IntPtr pcbStructInfo);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] byte[] pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In, Out] SafeLocalAllocHandle pvStructInfo, [In, Out] IntPtr pcbStructInfo);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptEncodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] IntPtr pvStructInfo, [In, Out] SafeLocalAllocHandle pbEncoded, [In, Out] IntPtr pcbEncoded);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
            internal extern static bool CryptEncodeObject([In] uint dwCertEncodingType, [MarshalAs(UnmanagedType.LPStr), In] string lpszStructType, [In] IntPtr pvStructInfo, [In, Out] SafeLocalAllocHandle pbEncoded, [In, Out] IntPtr pcbEncoded);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] uint dwGroupId);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] uint dwGroupId);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptGetProvParam([In] SafeCryptProvHandle hProv, [In] uint dwParam, [In] IntPtr pbData, [In] IntPtr pdwDataLen, [In] uint dwFlags);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptMsgGetParam([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwParamType, [In] uint dwIndex, [In, Out] IntPtr pvData, [In, Out] IntPtr pcbData);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptMsgGetParam([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwParamType, [In] uint dwIndex, [In, Out] SafeLocalAllocHandle pvData, [In, Out] IntPtr pcbData);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static SafeCryptMsgHandle CryptMsgOpenToDecode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr hCryptProv, [In] IntPtr pRecipientInfo, [In] IntPtr pStreamInfo);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptMsgUpdate([In] SafeCryptMsgHandle hCryptMsg, [In] byte[] pbData, [In] uint cbData, [In] bool fFinal);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptMsgUpdate([In] SafeCryptMsgHandle hCryptMsg, [In] IntPtr pbData, [In] uint cbData, [In] bool fFinal);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptMsgVerifyCountersignatureEncoded([In] IntPtr hCryptProv, [In] uint dwEncodingType, [In] IntPtr pbSignerInfo, [In] uint cbSignerInfo, [In] IntPtr pbSignerInfoCountersignature, [In] uint cbSignerInfoCountersignature, [In] IntPtr pciCountersigner);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct BLOBHEADER
        {
            internal byte bType;
            internal byte bVersion;
            internal short reserved;
            internal uint aiKeyAlg;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_ALT_NAME_INFO
        {
            internal uint cAltEntry;
            internal IntPtr rgAltEntry;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_BASIC_CONSTRAINTS_INFO
        {
            internal CAPI.CRYPT_BIT_BLOB SubjectType;
            internal bool fPathLenConstraint;
            internal uint dwPathLenConstraint;
            internal uint cSubtreesConstraint;
            internal IntPtr rgSubtreesConstraint;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_BASIC_CONSTRAINTS2_INFO
        {
            internal int fCA;
            internal int fPathLenConstraint;
            internal uint dwPathLenConstraint;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_CHAIN_PARA
        {
            internal uint cbSize;
            internal CAPI.CERT_USAGE_MATCH RequestedUsage;
            internal CAPI.CERT_USAGE_MATCH RequestedIssuancePolicy;
            internal uint dwUrlRetrievalTimeout;
            internal bool fCheckRevocationFreshnessTime;
            internal uint dwRevocationFreshnessTime;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_CHAIN_POLICY_PARA
        {
            internal uint cbSize;
            internal uint dwFlags;
            internal IntPtr pvExtraPolicyPara;

            internal CERT_CHAIN_POLICY_PARA(int size)
            {
                this.cbSize = (uint)size;
                this.dwFlags = 0U;
                this.pvExtraPolicyPara = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_CHAIN_POLICY_STATUS
        {
            internal uint cbSize;
            internal uint dwError;
            internal IntPtr lChainIndex;
            internal IntPtr lElementIndex;
            internal IntPtr pvExtraPolicyStatus;

            internal CERT_CHAIN_POLICY_STATUS(int size)
            {
                this.cbSize = (uint)size;
                this.dwError = 0U;
                this.lChainIndex = IntPtr.Zero;
                this.lElementIndex = IntPtr.Zero;
                this.pvExtraPolicyStatus = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_CONTEXT
        {
            internal uint dwCertEncodingType;
            internal IntPtr pbCertEncoded;
            internal uint cbCertEncoded;
            internal IntPtr pCertInfo;
            internal IntPtr hCertStore;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_DSS_PARAMETERS
        {
            internal CAPI.CRYPTOAPI_BLOB p;
            internal CAPI.CRYPTOAPI_BLOB q;
            internal CAPI.CRYPTOAPI_BLOB g;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_ENHKEY_USAGE
        {
            internal uint cUsageIdentifier;
            internal IntPtr rgpszUsageIdentifier;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_EXTENSION
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszObjId;
            internal bool fCritical;
            internal CAPI.CRYPTOAPI_BLOB Value;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_INFO
        {
            internal uint dwVersion;
            internal CAPI.CRYPTOAPI_BLOB SerialNumber;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;
            internal CAPI.CRYPTOAPI_BLOB Issuer;
            internal System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;
            internal System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;
            internal CAPI.CRYPTOAPI_BLOB Subject;
            internal CAPI.CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;
            internal CAPI.CRYPT_BIT_BLOB IssuerUniqueId;
            internal CAPI.CRYPT_BIT_BLOB SubjectUniqueId;
            internal uint cExtension;
            internal IntPtr rgExtension;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_KEY_USAGE_RESTRICTION_INFO
        {
            internal uint cCertPolicyId;
            internal IntPtr rgCertPolicyId;
            internal CAPI.CRYPT_BIT_BLOB RestrictedKeyUsage;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_NAME_INFO
        {
            internal uint cRDN;
            internal IntPtr rgRDN;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_NAME_VALUE
        {
            internal uint dwValueType;
            internal CAPI.CRYPTOAPI_BLOB Value;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_OTHER_NAME
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszObjId;
            internal CAPI.CRYPTOAPI_BLOB Value;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_POLICY_ID
        {
            internal uint cCertPolicyElementId;
            internal IntPtr rgpszCertPolicyElementId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_POLICIES_INFO
        {
            internal uint cPolicyInfo;
            internal IntPtr rgPolicyInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_POLICY_INFO
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszPolicyIdentifier;
            internal uint cPolicyQualifier;
            internal IntPtr rgPolicyQualifier;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_POLICY_QUALIFIER_INFO
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszPolicyQualifierId;
            private CAPI.CRYPTOAPI_BLOB Qualifier;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_PUBLIC_KEY_INFO
        {
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            internal CAPI.CRYPT_BIT_BLOB PublicKey;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_PUBLIC_KEY_INFO2
        {
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER2 Algorithm;
            internal CAPI.CRYPT_BIT_BLOB PublicKey;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_RDN
        {
            internal uint cRDNAttr;
            internal IntPtr rgRDNAttr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_RDN_ATTR
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszObjId;
            internal uint dwValueType;
            internal CAPI.CRYPTOAPI_BLOB Value;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_TEMPLATE_EXT
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszObjId;
            internal uint dwMajorVersion;
            private bool fMinorVersion;
            private uint dwMinorVersion;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_TRUST_STATUS
        {
            internal uint dwErrorStatus;
            internal uint dwInfoStatus;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_USAGE_MATCH
        {
            internal uint dwType;
            internal CAPI.CERT_ENHKEY_USAGE Usage;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CMS_RECIPIENT_INFO
        {
            internal uint dwRecipientChoice;
            internal IntPtr pRecipientInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CMS_SIGNER_INFO
        {
            internal uint dwVersion;
            internal CAPI.CERT_ID SignerId;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;
            internal CAPI.CRYPTOAPI_BLOB EncryptedHash;
            internal CAPI.CRYPT_ATTRIBUTES AuthAttrs;
            internal CAPI.CRYPT_ATTRIBUTES UnauthAttrs;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA
        {
            internal uint cbSize;
            internal uint dwSignerIndex;
            internal CAPI.CRYPTOAPI_BLOB blob;

            internal CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA(int size)
            {
                this.cbSize = (uint)size;
                this.dwSignerIndex = 0U;
                this.blob = new CAPI.CRYPTOAPI_BLOB();
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CTRL_DECRYPT_PARA
        {
            internal uint cbSize;
            internal IntPtr hCryptProv;
            internal uint dwKeySpec;
            internal uint dwRecipientIndex;

            internal CMSG_CTRL_DECRYPT_PARA(int size)
            {
                this.cbSize = (uint)size;
                this.hCryptProv = IntPtr.Zero;
                this.dwKeySpec = 0U;
                this.dwRecipientIndex = 0U;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA
        {
            internal uint cbSize;
            internal uint dwSignerIndex;
            internal uint dwUnauthAttrIndex;

            internal CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA(int size)
            {
                this.cbSize = (uint)size;
                this.dwSignerIndex = 0U;
                this.dwUnauthAttrIndex = 0U;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CTRL_KEY_TRANS_DECRYPT_PARA
        {
            internal uint cbSize;
            [SecurityCritical]
            internal SafeCryptProvHandle hCryptProv;
            internal uint dwKeySpec;
            internal IntPtr pKeyTrans;
            internal uint dwRecipientIndex;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO
        {
            internal uint cbSize;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;
            internal IntPtr pvKeyEncryptionAuxInfo;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyWrapAlgorithm;
            internal IntPtr pvKeyWrapAuxInfo;
            internal IntPtr hCryptProv;
            internal uint dwKeySpec;
            internal uint dwKeyChoice;
            internal IntPtr pEphemeralAlgorithmOrSenderId;
            internal CAPI.CRYPTOAPI_BLOB UserKeyingMaterial;
            internal uint cRecipientEncryptedKeys;
            internal IntPtr rgpRecipientEncryptedKeys;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO
        {
            internal uint cbSize;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;
            internal IntPtr pvKeyEncryptionAuxInfo;
            internal IntPtr hCryptProv;
            internal CAPI.CRYPT_BIT_BLOB RecipientPublicKey;
            internal CAPI.CERT_ID RecipientId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_RC2_AUX_INFO
        {
            internal uint cbSize;
            internal uint dwBitLen;

            internal CMSG_RC2_AUX_INFO(int size)
            {
                this.cbSize = (uint)size;
                this.dwBitLen = 0U;
            }
        }

        internal struct CMSG_RECIPIENT_ENCODE_INFO
        {
            internal uint dwRecipientChoice;
            internal IntPtr pRecipientInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO
        {
            internal uint cbSize;
            internal CAPI.CRYPT_BIT_BLOB RecipientPublicKey;
            internal CAPI.CERT_ID RecipientId;
            internal System.Runtime.InteropServices.ComTypes.FILETIME Date;
            internal IntPtr pOtherAttr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_ENVELOPED_ENCODE_INFO
        {
            internal uint cbSize;
            internal IntPtr hCryptProv;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER ContentEncryptionAlgorithm;
            internal IntPtr pvEncryptionAuxInfo;
            internal uint cRecipients;
            internal IntPtr rgpRecipients;
            internal IntPtr rgCmsRecipients;
            internal uint cCertEncoded;
            internal IntPtr rgCertEncoded;
            internal uint cCrlEncoded;
            internal IntPtr rgCrlEncoded;
            internal uint cAttrCertEncoded;
            internal IntPtr rgAttrCertEncoded;
            internal uint cUnprotectedAttr;
            internal IntPtr rgUnprotectedAttr;

            internal CMSG_ENVELOPED_ENCODE_INFO(int size)
            {
                this.cbSize = (uint)size;
                this.hCryptProv = IntPtr.Zero;
                this.ContentEncryptionAlgorithm = new CAPI.CRYPT_ALGORITHM_IDENTIFIER();
                this.pvEncryptionAuxInfo = IntPtr.Zero;
                this.cRecipients = 0U;
                this.rgpRecipients = IntPtr.Zero;
                this.rgCmsRecipients = IntPtr.Zero;
                this.cCertEncoded = 0U;
                this.rgCertEncoded = IntPtr.Zero;
                this.cCrlEncoded = 0U;
                this.rgCrlEncoded = IntPtr.Zero;
                this.cAttrCertEncoded = 0U;
                this.rgAttrCertEncoded = IntPtr.Zero;
                this.cUnprotectedAttr = 0U;
                this.rgUnprotectedAttr = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CTRL_KEY_AGREE_DECRYPT_PARA
        {
            internal uint cbSize;
            internal IntPtr hCryptProv;
            internal uint dwKeySpec;
            internal IntPtr pKeyAgree;
            internal uint dwRecipientIndex;
            internal uint dwRecipientEncryptedKeyIndex;
            internal CAPI.CRYPT_BIT_BLOB OriginatorPublicKey;

            internal CMSG_CTRL_KEY_AGREE_DECRYPT_PARA(int size)
            {
                this.cbSize = (uint)size;
                this.hCryptProv = IntPtr.Zero;
                this.dwKeySpec = 0U;
                this.pKeyAgree = IntPtr.Zero;
                this.dwRecipientIndex = 0U;
                this.dwRecipientEncryptedKeyIndex = 0U;
                this.OriginatorPublicKey = new CAPI.CRYPT_BIT_BLOB();
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_KEY_AGREE_RECIPIENT_INFO
        {
            internal uint dwVersion;
            internal uint dwOriginatorChoice;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO
        {
            internal uint dwVersion;
            internal uint dwOriginatorChoice;
            internal CAPI.CERT_ID OriginatorCertId;
            internal IntPtr Padding;
            internal CAPI.CRYPTOAPI_BLOB UserKeyingMaterial;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;
            internal uint cRecipientEncryptedKeys;
            internal IntPtr rgpRecipientEncryptedKeys;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO
        {
            internal uint dwVersion;
            internal uint dwOriginatorChoice;
            internal CAPI.CERT_PUBLIC_KEY_INFO OriginatorPublicKeyInfo;
            internal CAPI.CRYPTOAPI_BLOB UserKeyingMaterial;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;
            internal uint cRecipientEncryptedKeys;
            internal IntPtr rgpRecipientEncryptedKeys;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA
        {
            internal uint cbSize;
            internal IntPtr hCryptProv;
            internal uint dwSignerIndex;
            internal uint dwSignerType;
            internal IntPtr pvSigner;

            internal CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA(int size)
            {
                this.cbSize = (uint)size;
                this.hCryptProv = IntPtr.Zero;
                this.dwSignerIndex = 0U;
                this.dwSignerType = 0U;
                this.pvSigner = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_KEY_TRANS_RECIPIENT_INFO
        {
            internal uint dwVersion;
            internal CAPI.CERT_ID RecipientId;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;
            internal CAPI.CRYPTOAPI_BLOB EncryptedKey;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_SIGNED_ENCODE_INFO
        {
            internal uint cbSize;
            internal uint cSigners;
            internal IntPtr rgSigners;
            internal uint cCertEncoded;
            internal IntPtr rgCertEncoded;
            internal uint cCrlEncoded;
            internal IntPtr rgCrlEncoded;
            internal uint cAttrCertEncoded;
            internal IntPtr rgAttrCertEncoded;

            internal CMSG_SIGNED_ENCODE_INFO(int size)
            {
                this.cbSize = (uint)size;
                this.cSigners = 0U;
                this.rgSigners = IntPtr.Zero;
                this.cCertEncoded = 0U;
                this.rgCertEncoded = IntPtr.Zero;
                this.cCrlEncoded = 0U;
                this.rgCrlEncoded = IntPtr.Zero;
                this.cAttrCertEncoded = 0U;
                this.rgAttrCertEncoded = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CMSG_SIGNER_ENCODE_INFO
        {
            internal uint cbSize;
            internal IntPtr pCertInfo;
            internal IntPtr hCryptProv;
            internal uint dwKeySpec;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;
            internal IntPtr pvHashAuxInfo;
            internal uint cAuthAttr;
            internal IntPtr rgAuthAttr;
            internal uint cUnauthAttr;
            internal IntPtr rgUnauthAttr;
            internal CAPI.CERT_ID SignerId;
            internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;
            internal IntPtr pvHashEncryptionAuxInfo;

            internal CMSG_SIGNER_ENCODE_INFO(int size)
            {
                this.cbSize = (uint)size;
                this.pCertInfo = IntPtr.Zero;
                this.hCryptProv = IntPtr.Zero;
                this.dwKeySpec = 0U;
                this.HashAlgorithm = new CAPI.CRYPT_ALGORITHM_IDENTIFIER();
                this.pvHashAuxInfo = IntPtr.Zero;
                this.cAuthAttr = 0U;
                this.rgAuthAttr = IntPtr.Zero;
                this.cUnauthAttr = 0U;
                this.rgUnauthAttr = IntPtr.Zero;
                this.SignerId = new CAPI.CERT_ID();
                this.HashEncryptionAlgorithm = new CAPI.CRYPT_ALGORITHM_IDENTIFIER();
                this.pvHashEncryptionAuxInfo = IntPtr.Zero;
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            internal extern static IntPtr LocalFree(IntPtr hMem);

            [DllImport("advapi32.dll", SetLastError = true)]
            internal extern static bool CryptReleaseContext([In] IntPtr hProv, [In] uint dwFlags);

            [SecuritySafeCritical]
            internal void Dispose()
            {
                if (this.hCryptProv != IntPtr.Zero)
                    CAPI.CMSG_SIGNER_ENCODE_INFO.CryptReleaseContext(this.hCryptProv, 0U);
                if (this.SignerId.Value.KeyId.pbData != IntPtr.Zero)
                    CAPI.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.SignerId.Value.KeyId.pbData);
                if (this.rgAuthAttr != IntPtr.Zero)
                    CAPI.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.rgAuthAttr);
                if (!(this.rgUnauthAttr != IntPtr.Zero))
                    return;
                CAPI.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.rgUnauthAttr);
            }
        }

        internal delegate bool PFN_CMSG_STREAM_OUTPUT(IntPtr pvArg, IntPtr pbData, uint cbData, bool fFinal);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class CMSG_STREAM_INFO
        {
            internal uint cbContent;
            internal CAPI.PFN_CMSG_STREAM_OUTPUT pfnStreamOutput;
            internal IntPtr pvArg;

            internal CMSG_STREAM_INFO(uint cbContent, CAPI.PFN_CMSG_STREAM_OUTPUT pfnStreamOutput, IntPtr pvArg)
            {
                this.cbContent = cbContent;
                this.pfnStreamOutput = pfnStreamOutput;
                this.pvArg = pvArg;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER2
        {
            internal IntPtr pszObjId;
            internal CAPI.CRYPTOAPI_BLOB Parameters;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_ATTRIBUTE
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszObjId;
            internal uint cValue;
            internal IntPtr rgValue;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_ATTRIBUTE_TYPE_VALUE
        {
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszObjId;
            internal CAPI.CRYPTOAPI_BLOB Value;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_BIT_BLOB
        {
            internal uint cbData;
            internal IntPtr pbData;
            internal uint cUnusedBits;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_KEY_PROV_INFO
        {
            internal string pwszContainerName;
            internal string pwszProvName;
            internal uint dwProvType;
            internal uint dwFlags;
            internal uint cProvParam;
            internal IntPtr rgProvParam;
            internal uint dwKeySpec;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_OID_INFO
        {
            internal uint cbSize;
            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszOID;
            internal string pwszName;
            internal uint dwGroupId;
            internal uint Algid;
            internal CAPI.CRYPTOAPI_BLOB ExtraInfo;

            internal CRYPT_OID_INFO(int size)
            {
                this.cbSize = (uint)size;
                this.pszOID = (string)null;
                this.pwszName = (string)null;
                this.dwGroupId = 0U;
                this.Algid = 0U;
                this.ExtraInfo = new CAPI.CRYPTOAPI_BLOB();
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_RC2_CBC_PARAMETERS
        {
            internal uint dwVersion;
            internal bool fIV;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            internal byte[] rgbIV;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class CRYPTUI_SELECTCERTIFICATE_STRUCTW
        {
            internal uint dwSize;
            internal IntPtr hwndParent;
            internal uint dwFlags;
            internal string szTitle;
            internal uint dwDontUseColumn;
            internal string szDisplayString;
            internal IntPtr pFilterCallback;
            internal IntPtr pDisplayCallback;
            internal IntPtr pvCallbackData;
            internal uint cDisplayStores;
            internal IntPtr rghDisplayStores;
            internal uint cStores;
            internal IntPtr rghStores;
            internal uint cPropSheetPages;
            internal IntPtr rgPropSheetPages;
            internal IntPtr hSelectedCertStore;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class CRYPTUI_VIEWCERTIFICATE_STRUCTW
        {
            internal uint dwSize;
            internal IntPtr hwndParent;
            internal uint dwFlags;
            internal string szTitle;
            internal IntPtr pCertContext;
            internal IntPtr rgszPurposes;
            internal uint cPurposes;
            internal IntPtr pCryptProviderData;
            internal bool fpCryptProviderDataTrustedUsage;
            internal uint idxSigner;
            internal uint idxCert;
            internal bool fCounterSigner;
            internal uint idxCounterSigner;
            internal uint cStores;
            internal IntPtr rghStores;
            internal uint cPropSheetPages;
            internal IntPtr rgPropSheetPages;
            internal uint nStartPage;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DSSPUBKEY
        {
            internal uint magic;
            internal uint bitlen;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct PROV_ENUMALGS_EX
        {
            internal uint aiAlgid;
            internal uint dwDefaultLen;
            internal uint dwMinLen;
            internal uint dwMaxLen;
            internal uint dwProtocols;
            internal uint dwNameLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            internal byte[] szName;
            internal uint dwLongNameLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            internal byte[] szLongName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct RSAPUBKEY
        {
            internal uint magic;
            internal uint bitlen;
            internal uint pubexp;
        }

        [SecurityCritical]
        [SuppressUnmanagedCodeSecurity]
        internal static class CAPIUnsafe
        {
            [DllImport("advapi32.dll", EntryPoint = "CryptAcquireContextA", CharSet = CharSet.Auto, BestFitMapping = false)]
            internal extern static bool CryptAcquireContext([In, Out] ref SafeCryptProvHandle hCryptProv, [MarshalAs(UnmanagedType.LPStr), In] string pszContainer, [MarshalAs(UnmanagedType.LPStr), In] string pszProvider, [In] uint dwProvType, [In] uint dwFlags);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CertAddCertificateContextToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In, Out] SafeCertContextHandle ppStoreContext);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In, Out] SafeCertContextHandle ppStoreContext);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static IntPtr CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pPrevCertContext);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] IntPtr pvFindPara, [In] SafeCertContextHandle pPrevCertContext);

            [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal extern static SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static SafeCertContextHandle CertCreateSelfSignCertificate([In] SafeCryptProvHandle hProv, [In] IntPtr pSubjectIssuerBlob, [In] uint dwFlags, [In] IntPtr pKeyProvInfo, [In] IntPtr pSignatureAlgorithm, [In] IntPtr pStartTime, [In] IntPtr pEndTime, [In] IntPtr pExtensions);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptMsgControl([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static bool CryptMsgCountersign([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwIndex, [In] uint cCountersigners, [In] IntPtr rgCountersigners);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal extern static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] IntPtr pszInnerContentObjID, [In] IntPtr pStreamInfo);

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
            internal extern static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [MarshalAs(UnmanagedType.LPStr), In] string pszInnerContentObjID, [In] IntPtr pStreamInfo);

            [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal extern static bool CryptProtectData([In] IntPtr pDataIn, [In] string szDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In, Out] IntPtr pDataBlob);

            [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal extern static bool CryptUnprotectData([In] IntPtr pDataIn, [In] IntPtr ppszDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In, Out] IntPtr pDataBlob);

            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal extern static int SystemFunction040([In, Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags);

            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal extern static int SystemFunction041([In, Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags);

            [DllImport("cryptui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal extern static SafeCertContextHandle CryptUIDlgSelectCertificateW([MarshalAs(UnmanagedType.LPStruct), In, Out] CAPI.CRYPTUI_SELECTCERTIFICATE_STRUCTW csc);

            [DllImport("cryptui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal extern static bool CryptUIDlgViewCertificateW([MarshalAs(UnmanagedType.LPStruct), In] CAPI.CRYPTUI_VIEWCERTIFICATE_STRUCTW ViewInfo, [In, Out] IntPtr pfPropertiesChanged);
        }
    }
}