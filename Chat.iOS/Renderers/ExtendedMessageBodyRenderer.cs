using System;
using Chat.Controls;
using Chat.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedMessageBody), typeof(ExtendedMessageBodyRenderer))]
namespace Chat.iOS.Renderers
{
    public class ExtendedMessageBodyRenderer:LabelRenderer
    {

    
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var screenWidth = UIScreen.MainScreen.Bounds.Width;

                this.Control.PreferredMaxLayoutWidth = Convert.ToInt32(screenWidth * 0.7);                

            }
        }
    }
}
