using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Chat.Models;

namespace Chat.ViewModels
{
    public class ConversationViewModel:INotifyPropertyChanged
    {

        public ObservableCollection<Conversation> ConversationList { get; set; } = new ObservableCollection<Conversation>();

        public ConversationViewModel()
        {
            ConversationList = new ObservableCollection<Conversation>(FakeConversations.GenerateFakeConversations());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
