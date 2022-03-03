using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterPlant.Model.DBContext
{
    public class PlantContext: DbContext
    {
        public PlantContext(DbContextOptions<PlantContext> options) : base(options)
        {

        }
        public DbSet<Plant> Plants { get; set; }
    }
}
