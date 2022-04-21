using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SchoolWallpaperChanger.Functions
{
    internal class UIFunctions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public static void SetWallpaper(string thePath)
        {
            SystemParametersInfo(20, 0, thePath, 4);
        }
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);
        public static double GetWindowsScreenScalingFactor(bool percentage = true)
        {
            Graphics GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr DeviceContextHandle = GraphicsObject.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(DeviceContextHandle, 10);
            int PhysicalScreenHeight = GetDeviceCaps(DeviceContextHandle, 117);
            double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);
            if (percentage)
            {
                ScreenScalingFactor *= 100.0;
            }
            GraphicsObject.ReleaseHdc(DeviceContextHandle);
            GraphicsObject.Dispose();
            return ScreenScalingFactor;
        }
        public static Size GetDisplayResolution()
        {
            var sf = GetWindowsScreenScalingFactor(false);
            var screenWidth = Screen.PrimaryScreen.Bounds.Width * sf;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height * sf;
            return new Size((int)screenWidth, (int)screenHeight);
        }
    }
}