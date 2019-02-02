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
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Chat.Views.Popups;

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
                ForceUpdateSize();
                //MessagingCenter.Subscribe<IncomingViewCell, Message>(this, "IsMaskedUpdate", (x, _msg) =>
                //MessagingCenter.Send<IncomingViewCell, Message>(this, "IsMaskedUpdate", msg);

               // ForceUpdateSize();
            }                    
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new TestPopup());
            });
        }


        //protected override void OnBindingContextChanged()
        //{
        //    lblBody.Text = "";
        //    var msg = this.BindingContext as Message;
        //    if(msg == null)
        //    {
        //        return;
        //    }
        //    if(msg.Text=="")
        //    {

        //    }

        //    lblBody.Text = msg.Text;    

        //    base.OnBindingContextChanged();
        //}

        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    var msg = this.BindingContext as Message;
        //    msg.PropertyChanged += (s, e) =>
        //      {

        //          //Maybe add old value checking here to prevent called multiple times..
        //          if(e.PropertyName == "IsMasked"||e.PropertyName == "IsExpired")
        //          {
        //              Device.BeginInvokeOnMainThread(async () =>
        //              {
        //                  await Task.Delay(1);
        //                  this.ForceUpdateSize();
        //              });

        //              //var listView = this.Parent as ExtendedListView;
        //              //if(listView !=null)
        //              //{
        //              //    Device.BeginInvokeOnMainThread(async () =>
        //              //    {
        //              //        await Task.Delay(1);
        //              //        listView.RefreshList();
        //              //    });
        //              //}
        //          }
        //      };
        //}

    }
}
