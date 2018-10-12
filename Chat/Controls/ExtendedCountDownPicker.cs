using System;
using System.Linq;
using Xamarin.Forms;

namespace Chat.Controls
{
    //TODO implement Android version..
    public class ExtendedCountDownPicker : Picker
    {
        public static readonly BindableProperty SelectedTimeProperty =
            BindableProperty.Create(nameof(SelectedTime), typeof(TimeSpan), typeof(ExtendedCountDownPicker), defaultValue: TimeSpan.Zero, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedTimePropertyPropertyChanged);

        public ExtendedCountDownPicker()
        {
            // Add only one item, later will manipulate only it's value for performance
            Items.Add("00:00:00");
            SelectedIndex = 0;
            SelectedTime = TimeSpan.Zero;
        
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
            picker.Items[0] = newValue.ToString();
            picker.SelectedIndex = 0;
        }
    }
}

