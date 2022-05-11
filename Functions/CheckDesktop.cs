using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;

namespace SchoolWallpaperChanger.Functions
{
    internal class CheckDesktop
    {
        public static void Check()
        {
            Thread.Sleep(5500);
            if (!Directory.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\CachedFiles\"))
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    DrawBehindDesktopIcons.Program.Start(ChangeButton.PicLocation);
                });
            }
            else
            {
                string[] pictures = Directory.GetFiles($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\CachedFiles\");
                if (pictures.Length == 0)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        DrawBehindDesktopIcons.Program.Start(ChangeButton.PicLocation);
                    });
                }
                foreach (string picture in pictures)
                {
                    Bitmap bmp = new Bitmap(picture);
                    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

                    IntPtr ptr = bmpData.Scan0;

                    int bytes = bmpData.Stride * bmp.Height;
                    byte[] rgbValues = new byte[bytes];

                    Marshal.Copy(ptr, rgbValues, 0, bytes);

                    bool allBlack = true;
                    for (int index = 0; index < rgbValues.Length; index++)
                    {
                        if (rgbValues[index] != 0)
                        {
                            allBlack = false;
                        }
                    }
                    bmp.UnlockBits(bmpData);
                    bmp.Dispose();
                    if (allBlack)
                    {
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            DrawBehindDesktopIcons.Program.Start(ChangeButton.PicLocation);
                        });
                    }
                }
            }
        }
    }
}
