using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Chat.Models;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Chat.ViewModels
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        private Conversation _conversation;
        private List<string> chatExample = new List<string>{
            @"你幾時拍拖呀？",
            @"你幾時結婚呀？",
            @"成3X歲人都重逗利是？",
            @"幾時生番個嚟玩下？",
            @"你好似肥咗喎！",
            @"你做緊邊行呀？",
            @"你有冇睇《愛．回家》？",
            @"我個仔好叻呀，早排佢先考第1，你讀書讀成點呀？",
            @"我近排先換咗架Tesla，快過之前架Audi幾多呀，0至100乜乜乜……",
            @"而家啲後生仔真係唔似樣，有書唔讀，走去罷課……"
            };

        private List<string> userExample = new List<string>
        {
            "Stan",
            "Kyle",
            "Kenny",
            "Cartman",
            "Butters",
            "Mandy",
            "Token",
            "Jimmy",
            "Timmy"
        };


        public ChatPageViewModel(Conversation con)
        {
            _conversation = con;
            OnSendCommand = new Command(() =>
            {
                if(!string.IsNullOrEmpty(TextToSend))
                {
                    SubmitMessage(TextToSend,App.User,DateTime.Now);
                }
            });
            Messages = new ObservableCollection<Message>(_conversation.Messages);
            Messages = new ObservableCollection<Message>(Messages.OrderBy(x => x.SubmittedDate).ToList());
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
             {
                 var msg = GenRandomMessage();
                 if (LastMessageVisible)
                 {

                     SubmitMessage(msg);
                 }
                 else
                 {
                     DelayedMessages.Enqueue(msg);
                     PendingMessageCount++;
                 }
                 return true;
             });

            MessageAppearingCommand = new Command<Message>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<Message>(OnMessageDisappearing);

        }

        Message GenRandomMessage()
        {
            var random = new Random();
            var text = chatExample[random.Next(chatExample.Count)];
            var user = userExample[random.Next(userExample.Count)];
            Message msg = new Message() { Text = text, User = user, SubmittedDate = DateTime.Now };
            return msg;
            
        }


        void SubmitMessage(string TextToSend,String User, DateTime SubmittedDate)
        {
            string formattedText = TextToSend;

            formattedText = formattedText.TrimStart('\n').TrimEnd('\n');

            Messages.Insert(0, new Message() { Text = formattedText, User = User, SubmittedDate = SubmittedDate });
            TextToSend = string.Empty;
        }

        void SubmitMessage(Message msg)
        {
            Messages.Insert(0,msg);
            TextToSend = string.Empty;
        }

        void OnMessageAppearing(Message msg)
        {
            var idx = Messages.IndexOf(msg);
            if (idx <= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        Messages.Insert(0, DelayedMessages.Dequeue());
                    }
                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }
        void OnMessageDisappearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }


        public ObservableCollection<Message> Messages
        {
            get;
            set;
        } = new ObservableCollection<Message>();

        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowScrollTap { get; set; } = false; //Show the jump icon 
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }
        public Queue<Message> DelayedMessages { get; set; } = new Queue<Message>();
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }
    }
}
