using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chat.Interfaces;
using Chat.Models;
using Chat.ViewModels;
using Xamarin.Forms;

namespace Chat.Views
{
    public partial class ChatPage : ContentPage, IChatPage
    {

        public ICommand ScrollListCommand { get; set; }

        public ChatPage()
        {
            InitializeComponent();
        
            //ChatList.ItemAppearing += (sender, e) =>
            //{
            //    Message msg = e.Item as Message;
            //    var itemSource = ChatList.ItemsSource as ObservableCollection<Message>;
            //    if (msg != null && itemSource != null && msg.OutgoingMessage && msg == (itemSource.Last()))
            //    {
            //        ChatList.ScrollTo(e.Item, ScrollToPosition.MakeVisible, false);
            //    }
            //};
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
         
        }


        public ChatPage(Conversation con) : this()
        {
            var vm = new Chat.ViewModels.ChatPageViewModel(con);
            
            vm.View = this;
            this.BindingContext = vm;

            this.Title = con.Title;
            ScrollListCommand = new Command(() =>
            {
                ChatList.ScrollToLast();
            });
            if (!string.IsNullOrEmpty(con.HTMLTable))
            {
                var webView = MessageBoard.FindByName<WebView>("webView");
                webView.Source = new HtmlWebViewSource
                {
                    Html = con.HTMLTable
                };
            }

            this.ChatList.ScrollToLast(false);

        }

        private void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            InputBar.UnFocusEntry();
        }


        public void ScrollTap(object sender, System.EventArgs e)
        {
            
                if (BindingContext != null)
                {
                    var vm = BindingContext as ChatPageViewModel;

                    
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;

                        ChatList.ScrollToLast();
                    
                }                
        }


        internal async Task FocusEntryAsync()
        {
            await Task.Delay(1);
            InputBar.FocusEntry();

        }

        public void GenerateButtons(IList<Button> buttonList)
        {
            var fl = ChatButtonsBar.FindByName<FlexLayout>("ButtonFlexLayout");
            if (fl != null)
            {
                foreach (var btn in buttonList)
                {
                    //EventHandler bb = null;
                    //bb = new EventHandler(delegate (object o, EventArgs e)
                    //{
                    //    var newBtn = new Button();
                    //    newBtn.Text = "NEW";
                    //    newBtn.BackgroundColor = Color.Yellow;
                    //    newBtn.Clicked += bb;
                    //    fl.Children.Add(newBtn);
                    //});                   
                    // btn.Clicked += bb;

                    btn.Clicked += (s, e) =>
                     {
                         MessagingCenter.Send<ChatPage, string>(this,"ButtonPressed", btn.Text);
                     };

                    //TODO maybe set on click binding here
                    fl.Children.Add(btn);
                }
            }

            fl.ForceLayout();
        }

        public void GenerateMessageBoard(string s)
        {
            throw new NotImplementedException();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnListTapped(null,null);
        }

    }
}
