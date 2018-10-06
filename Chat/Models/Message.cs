using System;
namespace Chat.Models
{
    public class Message
    {
        public string Text
        {
            get;
            set;
        }

        public string User
        {
            get;
            set;
        }

        public DateTime SubmittedDate
        {
            get;
            set;
        }

        public Conversation Conversation { get; set; }


    }
}

