using Chat.Models;
using Chat.Views.Popups;
using Rg.Plugins.Popup.Services;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Chat.Views.Cells
{
    public partial class OutgoingViewCell : BaseMessageViewCell
    {
        public OutgoingViewCell()
        {
            InitializeComponent();
          

        }

        
        //TODO Place in baseViewCell?
        //TODO use event?
        private void ImageThumbnail_Tapped(object sender, EventArgs e)
        {

            Message msg = this.BindingContext as Message;
            if (msg != null)
            {
                var tmpFile = System.IO.Path.GetTempFileName();
                tmpFile = Path.GetFileName(tmpFile);
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), tmpFile);
                File.WriteAllBytes(fileName, msg.PhotoByte);
                string url = @"file://" + fileName;
                new PhotoBrowser
                {
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            URL= url
                        }
                    }
                }.Show();

                //TODO Remove image after preview..
                //try
                //{
                //    File.Delete(fileName);
                //}catch(Exception ex)
                //{
                //    System.Diagnostics.Debug.WriteLine("Error when deleting tmp image: " + ex.Message);
                //}
            }
        }

        private void PDFIcon_Tapped(object sender, EventArgs e)
        {

            Message msg = this.BindingContext as Message;
            if (msg != null)
            {
                App.Navigation.PushModalAsync(new NavigationPage(new PDFWebViewPage(msg.PDFByte)));
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new TestPopup());
                });
            

        }
    }
}
