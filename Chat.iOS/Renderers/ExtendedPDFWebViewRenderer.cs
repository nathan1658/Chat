using System;
using System.IO;
using System.Net;
using Chat.Controls;
using Chat.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedPDFWebView), typeof(ExtendedPDFWebViewRenderer))]
namespace Chat.iOS.Renderers
{
    public class ExtendedPDFWebViewRenderer : ViewRenderer<ExtendedPDFWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedPDFWebView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as ExtendedPDFWebView;

                
                string base64Str = Convert.ToBase64String(customWebView.PDFByte);
                Control.LoadData(new NSData(base64Str, NSDataBase64DecodingOptions.None), @"application/pdf", "utf-8", new NSUrl(""));
                Control.ScalesPageToFit = true;
            }
        }
    }
}