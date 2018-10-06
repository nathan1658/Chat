using System.Collections.Generic;

namespace Chat.Models
{
    //TODO mark it abstract?
    public  class Conversation
    {
        public string Title
        {
            get;
            set;
        }

        public List<User> Participants
        {

            get;
            set;
        }


        //TODO Don't load all message!
        public List<Message> Messages { get; set; } = new List<Message>();

        public Message LatestMessage { get; set; }

        private ConversationTypeEnum ConversationType { get; set; }


    }
}