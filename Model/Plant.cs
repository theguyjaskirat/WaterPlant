using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterPlant.Model
{
    public class Plant
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public string status { get; set; }
        public DateTime lastWateredAt { get; set; }
        public bool isWaterAllowed { get; set; }
    }
}
