import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Plant } from '../model/plant';
@Injectable({
  providedIn: 'root'
})
export class PlantService {
  private userData = new BehaviorSubject<any>([]);
  userInfo$ = this.userData.asObservable();
  readonly APIUrl = "http://localhost:19946/api/";
  constructor(private http: HttpClient) { }

  private newCars$: Observable<any[]>;
  // this gets user data
  getUserInfo() {
    this.userData
    return this.http.get(this.APIUrl + 'Plants').pipe((userData) => {
      this.userData.next(userData);
      console.log("hwga v aaaa " + JSON.stringify(userData));
      return this.userInfo$;
    })
  }



  getPlantsList(): Observable<Plant[]> {
    // this.newCars$ = this.http.get<any[]>(this.APIUrl + 'Plants');
    // return this.newCars$;
    return this.http.get<Plant[]>(this.APIUrl + 'Plants').pipe(
      map((res: Plant[]) => {
        res.forEach(x => { x.canStopwatering = false; });
        return res;
      })
    );
  }
  waterthePlant(id, formData,connectionid): Observable<Plant[]> {
    const headers = { 'connectionid':connectionid };
    
    return this.http.put<any>(this.APIUrl + 'Plants/' + id,formData,{headers});
  }
  stopthewater(id, formData): Observable<Plant[]> {
    return this.http.put<any>(this.APIUrl + 'Plants/' + id, formData);
  }

}
