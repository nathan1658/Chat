using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Interfaces
{
    public interface IPhotoResizer
    {
        byte[] ResizeImage(byte[] imageData, float width, float height, int quality);      
    }
}
