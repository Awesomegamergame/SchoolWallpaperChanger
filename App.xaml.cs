using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            string[] args = Environment.GetCommandLineArgs();
            var exists = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1;
            if (exists)
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
                if (args.Length == 2)
                {
                    MainWindow window = new MainWindow();
                    Functions.Startup.StartupM();
                }
                else
                {
                    MainWindow window = new MainWindow();
                    window.Show();
                }
            }
        }
    }
}
