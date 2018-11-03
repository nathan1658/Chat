using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Chat.Models;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;
using Chat.Interfaces;
using Chat.Views;

namespace Chat.ViewModels
{

    public class GroupedMessage : ObservableCollection<Message>
    {
        public DateTime DateTime { get; set; }

        public string DateTimeString
        {
            get
            {

                if (DateTime != null)
                {
                    if (DateTime.Date == DateTime.Now.Date)
                    {
                        return "Today";
                    }
                    else if (DateTime.Date == DateTime.Now.AddDays(-1).Date)
                    {
                        return "Yesterday";
                    }
                    else
                    {


                        string suffix;

                        switch (DateTime.Day)
                        {
                            case 1:
                            case 21:
                            case 31:
                                suffix = "st";
                                break;
                            case 2:
                            case 22:
                                suffix = "nd";
                                break;
                            case 3:
                            case 23:
                                suffix = "rd";
                                break;
                            default:
                                suffix = "th";
                                break;
                        }

                        return string.Format("{0}{1} {2:MMMM} {3}", DateTime.Day, suffix, DateTime, DateTime.Year);
                    }
                }
                else//Null datetime
                {
                    return "";
                }

            }
        }

        public GroupedMessage(IEnumerable<Message> msg) : base(msg)
        { }

        public GroupedMessage() { }
    }


    public class ChatPageViewModel : INotifyPropertyChanged
    {
        static Random random = new Random();
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
            "留返拜山先講ok?",
            "派咁少不如唔好派",
            "😂😂😂😂😂",
            "😂😂😂😂",
            "😂😂😂",
            "😂"
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
            "Peter",
            "👼🏿"
        };

        //TODO for testing only, limit refresh count to 3.
        private int refreshCount = 0;
        async Task<List<Message>> RefreshData()
        {

            await Task.Delay(1500);
            //Each time get n old messages..

            List<Message> oldMessages = new List<Message>();
            if (refreshCount < 3)
            {
                for (int i = 0; i < LOAD_MESSAGE_COUNT; i++)
                {
                    oldMessages.Add(GenRandomMessage());
                }

                var oldestDateTime = GroupedMessages.Where(x => x.DateTime == GroupedMessages.Min(y => y.DateTime)).First().Min(x => x.SubmittedDate);

                var index = 1;
                foreach (var msg in oldMessages)
                {
                    msg.SubmittedDate = oldestDateTime.AddDays(index * -1).AddHours(index * -1);
                    index++;
                }
                oldMessages = new List<Message>(oldMessages.OrderByDescending(x => x.SubmittedDate.ToBinary()));
            }
            refreshCount++;
            return oldMessages;

        }

        void FormGroupMessages(IList<Message> messages)
        {
            foreach (var msg in messages)
            {
                addMessage(msg);
            }
            GroupedMessages = new ObservableCollection<GroupedMessage>(GroupedMessages.OrderBy(x => x.DateTime).ToList());
        }

        void addMessage(Message msg)
        {
            var date = msg.SubmittedDate.Date;
            GroupedMessage targetMessageGroup = null;
            targetMessageGroup = GroupedMessages.Where(x => x.DateTime == date).FirstOrDefault();
            if (targetMessageGroup == null)
            {
                targetMessageGroup = new GroupedMessage() { DateTime = date };
                if (GroupedMessages.Count == 0)
                {
                    GroupedMessages.Add(targetMessageGroup);
                }
                else
                {
                    for (int i = GroupedMessages.Count - 1; i >= 0; i--)
                    {
                        if (date > GroupedMessages[i].DateTime)
                        {
                            GroupedMessages.Insert(i + 1, targetMessageGroup);
                            break;
                        }
                        if (i == 0)//Last
                        {
                            GroupedMessages.Insert(i, targetMessageGroup);
                        }
                    }
                }
            }
            targetMessageGroup.Add(msg);
            targetMessageGroup = new GroupedMessage(targetMessageGroup.OrderBy(x => x.SubmittedDate).ToList());
        }
        private IChatPage _view;
        public IChatPage View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
                if (_view != null)
                {
                    //TODO init Buttons..
                    List<Button> buttons = new List<Button>() {
            new Button()
            {
                BackgroundColor = Color.LightGreen,
                TextColor= Color.White,
                Text = "Yes"
            },
             new Button()
            {
                BackgroundColor = Color.Red,
                TextColor = Color.White,
                Text = "No"
                        } };
                    View.GenerateButtons(buttons);
                }
            }
        }
        public ChatPageViewModel(Conversation con)
        {
            _conversation = con;
            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    SubmitMessage(TextToSend, null, null);
                }
            });

            FormGroupMessages(_conversation.Messages);
            var rnd = new Random();
            int rndInterval = 10 + rnd.Next(5);
            Timer timer = new Timer();
            timer.Interval = rndInterval * 1000;
            timer.Elapsed += (s, e) =>
              {

                  Device.BeginInvokeOnMainThread(() =>
                  {
                      var msg = GenRandomMessage();
                      ReceiveMessage(msg);
                      if (!LastMessageVisible)
                      {
                          PendingMessageCount++;
                      }
                  });

                  timer.Interval = (5 + rnd.Next(5)) * 1000;
              };
            //timer.Start();
            MessageAppearingCommand = new Command<Message>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<Message>(OnMessageDisappearing);

            //Subscribe MessageCenter for button clicked event
            MessagingCenter.Subscribe<ChatPage, string>(this, "ButtonPressed", (s,e)=>
            {
                Message msg = new Message();
                msg.SubmittedDate = DateTime.Now;
                msg.User = "";
                msg.Text = "You clicked " + e;
                this.addMessage(msg);
            });

        }

        public static Message GenRandomMessage()
        {
            Message msg;

            var text = chatExample[random.Next(chatExample.Count)];
            var user = userExample[random.Next(userExample.Count)];
            var expiryTime = TimeSpan.FromSeconds(10 + random.Next(10));
            if (expiryTime.TotalSeconds % 7 == 0)
            {
                msg = new Message() { Text = text, User = user, SubmittedDate = DateTime.Now, TimeOutValue = expiryTime };
            }
            else
            {
                msg = new Message() { Text = text, User = user, SubmittedDate = DateTime.Now, };
            }

            return msg;

        }

        void ReceiveMessage(Message msg)
        {
            addMessage(msg);
        }


        internal void SubmitMessage(string textToSend, byte[] imageByteArr, byte[] pdfByte)
        {
            string formattedText = null;
            if (!string.IsNullOrEmpty(textToSend))
            {
                formattedText = textToSend;
                formattedText = formattedText.TrimStart('\n').TrimEnd('\n');
            }
            Message msgToSend = new Message()
            {
                Text = formattedText,
                PhotoByte = imageByteArr,
                PDFByte = pdfByte,
                OutgoingMessage = true
            };
            SubmitMessage(msgToSend);
        }



        void SubmitMessage(Message msg)
        {
            msg.TimeOutValue = CountDownValue;
            msg.SubmittedDate = DateTime.Now;
            msg.User = App.User;



            addMessage(msg);
            TextToSend = string.Empty;
        }


        void OnMessageAppearing(Message msg)
        {
            //TODO this is slow omg~
            var idx = GroupedMessages.SelectMany(x => x).ToList().IndexOf(msg);
            if (idx >= GroupedMessages.SelectMany(x => x).ToList().Count - 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
            //TODO fix this
            //if(Messages.IndexOf(msg)==0)
            //{
            //    this.RefreshCommand.Execute(null);
            //}

        }
        void OnMessageDisappearing(Message message)
        {
            var idx = GroupedMessages.SelectMany(x => x).ToList().IndexOf(message);
            if (idx <= GroupedMessages.SelectMany(x => x).ToList().Count - 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }

        public ObservableCollection<GroupedMessage> GroupedMessages { get; set; } = new ObservableCollection<GroupedMessage>();

        //public ObservableCollection<Message> Messages
        //{
        //    get;
        //    set;
        //} = new ObservableCollection<Message>();

        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public TimeSpan CountDownValue { get; set; }
        public bool ShowScrollTap { get; set; } = false; //Show the jump icon 
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }
        public bool IsRefreshing { get; set; } = false;
        public readonly int LOAD_MESSAGE_COUNT = 5;
        // public Queue<Message> DelayedMessages { get; set; } = new Queue<Message>();
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    var loadedMessages = await RefreshData();

                    IsRefreshing = false;

                    if (loadedMessages.Count > 0)
                    {
                        foreach (var msg in loadedMessages)
                        {

                            addMessage(msg);
                        }
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Alert", "No more message to load!", "OK");
                    }

                });

            }
        }


    }
}
