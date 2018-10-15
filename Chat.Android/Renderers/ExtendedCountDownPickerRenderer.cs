using System;
using Android.Content;
using Android.Widget;
using Chat.Controls;
using Chat.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;




[assembly: ExportRenderer(typeof(ExtendedCountDownPicker), typeof(ExtendedCountDownPickerRenderer))]
namespace Chat.Droid.Renderers
{
    public class ExtendedCountDownPickerRenderer:PickerRenderer
    {
        public ExtendedCountDownPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
        
            base.OnElementChanged(e);

            if (Control != null)
            {
                EditText aa = Control as EditText;
                aa.InputType = new Android.Text.InputTypes();
                aa.Click += (s,e1) =>
                  {
                      var a = 123;
                  };

            }
        }
    }
}
