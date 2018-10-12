﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Chat.Models;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Timers;
using Chat.Views.Modals;
using Rg.Plugins.Popup.Services;

namespace Chat.ViewModels
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        private Conversation _conversation;
        private static readonly List<string> chatExample = new List<string>{
            @"你幾時拍拖呀？",
            @"你幾時結婚呀？",
            @"成3X歲人都重逗利是？",
            @"幾時生番個嚟玩下？",
            @"你好似肥咗喎！",
            @"你做緊邊行呀？",
            @"你有冇睇《愛．回家》？",
            @"我個仔好叻呀，早排佢先考第1，你讀書讀成點呀？",
            @"我近排先換咗架Tesla，快過之前架Audi幾多呀，0至100乜乜乜……",
            @"而家啲後生仔真係唔似樣，有書唔讀，走去罷課……",
            "今日lunch食咩好？",
            "幾時請食飯？",
            "幾時比家用？",
            "仲唔起身返工？",
            "關你咩事？",
            "你同我講野呀？",
            "收聲喇",
            "一人少句啦！！！！",
            "幾時開追悼會？",           
            "留返拜山先講ok?"
            };

        private static readonly List<string> userExample = new List<string>
        {
            "Stan",
            "Kyle",
            "Kenny",
            "Cartman",
            "Butters",
            "Tom",
            "Token",
            "Jimmy",
            "Timmy",
            "Peter"
        };



        public ChatPageViewModel(Conversation con)
        {
            _conversation = con;
            OnSendCommand = new Command(() =>
            {
                if(!string.IsNullOrEmpty(TextToSend))
                {
                    SubmitMessage(TextToSend,App.User,DateTime.Now);
                }
            });
            Messages = new ObservableCollection<Message>(_conversation.Messages);
            Messages = new ObservableCollection<Message>(Messages.OrderBy(x => x.SubmittedDate).ToList());
            var rnd = new Random();
            int rndInterval = 10 + rnd.Next(5);
            Timer timer = new Timer();
            timer.Interval = rndInterval*1000;
            timer.Elapsed += (s,e) =>
              {

                  Device.BeginInvokeOnMainThread(() =>
                  {
                      var msg = GenRandomMessage();
                      if (LastMessageVisible)
                      {
                          ReceiveMessage(msg);

                      }
                      else
                      {
                          DelayedMessages.Enqueue(msg);
                          PendingMessageCount++;
                      }
                  });

                timer.Interval = (5 + rnd.Next(5))*1000;
              };
            timer.Start();

            MessageAppearingCommand = new Command<Message>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<Message>(OnMessageDisappearing);

        }

        public static Message GenRandomMessage()
        {
            Message msg;
            var random = new Random();
            var text = chatExample[random.Next(chatExample.Count)];
            var user = userExample[random.Next(userExample.Count)];
            var expiryTime = TimeSpan.FromSeconds(10 + random.Next(10));
            if(expiryTime.TotalSeconds%2==0)
            {
                 msg = new Message() { Text = text, User = user, SubmittedDate = DateTime.Now, TimeOutValue = expiryTime };
            }
            else
            {
                 msg = new Message() { Text = text, User = user, SubmittedDate = DateTime.Now,  };
            }

            return msg;
            
        }

        void ReceiveMessage(Message msg)
        {
            Messages.Insert(0, msg);
        }


        internal void SubmitMessage(string textToSend,String User, DateTime SubmittedDate)
        {
            string formattedText = textToSend;

            formattedText = formattedText.TrimStart('\n').TrimEnd('\n');

            SubmitMessage(new Message() { Text = formattedText, User = User, SubmittedDate = SubmittedDate,TimeOutValue = CountDownValue });
        }

        void SubmitMessage(Message msg)
        {
            Messages.Insert(0,msg);
            TextToSend = string.Empty;
        }

        void OnMessageAppearing(Message msg)
        {
            var idx = Messages.IndexOf(msg);
            if (idx <= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        Messages.Insert(0, DelayedMessages.Dequeue());
                    }
                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }
        void OnMessageDisappearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }

        public async void DisplayCountDownModal()
        {
            await PopupNavigation.Instance.PushAsync(new CountDownPickerModal());

            //await Application.Current.MainPage.Navigation.PushAsync(new CountDownPickerModal());
        }


        public ObservableCollection<Message> Messages
        {
            get;
            set;
        } = new ObservableCollection<Message>();

        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public TimeSpan CountDownValue { get; set; }
        public bool ShowScrollTap { get; set; } = false; //Show the jump icon 
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }
        public Queue<Message> DelayedMessages { get; set; } = new Queue<Message>();
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }
    }
}