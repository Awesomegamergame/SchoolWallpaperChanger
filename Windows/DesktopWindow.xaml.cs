using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using SchoolWallpaperChanger.Functions;
using SchoolWallpaperChanger.ExternalFunctions;

namespace SchoolWallpaperChanger.Windows
{
    public partial class DesktopWindow : Window
    {
        public Image image;
        public DesktopWindow()
        {
            InitializeComponent();
            Start();
            Loaded += MyWindow_Loaded;
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e) 
        {
            NativeMethods.RemoveWindowFromTaskbar(new WindowInteropHelper(this).Handle);
            NativeMethods.SetParent(new WindowInteropHelper(this).Handle, WindowThread.workerw);
        }
        public void Start()
        {
            Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            ShowInTaskbar = true;
            ResizeMode = ResizeMode.NoResize;
            WindowState = WindowState.Minimized;
            WindowStyle = WindowStyle.None;
            Top = 0;
            Left = 0;
        }
        private void img_Initialized(object sender, EventArgs e)
        {
            image = (Image)sender;
        }
    }
}
