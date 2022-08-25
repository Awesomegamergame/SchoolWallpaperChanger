using System;
using System.IO;
using System.Windows;
using System.Threading;
using static SchoolWallpaperChanger.MainWindow;
using Windows.Native;

namespace SchoolWallpaperChanger.Functions
{
    internal class ChangeButton
    {
        public static string FileLocation;
        public static string PicLocation;
        public static Thread thread;
        public static bool stop = false;
        public static void Change(int Selected)
        {
            if (stop)
                WindowThread.window1.Close();
            GC.Collect();
            thread = new Thread(CheckDesktop.Check);
            IActiveDesktop iad = shlobj.GetActiveDesktop();
            switch (Selected)
            {
                case 0:
                    DisableUI();
                    iad.SetWallpaper(FileLocation, 0);
                    iad.ApplyChanges(AD_Apply.ALL | AD_Apply.FORCE | AD_Apply.BUFFERED_REFRESH);
                    thread.Start();
                    MainWindow.Selected = 2;
                    break;
                case 1:
                    if(window.Time.Text.Equals(null) || window.Time.Text.Equals(""))
                    {
                        MessageBox.Show("Time cant be empty");
                        break;
                    }
                    if (FileLocation == null)
                        break;
                    int time = int.Parse(window.Time.Text);
                    if(time < 5)
                    {
                        MessageBox.Show("Time cant be less than 5 seconds");
                        break;
                    }
                    window.Change.IsEnabled = false;
                    time *= 1000;
                    window.Time.IsEnabled = false;
                    int CP = 0;
                    if (int.Parse(settings.Read("CurrentPicture")) != CP && int.Parse(settings.Read("CurrentPicture")) < int.Parse(settings.Read("PictureCount")))
                        CP = int.Parse(settings.Read("CurrentPicture"));
                    var SL = new SlideShowS();
                    SL.SlideShow(time, CP);
                    break;
                case 2:
                    if (!File.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\0"))
                        break;
                    File.Copy(PicLocation, $@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
                    DisableUI();
                    iad.SetWallpaper(FileLocation, 0);
                    iad.ApplyChanges(AD_Apply.ALL | AD_Apply.FORCE | AD_Apply.BUFFERED_REFRESH);
                    thread.Start();
                    break;
            }
        }
        public static void DisableUI()
        {
            window.Change.IsEnabled = false;
            window.Select.IsEnabled = false;
            window.Picture.IsEnabled = false;
            window.SlideShow.IsEnabled = false;
            window.SettingL.Visibility = Visibility.Visible;
        }
        public static void EnableUI()
        {
            window.Change.IsEnabled = true;
            window.Select.IsEnabled = true;
            window.SlideShow.IsEnabled = true;
            window.SettingL.Visibility = Visibility.Collapsed;
        }
    }
}
