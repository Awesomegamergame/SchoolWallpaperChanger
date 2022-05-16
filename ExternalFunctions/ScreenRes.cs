using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace SchoolWallpaperChanger.ExternalFunctions
{
    internal class ScreenRes
    {
        private enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);

        public static void SetDpiAwareness()
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
                }
            }
            catch (EntryPointNotFoundException ex)
            {
                MessageBox.Show($"An unexpected error has happened ERROR:{ex}");
            }
        }
    }
}
