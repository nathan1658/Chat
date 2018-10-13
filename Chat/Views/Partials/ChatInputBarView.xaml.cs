using System;
using System.Collections.Generic;
using System.IO;
using Chat.ViewModels;
using Plugin.Media;
using Xamarin.Forms;



namespace Chat.Views.Partials
{
    public partial class ChatInputBarView : ContentView
    {

        ChatPage chatPage; 

        public ChatInputBarView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                //  this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, null, null, null, chatTextInput));
            }

            this.SizeChanged += (s, e) =>
              {
                  base.OnParentSet();
                  var g = this.Parent;
                  if (this.Parent != null)
                  {
                      while (!g.GetType().Equals(typeof(ChatPage)))
                      {
                          g = g.Parent;
                      }
                  }
                  chatPage = g as ChatPage;
              };
            
            

            chatTextInput.Focused += (s, e) =>
            {
                KeepTextInputFocus = true;
            };

        }

        private void setParent()
        {
          

        }

        public bool KeepTextInputFocus = false;



        protected void SendTapped(object sender, System.EventArgs e)
        {
            (this.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);
            if(chatPage == null)
            {
                setParent();
            }
            chatPage.ScrollListCommand.Execute(null);
        }


        protected void StopWatchTapped(object s, EventArgs e)
        {
            this.UnFocusEntry();
            picker.Focus();

        }
        //TODO launch panel for selecting attatchment
        protected async void AttatchmentTapped(object s, EventArgs e)
        {

            if (chatPage == null)
            {
                setParent();
            }

            var action  = await chatPage.DisplayActionSheet("Select an action", "Cancel", null,"Camera","Library","File");
            switch(action)
            {
                case "Camera":
                    takePhoto();
                    break;
                case "Library":
                    pickFromLibrary();
                    break;
                default:
                    //TODO
                    chatPage.DisplayAlert("Error", "Sosad not yet implement","Cancel");
                    break;
            }
        }


        async void takePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
              //  DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
             //   Directory = "Sample",
               // Name = "test.jpg",
                SaveToAlbum = false
                
            });

            if (file == null)
                return;

            // await DisplayAlert("File Location", file.Path, "OK");
            var vm = this.BindingContext as ChatPageViewModel;
           

            var stream = file.GetStream();
            var imageByteArr = readFully(stream);
            stream.Dispose();
            file.Dispose();
            vm.SubmitMessage(imageByteArr, App.User, DateTime.Now);
          
           
        }

        private byte[] readFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }


        async void pickFromLibrary()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported )
            {
                //  DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }


            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            // await DisplayAlert("File Location", file.Path, "OK");

            var vm = this.BindingContext as ChatPageViewModel;
            var stream = file.GetStream();
            var imageByteArr = readFully(stream);
            file.Dispose();
            stream.Dispose();
            vm.SubmitMessage(imageByteArr, App.User, DateTime.Now);
        }




        public void UnFocusEntry()
        {
            chatTextInput?.Unfocus();
            KeepTextInputFocus = false;
        }

        public void FocusEntry()
        {
            chatTextInput?.Focus();
            KeepTextInputFocus = true;
        }



    }
}
