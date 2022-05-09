using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolWallpaperChanger.External_Functions
{
    internal class FormThread
    {
        public static Form1 form1 = new Form1();
        public static void Thread()
        {


            /*form.Load += new EventHandler((s, e) =>
            {
                RemoveWindowFromTaskbar(form.Handle);
                // Move the form right next to the in demo 1 drawn rectangle
                form.Width = Screen.PrimaryScreen.Bounds.Width + 22;
                form.Height = Screen.PrimaryScreen.Bounds.Height;
                form.Left = 0;
                form.Top = 0;
                
                form.FormBorderStyle = FormBorderStyle.None;

                // Add a randomly moving button to the form
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile($"{Environment.CurrentDirectory}\\pic.jpg");
                pictureBox.Width = Screen.PrimaryScreen.Bounds.Width;
                pictureBox.Height = Screen.PrimaryScreen.Bounds.Height;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                form.Controls.Add(pictureBox);

                // This line makes the form a child of the WorkerW window, 
                // thus putting it behind the desktop icons and out of reach 
                // for any user input. The form will just be rendered, no 
                // keyboard or mouse input will reach it. You would have to use 
                // WH_KEYBOARD_LL and WH_MOUSE_LL hooks to capture mouse and 
                // keyboard input and redirect it to the windows form manually, 
                // but that's another story, to be told at a later time.
                W32.SetParent(form.Handle, workerw);
            });*/
            form1.ShowDialog();
        }
    }
}
