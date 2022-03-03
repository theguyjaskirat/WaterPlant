export class Plant {
    plantId?:number;
     plantName?:string;
     status?:string;
     lastWateredAt?:Date;
     isWaterAllowed?:boolean;
     canStopwatering=false;
      timeDifference;
      secondsToDday;
      minutesToDday;
      hoursToDday;
      daysToDday?:number;
      
}
