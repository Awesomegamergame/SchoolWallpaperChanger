using System.Windows;
using SchoolWallpaperChanger.Functions;

namespace SchoolWallpaperChanger
{
    public partial class MainWindow : Window
    {
        public static MainWindow window;
        public static int Selected = 0;
        public static bool Stopped = false;
        public MainWindow()
        {
            CheckInternet.CheckInternetState();
            window = this;
            InitializeComponent();
            MessageBox.Show("Warning this is a test build of version 1.1.0 I am not responsible if something bad happens to your computer by using any test build", "Test Build fe31f7a");
            //if (CheckInternet.IsOnline) { Updater.Update(); }
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {           
            SelectButton.Select();
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ChangeButton.Change();
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
            NoWallpaper.Content = "No Wallpaper Selected";
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Stopped = true;
            Change.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Collapsed;
            Select.Visibility = Visibility.Visible;
            SlideShow.IsEnabled = false;
            Picture.IsEnabled = true;
            Change.IsEnabled = true;
            Select.IsEnabled = true;
            SlideShowS.End();
        }
    }
}
