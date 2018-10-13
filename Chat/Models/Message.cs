using Chat.Helpers;
using Chat.Interfaces;
using Chat.Shared.Helpers;
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

        public byte[] PhotoAttatchment { get; set; }


        private ImageSource _imageThumbnail;

        public ImageSource ImageThumbnail
        {
            get
            {
                if (PhotoAttatchment != null && _imageThumbnail == null)
                {                                     
                    try
                    {
                        var compressedImage = DependencyService.Get<IPhotoResizer>().ResizeImage(PhotoAttatchment, 50, 50, 50);
                        //Generate compressed image..
                        
                        _imageThumbnail = ImageSource.FromStream(() => { return new MemoryStream(compressedImage); });
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error when generating image thumbnail, returning null, exception: "+ex.Message);
                        return null;
                    }
                    
                }
                if(PhotoAttatchment!=null)
                {
                    return _imageThumbnail;
                }
                else
                {
                    return null;
                }
            }
        }



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

        public bool IsExpired
        { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

