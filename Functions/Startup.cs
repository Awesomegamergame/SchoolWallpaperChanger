using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class Startup
    {
        public static int startup = 0;
        public static void Start()
        {
            if(File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0"))
                SetPicture();
            if (File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow\0"))
                SetSlideShow();
        }
        #region Setup preview and locations
        public static void SetPicture()
        {
            Selected = 2;
            var btm = new BitmapImage();
            btm.BeginInit();
            btm.DecodePixelHeight = 500;
            btm.UriSource = new Uri($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0");
            btm.CacheOption = BitmapCacheOption.OnLoad;
            btm.EndInit();
            window.Window2.Source = btm;
            ChangeButton.PicLocation = $@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0";
            window.Window2.Stretch = Stretch.Uniform;

            //Resolution Stuff
            var img = System.Drawing.Image.FromFile($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0");
            var size = UIFunctions.GetDisplayResolution();
            if (img.Width != size.Width || img.Height != size.Height)
            {
                window.Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                window.Warning.Visibility = Visibility.Visible;
            }
            else
                window.Warning.Visibility = Visibility.Collapsed;
        }
        public static void SetSlideShow()
        {
            var btm = new BitmapImage();
            btm.BeginInit();
            btm.DecodePixelHeight = 500;
            btm.UriSource = new Uri($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow\0");
            btm.CacheOption = BitmapCacheOption.OnLoad;
            btm.EndInit();
            window.Window3.Source = btm;
            ChangeButton.FileLocation = $@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow\0";
            window.Window3.Stretch = Stretch.Uniform;

            //Resolution Stuff
            var img = System.Drawing.Image.FromFile($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow\0");
            var size = UIFunctions.GetDisplayResolution();
            if (img.Width != size.Width || img.Height != size.Height)
            {
                window.Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                window.Warning.Visibility = Visibility.Visible;
            }
            else
                window.Warning.Visibility = Visibility.Collapsed;
        }
        #endregion
        public static void StartupM()
        {
            startup = 1;
            if (settings.KeyExists("Mode"))
            {
                if (settings.Read("Mode").Equals("slideshow"))
                    Selected = 1;
                else 
                    Selected = 2;
            }
            window.WindowState = WindowState.Minimized;
            ni.Visible = true;
            ChangeButton.Change(Selected);
        }
    }
}
