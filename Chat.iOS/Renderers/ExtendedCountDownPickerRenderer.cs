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

    public class ExtendedCountDownPickerRenderer : ViewRenderer<ExtendedCountDownPicker, UIView>
    {
        //public static double DisplayScale = UIScreen.MainScreen.Scale;
        public static double DispalyHeight = 0;
        //TODO fix this (maybe dynamic get parent view width?)
        public static double DisplayWidth = 0;

        internal const int ComponentCount = 8;

        private const int _labelSize = 25;

        private ExtendedCountDownPicker timeCountdownPicker;
        UIPickerView customModelPickerView = null;
        UIView uiView;
        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedCountDownPicker> e)
        {



            base.OnElementChanged(e);

            if (Control == null)
            {

                timeCountdownPicker = e.NewElement as ExtendedCountDownPicker;
                timeCountdownPicker.SizeChanged+= TimeCountdownPicker_SizeChanged;
                //      var aa = this.Control.Frame.Width;
                customModelPickerView = new UIPickerView
                {
                    Model = new TimeCountdownPickerView(timeCountdownPicker)
                };

                //SelectPickerValue(customModelPickerView, timeCountdownPicker);
           
                uiView = new UIView(new CGRect(0, 0, 300, 200));
                uiView.BackgroundColor = UIColor.Red;
                SetNativeControl(uiView);
            }

        }

        void TimeCountdownPicker_SizeChanged(object sender, EventArgs e)
        {

            var obj = sender as ExtendedCountDownPicker;
            DispalyHeight = obj.Height;
            DisplayWidth = obj.Width;
            customModelPickerView = new UIPickerView
            {
                Model = new TimeCountdownPickerView(timeCountdownPicker)
            };

            SelectPickerValue(customModelPickerView, timeCountdownPicker);
            CreatePickerLabels(uiView);
            uiView.BackgroundColor = UIColor.Green;
            uiView.Frame=new CGRect(0,0,DisplayWidth,DispalyHeight);
            uiView.Bounds = uiView.Frame;
          //  SetNativeControl(customModelPickerView);
        }



        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
        }

        private void SelectPickerValue(UIPickerView customModelPickerView, ExtendedCountDownPicker myTimePicker)
        {
          
        }

        private void CreatePickerLabels(UIView customModelPickerView)
        {
            nfloat verticalPosition = ((nfloat)DispalyHeight / 2) - (_labelSize / 2);
            nfloat componentWidth = new nfloat(DisplayWidth / ComponentCount );


            var tmp1= new UIPickerView(new CGRect(0, 0, componentWidth, DispalyHeight))
            {
                Model = new TimeCountdownPickerView(timeCountdownPicker)
            };
            var tmp2 = new UIPickerView(new CGRect(componentWidth*2, 0, componentWidth, DispalyHeight))
            {
                Model = new TimeCountdownPickerView(timeCountdownPicker)
            };
            var tmp3 = new UIPickerView(new CGRect(componentWidth * 4, 0, componentWidth, DispalyHeight))
            {
                Model = new TimeCountdownPickerView(timeCountdownPicker)
            };
            var tmp4 = new UIPickerView(new CGRect(componentWidth * 6, 0, componentWidth, DispalyHeight))
            {
                Model = new TimeCountdownPickerView(timeCountdownPicker)
            };


            var daysLabel = new UILabel(new CGRect(componentWidth, verticalPosition, _labelSize, _labelSize));
            daysLabel.TextAlignment = UITextAlignment.Center;

            daysLabel.Text = "d";

            var hoursLabel = new UILabel(new CGRect((componentWidth * 3) , verticalPosition, _labelSize, _labelSize));
            hoursLabel.TextAlignment = UITextAlignment.Center;

            hoursLabel.Text = "h";

            var minutesLabel = new UILabel(new CGRect((componentWidth * 5) , verticalPosition, _labelSize, _labelSize));
            minutesLabel.TextAlignment = UITextAlignment.Center;

            minutesLabel.Text = "m";

            var secondsLabel = new UILabel(new CGRect((componentWidth * 7) , verticalPosition, _labelSize, _labelSize));
            secondsLabel.TextAlignment = UITextAlignment.Center;

            secondsLabel.Text = "s";
            customModelPickerView.AddSubview(tmp1);
            customModelPickerView.AddSubview(tmp2);
            customModelPickerView.AddSubview(tmp3);
            customModelPickerView.AddSubview(tmp4);

            customModelPickerView.AddSubview(daysLabel);
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
           // if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
           //e.PropertyName == VisualElement.HeightProperty.PropertyName)
            //{
            //    if (Element != null && Element.Bounds.Height > 0 && Element.Bounds.Width > 0)
            //    {
            //        DisplayWidth = Element.Bounds.Width;
            //        //DisplayWidth *= DisplayScale;
                
            //        CreatePickerLabels(customModelPickerView);
                    
            //    }
            //}

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
                return new nint(1);
            }

            public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
            {
                var label = new UILabel();
                label.Text = row+1+"";
                //label.AutosizesSubviews = false;
                //label.ClipsToBounds = false;
                label.TextAlignment = UITextAlignment.Center;
                //label.SetContentHuggingPriority(252, UILayoutConstraintAxis.Horizontal);
                return label;

                //return base.GetView(pickerView, row, component, view);


            }


            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                if (component == 0)
                {
                    // Hours
                    return new nint(31);
                }

                if (component == 2)
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
                else if (component == 7)
                {
                    return null;
                }
                return row.ToString("#0");
            }




            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {

            }

            public override nfloat GetComponentWidth(UIPickerView pickerView, nint component)
            {
            
                var componentWidth = DisplayWidth /
                    ComponentCount;

                return new nfloat(componentWidth);

            }
        }
    }
    //public class TestDelegate : IUIPickerViewDelegate
    //{
    //    [Export("pickerView:viewForRow:forComponent:reusingView:")]

    //    public UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
    //    {
    //        UILabel label = new UILabel
    //        {
    //            TextColor = UIColor.Blue,
    //            Text = Element.Items[(int)row].ToString(),
    //            TextAlignment = UITextAlignment.Center,
    //        };
    //        test = label.Text;

    //        Console.WriteLine(Element.Items[(int)row]);

    //        return label;
    //    }


    //}

}

