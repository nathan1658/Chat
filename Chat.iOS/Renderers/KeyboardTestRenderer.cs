using System;
using Chat.iOS.Renderers;
using Chat.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(KeyboardTest), typeof(KeyboardTestRenderer))]
namespace Chat.iOS.Renderers
{
    public class KeyboardTestRenderer : PageRenderer
    {
        NSObject notificationToken;
        ListView listView = null;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            notificationToken = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.FrameBeginUserInfoKey, keyboardShow);
  
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if(e.NewElement!=null)
            {
                var page = e.NewElement as KeyboardTest;
                listView = page.FindByName<ListView>("listView");

            }
        }

        void keyboardShow(NSNotification not)
        {
            listView.BackgroundColor = Color.AliceBlue;
        }

        //public override void LoadView()
        //{
        //    base.LoadView();
        //    UIScrollView scrollView = new UIScrollView(UIScreen.MainScreen.Bounds);
        //    scrollView.ContentSize = new CoreGraphics.CGSize((int)UIScreen.MainScreen.Bounds.Height, (int)UIScreen.MainScreen.Bounds.Width);
        //    this.View = scrollView;
        //}

    }
}
