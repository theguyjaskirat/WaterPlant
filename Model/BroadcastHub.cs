
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterPlant.Interfaces;

namespace WaterPlant.Model
{
    public class BroadcastHub : Hub<IHubClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

    }

}
