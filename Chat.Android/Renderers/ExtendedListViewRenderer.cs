using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Chat.Controls;
using Chat.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace Chat.Droid.Renderers
{
    public class ExtendedListViewRenderer : ListViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    //Disable selector effect
                    Control.SetSelector(Android.Resource.Color.Transparent);
                    Control.CacheColorHint = Xamarin.Forms.Color.Transparent.ToAndroid();

                   // Control.StackFromBottom = true;
                    Control.TranscriptMode = TranscriptMode.Normal;
                    e.NewElement.SizeChanged += (s1, e1) =>
                    {
                        var ss = s1 as ExtendedListView;
                        var hh = ss.Height;
                    };
                }
            }
        }


        public ExtendedListViewRenderer(Context context) : base(context)
        {
        }



    }
}