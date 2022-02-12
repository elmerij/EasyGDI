using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace bagitti
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("User32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        public Form1()
        {
            InitializeComponent();
        }
        bool watermarkenabled = false;
        bool textfollowingenabled = false;
        bool menuenabled = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.FromArgb(2, 2, 2);
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.WindowState = FormWindowState.Maximized;

            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            Timer Update = new Timer();
            Update.Interval = 1;
            Update.Tick += new EventHandler(Update_Tick);
            Update.Start();

            this.Paint += new PaintEventHandler(Drawing_Paint);
        }
        private void Drawing_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Courier New", 12f);
            
            
            if (watermarkenabled==true)
            {
                Font watermarkfont = new Font("Courier New", 12f);
                SizeF watermarksize = e.Graphics.MeasureString("EasyGDI", font);
                e.Graphics.DrawString("EasyGDI", font, Brushes.LimeGreen, 0, 0);
            }
            if (textfollowingenabled == true)
            {
                SizeF textSize = e.Graphics.MeasureString("EasyGDI", font);
                e.Graphics.DrawString("EasyGDI", font, Brushes.Aqua, new Point(Cursor.Position.X - (int)textSize.Width / 2, Cursor.Position.Y - (int)textSize.Height / 2));
            }
            //e.Graphics.DrawRectangle(Pens.Red, Cursor.Position.X - textSize.Width / 2, Cursor.Position.Y - textSize.Height / 2, textSize.Width, textSize.Height);
            //e.Graphics.DrawLine(Pens.Red, 0, 0, Cursor.Position.X, Cursor.Position.Y - textSize.Height / 2);
        }
            
        private void Update_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
            }
            catch { }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case 0x000F: // WM_PAINT
                    this.Refresh();
                    break;
            }
            base.WndProc(ref m);
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Left)
            {
                if (watermarkenabled==true)
                { 
                    label4.Text = "disabled";
                    label4.ForeColor = Color.Red;
                    watermarkenabled=false;
                }
                else
                {
                    label4.Text = "enabled";
                    label4.ForeColor = Color.Green;
                    watermarkenabled = true;
                }
            }
            if (e.KeyCode==Keys.Down)
            {
                if (textfollowingenabled == true)
                {
                    label5.Text = "disabled";
                    label5.ForeColor = Color.Red;
                    textfollowingenabled = false;
                }
                else
                {
                    label5.Text = "enabled";
                    label5.ForeColor = Color.Green;
                    textfollowingenabled = true;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (menuenabled == true)
                {
                    label9.Text = "disabled";
                    label9.ForeColor = Color.Red;
                    menuenabled = false;
                }
                else
                {
                    label9.Text = "enabled";
                    label9.ForeColor = Color.Green;
                    menuenabled = true;
                }
            }
            if (menuenabled==false)
            {
                this.Opacity = 0;
            }
            else
            {
                this.Opacity=1;
            }

        }
    }
}
