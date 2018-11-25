//using System;
//using Chat.iOS.Renderers;
//using Chat.Views;
//using UIKit;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;

//[assembly:ExportRenderer(typeof(ChatPage),typeof(ChatPageRenderer))]
//namespace Chat.iOS.Renderers
//{
//    public class ChatPageRenderer:PageRenderer
//    {
//        public override void ViewDidLoad()
//        {
//            base.ViewDidLoad();


//            Xamarin.IQKeyboardManager.SharedManager.EnableAutoToolbar = false;
//            Xamarin.IQKeyboardManager.SharedManager.ShouldResignOnTouchOutside = true;
//            Xamarin.IQKeyboardManager.SharedManager.Enable = false;
//        }

//        //public override void LoadView()
//        //{
//        //    base.LoadView();
//        //    UIScrollView scrollView = new UIScrollView(UIScreen.MainScreen.Bounds);
//        //    scrollView.ContentSize = new CoreGraphics.CGSize((int)UIScreen.MainScreen.Bounds.Height, (int)UIScreen.MainScreen.Bounds.Width);
//        //    this.View = scrollView;
//        //}
        
//    }
//}
