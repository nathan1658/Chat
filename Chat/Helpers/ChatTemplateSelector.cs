﻿using System;
using Xamarin.Forms;
using Chat.Views.Cells;
using Chat.Models;

namespace Chat.Helpers
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;


        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;

            if (messageVm == null)
            {
                return null;
            }

            return (messageVm.User == App.User) ? outgoingDataTemplate : incomingDataTemplate;


        }
    }
}
