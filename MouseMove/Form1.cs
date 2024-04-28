using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseMove
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                notifyIcon1.BalloonTipText = "Uygulama Küçültüldü";
                var zmn = Properties.Settings.Default["Zaman"];
                var appName = Properties.Settings.Default["AppName"];
                notifyIcon1.BalloonTipTitle = appName.ToString();
                notifyIcon1.Text = appName.ToString();
                var welMsg = Properties.Settings.Default["welcomeMsg"];
                this.Text = appName.ToString();
                Properties.Settings.Default.Save(); // Saves settings in application configuration file
                MessageBox.Show(zmn.ToString() + " mili saniyede bir mouse hareket eder. " + welMsg.ToString(),"Hoşgeldin Mesajı");
                timer1.Interval = Convert.ToInt32(zmn.ToString());
                timer1.Start();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bir hata oluştu Hata Mesajı: " + ex.Message.ToString());
            }


        }
        private void MoveCursor()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - 5, Cursor.Position.Y - 5);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
           
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now + " " + timer1.Interval.ToString());
            MoveCursor();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
