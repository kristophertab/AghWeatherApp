using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;


namespace AghWeatherApp.Services
{
    public class SignalRChatService
    {
        private string signalrUrl = "http://192.168.43.195:60869/signalr";

        private readonly HubConnection myConnection;
        private readonly IHubProxy myProxy;

        public event EventHandler<ChatMessage> OnMessageReceived;

        public SignalRChatService()
        {
            myConnection = new HubConnection(signalrUrl);
            myProxy = myConnection.CreateHubProxy("ChatHub");
        }

        public async Task ChatConnect()
        {
                await myConnection.Start();

                myProxy.On("GetMessage", (string name, string message) => OnMessageReceived(this, new ChatMessage
                {
                    Name = name,
                    Message = message
                }));
        }

        public async Task ChatSend(ChatMessage message)
        {
            try
            {
                await myProxy.Invoke("SendMessage", message.Name, message.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }
    }

    public class ChatMessage
    {
        public string Name
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}
