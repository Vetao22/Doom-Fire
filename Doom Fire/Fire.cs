using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Doom_Fire
{
    public class Fire
    {
        List<BitmapSource> colors;
        List<int> levels;
        int rows, cols;
        Random rand;
        public Fire(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            rand = new Random();

            BitmapImage originalSrcColors = new BitmapImage(new Uri("Resources/DoomFire.png", UriKind.Relative));

            GetColors(originalSrcColors);

            if(rows == 0 || cols == 0)
            {
                throw new Exception("Rows and Cols cannot be zero.");
            }
            else
            {
                GenerateLevels(rows, cols);
            }
        }

        void GetColors(BitmapSource originalSrcColors)
        {
            colors = new List<BitmapSource>();

            int slices = 37, sliceWidth = 23, sliceHeight = (int)originalSrcColors.Height;

            for(int x = 0; x < slices; x++)
            {
                Int32Rect sliceRect = new Int32Rect(x * sliceWidth, 0, sliceWidth, sliceHeight);
                BitmapSource slice = BitmapHelper.GetSlice(originalSrcColors, sliceRect);

                colors.Add(slice);
            }
        }

        void GenerateLevels(int rows, int cols)
        {
            int maxLevel = rows - 1, curLevel = 0;
            levels = new List<int>();
            
            for(int y = 0; y < rows; y++)
            {
                for(int x = 0; x < cols; x++)
                {
                    levels.Add(curLevel);
                }
                curLevel++;
            }

        }

        public void Update()
        {
            int index = 0;
            
            for(int x = 0; x < cols; x++)
            {
                for(int y = 1; y < rows; y++)
                {
                    double randVal = (double)(rand.NextDouble() * 1.5f);
                    index = y * cols + x;
                    int dst = (int)(index - randVal + 1);
                    dst = dst - cols > -1 ? dst - cols : 0;

                    levels[dst] = levels[index] - ((int)randVal & 1);
                }
            }
        }

        public void Draw(DrawableCanvas dc, int initialX, int initialY)
        {
            int curX = initialX, curY = initialY, index = levels.Count - 1;
            double bmpWidth = 1, bmpHeight = 1;
            Rect destRect;

            for(int y = 0; y < rows; y++)
            {
                for(int x = 0; x < cols; x++)
                {
                    int lvl = levels[index];
                    index--;

                    curX += (int)bmpWidth;
                    destRect = new Rect(curX, curY, bmpWidth, bmpHeight);

                    dc.DrawImage(colors[lvl], destRect);
                }
                curY -= (int)bmpHeight;
                curX = initialX;
            }
        }
    }
}
