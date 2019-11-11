using System.Reflection;
using Microsoft.Win32;
using System.Linq;

namespace WinTermPlus.Interop
{
    public class WindowsStartup
    {
        public const string AppName = "WinTermPlus";

        public static void UpdateStartupKey(bool add)
        {
            var startupKey = GetStartupKey();
            if (startupKey.GetValueNames().Contains(AppName))
            {
                startupKey.DeleteValue(AppName);
            }
            if (add)
            {
                startupKey.SetValue(AppName, $"\"{GetStartupKeyValue()}\"");
            }
        }

        public static bool IsStartupKeySet()
        {
            var startupKey = GetStartupKey();
            return startupKey.GetValueNames().Contains(AppName)
                   && startupKey.GetValue(AppName).ToString().Replace("\"", "") == GetStartupKeyValue();
        }

        private static string GetStartupKeyValue()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        private static RegistryKey GetStartupKey()
        {
            return Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        }
    }
}