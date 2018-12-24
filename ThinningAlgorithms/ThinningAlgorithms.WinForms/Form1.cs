using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ThinningAlgorithms.WinForms
{
    public partial class Form1 : Form
    {
        int stopValue = 50;
        int saveValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.OpenFile());
                pictureBox1.Invalidate();
            }
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;

            saveValue = 0;
            binImage();
            Bitmap b = new Bitmap(1,1);
            if (modK3MBtn.Checked)
            {
                b = ModifiedK3M();
            }
            else if (K3MBtn.Checked)
            {
                b = K3M();
            }
            else if (zhangWangBtn.Checked)
            {
                b = ZhangAndWang();
            }
            else if (KMMBtn.Checked)
            {
                b = KMM();
            }
            pictureBox1.Image = b;
            pictureBox1.Refresh();
        }

        private int CalculateWeight(int i, int j, Bitmap b)
        {
            int[] N = new int[] { 128, 1, 2, 64, 0, 4, 32, 16, 8 };
            int weight = 0;
            if (i - 1 > 0 && j - 1 > 0 && b.GetPixel(i - 1, j - 1).ToArgb() == Color.Black.ToArgb())
                weight += N[0];
            if (j - 1 > 0 && b.GetPixel(i, j - 1).ToArgb() == Color.Black.ToArgb())
                weight += N[1];
            if (i + 1 < b.Width && j - 1 > 0 && b.GetPixel(i + 1, j - 1).ToArgb() == Color.Black.ToArgb())
                weight += N[2];
            if (i - 1 > 0 && b.GetPixel(i - 1, j).ToArgb() == Color.Black.ToArgb())
                weight += N[3];
            if (i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.Black.ToArgb())
                weight += N[5];
            if (i - 1 > 0 && j + 1 < b.Height && b.GetPixel(i - 1, j + 1).ToArgb() == Color.Black.ToArgb())
                weight += N[6];
            if (j + 1 < b.Height && b.GetPixel(i, j + 1).ToArgb() == Color.Black.ToArgb())
                weight += N[7];
            if (i + 1 < b.Width && j + 1 < b.Height && b.GetPixel(i + 1, j + 1).ToArgb() == Color.Black.ToArgb())
                weight += N[8];
            return weight;
        }

        private int ModifiedK3MCalculateWeight2(int i, int j, Bitmap b, List<(int, int)> marked)
        {
            int[] N = new int[] { 128, 1, 2, 64, 0, 4, 32, 16, 8 };
            int weight = 0;
            if (marked.Contains((i - 1, j - 1)) && i - 1 > 0 && j - 1 > 0 && b.GetPixel(i - 1, j - 1).ToArgb() == Color.Black.ToArgb())
                weight += N[0];
            if (marked.Contains((i, j - 1)) && j - 1 > 0 && b.GetPixel(i, j - 1).ToArgb() == Color.Black.ToArgb())
                weight += N[1];
            if (marked.Contains((i + 1, j - 1)) && i + 1 < b.Width && j - 1 > 0 && b.GetPixel(i + 1, j - 1).ToArgb() == Color.Black.ToArgb())
                weight += N[2];
            if (marked.Contains((i - 1, j)) && i - 1 > 0 && b.GetPixel(i - 1, j).ToArgb() == Color.Black.ToArgb())
                weight += N[3];
            if (marked.Contains((i + 1, j)) && i + 1 < b.Width && b.GetPixel(i + 1, j).ToArgb() == Color.Black.ToArgb())
                weight += N[5];
            if (marked.Contains((i - 1, j + 1)) && i - 1 > 0 && j + 1 < b.Height && b.GetPixel(i - 1, j + 1).ToArgb() == Color.Black.ToArgb())
                weight += N[6];
            if (marked.Contains((i, j + 1)) && j + 1 < b.Height && b.GetPixel(i, j + 1).ToArgb() == Color.Black.ToArgb())
                weight += N[7];
            if (marked.Contains((i + 1, j + 1)) && i + 1 < b.Width && j + 1 < b.Height && b.GetPixel(i + 1, j + 1).ToArgb() == Color.Black.ToArgb())
                weight += N[8];
            return weight;
        }

        private Bitmap K3M()
        {
            int[] A0 = new int[] { 3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60,
                                    62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135,
                                    143, 159, 191, 192, 193, 195, 199, 207, 223, 224,
                                    225, 227, 231, 239, 240, 241, 243, 247, 248, 249,
                                    251, 252, 253, 254 };
            int[] A1 = new int[] { 7, 14, 28, 56, 112, 131, 193, 224 };
            int[] A2 = new int[] { 7, 14, 15, 28, 30, 56, 60, 112, 120, 131, 135,
                                    193, 195, 224, 225, 240 };
            int[] A3 = new int[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 112, 120,
                                    124, 131, 135, 143, 193, 195, 199, 224, 225, 227,
                                    240, 241, 248 };
            int[] A4 = new int[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120,
                                    124, 126, 131, 135, 143, 159, 193, 195, 199, 207,
                                    224, 225, 227, 231, 240, 241, 243, 248, 249, 252 };
            int[] A5 = new int[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120,
                                    124, 126, 131, 135, 143, 159, 191, 193, 195, 199,
                                    207, 224, 225, 227, 231, 239, 240, 241, 243, 248,
                                    249, 251, 252, 254 };
            int[] A1px = new int[] { 3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56,
                                    60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131,
                                    135, 143, 159, 191, 192, 193, 195, 199, 207, 223,
                                    224, 225, 227, 231, 239, 240, 241, 243, 247, 248,
                                    249, 251, 252, 253, 254 };

            if (stopCheckBox.Checked) System.Threading.Thread.Sleep(stopValue);
            Bitmap b = (Bitmap)pictureBox1.Image;
            Image saveImage = b;
            if (saveCheckBox.Checked)
            {
                saveImage.Save("K3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                saveValue++;
            }
            int weight;
            bool change;
            List<(int, int)> marked = new List<(int, int)>();
            List<(int, int)> changed = new List<(int, int)>();
            do
            {
                change = false;
                for (int i = 0; i < b.Width; i++) //Phase 0 - Mark
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (b.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                        {
                            weight = CalculateWeight(i, j, b);
                            if (A0.Contains(weight))
                                marked.Add((i, j));
                        }
                    }
                }
                foreach ((int, int) p in marked) //Phase 1 - Delete 3
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A1.Contains(weight))
                    {
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        changed.Add(p);
                        change = true;
                    }
                }
                foreach ((int, int) p in changed)
                {
                    marked.Remove(p);
                }
                changed.Clear();
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("K3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 2 - Delete 3/4
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A2.Contains(weight))
                    {
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        changed.Add(p);
                        change = true;
                    }
                }
                foreach ((int, int) p in changed)
                {
                    marked.Remove(p);
                }
                changed.Clear();
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("K3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 3 - Delete 3/4/5
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A3.Contains(weight))
                    {
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        changed.Add(p);
                        change = true;
                    }
                }
                foreach ((int, int) p in changed)
                {
                    marked.Remove(p);
                }
                changed.Clear();
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("K3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 4 - Delete 3/4/5/6
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A4.Contains(weight))
                    {
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        changed.Add(p);
                        change = true;
                    }
                }
                foreach ((int, int) p in changed)
                {
                    marked.Remove(p);
                }
                changed.Clear();
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("K3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 5 - Delete 3/4/5/6/7
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A5.Contains(weight))
                    {
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        changed.Add(p);
                        change = true;
                    }
                }
                foreach ((int, int) p in changed)
                {
                    marked.Remove(p);
                }
                changed.Clear();
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("K3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                marked.Clear(); //Phase 6 - Unmark
            } while (change);
            for (int i = 0; i < b.Width; i++) //1 pixel Phase
            {
                for (int j = 0; j < b.Height; j++)
                {
                    weight = CalculateWeight(i, j, b);
                    if (A1px.Contains(weight))
                    {
                        b.SetPixel(i, j, Color.White);
                    }
                }
            }
            if (saveCheckBox.Checked)
            {
                saveImage = b;
                saveImage.Save("K3M" + saveValue.ToString() + ".png", ImageFormat.Png);
            }
            return b;
        }

        private Bitmap ModifiedK3M()
        {
            int[] A0 = new int[] { 3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60,
                                    62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135,
                                    143, 159, 191, 192, 193, 195, 199, 207, 223, 224,
                                    225, 227, 231, 239, 240, 241, 243, 247, 248, 249,
                                    251, 252, 253, 254 };
            int[] A0a = new int[] { 31, 124 };
            int[] A1 = new int[] { 7, 14, 28, 56, 112, 131, 193, 224 };
            int[] A2 = new int[] { 7, 14, 15, 24, 28, 30, 48, 56, 60, 112, 120, 131,
                                    135, 192, 193, 195, 224, 225, 240 };
            int[] A3 = new int[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 112, 120,
                                    124, 131, 135, 143, 193, 195, 199, 224, 225, 227,
                                    240, 241, 248 };
            int[] A4 = new int[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120,
                                    124, 126, 131, 135, 143, 159, 193, 195, 199, 207,
                                    224, 225, 227, 231, 240, 241, 243, 248, 249, 252 };
            int[] A5 = new int[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120,
                                    124, 126, 131, 135, 143, 159, 191, 193, 195, 199,
                                    207, 224, 225, 227, 231, 239, 240, 241, 243, 248,
                                    249, 251, 252, 254 };
            int[] A1px = new int[] { 2, 5, 13, 20, 21, 22, 32, 48, 52, 54, 65, 67,
                                    69, 80, 81, 84, 88, 97, 99, 128, 133, 141, 208, 216 };

            if (stopCheckBox.Checked) System.Threading.Thread.Sleep(stopValue);
            Bitmap b = (Bitmap)pictureBox1.Image;
            Image saveImage = b;
            if (saveCheckBox.Checked)
            { 
                saveImage.Save("ModK3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                saveValue++;
            }
            int weight;
            bool change;
            List<(int, int)> marked = new List<(int, int)>();
            List<(int, int)> markedForDeletion = new List<(int, int)>();
            do
            {
                change = false;
                for (int i = 0; i < b.Width; i++) //Phase 0 - Mark
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (b.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                        {
                            weight = CalculateWeight(i, j, b);
                            if (A0.Contains(weight))
                            {
                                marked.Add((i, j));
                                if (weight == 193 && i - 1 > 0 && j - 1 > 0 && !marked.Contains((i - 1, j - 1)) && b.GetPixel(i - 1, j - 1).ToArgb() == Color.Black.ToArgb())
                                    marked.Add((i - 1, j - 1));
                            }
                            else if ((weight == 95 && i - 2 > 0 && b.GetPixel(i - 2, j).ToArgb() == Color.White.ToArgb())
                                || (weight == 125 && j - 2 > 0 && b.GetPixel(i, j - 2).ToArgb() == Color.White.ToArgb()) 
                                || (weight == 215 && i + 2 < b.Width && b.GetPixel(i + 2, j).ToArgb() == Color.White.ToArgb())
                                || (weight == 245 && j + 2 < b.Height && b.GetPixel(i, j + 2).ToArgb() == Color.White.ToArgb()))
                            {
                                marked.Add((i, j));
                            }
                        }
                    }
                }
                for (int i = 0; i < b.Width; i++) //Phase0a - Adjust Border
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (marked.Contains((i, j)))
                            continue;
                        weight = ModifiedK3MCalculateWeight2(i, j, b, marked);
                        if (A0a.Contains(weight))
                            marked.Add((i, j));
                    }
                }
                foreach ((int, int) p in marked) //Phase 1 - Delete 3
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A1.Contains(weight) || markedForDeletion.Contains(p))
                    {
                        if (weight == 241 && p.Item2 + 2 < b.Height && p.Item1 + 1 < b.Width 
                            && marked.Contains((p.Item1, p.Item2 + 1))
                            && b.GetPixel(p.Item1, p.Item2 + 2).ToArgb() == Color.White.ToArgb()
                            && b.GetPixel(p.Item1 + 1, p.Item2 + 2).ToArgb() == Color.White.ToArgb())
                        {
                            weight = CalculateWeight(p.Item1, p.Item2 + 1, b);
                            if (A1.Contains(weight))
                            {
                                b.SetPixel(p.Item1, p.Item2 + 1, Color.White);
                            }
                        }
                        else if ((weight == 195 || weight == 227) 
                            && p.Item1 + 1 < b.Width && p.Item2 - 1 > 0 && marked.Contains((p.Item1 + 1, p.Item2 - 1)))
                        {
                            weight = CalculateWeight(p.Item1 + 1, p.Item2 - 1, b);
                            if (A1.Contains(weight))
                            {
                                markedForDeletion.Add((p.Item1 + 1, p.Item2 - 1));
                            }
                        }
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        if (markedForDeletion.Contains(p))
                            markedForDeletion.Remove(p);
                        change = true;
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 2 - Delete 3/4
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A2.Contains(weight) || markedForDeletion.Contains(p))
                    {
                        if (weight == 241 && p.Item2 + 2 < b.Height && p.Item1 + 1 < b.Width
                            && marked.Contains((p.Item1, p.Item2 + 1))
                            && b.GetPixel(p.Item1, p.Item2 + 2).ToArgb() == Color.White.ToArgb()
                            && b.GetPixel(p.Item1 + 1, p.Item2 + 2).ToArgb() == Color.White.ToArgb())
                        {
                            weight = CalculateWeight(p.Item1, p.Item2 + 1, b);
                            if (A2.Contains(weight))
                            {
                                b.SetPixel(p.Item1, p.Item2 + 1, Color.White);
                            }
                        }
                        else if ((weight == 195 || weight == 227)
                            && p.Item1 + 1 < b.Width && p.Item2 - 1 > 0 && marked.Contains((p.Item1 + 1, p.Item2 - 1)))
                        {
                            weight = CalculateWeight(p.Item1 + 1, p.Item2 - 1, b);
                            if (A2.Contains(weight))
                            {
                                markedForDeletion.Add((p.Item1 + 1, p.Item2 - 1));
                            }
                        }
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        if (markedForDeletion.Contains(p))
                            markedForDeletion.Remove(p);
                        change = true;
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 3 - Delete 3/4/5
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A3.Contains(weight) || markedForDeletion.Contains(p))
                    {
                        if (weight == 241 && p.Item2 + 2 < b.Height && p.Item1 + 1 < b.Width
                            && marked.Contains((p.Item1, p.Item2 + 1))
                            && b.GetPixel(p.Item1, p.Item2 + 2).ToArgb() == Color.White.ToArgb()
                            && b.GetPixel(p.Item1 + 1, p.Item2 + 2).ToArgb() == Color.White.ToArgb())
                        {
                            weight = CalculateWeight(p.Item1, p.Item2 + 1, b);
                            if (A3.Contains(weight))
                            {
                                b.SetPixel(p.Item1, p.Item2 + 1, Color.White);
                            }
                        }
                        else if ((weight == 195 || weight == 227)
                            && p.Item1 + 1 < b.Width && p.Item2 - 1 > 0 && marked.Contains((p.Item1 + 1, p.Item2 - 1)))
                        {
                            weight = CalculateWeight(p.Item1 + 1, p.Item2 - 1, b);
                            if (A3.Contains(weight))
                            {
                                markedForDeletion.Add((p.Item1 + 1, p.Item2 - 1));
                            }
                        }
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        if (markedForDeletion.Contains(p))
                            markedForDeletion.Remove(p);
                        change = true;
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 4 - Delete 3/4/5/6
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A4.Contains(weight) || markedForDeletion.Contains(p))
                    {
                        if (weight == 241 && p.Item2 + 2 < b.Height && p.Item1 + 1 < b.Width
                            && marked.Contains((p.Item1, p.Item2 + 1))
                            && b.GetPixel(p.Item1, p.Item2 + 2).ToArgb() == Color.White.ToArgb()
                            && b.GetPixel(p.Item1 + 1, p.Item2 + 2).ToArgb() == Color.White.ToArgb())
                        {
                            weight = CalculateWeight(p.Item1, p.Item2 + 1, b);
                            if (A4.Contains(weight))
                            {
                                b.SetPixel(p.Item1, p.Item2 + 1, Color.White);
                            }
                        }
                        else if ((weight == 195 || weight == 227)
                            && p.Item1 + 1 < b.Width && p.Item2 - 1 > 0 && marked.Contains((p.Item1 + 1, p.Item2 - 1)))
                        {
                            weight = CalculateWeight(p.Item1 + 1, p.Item2 - 1, b);
                            if (A4.Contains(weight))
                            {
                                markedForDeletion.Add((p.Item1 + 1, p.Item2 - 1));
                            }
                        }
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        if (markedForDeletion.Contains(p))
                            markedForDeletion.Remove(p);
                        change = true;
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                foreach ((int, int) p in marked) //Phase 5 - Delete 3/4/5/6/7
                {
                    weight = CalculateWeight(p.Item1, p.Item2, b);
                    if (A5.Contains(weight) || markedForDeletion.Contains(p))
                    {
                        if (weight == 241 && p.Item2 + 2 < b.Height && p.Item1 + 1 < b.Width
                            && marked.Contains((p.Item1, p.Item2 + 1))
                            && b.GetPixel(p.Item1, p.Item2 + 2).ToArgb() == Color.White.ToArgb()
                            && b.GetPixel(p.Item1 + 1, p.Item2 + 2).ToArgb() == Color.White.ToArgb())
                        {
                            weight = CalculateWeight(p.Item1, p.Item2 + 1, b);
                            if (A5.Contains(weight))
                            {
                                b.SetPixel(p.Item1, p.Item2 + 1, Color.White);
                            }
                        }
                        else if ((weight == 195 || weight == 227)
                            && p.Item1 + 1 < b.Width && p.Item2 - 1 > 0 && marked.Contains((p.Item1 + 1, p.Item2 - 1)))
                        {
                            weight = CalculateWeight(p.Item1 + 1, p.Item2 - 1, b);
                            if (A5.Contains(weight))
                            {
                                markedForDeletion.Add((p.Item1 + 1, p.Item2 - 1));
                            }
                        }
                        b.SetPixel(p.Item1, p.Item2, Color.White);
                        if (markedForDeletion.Contains(p))
                            markedForDeletion.Remove(p);
                        change = true;
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                marked.Clear(); //Phase 6 - Unmark
            } while (change);
            for (int i = 0; i < b.Width; i++) //1 pixel Phase
            {
                for (int j = 0; j < b.Height; j++)
                {
                    weight = CalculateWeight(i, j, b);
                    if (A1px.Contains(weight))
                    {
                        b.SetPixel(i, j, Color.White);
                    }
                }
            }
            if (saveCheckBox.Checked)
            {
                saveImage = b;
                saveImage.Save("ModK3M" + saveValue.ToString() + ".png", ImageFormat.Png);
            }
            return b;
        }

        private Bitmap ZhangAndWang()
        {
            if (stopCheckBox.Checked) System.Threading.Thread.Sleep(stopValue);
            Bitmap b = (Bitmap)pictureBox1.Image;
            Image saveImage = b;
            if (saveCheckBox.Checked)
            {
                saveImage.Save("ZhangAndWang" + saveValue.ToString() + ".png", ImageFormat.Png);
                saveValue++;
            }
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
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("ZhangAndWang" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
            } while (zwdeletable.Count != 0);
            return b;
        }

        private Bitmap KMM()
        {
            int[] A = new int[] { 3, 5, 7, 12, 13, 14, 15, 20,
                                21, 22, 23, 28, 29, 30, 31, 48,
                                52, 53, 54, 55, 56, 60, 61, 62,
                                63, 65, 67, 69, 71, 77, 79, 80,
                                81, 83, 84, 85, 86, 87, 88, 89,
                                91, 92, 93, 94, 95, 97, 99, 101,
                                103, 109, 111, 112, 113, 115, 116, 117,
                                118, 119, 120, 121, 123, 124, 125, 126,
                                127, 131, 133, 135, 141, 143, 149, 151,
                                157, 159, 181, 183, 189, 191, 192, 193,
                                195, 197, 199, 205, 207, 208, 209, 211,
                                212, 213, 214, 215, 216, 217, 219, 220,
                                221, 222, 223, 224, 225, 227, 229, 231,
                                237, 239, 240, 241, 243, 244, 245, 246,
                                247, 248, 249, 251, 252, 253, 254, 255 };

            if (stopCheckBox.Checked) System.Threading.Thread.Sleep(stopValue);
            Bitmap b = (Bitmap)pictureBox1.Image;
            Image saveImage = b;
            if (saveCheckBox.Checked)
            {
                saveImage.Save("KMM" + saveValue.ToString() + ".png", ImageFormat.Png);
                saveValue++;
            }
            int[,] pixels = new int[b.Width, b.Height];
            int[,] pixelsWeights = new int[b.Width, b.Height];
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    if (b.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                        pixels[i, j] = 1;
                    else
                        pixels[i, j] = 0;
                }
            }
            bool change = false;
            do
            {
                change = false;
                for (int i = 0; i < b.Width; i++) //mark '2's
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (pixels[i, j] == 1)
                        {
                            if (i > 0 && pixels[i - 1, j] == 0)
                                pixels[i, j] = 2;
                            else if (j > 0 && pixels[i, j - 1] == 0)
                                pixels[i, j] = 2;
                            else if (i < b.Width - 1 && pixels[i + 1, j] == 0)
                                pixels[i, j] = 2;
                            else if (j < b.Height - 1 && pixels[i, j + 1] == 0)
                                pixels[i, j] = 2;
                        }
                    }
                }
                for (int i = 0; i < b.Width; i++) //mark '3's
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (pixels[i, j] == 1)
                        {
                            if (i > 0 && j > 0 && pixels[i - 1, j - 1] == 0)
                                pixels[i, j] = 3;
                            else if (i < b.Width - 1 && j > 0 && pixels[i + 1, j - 1] == 0)
                                pixels[i, j] = 3;
                            else if (i < b.Width - 1 && j < b.Height - 1 && pixels[i + 1, j + 1] == 0)
                                pixels[i, j] = 3;
                            else if (i > 0 && j < b.Height - 1 && pixels[i - 1, j + 1] == 0)
                                pixels[i, j] = 3;
                        }
                    }
                }
                for (int i = 0; i < b.Width; i++) //calculate weight
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (pixels[i, j] != 0)
                        {
                            pixelsWeights[i, j] = CalculateWeight(i, j, b);
                        }
                    }
                }
                for (int i = 0; i < b.Width; i++) //delete '4's
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (pixels[i, j] == 4)
                        {
                            if (A.Contains(pixelsWeights[i, j]))
                            {
                                pixels[i, j] = 0;
                                b.SetPixel(i, j, Color.White);
                                change = true;
                            }
                        }
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("KMM" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                for (int i = 0; i < b.Width; i++) //delete not needed '2's
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (pixels[i, j] == 2)
                        {
                            if (A.Contains(CalculateWeight(i, j, b)))
                            {
                                pixels[i, j] = 0;
                                b.SetPixel(i, j, Color.White);
                                change = true;
                            }
                            else
                            {
                                pixels[i, j] = 1;
                            }
                        }
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("KMM" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
                for (int i = 0; i < b.Width; i++) //delete not needed '3's
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (pixels[i, j] == 3)
                        {
                            if (A.Contains(CalculateWeight(i, j, b)))
                            {
                                pixels[i, j] = 0;
                                b.SetPixel(i, j, Color.White);
                                change = true;
                            }
                            else
                            {
                                pixels[i, j] = 1;
                            }
                        }
                    }
                }
                if (stopCheckBox.Checked)
                {
                    pictureBox1.Image = b;
                    pictureBox1.Refresh();
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (saveCheckBox.Checked)
                {
                    saveImage = b;
                    saveImage.Save("KMM" + saveValue.ToString() + ".png", ImageFormat.Png);
                    saveValue++;
                }
            } while (change);
            return b;
        }

        private void binImage()
        {
            Bitmap b = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < b.Width; i++) //grayscale
            {
                for (int j = 0; j < b.Height; j++)
                {
                    int n = (b.GetPixel(i, j).R + b.GetPixel(i, j).G + b.GetPixel(i, j).B) / 3;
                    b.SetPixel(i, j, Color.FromArgb(n, n, n));
                }
            }
            int threshold = Otsu(b);
            for (int i = 0; i < b.Width; i++) //binaryzation
            {
                for (int j = 0; j < b.Height; j++)
                {
                    if (b.GetPixel(i, j).R < threshold)
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

        private int Otsu(Bitmap b)
        {
            int[] histogram = new int[256];
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    histogram[b.GetPixel(i, j).R]++;
                }
            }

            int total = b.Width * b.Height;

            float sum = 0;
            for (int i = 0; i < 256; i++)
                sum += i * histogram[i];

            float sumB = 0;
            int wB = 0;
            int wF = 0;

            float varMax = 0;
            int threshold = 0;
            for (int i = 0; i < 256; i++)
            {
                wB += histogram[i];
                if (wB == 0) continue;

                wF = total - wB;
                if (wF == 0) break;

                sumB += (float)(i * histogram[i]);

                float mB = sumB / wB;
                float mF = (sum - sumB) / wF;

                float varBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = i;
                }
            }
            return threshold;
        }
    }
}
