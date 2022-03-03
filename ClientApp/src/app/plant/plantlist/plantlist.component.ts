import { Component, OnInit } from '@angular/core';
import { PlantService } from 'src/app/services/plant.service';
import * as signalR from '@microsoft/signalr';
import { Observable, interval } from 'rxjs';
import { Plant } from 'src/app/model/plant';

@Component({
  selector: 'app-plantlist',
  templateUrl: './plantlist.component.html',
  styleUrls: ['./plantlist.component.css']
})
export class PlantlistComponent implements OnInit {
  plantList: Plant[];
  timeLeft: number = 10;
  interval;
  stoptimerinterval;
  status: string;
  myDate: Date = new Date();
  isbtnenabled: boolean = true;
  public dateNow = new Date();
  public timeDifference;
  public secondsToDday;
  public minutesToDday;
  //public hoursToDday;
  //public daysToDday;
  connectionid?:string;
  constructor(private _service: PlantService) { }


  startTimer() {

    this.interval = setInterval(() => {


      if (this.plantList !== undefined || this.plantList.length > 0) {
        this.plantList.forEach(function (value) {
          // console.log(value.lastWateredAt);
          var currentdatetime = new Date();
          var milliSecondsInASecond = 1000;
          var hoursInADay = 24;
          var minutesInAnHour = 60;
          var SecondsInAMinute = 60;
          //console.log("this.myDate=new Date();"+d);
          getTimeDifference(value.lastWateredAt);
          // var status = hoursdifference(currentdatetime, value.lastWateredAt);
          // console.log("this.myDate=new Date();"+status);
          // value.minutesToDday = status + "Min(s)";

          function getTimeDifference(lastdate) {
            lastdate = new Date(lastdate);
            var timeDifference = new Date().getTime() - lastdate.getTime();
            allocateTimeUnits(timeDifference);
          }

          function allocateTimeUnits(timeDifference) {
            value.secondsToDday = Math.floor((timeDifference) / (milliSecondsInASecond) % SecondsInAMinute);
            value.minutesToDday = Math.floor((timeDifference) / (milliSecondsInASecond * minutesInAnHour) % SecondsInAMinute);
            if (value.minutesToDday > 1) {
              value.status = "Y";

              if (value.canStopwatering == false)
                value.isWaterAllowed = false;
            }
            else {
              value.status = "N"; value.isWaterAllowed = true;
            }
          }

        });

      }

    }, 1000)
  }
 
  ngOnInit() {
    this.startTimer();
    this.getPlantsList();
    //this.getdbdata();
    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl("http://localhost:19946/" + 'notify')
      .build();

    connection.start().then(function () {
      connection.invoke('GetConnectionId')
      .then(function (connectionId) {
      
      localStorage.setItem('connectionid', connectionId);
      });
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });
    connection.on("updateMessages", () => {
      console.log('call from sqldb!');
      this.getPlantsList();
    });
    connection.on("BroadcastMessage", () => {
      console.log('broadcast call Connected!');
      this.getPlantsList();
    });


  }
  
  getPlantsList() {
    this._service.getPlantsList().subscribe(data => {
      this.plantList = data;

       console.log(JSON.stringify(data));
    });
  }
  giveWater(item: any) {
    
    item.canStopwatering = true;
    item.isWaterAllowed = true;
    this.stoptimerinterval = setTimeout(() => {
      this.givewaterAPI(item);
    }, 10000); 
  }
  stopWater(item: any) {
    item.canStopwatering = false;
    item.isWaterAllowed = false;
    //setinterval 10000 if click ta rok do else u[date]
    clearTimeout(this.stoptimerinterval);
    console.log("Watering stopped....");
  }
  givewaterAPI(item: any) {
   var cid=localStorage.getItem('connectionid');
  // console.log('cid hega'+cid);
    this._service.waterthePlant(item.plantId, item,cid).subscribe(data => {
      //this.plantList = data;
       
      item.canStopwatering = false;
      item.isWaterAllowed = true;
      item.lastWateredAt= new Date();
      //need to call this if can water plants from multiple devices for more consistency and comment line 140-142 and uncomment ln 139
     // this.getPlantsList();
    });
  }
}
