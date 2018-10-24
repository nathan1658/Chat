using System;
using System.ComponentModel;
using Android.Content;
using Chat.Controls;
using Chat.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedMessageBody), typeof(ExtendedMessageBodyRenderer))]
namespace Chat.Droid.Renderers
{
    public class ExtendedMessageBodyRenderer: LabelRenderer
    {


        public ExtendedMessageBodyRenderer(Context con):base(con)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if(Control!=null)
            {
                var screen  = Resources.DisplayMetrics;
                this.Control.SetMaxWidth( Convert.ToInt32(screen.WidthPixels * 0.7));

            }
        }

    }
}
