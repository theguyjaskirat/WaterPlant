using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterPlant.Model;
namespace WaterPlant.Interfaces
{
    public interface IRepository
    {
        List<data> GetAllMessages();
    }
}
