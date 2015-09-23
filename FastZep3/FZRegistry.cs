using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace FastZep3
{
    class FZRegistry
    {
        public static bool isSet(string key)
        {
            try
            {
                if (get(key) != "") return true;
            }
            catch
            {
                return false;
            }
            return false;

        }
        public static string get(string key)
        {
            RegistryKey key1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FastZep\FastZep3");
            if (key1 == null)
            {
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FastZep\FastZep3");
                key1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FastZep\FastZep3", true);
                if (key1 == null) throw new Exception("No key in Registry");
            }
            object obj = key1.GetValue(key);
            if (obj == null) throw new Exception("No key in Registry");
            return obj.ToString();
        }
        public static bool set(string key, string value)
        {
            RegistryKey key1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FastZep\FastZep3", true);
            if (key1 == null)
            {
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FastZep\FastZep3");
                key1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FastZep\FastZep3", true);
                if (key1 == null) throw new Exception("No key in Registry");
            }

            key1.SetValue(key, value);
            return true;
        }

    }
}
