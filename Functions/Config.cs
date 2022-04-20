using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using SchoolWallpaperChanger.ButtonFunctions;

namespace SchoolWallpaperChanger.Functions
{
    public class IniFile
    {
        readonly string Path;
        readonly string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        
        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }
        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
    public class Config
    {
        public static void NewConfig()
        {
            MainWindow.settings.Write("Mode", "picture");
            MainWindow.settings.Write("Timer", "5");
            if (Directory.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow"))
            {
                string[] arr = Directory.GetFiles($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow");
                MainWindow.settings.Write("PictureCount", arr.Length.ToString());
                MainWindow.settings.Write("CurrentPicture", "0");
            }
            ReadConfig();
            Startup.Start();
        }
        public static void ReadConfig()
        {
            if (MainWindow.settings.Read("Mode").Equals("slideshow"))
                ButtonClick.SlideShow();
            else if (MainWindow.settings.Read("Mode").Equals("picture"))
                ButtonClick.Picture();
            if (MainWindow.settings.KeyExists("Timer"))
                MainWindow.window.Time.Text = MainWindow.settings.Read("Timer");
        }
    }
}