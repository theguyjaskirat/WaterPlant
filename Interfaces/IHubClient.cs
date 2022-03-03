using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterPlant.Interfaces
{
    public interface IHubClient
    {
          Task  BroadcastMessage();
        
    }
}
