using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Chat.Controls;
using Chat.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Com.Aigestudio.Wheelpicker;
using Java.Lang;
using System.Linq;

[assembly: ExportRenderer(typeof(ExtendedCountDownPicker), typeof(ExtendedCountDownPickerRenderer))]
namespace Chat.Droid.Renderers
{
    public class ExtendedCountDownPickerRenderer: ViewRenderer<ExtendedCountDownPicker,Android.Widget.LinearLayout>
    {

        WheelPicker hourPicker;
        WheelPicker minutePicker;
        WheelPicker secondPicker;
        ExtendedCountDownPickerOnItemSelected selectedClass;
        ExtendedCountDownPicker extendedCountDownPicker;
        TimeSpan selectedTimeSpan = new TimeSpan();

        public ExtendedCountDownPickerRenderer(Context context) : base(context)
        {
            selectedClass = new ExtendedCountDownPickerOnItemSelected(this);
            configPicker(out hourPicker, Enumerable.Range(0, 24).Select(x => x.ToString()).ToList());
            configPicker(out minutePicker, Enumerable.Range(0, 60).Select(x => x.ToString()).ToList());
            configPicker(out secondPicker, Enumerable.Range(0, 60).Select(x => x.ToString()).ToList());
        }

        void configPicker(out WheelPicker picker,List<string> data)
        {
            picker = new WheelPicker(this.Context);
            //TODO maybe use format string is better here..
            for (int i = 0; i < data.Count; i++)
            {
                var d = data[i];
                if (int.Parse(d) < 10)
                {
                    data[i] = "0" + d;
                }

            }
                       


            var layoutParams = new Android.Widget.LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            layoutParams.SetMargins(10, 0, 10, 0);
            picker.LayoutParameters = layoutParams;
            picker.SetAtmospheric(true);
            picker.Curved = true;
            var ucolor = ColorToUInt(System.Drawing.Color.DarkGray);
            picker.SelectedItemTextColor = unchecked((int)ucolor);

            picker.Data = data;

            picker.SetOnItemSelectedListener(selectedClass);


        }


        private uint ColorToUInt(System.Drawing.Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedCountDownPicker> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                extendedCountDownPicker = e.NewElement as ExtendedCountDownPicker;
                var stack = new LinearLayout(this.Context)
                {
                    Orientation = Orientation.Horizontal
                };

                stack.AddView(hourPicker);
                stack.AddView(new TextView(this.Context) { Text = "h" });

                stack.AddView(minutePicker);
                stack.AddView(new TextView(this.Context) { Text = "m" });

                stack.AddView(secondPicker);
                stack.AddView(new TextView(this.Context) { Text = "s" });


                stack.SetGravity(Android.Views.GravityFlags.CenterVertical);


                SetNativeControl(stack);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
            //   editText.SetOnClickListener
            }
            if (e.NewElement != null)
            {
                // Subscribe
              //  uiCameraPreview.Tapped += OnCameraPreviewTapped;
            }
        }
        void EditTextTapped(object sender, EventArgs e)
        {
           
        }

        private void updateWheelIndex(TimeSpan ts)
        {
            hourPicker.SelectedItemPosition = ts.Hours;
            minutePicker.SelectedItemPosition = ts.Minutes;
            secondPicker.SelectedItemPosition = ts.Seconds;
        }

        protected void wheelItemSelected()
        {
            try
            {
              
                var hour = Integer.ParseInt(hourPicker.Data[hourPicker.SelectedItemPosition].ToString());
                var min = Integer.ParseInt(minutePicker.Data[minutePicker.SelectedItemPosition].ToString());            
                var sec = Integer.ParseInt(secondPicker.Data[secondPicker.SelectedItemPosition].ToString());
                selectedTimeSpan = new TimeSpan(hour, min, sec);
                extendedCountDownPicker.SelectedTime = selectedTimeSpan;
            }catch(System.Exception ex)
            {

            };
        }

         class ExtendedCountDownPickerOnItemSelected : Java.Lang.Object, WheelPicker.IOnItemSelectedListener
        {
            ExtendedCountDownPickerRenderer renderer;
            internal ExtendedCountDownPickerOnItemSelected(ExtendedCountDownPickerRenderer _renderer)
            {
                renderer = _renderer;
            }
            public void OnItemSelected(WheelPicker p0, Java.Lang.Object p1, int p2)
            {
                p0.SelectedItemPosition = p2;
                if (renderer != null)
                {
                    renderer.wheelItemSelected();
                }
            }
        }

       
    }
  

}
