using System;
using Chat.iOS.Renderers;
using Chat.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ChatPage), typeof(ChatPageRenderer))]
namespace Chat.iOS.Renderers
{
    public class ChatPageRenderer : PageRenderer
    {

        NSObject observerHideKeyboard;
        NSObject observerShowKeyboard;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            observerHideKeyboard = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
            observerShowKeyboard = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            NSNotificationCenter.DefaultCenter.RemoveObserver(observerHideKeyboard);
            NSNotificationCenter.DefaultCenter.RemoveObserver(observerShowKeyboard);
        }

        void OnKeyboardNotification(NSNotification notification)
        {
            if (!IsViewLoaded) return;

            var frameBegin = UIKeyboard.FrameBeginFromNotification(notification);
            var frameEnd = UIKeyboard.FrameEndFromNotification(notification);

            var page = Element as ContentPage;
            if (page != null && !(page.Content is ScrollView))
            {
                var padding = page.Padding;
                page.Padding = new Thickness(padding.Left, padding.Top, padding.Right, padding.Bottom + frameBegin.Top - frameEnd.Top);
            }
        }

    }
}
