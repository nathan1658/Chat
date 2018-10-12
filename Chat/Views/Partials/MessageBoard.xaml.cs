using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.ViewModels;
using Xamarin.Forms;

namespace Chat.Views.Partials
{
    public partial class MessageBoard : ContentView
    {

        private bool isExpanded = true;
        private double expandedHeight = -1;
        public MessageBoard()
        {
            InitializeComponent();
        
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {        
            //Get Rendered Height..

            //TODO Fix ios bugs (height issue..)
            if(expandedHeight == -1)
            {
                expandedHeight = CollapsableLayout.Height;
            }
            expandedHeight = 100;
            uint length = 500;
            if (isExpanded)
            {
                CollapsableLayout.Animate("inv", (x) => { CollapsableLayout.HeightRequest = x; }, expandedHeight, 0, 16, length, Easing.CubicOut);
                CollapseButton.Text = "⬇️";

            }else
            {
                CollapsableLayout.Animate("inv", (x) => { CollapsableLayout.HeightRequest = x; }, 0, expandedHeight, 16, length, Easing.CubicOut);
                CollapseButton.Text = "⬆️";
            }
            isExpanded = !isExpanded;

        }


    }

}
