using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Doom_Fire
{
    public abstract class BitmapHelper
    { 
        //This method expects a 8 bit per channel image 
        public static byte[] GetPixels(Int32Rect srcRect, BitmapSource srcImg)
        {
            WriteableBitmap result = new WriteableBitmap(srcRect.Width, srcRect.Height, 96, 96,
                srcImg.Format, null);

            int bitsPerChannel = srcImg.Format.BitsPerPixel / srcImg.Format.Masks.Count;

            int stride = (srcRect.Width * srcImg.Format.BitsPerPixel / bitsPerChannel);

            byte[] pixels = new byte[stride * srcRect.Height];

            srcImg.CopyPixels(srcRect, pixels, stride, 0);

            return pixels;
        }

        public static BitmapSource SetPixels(BitmapSource srcImg, byte[] pixels, Int32Rect destRect)
        {
            WriteableBitmap result = new WriteableBitmap(destRect.Width, destRect.Height, 96, 96,
                srcImg.Format, null);

            int bitsPerChannel = srcImg.Format.BitsPerPixel / srcImg.Format.Masks.Count;

            int stride = (destRect.Width * srcImg.Format.BitsPerPixel / bitsPerChannel);

            result.WritePixels(destRect, pixels, stride, 0);

            return result;
        }

        public static BitmapSource GetSlice(BitmapSource srcImg, Int32Rect srcRect)
        {
            byte[] pixels = GetPixels(srcRect, srcImg);

            Int32Rect destRect = new Int32Rect(0, 0, srcRect.Width, srcRect.Height);

            BitmapSource result = SetPixels(srcImg, pixels, destRect);

            return result;
        }
    }
}
