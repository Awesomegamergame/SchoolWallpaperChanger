using System;
using System.Windows.Media.Imaging;
using SchoolWallpaperChanger.Windows;
using SchoolWallpaperChanger.ExternalFunctions;

namespace SchoolWallpaperChanger.Functions
{
    class WindowThread
    {
        public static DesktopWindow window1;
        public static IntPtr workerw = IntPtr.Zero;
        public static void Start(string pictureLocation)
        {
            IntPtr progman = NativeMethods.FindWindow("Progman", null);
            IntPtr result = IntPtr.Zero;

            NativeMethods.SendMessageTimeout(progman, 0x052C, new IntPtr(0), IntPtr.Zero, NativeMethods.SendMessageTimeoutFlags.SMTO_NORMAL, 1000, out result);
            NativeMethods.EnumWindows(new NativeMethods.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = NativeMethods.FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero);
                if (p != IntPtr.Zero)
                {
                    workerw = NativeMethods.FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", IntPtr.Zero);
                }
                return true;
            }), IntPtr.Zero);
            ScreenRes.SetDpiAwareness();
            window1 = new DesktopWindow();
            window1.Show();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(pictureLocation);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            window1.image.Source = bitmap;
            ChangeButton.stop = true;
            ChangeButton.EnableUI();
        }
    }
}
