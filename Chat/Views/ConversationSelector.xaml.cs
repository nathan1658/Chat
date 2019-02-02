using System;
using System.Collections.Generic;
using Chat.Models;
using Chat.ViewModels;
using Chat.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Chat.Views
{
    public partial class ConversationSelector : ContentPage
    {

        public ConversationSelector()
        {
            InitializeComponent();
            this.BindingContext = new ConversationViewModel();
           // test();
        }

        async void test()
        {
            await PopupNavigation.Instance.PushAsync(new WheelPickerPopup(new TimeSpan()));
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            var conversation = e.Item as Conversation;
            int aa = 0;
            foreach (var msg in conversation.Messages)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", aa++, msg.Text));
            }
            await Navigation.PushAsync(new ChatPage(conversation));


        }
    }
}
