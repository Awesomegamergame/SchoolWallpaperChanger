using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SchoolWallpaperChanger.Functions;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace SchoolWallpaperChanger
{
    public partial class MainWindow : Window
    {
        public string FileLocation;

        public static Button Yes_Button;
        public static Button No_Button;
        public static Image UpdateScreen_Image;
        public static Label UpdateText1_Label;
        public static Label UpdateText2_Label;
        public static ProgressBar GameDownloadBar;
        public static ProgressBar UpdateDownloadBar;
        public static Label LocalVersionObject, LocalVersionNumberObject;
        public static Label OnlineVersionObject, OnlineVersionNumberObject;

        public MainWindow()
        {
            CheckInternet.CheckInternetState();
            InitializeComponent();
            if (CheckInternet.IsOnline) { Updater.Update(); }
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose Image",
                Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff" + "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
            };
            var dialog = openFileDialog;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                FileLocation = dialog.FileName;
                Change.IsEnabled = true;
                BitmapImage btm = new BitmapImage(new Uri(dialog.FileName));
                Window2.Source = btm;
                Window2.Stretch = Stretch.Fill;
                NoWallpaper.Visibility = Visibility.Collapsed;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public void SetWallpaper(string thePath)
        {
            SystemParametersInfo(20, 0, thePath, 4);
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Change.IsEnabled = false;
            File.Copy(FileLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
            SetWallpaper(FileLocation);
            MessageBox.Show("Wallpaper Changed");
        }
        #region Updates
        private void No_Click(object sender, RoutedEventArgs e)
        {
            UpdateScreen_Image.Visibility = Visibility.Collapsed;
            Yes_Button.Visibility = Visibility.Collapsed;
            No_Button.Visibility = Visibility.Collapsed;
            UpdateText1_Label.Visibility = Visibility.Collapsed;
            UpdateText2_Label.Visibility = Visibility.Collapsed;
            LocalVersionObject.Visibility = Visibility.Collapsed;
            LocalVersionNumberObject.Visibility = Visibility.Collapsed;
            OnlineVersionNumberObject.Visibility = Visibility.Collapsed;
            OnlineVersionObject.Visibility = Visibility.Collapsed;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Yes_Button.Visibility = Visibility.Collapsed;
            No_Button.Visibility = Visibility.Collapsed;
            UpdateDownloadBar.Visibility = Visibility.Visible;
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
        private void Yes_Initialized(object sender, EventArgs e)
        {
            Yes_Button = (Button)sender;
        }
        private void No_Initialized(object sender, EventArgs e)
        {
            No_Button = (Button)sender;
        }
        private void UpdateText1_Initialized(object sender, EventArgs e)
        {
            UpdateText1_Label = (Label)sender;
        }
        private void UpdateText2_Initialized(object sender, EventArgs e)
        {
            UpdateText2_Label = (Label)sender;
        }
        private void UpdateScreen_Initialized(object sender, EventArgs e)
        {
            UpdateScreen_Image = (Image)sender;
        }
        private void ProgressBar_Initialized(object sender, EventArgs e)
        {
            GameDownloadBar = (ProgressBar)sender;
        }
        private void UpdateBar_Initialized(object sender, EventArgs e)
        {
            UpdateDownloadBar = (ProgressBar)sender;
        }
        private void LocalVersionNumber_Initialized(object sender, EventArgs e)
        {
            LocalVersionNumberObject = (Label)sender;
        }
        private void LocalVersion_Initialized(object sender, EventArgs e)
        {
            LocalVersionObject = (Label)sender;
        }
        private void OnlineVersionNumber_Initialized(object sender, EventArgs e)
        {
            OnlineVersionNumberObject = (Label)sender;
        }

        private void OnlineVersion_Initialized(object sender, EventArgs e)
        {
            OnlineVersionObject = (Label)sender;
        }
        #endregion
    }
}
