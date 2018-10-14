using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Chat.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Chat
{
    public partial class App : Application
    {
        public static string User = "SpongeBob";
        public static INavigation Navigation { get; set; }

        public App()
        {
#if DEBUG
            //LiveReload.Init();
#endif

            InitializeComponent();
            
            MainPage = new NavigationPage(new ConversationSelector());
            Navigation = MainPage.Navigation;
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
