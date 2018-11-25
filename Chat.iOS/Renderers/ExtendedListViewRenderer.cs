﻿using Chat.Controls;
using Chat.iOS.Renderers;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace Chat.iOS.Renderers
{
    public class ExtendedListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if(e.NewElement as ExtendedListView!=null)

                if (Control != null)
                {        
                    Control.AllowsSelection = false;
                    Control.AlwaysBounceVertical = false;
                    Control.Bounces = true;
                    Control.ScrollsToTop = true;

                   // Control.RowHeight = UITableView.AutomaticDimension;
                   // Control.EstimatedRowHeight = 100;
                }
            }

        }
    }
}
