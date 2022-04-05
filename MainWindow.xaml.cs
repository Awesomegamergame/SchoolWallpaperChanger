using System;
using System.Windows;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SchoolWallpaperChanger.Functions;

namespace SchoolWallpaperChanger
{
    public partial class MainWindow : Window
    {
        public static MainWindow window;
        public int Selected = 0;
        public static bool Stopped = false;
        public NotifyIcon ni = new NotifyIcon();
        private readonly Regex _regex = new Regex("[^0-9]+");
        public MainWindow()
        {
            Updater.CheckInternetState();
            window = this;
            InitializeComponent();
            //if (CheckInternet.IsOnline) { Updater.Update(); }
            #region Tray
            ni.Icon = Properties.Resources.icon;
            ni.Visible = false;
            ni.DoubleClick +=
            delegate (object sender, EventArgs args)
            {
                Show();
                WindowState = WindowState.Normal;
                ni.Visible = true;
            };
            #endregion
        }
        #region Tray
        protected override void OnStateChanged(EventArgs e)
        {
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
            Selected = 1;
            Change.Content = "Start";
            Select.Content = "Select Images";
            Picture.IsEnabled = true;
            SlideShow.IsEnabled = false;
            Change.IsEnabled = false;
            Warning.Visibility = Visibility.Collapsed;
            NoWallpaper.Visibility = Visibility.Visible;
            Window3.Visibility = Visibility.Visible;
            NoWallpaper.Content = "No Images Selected";
            TimeL.Visibility = Visibility.Visible;
            Time.Visibility = Visibility.Visible;
        }
        private void Picture_Click(object sender, RoutedEventArgs e)
        {
            Selected = 0;
            Change.Content = "Change";
            Select.Content = "Select";
            Picture.IsEnabled = false;
            SlideShow.IsEnabled = true;
            Change.IsEnabled = false;
            Warning.Visibility = Visibility.Collapsed;
            NoWallpaper.Visibility = Visibility.Visible;
            Window3.Visibility = Visibility.Visible;
            TimeL.Visibility = Visibility.Collapsed;
            Time.Visibility = Visibility.Collapsed;
            NoWallpaper.Content = "No Wallpaper Selected";
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
    }
}
