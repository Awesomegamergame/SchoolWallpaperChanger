using System;
using System.IO;
using System.Windows;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class ChangeButton
    {
        public static void Change()
        {
            switch (Selected)
            {
                case 0:
                    window.Change.IsEnabled = false;
                    File.Copy(SelectButton.FileLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
                    RefreshUI.SetWallpaper(SelectButton.FileLocation);
                    MessageBox.Show("Wallpaper Changed");
                    break;
                case 1:
                    window.Change.IsEnabled = false;
                    var SL = new SlideShowS();
                    SL.SlideShow(SelectButton.PictureNameList, SelectButton.PictureList);
                    break;
            }
        }
    }
}
