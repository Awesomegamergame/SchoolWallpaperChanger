using System;
using System.IO;
using System.Windows;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class ChangeButton
    {
        public static string FileLocation;
        public static void Change(int Selected)
        {
            switch (Selected)
            {
                case 0:
                    window.Change.IsEnabled = false;
                    File.Copy(FileLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
                    File.Copy(FileLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\0", true);
                    UIFunctions.SetWallpaper(FileLocation);
                    MessageBox.Show("Wallpaper Changed");
                    break;
                case 1:
                    if(window.Time.Text.Equals(null) || window.Time.Text.Equals(""))
                    {
                        MessageBox.Show("Time cant be empty");
                        break;
                    }
                    int time = int.Parse(window.Time.Text);
                    if(time < 5)
                    {
                        MessageBox.Show("Time cant be less than 5 seconds");
                        break;
                    }
                    window.Change.IsEnabled = false;
                    time *= 1000;
                    window.Time.IsEnabled = false;
                    var SL = new SlideShowS();
                    SL.SlideShow(time);
                    break;
                case 2:
                    window.Change.IsEnabled = false;
                    File.Copy(FileLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
                    UIFunctions.SetWallpaper(FileLocation);
                    MessageBox.Show("Wallpaper Changed");
                    MainWindow.Selected = 0;
                    break;
            }
        }
    }
}
