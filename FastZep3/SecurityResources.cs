// Type: System.Security.SecurityResources
// Assembly: System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Security.dll

using System.Globalization;
using System.Resources;

namespace System.Security
{
    internal static class SecurityResources
    {
        private static ResourceManager s_resMgr;

        internal static string GetResourceString(string key)
        {
            if (SecurityResources.s_resMgr == null)
                SecurityResources.s_resMgr = new ResourceManager("system.security", typeof(SecurityResources).Assembly);
            return SecurityResources.s_resMgr.GetString(key, (CultureInfo)null);
        }
    }
}
