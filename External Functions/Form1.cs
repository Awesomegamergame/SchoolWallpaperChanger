using DrawBehindDesktopIcons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolWallpaperChanger.External_Functions
{
    public partial class Form1 : Form
    {
        public PictureBox pictureBox = new PictureBox();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.RemoveWindowFromTaskbar(Handle);
            // Move the form right next to the in demo 1 drawn rectangle
            Width = Screen.PrimaryScreen.Bounds.Width + 22;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Left = 0;
            Top = 0;

            FormBorderStyle = FormBorderStyle.None;

            // Add a randomly moving button to the form
            
            pictureBox.Name = "wallpaper";
            pictureBox.Image = Image.FromFile($"{Environment.CurrentDirectory}\\pic.jpg");
            pictureBox.Width = Screen.PrimaryScreen.Bounds.Width;
            pictureBox.Height = Screen.PrimaryScreen.Bounds.Height;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(pictureBox);

            // This line makes the form a child of the WorkerW window, 
            // thus putting it behind the desktop icons and out of reach 
            // for any user input. The form will just be rendered, no 
            // keyboard or mouse input will reach it. You would have to use 
            // WH_KEYBOARD_LL and WH_MOUSE_LL hooks to capture mouse and 
            // keyboard input and redirect it to the windows form manually, 
            // but that's another story, to be told at a later time.
            W32.SetParent(Handle, Program.workerw);
        }
    }
}
