using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolWallpaperChanger
{
    public partial class App : Application
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            #region Disable IntelliSense Here
            #pragma warning disable IDE0059 // Unnecessary assignment of a value
            var mutex = new Mutex(true, "SchoolWallpaperChanger", out bool aIsNewInstance);
            #pragma warning restore IDE0059 // Unnecessary assignment of a value
            #endregion
            if (!aIsNewInstance)
            {
                Process current = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    Current.Shutdown();
                    if (process.Id != current.Id)
                    {
                        IntPtr pointer = FindWindow(null, "SchoolWallpaperChanger");
                        ShowWindow(pointer, 1);
                        break;
                    }
                }
            }
            else
            {
                MainWindow window = new MainWindow();
                window.Show();
            }
        }
    }
}
