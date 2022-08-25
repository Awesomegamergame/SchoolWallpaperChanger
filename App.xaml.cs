using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace SchoolWallpaperChanger
{
    public partial class App : Application
    {
        public App() : base()
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Crash occurred, report this error: \n" + e.Exception.Message, "Crash Log", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Process.Start("https://github.com/awesomegamergame/SchoolWallpaperChanger/issues");
            Current.Shutdown();
        }

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
                    MainWindow window = new MainWindow
                    {
                        WindowState = WindowState.Minimized,
                        ShowInTaskbar = false
                    };
                    window.Show();
                    window.Hide();
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
