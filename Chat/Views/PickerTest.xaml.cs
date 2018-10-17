using System;
using System.Collections.Generic;
using System.ComponentModel;
using Chat.Views.Popups;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Chat.Views
{
    public partial class PickerTest : ContentPage
    {
 
        public PickerTest()
        {
            InitializeComponent();
            this.BindingContext = new PickerTestViewModel();
        
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

         
            MessagingCenter.Subscribe<WheelPickerPopup,TimeSpan>(this, "ReturnedTimeSpan", (s,ts) =>
            {
                var vm = this.BindingContext as PickerTestViewModel;
                vm.TimeSpanValue = ts;
                MessagingCenter.Unsubscribe<WheelPickerPopup, TimeSpan>(this, "ReturnedTimeSpan");
             
            });
        
            await PopupNavigation.Instance.PushAsync(new WheelPickerPopup((this.BindingContext as PickerTestViewModel).TimeSpanValue));
            
        }
    }


    public class PickerTestViewModel : INotifyPropertyChanged
    {

        public string TimeSpanValueString{get{
                return TimeSpanValue.ToString();
            }}

        public TimeSpan TimeSpanValue { get; set; } = new TimeSpan(1, 2, 3);
        public event PropertyChangedEventHandler PropertyChanged;


        private TimeSpan jer;
        public TimeSpan CountDownValue{
            get
            { return jer; }
            set
            {
                jer = value;
            }
        }

    }
}
