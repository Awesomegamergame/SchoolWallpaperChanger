using System;
using System.Windows;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.ButtonFunctions
{
    internal class ButtonClick
    {
        public static void SlideShow()
        {
            Selected = 1;
            window.Change.Content = "Start";
            window.Select.Content = "Select Images";
            window.Picture.IsEnabled = true;
            window.SlideShow.IsEnabled = false;
            window.Change.IsEnabled = false;
            window.Warning.Visibility = Visibility.Collapsed;
            window.NoWallpaper.Visibility = Visibility.Visible;
            window.Window3.Visibility = Visibility.Visible;
            window.NoWallpaper.Content = "No Images Selected";
            window.TimeL.Visibility = Visibility.Visible;
            window.Time.Visibility = Visibility.Visible;
            settings.Write("Mode", "slideshow");
        }
        public static void Picture()
        {
            Selected = 0;
            window.Change.Content = "Change";
            window.Select.Content = "Select";
            window.Picture.IsEnabled = false;
            window.SlideShow.IsEnabled = true;
            window.Change.IsEnabled = false;
            window.Warning.Visibility = Visibility.Collapsed;
            window.NoWallpaper.Visibility = Visibility.Visible;
            window.Window3.Visibility = Visibility.Visible;
            window.TimeL.Visibility = Visibility.Collapsed;
            window.Time.Visibility = Visibility.Collapsed;
            window.NoWallpaper.Content = "No Wallpaper Selected";
            settings.Write("Mode", "picture");
        }
    }
}
