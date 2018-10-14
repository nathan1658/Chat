using System;
using Xamarin.Forms;

namespace Chat.Views.Cells
{
    public abstract class BaseMessageViewCell : ViewCell
    {
        internal void BaseMessageViewCell_Appearing(object sender, EventArgs e)
        {
            var aa = this.Height;
        }
    }
}
