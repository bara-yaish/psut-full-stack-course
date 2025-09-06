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

    let formData = new FormData();

    formData.set("Id", employee.id.toString());
    formData.set("Name", employee.name);
    formData.set("Phone", employee.phone ?? "");
    formData.set("BirthDate", employee.birthDate?.toString() ?? "");
    formData.set("PositionId", employee.positionId?.toString() ?? "");
    formData.set("IsActive", employee.isActive.toString() ?? "");
    formData.set("StartDate", employee.startDate?.toString() ?? "");
    formData.set("ManagerId", employee.managerId?.toString() ?? "");
    formData.set("DepartmentId", employee.departmentId?.toString() ?? "");
    formData.set("Image", employee.image);

    return this._http.post(`${this.employeesUrl}/Add`, formData);
  }

  update(employee: Employee) {

    let formData = new FormData();

    formData.set("Id", employee.id.toString());
    formData.set("Name", employee.name);
    formData.set("Phone", employee.phone ?? "");
    formData.set("BirthDate", employee.birthDate?.toString() ?? "");
    formData.set("PositionId", employee.positionId?.toString() ?? "");
    formData.set("IsActive", employee.isActive.toString() ?? "");
    formData.set("StartDate", employee.startDate?.toString() ?? "");
    formData.set("ManagerId", employee.managerId?.toString() ?? "");
    formData.set("DepartmentId", employee.departmentId?.toString() ?? "");
    formData.set("Image", employee.image);
    formData.set("HaveImage", employee.haveImage?.toString() ?? "")

    return this._http.put(`${this.employeesUrl}/Update`, formData);
  }

  delete(id: number) {

    let params = new HttpParams();
    params = params.set("id", id);
    return this._http.delete(`${this.employeesUrl}/Delete`, { params })
  }
}
