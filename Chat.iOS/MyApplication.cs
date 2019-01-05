using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Chat.iOS
{
    [Register("MyApplication")]
    public class MyApplication : UIApplication
    {
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // ...
        }
    }
}