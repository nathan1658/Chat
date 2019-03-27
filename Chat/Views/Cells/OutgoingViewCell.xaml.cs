using Chat.Controls;
using Chat.Models;
using Chat.Views.Popups;
using Rg.Plugins.Popup.Services;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat.Views.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OutgoingViewCell : BaseMessageViewCell
    {

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var gg = baseGrid.Width;
        }

        public OutgoingViewCell()
        {
            InitializeComponent();

         
                
       

            this.CellFrame.SizeChanged += (s, e) =>
            {
                stackLayoutCalculateHeight();
                MaskFrame.HeightRequest = this.CellFrame.Height + this.CellFrame.Margin.VerticalThickness;

            };

            this.ReplyMessageFrame.SizeChanged += (s, e) =>
            {
                updateCellSize();
            };


            ////Workaround, seems routing effect is not working on android, will implement it at renederer; To avoid duplicate effect, apply it here for iOS only
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    CellFrame.Effects.Add(new LongPressedEffect());
            //}



        }

      

        void stackLayoutCalculateHeight()
        {
            var childrenSum = CellStack.Children.Sum(x => x.IsVisible ? x.Height : 0);
            if ((int)CellStack.Height == (int)childrenSum)
            {
                return;
            }
            if (CellStack.Height < childrenSum)
            {
                CellStack.HeightRequest = childrenSum;
                CellStack.ForceLayout();
            };
            if (Device.RuntimePlatform == Device.iOS)
            {
                var parent = this.Parent as ExtendedListView;
                if (parent != null)
                {
                    parent.iOSUpdateListViewAction?.Invoke();
                }

            }
        }
        protected override void OnBindingContextChanged()
        {
            // you can also put cachedImage.Source = null; here to prevent showing old images occasionally

            var item = BindingContext as Models.Message;

            if (item == null)
            {
                return;
            }


            item.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsMessageVisible" || e.PropertyName == "IsExpired")
                {
                    updateCellSize();

                    //Device.BeginInvokeOnMainThread(() => { ForceUpdateSize(); });


                }
            };
            base.OnBindingContextChanged();
        }

        void updateCellSize()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(50);
                //Work around for incorrect cell height
                var newHeight = CellStack.Children.Sum(x => x.IsVisible ? x.Height : 0);
                if (newHeight <= 0)
                    return;

                if ((int)CellStack.HeightRequest == (int)newHeight)
                    return;

                CellStack.HeightRequest = newHeight;
                await Task.Delay(50);
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var parent = this.Parent as ExtendedListView;
                    if (parent != null)
                    {
                        parent.iOSUpdateListViewAction?.Invoke();
                    }

                }
                else
                {
                    ForceUpdateSize();
                }
            });
        }


    

        //TODO Place in baseViewCell?
        //TODO use event?
        //private void ImageThumbnail_Tapped(object sender, EventArgs e)
        //{
        //    Models.Message msg = this.BindingContext as Models.Message;
        //    if (msg.Attachment != null)
        //    {
        //        if (msg != null)
        //        {

        //            MessagingCenter.Send<BaseMessageViewCell, Attachment>(this, "AttachmentTapped", msg.Attachment);
        //        }
        //    }
        //}

        //private void PDFIcon_Tapped(object sender, EventArgs e)
        //{
        //    Models.Message msg = this.BindingContext as Models.Message;
        //    if (msg.Attachment != null)
        //    {             
        //        if (msg != null)
        //        {
        //            MessagingCenter.Send<BaseMessageViewCell, Attachment>(this, "AttachmentTapped",msg.Attachment);
        //        }
        //    }
        //}
    }

}
