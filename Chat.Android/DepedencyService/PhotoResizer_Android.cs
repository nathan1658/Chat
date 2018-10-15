using System;
using System.IO;
using Android.Graphics;
using Chat.Droid.DepedencyService;
using Chat.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoResizer_Android))]
namespace Chat.Droid.DepedencyService
{
    public class PhotoResizer_Android : IPhotoResizer
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height, int quality)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            float oldWidth = (float)originalImage.Width;
            float oldHeight = (float)originalImage.Height;
            float scaleFactor = 0f;

            if (oldWidth > oldHeight)
            {
                scaleFactor = width / oldWidth;
            }
            else
            {
                scaleFactor = height / oldHeight;
            }

            float newHeight = oldHeight * scaleFactor;
            float newWidth = oldWidth * scaleFactor;

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                return ms.ToArray();
            }
        }

    }
}
