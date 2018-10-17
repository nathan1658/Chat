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
            PopupNavigation.Instance.PushAsync(new WheelPickerPopup());
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
