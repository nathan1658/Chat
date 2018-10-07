using System;
using System.Collections.Generic;
using Chat.ViewModels;
using Xamarin.Forms;



namespace Chat.Views.Partials
{
    public partial class ChatInputBarView : ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();
            
            if (Device.RuntimePlatform == Device.iOS)
            {
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, null, null, null, chatTextInput));
            }
        }

        protected void Handle_Completed(object sender, System.EventArgs e)
        {
            (this.Parent.Parent.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);

            (this.Parent.Parent as ChatPage).ScrollListCommand.Execute(null);
        }
        public void UnFocusEntry()
        {
            chatTextInput?.Unfocus();
        }
    }
}
