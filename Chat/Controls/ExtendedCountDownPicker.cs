using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Chat.Controls
{
    //TODO implement Android version..
    public class ExtendedCountDownPicker : View
    {
        public static readonly BindableProperty SelectedTimeProperty =
            BindableProperty.Create(nameof(SelectedTime), typeof(TimeSpan), typeof(ExtendedCountDownPicker), defaultValue: TimeSpan.Zero, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedTimePropertyPropertyChanged);

        public ExtendedCountDownPicker()
        {            
        
        }




        public TimeSpan SelectedTime
        {
            get { return (TimeSpan)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        private static void OnSelectedTimePropertyPropertyChanged(BindableObject bindable, object value, object newValue)
        {
            var picker = (ExtendedCountDownPicker)bindable;
            // Update value
            

        }
    }
}

