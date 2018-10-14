using Chat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Chat.Helpers
{
    public class MessageBodyTemplateSelectorj : DataTemplateSelector
    {
        public DataTemplate ExpiredMessage { get; set; }
        public DataTemplate NormalMessage { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Message msg = item as Message;
            if(msg!=null)
            {
                if (msg.IsExpired)
                {
                    return ExpiredMessage;
                }
                else
                {
                    return NormalMessage;
                }
            }
            return NormalMessage;
        }
    }
}
