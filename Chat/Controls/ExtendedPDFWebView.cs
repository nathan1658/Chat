using System;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace Chat.Controls
{
    public class ExtendedPDFWebView : WebView
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
        }

    }
}

