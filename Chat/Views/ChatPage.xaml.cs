using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chat.Models;
using Chat.ViewModels;
using Xamarin.Forms;

namespace Chat.Views
{
    public partial class ChatPage : ContentPage
    {

        public ICommand ScrollListCommand { get; set; }

        public ChatPage()
        {
            InitializeComponent();
        }

        public ChatPage(Conversation con):this()
        {
            
            this.BindingContext = new Chat.ViewModels.ChatPageViewModel(con);
            this.Title = con.Title;
            ScrollListCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ChatList.ScrollTo((this.BindingContext as ChatPageViewModel).Messages.Last(), ScrollToPosition.End, true);                    
                });
            });

        }
    }
}
