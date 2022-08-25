using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using SchoolWallpaperChanger.Functions;
using SchoolWallpaperChanger.ButtonFunctions;
using IWshRuntimeLibrary;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;

namespace SchoolWallpaperChanger
{
    public partial class MainWindow : Window
    {
        public static MainWindow window;
        public static int Selected = 0;
        public static bool Stopped = false;
        public static System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
        private readonly Regex _regex = new Regex("[^0-9]+");
        public static IniFile settings = new IniFile($"{AppDomain.CurrentDomain.BaseDirectory}\\Settings.ini");
        private bool ignoreFirst = true;
        public MainWindow()
        {
            Updater.CheckInternetState();
            window = this;
            InitializeComponent();
            if (!System.IO.File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\Settings.ini"))
                Config.NewConfig();
            else
            {
                Config.ReadConfig();
                Startup.Start();
                GC.Collect();
            }
            if (Updater.IsOnline) { Updater.Update(); }
            #region Tray
            ni.Icon = Properties.Resources.icon;
            ni.Visible = false;
            ni.DoubleClick +=
            delegate (object sender, EventArgs args)
            {
                Show();
                WindowState = WindowState.Normal;
                ni.Visible = false;
            };
            #endregion
        }
        #region Tray
        protected override void OnStateChanged(EventArgs e)
        {
            if(WindowState == WindowState.Normal)
            {
                window.ShowInTaskbar = true;
                window.Activate();
                Show();
                ni.Visible = false;
            }
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                ni.Visible = true;
            }
            base.OnStateChanged(e);
        }
        #endregion
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            SelectButton.Select(Selected);
            GC.Collect();
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ChangeButton.Change(Selected);
        }
        #region Updates
        private void No_Click(object sender, RoutedEventArgs e)
        {
            UpdateScreen.Visibility = Visibility.Collapsed;
            Yes.Visibility = Visibility.Collapsed;
            No.Visibility = Visibility.Collapsed;
            UpdateText1.Visibility = Visibility.Collapsed;
            UpdateText2.Visibility = Visibility.Collapsed;
            LocalVersion.Visibility = Visibility.Collapsed;
            LocalVersionNumber.Visibility = Visibility.Collapsed;
            OnlineVersionNumber.Visibility = Visibility.Collapsed;
            OnlineVersion.Visibility = Visibility.Collapsed;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Yes.Visibility = Visibility.Collapsed;
            No.Visibility = Visibility.Collapsed;
            UpdateProgress.Visibility = Visibility.Visible;
            if (Updater.VersionDetector == 1)
            {
                Updater.UpdaterVersion();
                Updater.VersionDetector = 0;
            }
            else if (Updater.VersionDetector == 2)
            {
                Updater.UpdaterVersion();
                Updater.VersionDetector = 0;
            }
        }
        #endregion
        private void SlideShow_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick.SlideShow();
        }
        private void Picture_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick.Picture();
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            SlideShowS.End();
        }
        #region Timer Box
        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        #endregion

        private void StartupB_Click(object sender, RoutedEventArgs e)
        {
            if (!System.IO.File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Start Menu\Programs\Startup\SchoolWallpaperChanger.lnk"))
            {
                MessageBox.Show("Startup Enabled");
                StartupB.Content = " Startup \nEnabled";
                CreateShortcut("SchoolWallpaperChanger", $@"{SlideShowS.AppDataPath}\Microsoft\Windows\Start Menu\Programs\Startup\", Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                MessageBox.Show("Statup Disabled");
                StartupB.Content = " Startup \nDisabled";
                System.IO.File.Delete($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Start Menu\Programs\Startup\SchoolWallpaperChanger.lnk");
            }
        }

        private void StartupB_Initialized(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Start Menu\Programs\Startup\SchoolWallpaperChanger.lnk"))
                StartupB.Content = " Startup \nDisabled";
            else
                StartupB.Content = " Startup \nEnabled";
        }
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Arguments = "Startup";
            shortcut.TargetPath = targetFileLocation;
            shortcut.Save();
        }

        #region Windows Scale

        [DllImport("DpiHelper.dll")]
        static public extern void PrintDpiInfo();

        [DllImport("DpiHelper.dll")]
        static public extern int SetDPIScaling(Int32 adapterIDHigh, UInt32 adapterIDlow, UInt32 sourceID, UInt32 dpiPercentToSet);
        [DllImport("DpiHelper.dll")]
        static public extern void RestoreDPIScaling();

        string GetLine(string fileName, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }

        private void Scale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ignoreFirst)
                ignoreFirst = false;
            else
            {
                string adaptorID = GetLine("DPI.txt", 1);
                string[] Split = adaptorID.Split('.');
                uint sourceID = uint.Parse(GetLine("DPI.txt", 2));
                int adapterIDHigh = int.Parse(Split[0]);
                uint adapterIDLow = uint.Parse(Split[1]);
                uint scalePercent = uint.Parse(Scale.SelectedItem.ToString());
                SetDPIScaling(adapterIDHigh, adapterIDLow, sourceID, scalePercent);
                MessageBox.Show("It is Recommended to log out and back in to fully apply the scale to all apps.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists("DPI.txt"))
            {
                PrintDpiInfo();
                string Scales = GetLine("DPI.txt", 5);
                string currentScale = GetLine("DPI.txt", 3);
                string[] Split = Scales.Split(' ');
                int s = Split.Length - 1;
                for (int x = 0; x < s; x++)
                {
                    Scale.Items.Add(Split[x]);
                    if (currentScale.Equals(Split[x]))
                        Scale.SelectedIndex = x;
                }
                string rec = GetLine("DPI.txt", 4);
                ScaleLRec.Content = $"Recommended: {rec}%";
            }
            else
            {
                ScaleLRec.Visibility = Visibility.Collapsed;
                ScaleL.Visibility = Visibility.Collapsed;
                Scale.Visibility = Visibility.Collapsed;
                MessageBox.Show("Warning something is wrong with DpiHelper's api please report this error");
            }
        }
        #endregion
    }
}
