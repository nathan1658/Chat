﻿using System;
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
using Chat.Controls;
using System.Runtime.CompilerServices;

namespace Chat.Views.Cells
{
    public partial class IncomingViewCell : BaseMessageViewCell
    {
        public IncomingViewCell()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) => {
                TestLabel.IsVisible = !TestLabel.IsVisible;
                (this.Parent as ExtendedListView).IOSUPdateListView();
                //await Task.Delay(100);
                //this.ForceUpdateSize();
            };
            cellFrame.GestureRecognizers.Add(tapGestureRecognizer);

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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // msg.IsMasked = false;
                    //  await Task.Delay(100);
                    //(this.Parent as ExtendedListView).IOSUPdateListView();
                    (this.Parent as ExtendedListView).ReadMessageCommand.Execute(this.BindingContext);
                });
            }                    
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
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

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var msg = this.BindingContext as Message;
            msg.PropertyChanged += (s, e) =>
              {

                  //Maybe add old value checking here to prevent called multiple times..
                  if (e.PropertyName == "IsMasked" || e.PropertyName == "IsExpired")
                  {
                      Device.BeginInvokeOnMainThread(async () =>
                      {
                          msg.IsMasked = false;
                          await Task.Delay(100);
                          (this.Parent as ExtendedListView).IOSUPdateListView();
                      });
                  }
              };
                      //var listView = this.Parent as ExtendedListView;
                      //if(listView !=null)
                      //{
                      //    Device.BeginInvokeOnMainThread(async () =>
                      //    {
                      //        await Task.Delay(1);
                      //        listView.RefreshList();
                      //    });
                      //}
           
        }

    }
}
