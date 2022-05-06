using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class SelectButton
    {
        public static void Select(int Selected)
        {
            switch (Selected)
            {
                case 0:
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Title = "Choose Image",
                        Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.gif;*.apng;"
                    };
                    var dialog = openFileDialog;
                    bool? result = dialog.ShowDialog();
                    if (result == true)
                    {
                        ChangeButton.PicLocation = dialog.FileName;
                        window.Change.IsEnabled = true;
                        var btm = new BitmapImage();
                        btm.BeginInit();
                        btm.UriSource = new Uri(dialog.FileName);
                        btm.DecodePixelHeight = 500;
                        btm.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        btm.CacheOption = BitmapCacheOption.OnLoad;
                        btm.EndInit();
                        window.Window2.Source = btm;
                        window.Window2.Stretch = Stretch.Uniform;
                        window.NoWallpaper.Visibility = Visibility.Collapsed;

                        //Resolution Stuff
                        var img = System.Drawing.Image.FromFile(dialog.FileName);
                        var size = UIFunctions.GetDisplayResolution();
                        if (img.Width != size.Width || img.Height != size.Height)
                        {
                            window.Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                            window.Warning.Visibility = Visibility.Visible;
                        }
                        else
                            window.Warning.Visibility = Visibility.Collapsed;
                        File.Copy(ChangeButton.PicLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
                        File.Copy(ChangeButton.PicLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\0", true);
                    }
                    break;
                case 1:
                    OpenFileDialog openFileDialogS = new OpenFileDialog
                    {
                        Title = "Choose Image",
                        Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.gif;*.apng;*.agg;",
                        Multiselect = true
                    };
                    var dialogS = openFileDialogS;
                    bool? resultS = dialogS.ShowDialog();
                    if (resultS == true)
                    {
                        SlideShowS.PictureList.Clear();
                        int x = 0;
                        foreach (string Filename in dialogS.FileNames)
                        {
                            x++;
                            SlideShowS.PictureList.Add(Filename);
                        }
                        if (x == 1)
                        {
                            MessageBox.Show("You need to select more than 1 image for the slideshow");
                            window.Change.IsEnabled = false;
                            break;
                        }
                        x = 0;
                        if (Directory.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow"))
                            SlideShowS.DeleteDirectory($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow");
                        if (!Directory.Exists($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow"))
                            Directory.CreateDirectory($@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow");
                        foreach (string PictureLocation in SlideShowS.PictureList)
                        {
                            File.Copy(PictureLocation, $@"{SlideShowS.AppDataPath}\Microsoft\Windows\Themes\SlideShow\{x}");
                            x++;
                        }
                        settings.Write("PictureCount", SlideShowS.PictureList.Count.ToString());
                        settings.Write("CurrentPicture", "0");
                        Stopped = false;
                        window.Change.IsEnabled = true;
                        var btm = new BitmapImage();
                        btm.BeginInit();
                        btm.UriSource = new Uri(dialogS.FileName);
                        btm.DecodePixelHeight = 500;
                        btm.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        btm.CacheOption = BitmapCacheOption.OnLoad;
                        btm.EndInit();
                        window.Window3.Source = btm;
                        window.Window3.Stretch = Stretch.Uniform;
                        window.NoWallpaper.Visibility = Visibility.Collapsed;

                        //Resolution Stuff
                        var img = System.Drawing.Image.FromFile(dialogS.FileName);
                        var size = UIFunctions.GetDisplayResolution();
                        if (img.Width != size.Width || img.Height != size.Height)
                        {
                            window.Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                            window.Warning.Visibility = Visibility.Visible;
                        }
                        else
                            window.Warning.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:
                    MainWindow.Selected = 0;
                    Select(MainWindow.Selected);
                    break;
            }
        }
    }
}
