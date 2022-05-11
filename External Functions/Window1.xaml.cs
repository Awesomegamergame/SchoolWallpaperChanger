using DrawBehindDesktopIcons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchoolWallpaperChanger.External_Functions
{
    public partial class Window1 : Window
    {
        public Image image;
        public Window1()
        {
            InitializeComponent();
            Start();
            Loaded += MyWindow_Loaded;
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e) 
        {
            Program.RemoveWindowFromTaskbar(new WindowInteropHelper(this).Handle);

            W32.SetParent(new WindowInteropHelper(this).Handle, Program.workerw);
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
