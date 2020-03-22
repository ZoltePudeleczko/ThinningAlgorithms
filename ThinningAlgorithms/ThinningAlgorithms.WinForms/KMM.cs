using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ThinningAlgorithms.WinForms
{
	class KMM : ThinningAlgorithm
	{
		public KMM() : base("KMM (2002)") { }

		public override Bitmap Thin(MainWindow win, Bitmap b, bool stop, int stopValue, bool save)
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

            if (stop) System.Threading.Thread.Sleep(stopValue);
            Image saveImage = b;
            if (save)
            {
                saveImage.Save("KMM" + SaveValue.ToString() + ".png", ImageFormat.Png);
                SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("KMM" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("KMM" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("KMM" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
                }
            } while (change);
            return b;
        }
    }
}