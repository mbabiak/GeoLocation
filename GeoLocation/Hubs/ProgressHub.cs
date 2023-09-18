using Microsoft.AspNetCore.SignalR;

namespace GeoLocation.Hubs
{
    public class ProgressHub : Hub<IProgressHub>
    {
        public Task SendMessage(long count)
        {
            return Clients.Caller.CountUpdate(count);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
