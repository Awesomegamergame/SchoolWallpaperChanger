using System;
using System.Windows;
using System.Text.RegularExpressions;
using SchoolWallpaperChanger.Functions;
using SchoolWallpaperChanger.ButtonFunctions;
using IWshRuntimeLibrary;
using System.Reflection;

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
            //if (CheckInternet.IsOnline) { Updater.Update(); }
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
    }
}
