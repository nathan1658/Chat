//using System;
//using System.Collections.Generic;
//using Android.Content;
//using Android.Widget;
//using Chat.Controls;
//using Chat.Droid.Renderers;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using XLabs.Forms.Controls;
//using XLabs.Serialization.JsonNET;

//[assembly: ExportRenderer(typeof(ExtendedPDFWebView), typeof(ExtendedPDFWebViewRenderer))]
//namespace Chat.Droid.Renderers
//{
//    public class ExtendedPDFWebViewRenderer : WebViewRenderer
//    {

//        Context _context;
//        public ExtendedPDFWebViewRenderer(Context context) : base(context)
//        {
//            _context = context;
//        }

//       protected override void OnElementChanged (ElementChangedEventArgs<WebView> e)
//		{
//			base.OnElementChanged (e);

//			if (e.NewElement != null) {
//				var customWebView = Element as CustomWebView;
//				Control.Settings.AllowUniversalAccessFromFileURLs = true;
//				Control.LoadUrl (string.Format ("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format ("file:///android_asset/Content/{0}", WebUtility.UrlEncode (customWebView.Uri))));
//			}
//		}

//        }
//    }
//}