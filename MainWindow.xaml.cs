using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SchoolWallpaperChanger.Functions;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace SchoolWallpaperChanger
{
    public partial class MainWindow : Window
    {
        public string FileLocation;

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
                Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.gif;*.apng"
            };
            var dialog = openFileDialog;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                FileLocation = dialog.FileName;
                Change.IsEnabled = true;
                BitmapImage btm = new BitmapImage(new Uri(dialog.FileName));
                Window2.Source = btm;
                Window2.Stretch = Stretch.Uniform;
                NoWallpaper.Visibility = Visibility.Collapsed;

                //Resolution Stuff
                var img = System.Drawing.Image.FromFile(dialog.FileName);
                var size = GetResolution.GetDisplayResolution();
                if (img.Width != size.Width || img.Height != size.Height)
                {
                    Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                    Warning.Visibility = Visibility.Visible;
                }
                else
                    Warning.Visibility = Visibility.Collapsed;
            }
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Change.IsEnabled = false;
            File.Copy(FileLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
            RefreshUI.SetWallpaper(FileLocation);
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
        #endregion
    }
}
