using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class SelectButton
    {
        public static string FileLocation;
        public static List<string> PictureNameList = new List<string>();
        public static List<string> PictureList = new List<string>();
        public static void Select()
        {
            switch (Selected)
            {
                case 0:
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Title = "Choose Image",
                        Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.gif;*.apng;*.agg;"
                    };
                    var dialog = openFileDialog;
                    bool? result = dialog.ShowDialog();
                    if (result == true)
                    {
                        window.Window3.Visibility = Visibility.Collapsed;
                        FileLocation = dialog.FileName;
                        window.Change.IsEnabled = true;
                        BitmapImage btm = new BitmapImage(new Uri(dialog.FileName));
                        window.Window2.Source = btm;
                        window.Window2.Stretch = Stretch.Uniform;
                        window.NoWallpaper.Visibility = Visibility.Collapsed;

                        //Resolution Stuff
                        var img = System.Drawing.Image.FromFile(dialog.FileName);
                        var size = GetResolution.GetDisplayResolution();
                        if (img.Width != size.Width || img.Height != size.Height)
                        {
                            window.Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                            window.Warning.Visibility = Visibility.Visible;
                        }
                        else
                            window.Warning.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1:
                    MessageBox.Show("SlideShow");
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
                        window.Window3.Visibility = Visibility.Collapsed;
                        PictureList.Clear();
                        PictureNameList.Clear();
                        int x = 0;
                        foreach (string Filename in dialogS.FileNames)
                        {
                            x++;
                            MessageBox.Show(Filename);
                            PictureList.Add(Filename);
                        }
                        foreach (string Filename in dialogS.SafeFileNames)
                        {
                            MessageBox.Show(Filename);
                            PictureNameList.Add(Filename);
                        }
                        if (x == 1)
                        {
                            MessageBox.Show("You need to select more than 1 image for the slideshow");
                            window.Change.IsEnabled = false;
                            break;
                        }
                        Stopped = false;
                        window.Change.IsEnabled = true;
                        BitmapImage btm = new BitmapImage(new Uri(dialogS.FileName));
                        window.Window2.Source = btm;
                        window.Window2.Stretch = Stretch.Uniform;
                        window.NoWallpaper.Visibility = Visibility.Collapsed;

                        //Resolution Stuff
                        var img = System.Drawing.Image.FromFile(dialogS.FileName);
                        var size = GetResolution.GetDisplayResolution();
                        if (img.Width != size.Width || img.Height != size.Height)
                        {
                            window.Warning.Content = $"Warning Picture Should Be {size.Width}x{size.Height}";
                            window.Warning.Visibility = Visibility.Visible;
                        }
                        else
                            window.Warning.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
        }
    }
}
