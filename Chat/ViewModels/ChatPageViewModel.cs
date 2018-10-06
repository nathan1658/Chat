using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Chat.Models;
using System.Linq;
using Xamarin.Forms;

namespace Chat.ViewModels
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        private Conversation _conversation;

        public ChatPageViewModel(Conversation con)
        {
            _conversation = con;
            OnSendCommand = new Command(() =>
            {
                if(!string.IsNullOrEmpty(TextToSend))
                {
                    Messages.Add(new Message() { Text = TextToSend, User = App.User,SubmittedDate = DateTime.Now });
                    TextToSend = string.Empty;

                }
            });
            Messages = new ObservableCollection<Message>(_conversation.Messages);
            Messages = new ObservableCollection<Message>(Messages.OrderBy(x => x.SubmittedDate).ToList());

        }
        public ObservableCollection<Message> Messages
        {
            get;
            set;
        } = new ObservableCollection<Message>();

        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
