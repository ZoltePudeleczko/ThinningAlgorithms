using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ThinningAlgorithms.Algorithms;

namespace ThinningAlgorithms
{
    public partial class MainWindow : Form
    {
		readonly int stopValue = 50;

		readonly List<ThinningAlgorithm> thinningAlgorithms = new List<ThinningAlgorithm> {
            new ZhangWang(),
            new KMM(),
            new K3M(),
            new ModifiedK3M()
        };

        public MainWindow()
        {
            InitializeComponent();

            thinComboBox.Items.AddRange(thinningAlgorithms.ToArray());
            thinComboBox.SelectedIndex = 0;
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load image";
            openFileDialog1.FileName = "Image";

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

            BinImage();
            pictureBox1.Image = ((ThinningAlgorithm)thinComboBox.SelectedItem).Thin(
                this,
                (Bitmap)pictureBox1.Image,
                stopCheckBox.Checked,
                stopValue,
                saveCheckBox.Checked);
            pictureBox1.Refresh();
        }

        public void UpdateImage(Bitmap b)
        {
            pictureBox1.Image = b;
            pictureBox1.Refresh();
        }

        private void BinImage()
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
