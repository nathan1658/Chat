using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Chat.Controls;
using Chat.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;



[assembly: ExportRenderer(typeof(ExtendedEditorControl), typeof(ExtendedEditorControlRenderer))]
namespace Chat.Droid.Renderers
{
	public class ExtendedEditorControlRenderer: EditorRenderer
    {
        bool initial = true;
        Drawable originalBackground;

        public ExtendedEditorControlRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (initial)
                {                  
                    Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    Control.SetPadding(10, 5, 5, 5);
                    Control.TextSize = 18;
                    originalBackground = Control.Background;
                    initial = false;
                }
                Control.MovementMethod= new Android.Text.Method.ScrollingMovementMethod();
                Control.SetMaxLines(5);

            }

            if (e.NewElement != null)
            {
                var customControl = (ExtendedEditorControl)Element;
                var aa = customControl.Parent.Parent as Xamarin.Forms.Grid;
                
                if (customControl.HasRoundedCorner)
                {
                    ApplyBorder();
                }

                if (!string.IsNullOrEmpty(customControl.Placeholder))
                {
                    Control.Hint = customControl.Placeholder;
                    Control.SetHintTextColor(customControl.PlaceholderColor.ToAndroid());

                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var customControl = (ExtendedEditorControl)Element;

            if (ExtendedEditorControl.PlaceholderProperty.PropertyName == e.PropertyName)
            {
                Control.Hint = customControl.Placeholder;

            }
            else if (ExtendedEditorControl.PlaceholderColorProperty.PropertyName == e.PropertyName)
            {

                Control.SetHintTextColor(customControl.PlaceholderColor.ToAndroid());

            }
            else if (ExtendedEditorControl.HasRoundedCornerProperty.PropertyName == e.PropertyName)
            {
                if (customControl.HasRoundedCorner)
                {
                    //ApplyBorder();

                }
                else
                {
                    //this.Control.Background = originalBackground;
                    this.Control.Background = new ColorDrawable(Android.Graphics.Color.White);
                }
            }
            else if (ExtendedEditorControl.IsFocusedProperty.PropertyName == e.PropertyName)
            {
                var chatEntryView = this.Element as ExtendedEditorControl;
             //   Control.OnFocusChangeListener = new dd();
                //Control.ShouldEndEditing = (textField) =>
                //{
                //    ChatInputBarView chatInputBarView = null;
                //    var parent = chatEntryView.Parent;
                //    while (parent as ChatInputBarView == null)
                //    {
                //        parent = parent.Parent;
                //    }
                //    chatInputBarView = parent as ChatInputBarView;

                //    return !chatInputBarView.KeepTextInputFocus;
                //};
            }
        }

        void ApplyBorder()
        {
            //GradientDrawable gd = new GradientDrawable();
            //gd.SetCornerRadius(10);
            //gd.SetStroke(2, Color.Black.ToAndroid());
            //this.Control.Background = gd;
        }
    }
    class dd : Java.Lang.Object,Android.Views.View.IOnFocusChangeListener
    {
        

        public void Dispose()
        {
            this.Dispose();
        }

        public void OnFocusChange(Android.Views.View v, bool hasFocus)
        {
            var formsEditText  = v as FormsEditText;
            if (formsEditText as FormsEditText !=null)
            {
                formsEditText.RequestFocus();
            }
        }
    }
}
