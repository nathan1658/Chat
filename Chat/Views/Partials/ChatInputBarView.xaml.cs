using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Chat.ViewModels;
using Chat.Views.Popups;
using Plugin.Media;
using Rg.Plugins.Popup.Services;
using Stormlion.PhotoBrowser;
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
            if (chatPage == null)
            {
                setParent();
            }
            chatPage.ScrollListCommand.Execute(null);
            chatPage.FocusEntryAsync();
            //chatPage.ChatList.Focused += (s, e) =>
            //{
            //    InputBar.FocusEntry();
            //};
        }


        protected async void StopWatchTapped(object s1, EventArgs e)
        {
            this.UnFocusEntry();

            MessagingCenter.Subscribe<WheelPickerPopup, TimeSpan>(this, "ReturnedTimeSpan", (s, ts) =>
            {
                var vm = this.BindingContext as ChatPageViewModel;
                vm.CountDownValue = ts;
                MessagingCenter.Unsubscribe<WheelPickerPopup, TimeSpan>(this, "ReturnedTimeSpan");

            });

            new PhotoBrowser
            {
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Vincent.jpg",
                        Title = "Vincent"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Jules.jpg",
                        Title = "Jules"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Korben.jpg",
                        Title = "Korben"
                    }
                }

            }.Show();


            //  await PopupNavigation.Instance.PushAsync(new WheelPickerPopup((this.BindingContext as ChatPageViewModel).CountDownValue));

        }
        //TODO launch panel for selecting attatchment
        protected async void AttatchmentTapped(object s, EventArgs e)
        {

            if (chatPage == null)
            {
                setParent();
            }

            var action = await chatPage.DisplayActionSheet("Select an action", "Cancel", null, "Camera", "Library", "PDF", "File");
            switch (action)
            {
                case "Camera":
                    takePhoto();
                    break;
                case "Library":
                    pickFromLibrary();
                    break;
                case "PDF":
                    submitPDF();
                    break;
                default:
                    //TODO
                    chatPage.DisplayAlert("Error", "Sosad not yet implement", "Cancel");
                    break;
            }
        }

        void submitPDF()
        {
            //TODO Think the mechanism here, cache it or download it everytime?
            var vm = (this.BindingContext as ChatPageViewModel);
            if (vm != null)
            {
                var webClient = new WebClient();
                var data = webClient.DownloadData("http://www.africau.edu/images/default/sample.pdf");
                vm.SubmitMessage(null, null, data);
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
                Directory = "Temp",
                Name = "test.jpg",
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
            vm.SubmitMessage(null, imageByteArr, null);


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
            if (!CrossMedia.Current.IsPickPhotoSupported)
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
            vm.SubmitMessage(null, imageByteArr, null);
        }




        public void UnFocusEntry()
        {
            KeepTextInputFocus = false;
            chatTextInput?.Unfocus();
           
        }

        public void FocusEntry()
        {
            KeepTextInputFocus = true;
            chatTextInput?.Focus();
            
        }



    }
}
