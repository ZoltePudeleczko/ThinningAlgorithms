using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ThinningAlgorithms.Algorithms
{
    class ModifiedK3M : ThinningAlgorithm
	{
		public ModifiedK3M() : base("Modified K3M (2016)") { }

        private int ModifiedK3MCalculateWeight(int i, int j, Bitmap b, List<(int, int)> marked)
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

        public override Bitmap Thin(MainWindow win, Bitmap b, bool stop, int stopValue, bool save)
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

            if (stop) System.Threading.Thread.Sleep(stopValue);
            Image saveImage = b;
            if (save)
            {
                saveImage.Save("ModK3M" + SaveValue.ToString() + ".png", ImageFormat.Png);
                SaveValue++;
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
                        weight = ModifiedK3MCalculateWeight(i, j, b, marked);
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("ModK3M" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
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
            if (save)
            {
                saveImage = b;
                saveImage.Save("ModK3M" + SaveValue.ToString() + ".png", ImageFormat.Png);
            }
            return b;
        }
    }
}
