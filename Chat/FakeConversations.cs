using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Chat.Models;
using Chat.ViewModels;

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
                LatestMessage = new Message(){ User="Stan",Text="Some Latest Message", SubmittedDate=DateTime.Now                   
                    },
                    HTMLTable=@"<table style=""width:100 % "">
  <tr>
    <th> Firstname </th>
    <th> Lastname </th>
    <th> Age </th>
  </tr>
  <tr>
    <td> Jill </td>
    <td> Smith </td>
    <td> 50 </td>
  </tr>
  <tr>
    <td> Eve </td>
    <td> Jackson </td>
    <td> 94 </td>
  </tr>
</table> "
        },
            new Conversation()
        {
            Title = "aisdjiasdji"
                }};


            var Messages = result[0].Messages;
            for (int i = 0; i < 100; i++)
            {
                var msg = ChatPageViewModel.GenRandomMessage();
                msg.SubmittedDate = DateTime.Now.Add(new TimeSpan(0, -i, 0));
                Messages.Add(msg);
            }
            for (int i = 0; i < 100; i++)
            {
                result.Add(new Conversation() { Title = $"Conversation {i}" });
            }


            return result;

          


        }


        public FakeConversations()
        {




        }
    }
}
