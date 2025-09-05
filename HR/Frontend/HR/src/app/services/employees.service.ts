import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../interfaces/employee-interface';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  employeesUrl: string = "https://localhost:7021/api/Employees";

  constructor(private _http: HttpClient) { }

  getAll(searchObj: any) {

    let params = new HttpParams();
    params = params.set("PositionId", searchObj.positionId ?? "");
    params = params.set("Name", searchObj.name ?? "");
    params = params.set("IsActive", searchObj.isActive ?? "");
    return this._http.get(`${this.employeesUrl}/GetAll`, { params });

  }

  getManagers(id?: number) {

    let params = new HttpParams();
    params = params.set("employeeId", id ?? "");
    return this._http.get(`${this.employeesUrl}/Managers`, { params });
  }

  add(employee: Employee) {

    return this._http.post(`${this.employeesUrl}/Add`, employee);
  }

  update(employee: Employee) {

    return this._http.put(`${this.employeesUrl}/Update`, employee);
  }

  delete(id: number) {

    let params = new HttpParams();
    params = params.set("id", id);
    return this._http.delete(`${this.employeesUrl}/Delete`, { params })
  }
}
