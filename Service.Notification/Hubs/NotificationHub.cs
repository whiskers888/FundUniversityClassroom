using Microsoft.AspNetCore.SignalR;

namespace Service.Notification.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task<Task> OnConnectedAsync()
        {
            // Отправка приветственного сообщения клиенту
            await Clients.Caller.SendAsync("ReceiveNotification", "Connection is succesfull");
            return base.OnConnectedAsync();
        }
    }
}
