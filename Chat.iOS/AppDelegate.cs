using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using Plugin.CrossPlatformTintedImage.iOS;
using UIKit;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Serialization;

namespace Chat.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

            

        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            #region plugin init
            Rg.Plugins.Popup.Popup.Init();
            Stormlion.PhotoBrowser.iOS.Platform.Init();
            XLabsInit();
            TintedImageRenderer.Init();
            #endregion

            Xamarin.Calabash.Start();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            KeyboardOverlapRenderer.Init();
            return base.FinishedLaunching(app, options);
        }

        private void XLabsInit()
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer.Register(t => AppleDevice.CurrentDevice);
            resolverContainer.Register<IJsonSerializer, XLabs.Serialization.JsonNET.JsonSerializer>();
            resolverContainer.Register<IDependencyContainer>(resolverContainer);
  

            Resolver.SetResolver(resolverContainer.GetResolver());
        }

        

    }
}
