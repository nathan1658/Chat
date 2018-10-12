using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.ViewModels;
using Xamarin.Forms;

namespace Chat.Views.Partials
{
    public partial class ChatButtonsBarView : ContentView
    {
        public ChatButtonsBarView()
        {
            InitializeComponent();
        }

        void OK_ButtonClicked(object sender, System.EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var aa = await Application.Current.MainPage.DisplayAlert("OK", "Are you sure???", "Yes", "No");
                if (aa)
                {

                    var vm = this.BindingContext as ChatPageViewModel;
                    vm.SubmitMessage("You clicked yes", null, DateTime.Now);


                    ButtonsGrid.IsVisible = false;
                    ButtonResultLabel.IsVisible = true;

                    Task.Delay(5 * 1000).ContinueWith((x) =>
                      {
                          Device.BeginInvokeOnMainThread(() =>
                          {
                              ButtonsGrid.IsVisible = true;
                              ButtonResultLabel.IsVisible = false;
                          });
                      });
                };
            });


        }
    }
}
