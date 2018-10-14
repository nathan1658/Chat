using Chat.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PDFWebViewPage :ContentPage
    {
        public PDFWebViewPage(byte[] pdfData)
        {


            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem
            {
                Name = "Done",
                Command = new Command(() => Navigation.PopModalAsync()),
            });
            //<controls:ExtendedPDFWebView HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand" />
            
            this.Content = new ExtendedPDFWebView(pdfData) { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
        }
    }
}