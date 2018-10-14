using System;
using System.IO;
using System.Net;
using Chat.Controls;
using Chat.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedPDFWebView), typeof(ExtendedPDFWebViewRenderers))]
namespace Chat.iOS.Renderers
{
    public class ExtendedPDFWebViewRenderers : ViewRenderer<ExtendedPDFWebView, UIWebView>
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
                //string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(customWebView.Uri)));
                //Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));

                //Control.LoadRequest(new NSUrlRequest(new NSUrl(@"http://www.africau.edu/images/default/sample.pdf",false)));
                //Control.LoadRequest(new NSUrlRequest(new NSUrl(@"http://www.africau.edu/images/default/sample.pdf")));
              //  Control.LoadRequest(new NSUrlRequest(new NSUrl(WebUtility.UrlEncode(customWebView.fileNamell),false)));
                
                string base64Str = Convert.ToBase64String(customWebView.PDFByte);
                Control.LoadData(new NSData(base64Str, NSDataBase64DecodingOptions.None), @"application/pdf", "utf-8", new NSUrl(""));
                Control.ScalesPageToFit = true;
            }
        }
    }
}