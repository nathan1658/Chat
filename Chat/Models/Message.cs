using Chat.Helpers;
using Chat.Interfaces;
using System;
using System.ComponentModel;
using System.IO;
using System.Timers;
using Xamarin.Forms;

namespace Chat.Models
{
    public class Message : INotifyPropertyChanged
    {

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public string User
        {
            get;
            set;
        }

        public DateTime SubmittedDate
        {
            get;
            set;
        }

        public byte[] PhotoByte { get; set; }


        private ImageSource _imageThumbnail;

        public ImageSource ImageThumbnail
        {
            get
            {
                if (PhotoByte != null && _imageThumbnail == null)
                {                                     
                    try
                    {
                        var compressedImage = DependencyService.Get<IPhotoResizer>().ResizeImage(PhotoByte, 150, 150, 75);
                        //Generate compressed image..
                        
                        _imageThumbnail = ImageSource.FromStream(() => { return new MemoryStream(compressedImage); });
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error when generating image thumbnail, returning null, exception: "+ex.Message);
                        return null;
                    }
                    
                }
                if(PhotoByte!=null)
                {
                    return _imageThumbnail;
                }
                else
                {
                    return null;
                }
            }
        }


        public byte[] PDFByte { get; set; }


        public Conversation Conversation { get; set; }

        private DateTime startTime;
        private TimeSpan timeoutValue;
        public TimeSpan TimeOutValue
        {
            get
            {
                return timeoutValue;
            }
            set
            {
                timeoutValue = value;
                if (timeoutValue.TotalSeconds > 0)
                {                    
                    RemainingTime = value;
                    startTime = DateTime.Now;
                    timer = new Timer(1);
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();
                }
            }
        }

        public bool OutgoingMessage { get; set; } = false;

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
            if (timeoutValue.TotalSeconds <= elapsedSeconds)
            {
                IsExpired = true;
                timer.Stop();
                timer.Dispose();
            };
            RemainingTime = TimeSpan.FromSeconds(timeoutValue.TotalSeconds - elapsedSeconds);
        }

        public TimeSpan RemainingTime { get; set; }

        private Timer timer = new Timer();

        public String RemainingTimeString
        {
            get
            {
                if (RemainingTime != null && RemainingTime.TotalSeconds > 0)
                {
                    return RemainingTime.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        private bool _isExpired;
        public bool IsExpired
        {
            get
            {
                return _isExpired;
            }
            set
            {
                _isExpired = value;
                if(IsExpired == true)
                {
                    Text = null;
                    PhotoByte = null;
                    PDFByte = null;
               }

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

