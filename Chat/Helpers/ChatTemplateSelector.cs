using System;
using Xamarin.Forms;
using Chat.Views.Cells;
using Chat.Models;

namespace Chat.Helpers
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;
        DataTemplate systemMessageDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
            this.systemMessageDataTemplate = new DataTemplate(typeof(SystemMessageViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;



            if (messageVm == null)
            {
                return null;
            }

            //TODO currently if messageVM.user is null, return system message cell.
            if(string.IsNullOrEmpty(messageVm.User))
            {
                return systemMessageDataTemplate;
            }

            return (messageVm.User == App.User) ? outgoingDataTemplate : incomingDataTemplate;


        }
    }
}
