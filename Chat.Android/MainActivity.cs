using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Serialization;
using XLabs.Platform.Device;

namespace Chat.Droid
{
    [Activity(Label = "Chat", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            #region plugin init
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Stormlion.PhotoBrowser.Droid.Platform.Init(this);
            XlabsInit();
            #endregion

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }


        private void XlabsInit()
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer.Register(t => AndroidDevice.CurrentDevice);
            resolverContainer.Register<IJsonSerializer, XLabs.Serialization.JsonNET.JsonSerializer>();
            resolverContainer.Register<IDependencyContainer>(resolverContainer);            

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}