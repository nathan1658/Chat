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
            expandedHeight = 100;
            //Get Rendered Height..
            var parentGrid = this.Parent as Xamarin.Forms.Grid;

            if (parentGrid!=null)
            {
                expandedHeight = parentGrid.Height ;
            }
            //TODO Fix ios bugs (height issue..)
            if(expandedHeight == -1)
            {
                expandedHeight = CollapsableLayout.Height;
            }
            Action<bool> updateChildVisibility = new Action<bool>((x) =>
             {
                 if(parentGrid!=null)
                 {
                     foreach(var child in parentGrid.Children)
                     {
                         if(child!=this && child as ListView==null)
                         {
                             child.IsVisible = x;
                         }
                     }
                 }
             });
           
            uint length = 500;
            if (isExpanded)
            {
                CollapsableLayout.Animate("inv", (x) => { CollapsableLayout.HeightRequest = x; }, expandedHeight, 0, 16, length, Easing.CubicOut);
                CollapseButton.Text = "⬇️";
                updateChildVisibility(true);
            }
            else
            {
                CollapsableLayout.Animate("inv", (x) => { CollapsableLayout.HeightRequest = x; }, 0, expandedHeight, 16, length, Easing.CubicOut);
                CollapseButton.Text = "⬆️";
                updateChildVisibility(false);
            }
            isExpanded = !isExpanded;

        }


    }

}
