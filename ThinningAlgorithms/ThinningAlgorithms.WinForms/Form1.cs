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
            System.Threading.Thread.Sleep(500);
            Bitmap b = (Bitmap)pictureBox1.Image;
            List<(int, int)> zwdeletable = new List<(int, int)>();
            bool d;
            int bp, cp;
            do
            {
                zwdeletable.Clear();
                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        //1)
                        if (b.GetPixel(i, j).ToArgb() != Color.Black.ToArgb())
                            continue;

                        //2)
                        bp = 0;
                        if (i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (j - 1 > 0 && i + 1 < b.Width && b.GetPixel(i + 1, j - 1).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (j - 1 > 0 && b.GetPixel(i, j - 1).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (i - 1 > 0 && j - 1 > 0 && b.GetPixel(i - 1, j - 1).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (i - 1 > 0 && b.GetPixel(i - 1, j).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (i - 1 > 0 && j + 1 < b.Height && b.GetPixel(i - 1, j + 1).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (j + 1 < b.Height && b.GetPixel(i, j + 1).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (i + 1 < b.Width && j + 1 < b.Height && b.GetPixel(i + 1, j + 1).ToArgb() == Color.Black.ToArgb())
                            bp++;
                        if (bp < 2 || bp > 6)
                            continue;

                        //3)
                        cp = 0;
                        if (i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.White.ToArgb())
                            if (j - 1 > 0 && i + 1 < b.Width && b.GetPixel(i + 1, j - 1).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (j - 1 > 0 && i + 1 < b.Width && b.GetPixel(i + 1, j - 1).ToArgb() == Color.White.ToArgb())
                            if (j - 1 > 0 && b.GetPixel(i, j - 1).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (j - 1 > 0 && b.GetPixel(i, j - 1).ToArgb() == Color.White.ToArgb())
                            if (i - 1 > 0 && j - 1 > 0 && b.GetPixel(i - 1, j - 1).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (i - 1 > 0 && j - 1 > 0 && b.GetPixel(i - 1, j - 1).ToArgb() == Color.White.ToArgb())
                            if (i - 1 > 0 && b.GetPixel(i - 1, j).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (i - 1 > 0 && b.GetPixel(i - 1, j).ToArgb() == Color.White.ToArgb())
                            if (i - 1 > 0 && j + 1 < b.Height && b.GetPixel(i - 1, j + 1).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (i - 1 > 0 && j + 1 < b.Height && b.GetPixel(i - 1, j + 1).ToArgb() == Color.White.ToArgb())
                            if (j + 1 < b.Height && b.GetPixel(i, j + 1).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (j + 1 < b.Height && b.GetPixel(i, j + 1).ToArgb() == Color.White.ToArgb())
                            if (i + 1 < b.Width && j + 1 < b.Height && b.GetPixel(i + 1, j + 1).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (i + 1 < b.Width && j + 1 < b.Height && b.GetPixel(i + 1, j + 1).ToArgb() == Color.White.ToArgb())
                            if (i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.Black.ToArgb())
                                cp++;
                        if (cp != 1)
                            continue;

                        //4)
                        d = true;
                        if (i + 1 >= b.Width || (i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.White.ToArgb()))
                            if (j - 1 < 0 || (j - 1 > 0 && b.GetPixel(i, j - 1).ToArgb() == Color.White.ToArgb()))
                                if (i - 1 < 0 || (i - 1 > 0 && b.GetPixel(i - 1, j).ToArgb() == Color.White.ToArgb()))
                                    d = false;
                        if (d)
                            if (j - 2 > 0 && b.GetPixel(i, j - 2).ToArgb() == Color.Black.ToArgb())
                                d = false;
                        if (d)
                            continue;

                        //5)
                        d = true;
                        if (i + 1 >= b.Width || (i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.White.ToArgb()))
                            if (j - 1 < 0 || (j - 1 > 0 && b.GetPixel(i, j - 1).ToArgb() == Color.White.ToArgb()))
                                if (i + 1 >= b.Width || (i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.White.ToArgb()))
                                    d = false;
                        if (d)
                            if (i + 2 < b.Width && b.GetPixel(i + 2, j).ToArgb() == Color.Black.ToArgb())
                                d = false;
                        if (d)
                            continue;

                        zwdeletable.Add((i, j));
                    }
                }
                foreach ((int, int) p in zwdeletable)
                {
                    b.SetPixel(p.Item1, p.Item2, Color.White);
                }

                pictureBox1.Image = b;
                pictureBox1.Refresh();
                System.Threading.Thread.Sleep(500);
            } while (zwdeletable.Count != 0);
            pictureBox1.Image = b;
            pictureBox1.Refresh();
        }

        private void binImage()
        {
            Bitmap b = (Bitmap)pictureBox1.Image;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    int n = (b.GetPixel(i, j).R + b.GetPixel(i, j).G + b.GetPixel(i, j).B) / 3;
                    b.SetPixel(i, j, Color.FromArgb(n, n, n));
                }
            }
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    if (b.GetPixel(i, j).R < 100)
                    {
                        b.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        b.SetPixel(i, j, Color.White);
                    }
                }
            }
            pictureBox1.Image = b;
            pictureBox1.Refresh();
        }
    }
}
