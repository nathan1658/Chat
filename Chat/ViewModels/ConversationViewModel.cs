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
            int aa = 0;
            foreach(var msg in ConversationList[0].Messages)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", aa++, msg.Text));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
