import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LookupsService {

  apiUrl: string = "https://localhost:7021/api/Lookups";

  constructor(private _http: HttpClient) { }

  getBtMajorCode(majorCode: number) {
    let params = new HttpParams();
    params.set("MajorCode", majorCode);
    return this._http.get(this.apiUrl + "/GetByMajorCode", { params });
  }
}
