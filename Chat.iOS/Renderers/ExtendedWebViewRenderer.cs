using System;
using Chat.Controls;
using Chat.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace Chat.iOS.Renderers
{
    public class ExtendedWebViewRenderer:Xamarin.Forms.Platform.iOS.WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (NativeView != null)
            {
                var webView = (UIWebView)NativeView;

                webView.Opaque = false;
                //webView.BackgroundColor = UIColor.Green;
            }
        }
    }
}
