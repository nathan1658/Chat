using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Chat.Models;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Chat.Views.Cells
{
    public partial class IncomingViewCell : BaseMessageViewCell
    {
        public IncomingViewCell()
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
            Message msg = this.BindingContext as Message;
            if(msg!=null)
            {
                msg.IsMasked = false;
                this.ForceUpdateSize();
            }                    
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var msg = this.BindingContext as Message;
            msg.PropertyChanged += (s, e) =>
              {

                  //Maybe add old value checking here to prevent called multiple times..
                  if(e.PropertyName == "IsMasked")
                  {
                      this.ForceUpdateSize();
                  }
              };
        }

    }
}
