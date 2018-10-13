using Chat.Models;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Chat.Views.Cells
{
    public partial class OutgoingViewCell : ViewCell
    {
        public OutgoingViewCell()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Message msg = this.BindingContext as Message;
            if (msg != null)
            {
                var tmpFile = System.IO.Path.GetTempFileName();
                tmpFile = Path.GetFileName(tmpFile);
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), tmpFile);

                File.WriteAllBytes(fileName, msg.PhotoAttatchment);


                new PhotoBrowser
                {
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            URL= @"File://"+fileName
                        }
                    },
                    ActionButtonPressed = (index) =>
                    {
                        System.Diagnostics.Debug.WriteLine($"Clicked {index}");
                    }
                }.Show();

                //Remove image after preview..
                try
                {
                    File.Delete(fileName);
                }catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error when deleting tmp image: " + ex.Message);
                }
            }
        }
    }
}
