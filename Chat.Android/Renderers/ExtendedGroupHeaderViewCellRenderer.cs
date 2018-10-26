using System;
using Chat.Controls;
using Chat.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedGroupHeaderViewCell), typeof(ExtendedGroupHeaderViewCellRenderer))]
namespace Chat.Droid.Renderers
{
    public class ExtendedGroupHeaderViewCellRenderer:ViewCellRenderer
    {
        public ExtendedGroupHeaderViewCellRenderer()
        {
        }
    }
}
