using System;
using System.Collections.Generic;
using Chat.Models;
using Chat.ViewModels;
using Xamarin.Forms;

namespace Chat.Views
{
    public partial class ConversationSelector : ContentPage
    {

        public ConversationSelector()
        {
            InitializeComponent();
            this.BindingContext = new ConversationViewModel();
        }


        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            var conversation = e.Item as Conversation;
            await Navigation.PushAsync(new ChatPage(conversation));


        }
    }
}
