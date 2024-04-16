using BasicNotification;
using Domain.NotficationNS;
using Domain.UserNS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HardWherePresenter
{
    public sealed class NotificationHub : Hub<IClientNotificationHub>
    {
        private static readonly ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public override async Task OnConnectedAsync()
        {
            Notfication notfication = new Notfication();
            if (Context.User.Identity.Name != null)
            {
                var user = Context.User;

                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userIdClaim != null)
                {
                    _connections.Add(userIdClaim, Context.ConnectionId);
                }
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = Context.User;
            if (Context.User.Identity.Name != null)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier).Value;

                _connections.Remove(userIdClaim, Context.ConnectionId);
            }
            return Task.CompletedTask;
        }

        public async Task JoinNotificationGroup(int userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
        }
    }
}
