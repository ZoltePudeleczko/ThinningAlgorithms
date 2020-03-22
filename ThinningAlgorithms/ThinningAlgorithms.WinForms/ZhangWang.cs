using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ThinningAlgorithms.WinForms
{
	public class ZhangWang : ThinningAlgorithm
	{
		public ZhangWang() : base("Zhang and Wang (1988)") { }

		public override Bitmap Thin(MainWindow win, Bitmap b, bool stop, int stopValue, bool save)
		{
            if (stop) System.Threading.Thread.Sleep(stopValue);
            Image saveImage = b;
            if (save)
            {
                saveImage.Save("ZhangAndWang" + SaveValue.ToString() + ".png", ImageFormat.Png);
                SaveValue++;
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
                if (stop)
                {
                    win.UpdateImage(b);
                    System.Threading.Thread.Sleep(stopValue);
                }
                if (save)
                {
                    saveImage = b;
                    saveImage.Save("ZhangAndWang" + SaveValue.ToString() + ".png", ImageFormat.Png);
                    SaveValue++;
                }
            } while (zwdeletable.Count != 0);
            return b;
        }
	}
}
