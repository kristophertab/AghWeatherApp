using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AghWeatherApp.Services;
using AghWeatherApp.Models;

namespace AghWeatherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
        private SignalRChatService ChatService;
        private List<String> Messages;

		public ChatPage ()
		{
			InitializeComponent ();

            Messages = new List<string>();
            ChatService = new SignalRChatService();

            ChatService.ChatConnect();
            ChatService.OnMessageReceived += ChatServices_OnMessageReceived;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => refreshFuction());
                return true;
            });
        }

        void ChatServices_OnMessageReceived(object sender, ChatMessage e)
        {
            Messages.Add(e.Name + ": " + e.Message);
        }

        async void ChatService_SendMessageCommand()
        {
            if(this.messageText.Text != null)
            {
                ChatMessage myMessage = new ChatMessage { Name = "user", Message = this.messageText.Text };

                ChatServices_OnMessageReceived(this, myMessage);

                await ChatService.ChatSend(myMessage);
            }
            
        }

        void refreshFuction()
        {
            foreach(string m in Messages)
            {
                if (!this.chatText.Text.Contains(m))
                {
                    this.chatText.Text += Environment.NewLine;
                    this.chatText.Text += m;
                }
                
            }
            
        }
    }
}