using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.SignalR;
using WaterPlant.Interfaces;

namespace WaterPlant.Model
{
    public class data
    {
        public int plantid { get; set; }
        public string plantname { get; set; }
        public string status { get; set; }
        public string lastWateredAt { get; set; }
    }
    public class Repository: IRepository
    {
        private readonly IHubContext<BroadcastHub> _hubContext; private readonly IRepository _repo;
        public Repository( )
        {
           
            
        }
       
        string constr = @"Data Source=.;Initial Catalog=PlantDB;Integrated Security=true";
        //SqlConnection co = new SqlConnection(conn);
        public List<data> GetAllMessages()
        {
            var messages = new List<data>();
            using (SqlConnection con = new SqlConnection(constr))
            {
               // SqlDependency.Start(constr);
                using (var cmd = new SqlCommand(@"SELECT [PlantId]
      ,[PlantName]  ,[status],[lastWateredAt] from [PlantDB].[dbo].[Plants]", con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    var dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        messages.Add(item: new data
                        {
                            plantid = int.Parse(ds.Tables[0].Rows[i]["PlantId"].ToString()),
                            plantname = ds.Tables[0].Rows[i]["PlantName"].ToString(),
                            status = ds.Tables[0].Rows[i]["status"].ToString(),
                            lastWateredAt = ds.Tables[0].Rows[i]["lastWateredAt"].ToString(),
                        });
                    }
                }
            }
               
            return messages;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table. 
        {
            if (e.Type == SqlNotificationType.Change)
            {
                BroadcastHub hub = new BroadcastHub();
              
               // hub.Clients.All.BroadcastMessage();
                  //_hubContext.Clients.All.send("BroadcastMessage");
            }
        }
    }
}
