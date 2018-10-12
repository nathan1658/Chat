using System;
using System.ComponentModel;
using System.Timers;

namespace Chat.Models
{
    public class Message:INotifyPropertyChanged
    {
        public string Text
        {
            get;
            set;
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
                    HaveTimeout = true;
                    RemainingTime = value;
                    startTime = DateTime.Now;
                    timer = new Timer(1);
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();
                }
            }
        }

        public bool HaveTimeout { get; set; }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
            if(timeoutValue.TotalSeconds <= elapsedSeconds)
            {
                IsExpired = true;
                timer.Stop();
            };
            RemainingTime =  TimeSpan.FromSeconds(timeoutValue.TotalSeconds - elapsedSeconds);
        }

        public TimeSpan RemainingTime { get; set; }

        private Timer timer = new Timer();

        public String RemainingTimeString { get { if(RemainingTime!=null)return RemainingTime.ToString();return "aaa";}}

        public bool IsExpired
        { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

