using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThinningAlgorithms.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG|*.jpg|Bitmap|*.bmp|Gif|*.gif|PNG|*.png";
            openFileDialog1.Title = "Load image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.OpenFile());
                pictureBox1.Invalidate();
            }
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            binImage();
            if (modK3MBtn.Checked)
            {
                ModifiedK3M();
            }
            else if (zhangWangBtn.Checked)
            {
                ZhangAndWang();
            }
                
        }

        private void ModifiedK3M()
        {

        }

        private void ZhangAndWang()
        {

        }

        private void binImage()
        {
            Bitmap b = (Bitmap)pictureBox1.Image;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    if (b.GetPixel(i, j).R > 100)
                    {
                        b.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        b.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            pictureBox1.Image = b;
            pictureBox1.Invalidate();
        }
    }
}
