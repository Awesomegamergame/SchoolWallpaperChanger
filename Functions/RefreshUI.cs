using System.Runtime.InteropServices;

namespace SchoolWallpaperChanger.Functions
{
    internal class RefreshUI
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public static void SetWallpaper(string thePath)
        {
            SystemParametersInfo(20, 0, thePath, 4);
        }
    }
}
