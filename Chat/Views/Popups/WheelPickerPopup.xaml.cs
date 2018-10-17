using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Chat.Views.Popups
{
    public partial class WheelPickerPopup : PopupPage
    {



        public WheelPickerPopup() : this(new TimeSpan())
        {
        }

        public WheelPickerPopup(TimeSpan defaultValue)
        {
            InitializeComponent();
            picker.SelectedTime= defaultValue;
        }


        private async void OnOK(object o, EventArgs e)
        {
            MessagingCenter.Send<WheelPickerPopup, TimeSpan>(this, "ReturnedTimeSpan", picker.SelectedTime);
            this.OnClose(o, e);
        }


        private async void OnClose(object o, EventArgs e)
        {                     
            await PopupNavigation.Instance.PopAsync();
        }



        protected override bool OnBackgroundClicked()
        {
            base.OnBackgroundClicked();
            this.OnClose(null,null);
            return true;
        }
        
    }
}
