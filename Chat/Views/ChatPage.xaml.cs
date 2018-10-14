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
                Device.BeginInvokeOnMainThread(scrollToBottom);
            });
            if(!string.IsNullOrEmpty(con.HTMLTable))
            {
                var webView = MessageBoard.FindByName<WebView>("webView");
                webView.Source = new HtmlWebViewSource
                {
                    Html = con.HTMLTable
                };
            }
                   
        }

        private void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            InputBar.UnFocusEntry();
        }


        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = BindingContext as ChatPageViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (vm.DelayedMessages.Count > 0)
                        {
                            vm.Messages.Insert(0, vm.DelayedMessages.Dequeue());
                        }
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;

                        scrollToBottom();
                    });
                }

            }
        }

        void scrollToBottom()
        {           
            ChatList.ScrollTo((this.BindingContext as ChatPageViewModel).Messages.Last(), ScrollToPosition.End, true);   
        }

        internal void FocusEntry()
        {            
                InputBar.FocusEntry();          
        }


    }
}
