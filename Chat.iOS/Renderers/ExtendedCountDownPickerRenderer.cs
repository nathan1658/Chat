using System;
using Chat.Controls;
using Chat.iOS.Renderers;
using Chat.Views.Partials;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(ExtendedCountDownPicker), typeof(ExtendedCountDownPickerRenderer))]
namespace Chat.iOS.Renderers
{

    public class ExtendedCountDownPickerRenderer : ViewRenderer<ExtendedCountDownPicker, UIPickerView>
    {
        public static double DisplayScale = UIScreen.MainScreen.Scale;
        public static int DispalyHeight = (int)UIScreen.MainScreen.NativeBounds.Height;
        //TODO fix this (maybe dynamic get parent view width?)
        public static double DisplayWidth = 560;

        internal const int ComponentCount = 6;

        private const int _labelSize = 30;

        private ExtendedCountDownPicker timeCountdownPicker;

        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedCountDownPicker> e)
        {



            base.OnElementChanged(e);

            if (Control == null)
            {

                timeCountdownPicker = e.NewElement as ExtendedCountDownPicker;

                var customModelPickerView = new UIPickerView
                {
                    Model = new TimeCountdownPickerView(timeCountdownPicker),
                    BackgroundColor = UIColor.Red

                };
              
                SelectPickerValue(customModelPickerView, timeCountdownPicker);
                CreatePickerLabels(customModelPickerView);

                SetNativeControl(customModelPickerView);
            }

        }

        private void SelectPickerValue(UIPickerView customModelPickerView, ExtendedCountDownPicker myTimePicker)
        {
            if (myTimePicker == null)
                return;
            customModelPickerView.Select(new nint(myTimePicker.SelectedTime.Hours), 0, false);
            customModelPickerView.Select(new nint(myTimePicker.SelectedTime.Minutes), 2, false);
            customModelPickerView.Select(new nint(myTimePicker.SelectedTime.Seconds), 4, false);
        }

        private void CreatePickerLabels(UIPickerView customModelPickerView)
        {
            nfloat verticalPosition = (customModelPickerView.Frame.Size.Height / 2) - (_labelSize / 2);
            nfloat componentWidth = new nfloat(DisplayWidth / ComponentCount / DisplayScale);

            var hoursLabel = new UILabel(new CGRect(componentWidth, verticalPosition, _labelSize, _labelSize));
            hoursLabel.TextAlignment = UITextAlignment.Center;

            hoursLabel.Text = "h";

            var minutesLabel = new UILabel(new CGRect((componentWidth * 3) + (componentWidth / 2), verticalPosition, _labelSize, _labelSize));
            minutesLabel.TextAlignment = UITextAlignment.Center;

            minutesLabel.Text = "m";

            var secondsLabel = new UILabel(new CGRect((componentWidth * 5) + (componentWidth / 2), verticalPosition, _labelSize, _labelSize));
            secondsLabel.TextAlignment = UITextAlignment.Center;

            secondsLabel.Text = "s";

            customModelPickerView.AddSubview(hoursLabel);
            customModelPickerView.AddSubview(minutesLabel);
            customModelPickerView.AddSubview(secondsLabel);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
            {
                var customModelPickerView = (UIPickerView)Control.InputView;

                SelectPickerValue(customModelPickerView, timeCountdownPicker);
            }
        }

        public class TimeCountdownPickerView : UIPickerViewModel
        {
            private ExtendedCountDownPicker timeCountdownPicker { get; }

            public TimeCountdownPickerView(ExtendedCountDownPicker picker)
            {
                timeCountdownPicker = picker;
            }

            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return new nint(ComponentCount);
            }

          

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                if (component == 0)
                {
                    // Hours
                    return new nint(24);
                }

                if (component % 2 != 0)
                {
                    // Odd components are labels for hrs, mins and secs
                    return new nint(1);
                }

                // Minutes & seconds
                return new nint(60);
            }

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                if (component == 0)
                {
                    return row.ToString();
                }
                else if (component == 1)
                {
                    return null;
                }
                else if (component == 3)
                {
                    return null;
                }
                else if (component == 5)
                {
                    return null;
                }
                return row.ToString("#0");
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                var selectedHours = pickerView.SelectedRowInComponent(0);
                var selectedMinutes = pickerView.SelectedRowInComponent(2);
                var selectedSeconds = pickerView.SelectedRowInComponent(4);

                var time = new TimeSpan((int)selectedHours, (int)selectedMinutes, (int)selectedSeconds);

                timeCountdownPicker.SelectedTime = time;
            }

            public override nfloat GetComponentWidth(UIPickerView pickerView, nint component)
            {
                var screenWidth = DisplayWidth;

                var componentWidth = screenWidth /
                    ComponentCount /
                    DisplayScale;

                return new nfloat(componentWidth);
            }
        }
    }
}

