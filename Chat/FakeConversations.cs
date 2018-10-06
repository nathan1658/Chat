using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Chat.Models;

namespace Chat
{
    public class FakeConversations
    {



        public static List<Conversation> GenerateFakeConversations()
        {

            var result = new List<Conversation>()
              {
            new Conversation(){
                Title = "Normal Conversation",
                LatestMessage = new Message(){ User="Stan",Text="Some Latest Message", SubmittedDate=DateTime.Now}
        },
            new Conversation()
        {
            Title = "aisdjiasdji"
                }};


            var Messages = result[0].Messages;

            Messages.Add(new Message() { Text = "yoyofyo~", User = "Peter", SubmittedDate = DateTime.Now });
            Messages.Add(new Message() { Text = "hihihi", User = "Mary", SubmittedDate = DateTime.Now });
            for (int i = 0; i < 20; i++)
            {
                Messages.Add(new Message() { Text = $"Message {i}", User = "Cartman", SubmittedDate = DateTime.Now.Add(new TimeSpan(0, -i, 0)) });
            }
            return result;

        }


        public FakeConversations()
        {




        }
    }
}
