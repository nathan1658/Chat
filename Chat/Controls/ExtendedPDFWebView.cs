using System;
using System.IO;
using System.Net;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Serialization.JsonNET;

namespace Chat.Controls
{
    public class ExtendedPDFWebView : HybridWebView
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri",
                returnType: typeof(string),
                declaringType: typeof(ExtendedPDFWebView),
                defaultValue: default(string));
     

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly BindableProperty PDFByteProperty = BindableProperty.Create("PDFByte", typeof(byte[]), typeof(ExtendedPDFWebView), default(byte[]));

        public byte[] PDFByte
        {
            get { return (byte[])GetValue(PDFByteProperty); }
            set { SetValue(PDFByteProperty, value); }
        }

        public ExtendedPDFWebView(byte[] pdfByte)
        {
            this.PDFByte = pdfByte;


            if (Device.RuntimePlatform == Device.Android)
            {
                string base64Str = Convert.ToBase64String(PDFByte);
                this.Source = @"file:///android_asset/pdfjs/web/viewer.html?file=";
                bool pdfInited = false;
                this.LoadFinished += (s, e) =>
                {

                    if (!pdfInited)
                    {
                        this.InjectJavaScript(@"
        function base64ToUint8Array(base64) {
    var raw = atob(base64);
    var uint8Array = new Uint8Array(raw.length);
    for(var i = 0; i < raw.length; i++) {
      uint8Array[i] = raw.charCodeAt(i);
    }
    return uint8Array;
  }
function loadPdfFromBase64(base64Str){
var pdfData = base64ToUint8Array(base64Str);
PDFViewerApplication.open(pdfData);}");
                        //this.InjectJavaScript(@"Alert(123);");

                        this.CallJsFunction("loadPdfFromBase64", base64Str);
                        pdfInited = true;
                    }
                };

            }


        }

        

    }
}

