using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.ViewModels;
using Xamarin.Forms;

namespace Chat.Views.Partials
{
    public partial class MessageBoard : ContentView
    {
        enum ScreenState
        {
            Mininize = 0,
            OneThird = 1,
            Maximize = 2 
        }

        Grid parentGrid = null;
        double oneThirdHeight = Application.Current.MainPage.Height/3.2;
        double parentGridHeight = 0;
        private uint animateLength = 500;
        private ScreenState screenState =  ScreenState.OneThird;        
        public MessageBoard()
        {
            InitializeComponent();
            CollapsableLayout.HeightRequest = oneThirdHeight;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
      
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {

            //Get Rendered Height..
            parentGrid = this.Parent as Grid;

            if (parentGrid != null)
            {
                parentGridHeight = parentGrid.Height;                    
            }
            switch (screenState)
            {
                case ScreenState.Mininize://Go to 1/3 size
                    CollapsableLayout.Animate("inv", (x) => { CollapsableLayout.HeightRequest = x; }, this.Height, oneThirdHeight, 16, animateLength, Easing.CubicOut);
                    CollapseButton.Text = "⬆️";
                    updateChildVisibility(true);
                    this.screenState = ScreenState.OneThird;
                    break;
                case ScreenState.OneThird: //Go full size
                    CollapsableLayout.Animate("inv", (x) => { CollapsableLayout.HeightRequest = x; }, this.Height, parentGridHeight, 16, animateLength, Easing.CubicOut);
                    CollapseButton.Text = "⬆️";
                    updateChildVisibility(false);
                    this.screenState = ScreenState.Maximize;
                    break;
                case ScreenState.Maximize:
                    CollapsableLayout.Animate("inv", (x) => { CollapsableLayout.HeightRequest = x; }, this.Height, 0, 16, animateLength, Easing.CubicOut);
                    CollapseButton.Text = "⬆️";
                    updateChildVisibility(true);
                    this.screenState = ScreenState.Mininize;
                    break;
            }

                       

        }

        void updateChildVisibility(bool x)
        {
            var parentGrid = this.Parent as Xamarin.Forms.Grid;            
            if (parentGrid != null)
            {
                foreach (var child in parentGrid.Children)
                {
                    if (child != this && child as ListView == null)
                    {
                        child.IsVisible = x;
                    }
                }
            }
        }
    }

}
