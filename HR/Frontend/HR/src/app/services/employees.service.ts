import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  employeesUrl: string = "https://localhost:7021/api/Employees";

  constructor(private _http: HttpClient) { }

  getAll() {
    let params = new HttpParams();
    params = params.set("PositionId", "");
    params = params.set("Name", "");
    params = params.set("IsActive", "");
    return this._http.get(`${this.employeesUrl}/GetAll`, { params });

  }

  getManagers(employeeId?: number) {
    let params = new HttpParams();
    params = params.set("employeeId", employeeId ?? "");
    return this._http.get(`${this.employeesUrl}/Managers`, { params });
  }

}
