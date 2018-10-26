using System;
using Chat.Controls;
using Chat.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedGroupHeaderViewCell), typeof(ExtendedGroupHeaderViewCellRenderer))]
namespace Chat.iOS.Renderers
{
    public class ExtendedGroupHeaderViewCellRenderer : ViewCellRenderer
    {
        public ExtendedGroupHeaderViewCellRenderer()
        {
        }


        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item,reusableCell, tv);
            if (cell != null)
            {
                //TODO row height is not working..
                cell.BackgroundColor = UIColor.Clear;
                tv.RowHeight = UITableView.AutomaticDimension;
                tv.EstimatedRowHeight = 100;

            }
            return cell;
        }
    }
}
