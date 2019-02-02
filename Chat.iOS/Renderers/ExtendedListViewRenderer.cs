using Chat.Controls;
using Chat.iOS.Renderers;
using CoreGraphics;
using Foundation;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace Chat.iOS.Renderers
{
    public class ExtendedListViewRenderer : ListViewRenderer
    {

        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (e.NewElement as ExtendedListView != null)
                {
                    if (Control != null)
                    {
                        Control.AllowsSelection = false;
                        Control.AlwaysBounceVertical = false;
                        Control.Bounces = true;
                        Control.ScrollsToTop = true;

                        // Control.RowHeight = UITableView.AutomaticDimension;
                        // Control.EstimatedRowHeight = 100;
                    }
                }
            }


            //Keyboard
            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
                (e.NewElement as ExtendedListView).IOSUPdateListView = new Action(() =>
                {
                    Control.BeginUpdates();
                    Control.EndUpdates();
                });

            }

            if (e.OldElement != null)
            {
                UnregisterForKeyboardNotifications();
            }

        }

        void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs args)
        {
            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;
            var inset = Control.ContentInset;
            inset.Bottom = 0;
            Control.ContentInset = inset;
            Control.ScrollIndicatorInsets = Control.ContentInset;
            var offset = Control.ContentOffset;
            offset.Y += keyboardSize.Height;
            Control.SetContentOffset(offset, true);
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
            //if (Element != null)
            //{
            //    Element.Margin = new Thickness(0); //set the margins to zero when keyboard is dismissed
            //}

        }


        void UnregisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver != null)
            {
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }
    }
}
