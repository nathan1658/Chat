using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPopup : PopupPage
    {
        public TestPopup()
        {
            InitializeComponent();
        }

        private async void OnClose(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }



        protected override bool OnBackgroundClicked()
        {
            base.OnBackgroundClicked();
            this.OnClose(null, null);
            return true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            new PhotoBrowser
            {
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Vincent.jpg",
                        Title = "Vincent"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Jules.jpg",
                        Title = "Jules"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Korben.jpg",
                        Title = "Korben"
                    }
                }

            }.Show();
        }

    }
}