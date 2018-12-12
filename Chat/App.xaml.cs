using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Chat.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Chat.ViewModels;
using DLToolkit.Forms.Controls;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Chat
{
    public partial class App : Xamarin.Forms.Application
    {
        public static string User = "SpongeBob";
        public static INavigation Navigation { get; set; }

        public App()
        {
#if DEBUG
              LiveReload.Init();
#endif
            FlowListView.Init();
            InitializeComponent();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            MainPage = new NavigationPage(new ConversationSelector());
            //var nav = new NavigationPage();
            //MainPage = nav;

            ////nav.PushAsync(new ChatPage(FakeConversations.GenerateFakeConversations()[0]));
            ////MainPage = new NavigationPage(new PickerTest());
            //MainPage = new KeyboardTest();
            //Navigation = MainPage.Navigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
