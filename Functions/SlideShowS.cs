using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class SlideShowS
    {
        public int y = 1;
        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static Timer aTimer = new Timer();
        public static List<string> PictureList = new List<string>();
        public void SlideShow(int Time, int CP)
        {
            int x;
            window.Picture.IsEnabled = false;
            window.Stop.Visibility = Visibility.Visible;
            window.Change.Visibility = Visibility.Collapsed;
            window.Select.IsEnabled = false;
            if (CP <= -1)
                x = 0;
            else
                x = CP;
            if (!Directory.Exists($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow"))
                return;
            File.Copy($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{x}", $@"{AppDataPath}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
            UIFunctions.SetWallpaper($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{x}");
            settings.Write("Timer", (Time / 1000).ToString());
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = Time;
            aTimer.Enabled = true;
        }
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (y >= int.Parse(settings.Read("PictureCount")))
                y = 0;
            File.Copy($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{y}", $@"{AppDataPath}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
            UIFunctions.SetWallpaper($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{y}");
            settings.Write("CurrentPicture", y.ToString());
            y++;
        }

        public static void End()
        {
            aTimer.Close();
            Stopped = true;
            window.Time.IsEnabled = true;
            window.Change.Visibility = Visibility.Visible;
            window.Stop.Visibility = Visibility.Collapsed;
            window.Select.Visibility = Visibility.Visible;
            window.SlideShow.IsEnabled = false;
            window.Picture.IsEnabled = true;
            window.Change.IsEnabled = true;
            window.Select.IsEnabled = true;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}
