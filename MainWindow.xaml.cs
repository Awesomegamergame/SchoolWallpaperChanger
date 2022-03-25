using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using SchoolWallpaperChanger.Functions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

namespace SchoolWallpaperChanger
{
    public partial class MainWindow : Window
    {
        public string FileLocation;
        public static MainWindow window;
        public int Selected = 0;
        public static bool Stopped = false;
        List<string> PictureNameList = new List<string>();
        List<string> PictureList = new List<string>();

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
            switch (Selected)
            {
                case 0:
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
                    break;
                case 1:
                    MessageBox.Show("SlideShow");
                    OpenFileDialog openFileDialogS = new OpenFileDialog
                    {
                        Title = "Choose Image",
                        Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.gif;*.apng",
                        Multiselect = true
                    };
                    var dialogS = openFileDialogS;
                    bool? resultS = dialogS.ShowDialog();
                    if (resultS == true)
                    {
                        PictureList.Clear();
                        PictureNameList.Clear();
                        int x = 0;
                        foreach(string Filename in dialogS.FileNames)
                        {
                            x++;
                            MessageBox.Show(Filename);
                            PictureList.Add(Filename);
                        }
                        foreach (string Filename in dialogS.SafeFileNames)
                        {
                            MessageBox.Show(Filename);
                            PictureNameList.Add(Filename);
                        }
                        if(x == 1)
                        {
                            MessageBox.Show("You need to select more than 1 image for the slideshow");
                            Change.IsEnabled = false;
                            break;
                        }
                        Stopped = false;
                        Change.IsEnabled = true;
                        BitmapImage btm = new BitmapImage(new Uri(dialogS.FileName));
                        Window2.Source = btm;
                        Window2.Stretch = Stretch.Uniform;
                        NoWallpaper.Visibility = Visibility.Collapsed;

                        //Resolution Stuff
                        var img = System.Drawing.Image.FromFile(dialogS.FileName);
                        var size = GetResolution.GetDisplayResolution();
                        if (img.Width != size.Width || img.Height != size.Height)
                        {
                            Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                            Warning.Visibility = Visibility.Visible;
                        }
                        else
                            Warning.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
            
            
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            switch (Selected)
            {
                case 0:
                    Change.IsEnabled = false;
                    File.Copy(FileLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
                    RefreshUI.SetWallpaper(FileLocation);
                    MessageBox.Show("Wallpaper Changed");
                    break;
                case 1:
                    Change.IsEnabled = false;
                    var SL = new SlideShowS();
                    SL.SlideShow(PictureNameList, PictureList);
                    break;
            }
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
