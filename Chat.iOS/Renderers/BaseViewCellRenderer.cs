using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.iOS.Renderers;
using Chat.Views.Cells;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseMessageViewCell), typeof(BaseViewCellRenderer))]
namespace Chat.iOS.Renderers
{
    public class BaseViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {

            if (reusableCell != null)
                reusableCell.BackgroundColor = UIColor.Blue;


            return base.GetCell(item, reusableCell, tv);

        }
        
    }

    //internal class NativeiOSCell : UITableViewCell, INativeElementView
    //{
    //    bool constraintsUpdated;

    //    public UILabel HeadingLabel { get; set; }

    //    public BaseMessageViewCell NativeCell { get; private set; }
    //    public Element Element => NativeCell;

    //    public NativeiOSCell(string cellId, BaseMessageViewCell cell) : base(UITableViewCellStyle.Default, cellId)
    //    {
    //        NativeCell = cell;

    //        SelectionStyle = UITableViewCellSelectionStyle.Gray;
    //        ContentView.BackgroundColor = UIColor.FromRGB(255, 255, 224);

    //        HeadingLabel = new UILabel()
    //        {
    //            Font = UIFont.FromName("Cochin-BoldItalic", 22f),
    //            TextColor = UIColor.FromRGB(127, 51, 0),
    //            BackgroundColor = UIColor.Clear,
    //            Lines = 0,
    //            LineBreakMode = UILineBreakMode.WordWrap
    //        };

    //        ContentView.Add(HeadingLabel);
    //    }

    //    public override void UpdateConstraints()
    //    {
    //        base.UpdateConstraints();
    //        if (!constraintsUpdated)
    //        {
    //            HeadingLabel.TranslatesAutoresizingMaskIntoConstraints = false;
    //            HeadingLabel.PreferredMaxLayoutWidth = ContentView.Bounds.Width - 30;
    //            HeadingLabel.SetContentCompressionResistancePriority(1000, UILayoutConstraintAxis.Vertical);
    //            HeadingLabel.LeftAnchor.ConstraintEqualTo(ContentView.LeftAnchor, 15).Active = true;
    //            HeadingLabel.RightAnchor.ConstraintEqualTo(ContentView.RightAnchor, -15).Active = true;
    //            HeadingLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, 10).Active = true;
    //            HeadingLabel.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -10).Active = true;
    //            constraintsUpdated = true;
    //        }
    //    }

    //    //public void UpdateCell(BaseMessageViewCell cell)
    //    //{
    //    //    HeadingLabel.Text = cell.Name;
    //    //}

    //    public override CGSize SizeThatFits(CGSize size)
    //    {
    //        return ContentView.SystemLayoutSizeFittingSize(UIView.UILayoutFittingCompressedSize);
    //    }
    //}
}