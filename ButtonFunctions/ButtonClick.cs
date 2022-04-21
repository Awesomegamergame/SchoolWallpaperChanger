using System.IO;
using System.Windows;
using SchoolWallpaperChanger.Functions;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.ButtonFunctions
{
    internal class ButtonClick
    {
        public static void SlideShow()
        {
            window.Warning.Visibility = Visibility.Collapsed;
            Selected = 1;
            window.Change.Content = "Start";
            window.Select.Content = "Select Images";
            window.Picture.IsEnabled = true;
            window.SlideShow.IsEnabled = false;
            if (File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow\0"))
            {
                var img = System.Drawing.Image.FromFile($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow\0");
                var size = UIFunctions.GetDisplayResolution();
                if (img.Width != size.Width || img.Height != size.Height)
                {
                    window.Warning.Content = $"Warning All Pictures Should Be {size.Width}x{size.Height}";
                    window.Warning.Visibility = Visibility.Visible;
                }
                img.Dispose();
                window.NoWallpaper.Content = "";
                window.Change.IsEnabled = true;
            }
            else
            {
                window.NoWallpaper.Content = "No Images Selected";
                window.Change.IsEnabled = false;
            }
            window.NoWallpaper.Visibility = Visibility.Visible;
            window.Window3.Visibility = Visibility.Visible;
            window.Window2.Visibility = Visibility.Collapsed;
            window.TimeL.Visibility = Visibility.Visible;
            window.Time.Visibility = Visibility.Visible;
            settings.Write("Mode", "slideshow");
        }
        public static void Picture()
        {
            window.Warning.Visibility = Visibility.Collapsed;
            if (File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0"))
            {
                var img = System.Drawing.Image.FromFile($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0");
                var size = UIFunctions.GetDisplayResolution();
                if (img.Width != size.Width || img.Height != size.Height)
                {
                    window.Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                    window.Warning.Visibility = Visibility.Visible;
                }
                img.Dispose();
                Selected = 2;
            }
            else
                Selected = 0;
            window.Change.Content = "Change";
            window.Select.Content = "Select";
            window.Picture.IsEnabled = false;
            if (File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0"))
            {
                window.NoWallpaper.Content = "";
                window.Change.IsEnabled = true;
            }
            else
            {
                window.NoWallpaper.Content = "No Wallpaper Selected";
                window.Change.IsEnabled = false;
            }
            window.SlideShow.IsEnabled = true;
            window.NoWallpaper.Visibility = Visibility.Visible;
            window.Window3.Visibility = Visibility.Collapsed;
            window.Window2.Visibility= Visibility.Visible;
            window.TimeL.Visibility = Visibility.Collapsed;
            window.Time.Visibility = Visibility.Collapsed;
            settings.Write("Mode", "picture");
        }
    }
}
