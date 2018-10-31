using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Chat.Interfaces
{
    public interface IChatPage
    {
        void GenerateButtons(IList<Button> buttonList);
        void GenerateMessageBoard(string s);
    }
}
