using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Windows;

namespace SchoolWallpaperChanger.Functions
{
    internal class SlideShowS
    {
        public static int x;
        public static int y;
        public static List<string> PictureNameList = new List<string>();
        public static List<string> PictureList = new List<string>();
        public static void SlideShow()
        {
            x = 0;
            y = 0;
            if (Directory.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\SlideShow"))
                DeleteDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\SlideShow");
            if (!Directory.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\SlideShow"))
                Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\SlideShow");
            foreach (string PictureLocation in PictureList)
            {
                File.Copy(PictureLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\SlideShow\{PictureNameList[x]}");
                x++;
            }
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (y >= PictureNameList.Count)
                y = 0;
            MessageBox.Show("Time");
            string Picture = PictureNameList[y];
            File.Copy($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\SlideShow\{Picture}", $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
            RefreshUI.SetWallpaper("");
            y++;
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
