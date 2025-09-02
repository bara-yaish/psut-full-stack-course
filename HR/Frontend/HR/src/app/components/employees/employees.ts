import { CommonModule, DatePipe } from '@angular/common';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { EmployeesService } from '../../services/employees.service';
import { List } from '../../interfaces/list-interface';
import { Employee } from '../../interfaces/employee-interface';
import { DepartmentsService } from '../../services/departments.service';
import { LookupsService } from '../../services/lookups.service';
import { LookupsMajorCodes } from '../../enums/major-codes';

@Component({
  selector: 'app-employees',
  imports: [CommonModule, ReactiveFormsModule, NgxPaginationModule],
  providers: [DatePipe],
  templateUrl: './employees.html',
  styleUrl: './employees.css'
})
export class Employees implements OnInit, OnDestroy {

  constructor(
    private _datePipe: DatePipe,
    private _employeesService: EmployeesService,
    private _departmentsService: DepartmentsService,
    private _lookupsService: LookupsService) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  ngOnDestroy(): void {
    console.log("Component Destroyed");
  }

  @ViewChild('closeButton') closeButton: ElementRef | undefined;

  paginationConfig = { itemsPerPage: 5, currentPage: 1 };

  employees: Employee[] = [];

  employeesTableColumns: string[] = [
    "#",
    "Name",
    "Phone",
    "Birth Date",
    "Status",
    "Start Date",
    "Position",
    "Department",
    "Manager"
  ]

  departments: List[] = [];

  positions: List[] = [];

  managers: List[] = [];

  employeeForm: FormGroup = new FormGroup({
    Id: new FormControl(null),
    Name: new FormControl(null, [Validators.required]),
    Phone: new FormControl(null, [Validators.required]),
    StartDate: new FormControl(null, [Validators.required]),
    BirthDate: new FormControl(null, [Validators.required]),
    Position: new FormControl(null, [Validators.required]),
    Department: new FormControl(null, [Validators.required]),
    Manager: new FormControl(null),
    IsActive: new FormControl(true, [Validators.required]),
  })

  saveEmployee() {

    if (!this.employeeForm.value.Id) {
      let newEmployee: Employee = {
        id: this.employees[this.employees.length - 1].id + 1,
        name: this.employeeForm.value.Name,
        phone: this.employeeForm.value.Phone,
        startDate: this.employeeForm.value.StartDate,
        birthDate: this.employeeForm.value.BirthDate,

        positionId: this.employeeForm.value.Position,
        positionName: this.positions.find(x => x.id == this.employeeForm.value.Position)?.name,

        departmentId: this.employeeForm.value.Department,
        departmentName: this.departments.find(x => x.id == this.employeeForm.value.Department)?.name,

        managerId: this.employeeForm.value.Manager,
        managerName: this.employeeForm.value.Manager ?
          this.managers.find(x => x.id == this.employeeForm.value.Manager)?.name :
          null,

        isActive: this.employeeForm.value.IsActive
      };

      this.employees.push(newEmployee);
    }
    else {
      let index = this.employees.findIndex(x => x.id == this.employeeForm.value.Id);

      this.employees[index].name = this.employeeForm.value.Name;
      this.employees[index].phone = this.employeeForm.value.Phone;
      this.employees[index].startDate = this.employeeForm.value.StartDate;
      this.employees[index].birthDate = this.employeeForm.value.BirthDate;

      this.employees[index].positionId = this.employeeForm.value.Position;
      this.employees[index].positionName = this.positions.find(x => x.id == this.employeeForm.value.Position)?.name;

      this.employees[index].departmentId = this.employeeForm.value.Department;
      this.employees[index].departmentName = this.departments.find(x => x.id == this.employeeForm.value.Department)?.name;

      this.employees[index].managerId = this.employeeForm.value.Manager;
      this.employees[index].managerName = this.employeeForm.value.Manager ?
        this.managers.find(x => x.id == this.employeeForm.value.Manager)?.name :
        null;

      this.employees[index].isActive = this.employeeForm.value.IsActive;
    }

    this.closeButton?.nativeElement.click();
    this.clearEmployeeForm();
  }

  clearEmployeeForm() {
    this.employeeForm.reset({
      IsActive: true
    });
  }

  editEmployee(id: number) {
    let employee = this.employees.find(x => x.id === id);

    if (employee != null) {
      this.employeeForm.patchValue({
        Id: employee?.id,
        Name: employee?.name,
        Phone: employee?.phone,
        StartDate: this._datePipe.transform(employee?.startDate, 'yyyy-MM-dd'),
        BirthDate: this._datePipe.transform(employee?.birthDate, 'yyyy-MM-dd'),
        Position: employee?.positionId,
        Department: employee?.departmentId,
        Manager: employee?.managerId,
        IsActive: employee?.isActive,
      })
    }
  }

  removeEmployee(id: number) {
    this.employees = this.employees.filter(x => x.id !== id);

    // let index = this.employees.findIndex(x => x.id === id);
    // this.employees.splice(index, 1);
  }

  changePage(pageNumber: number) {
    this.paginationConfig.currentPage = pageNumber;
  }

  loadEmployees() {
    this.employees = [];

    this._employeesService.getAll().subscribe({
      next: (res: any) => {
        if (res?.length > 0) {
          res.forEach((x: any) => {
            let employee: Employee = {
              id: x.id,
              name: x.name,
              phone: x.phone,
              birthDate: x.birthDate,
              isActive: x.isActive,
              startDate: x.startDate,
              positionId: x.positionId,
              positionName: x.positionName,
              departmentId: x.departmentId,
              departmentName: x.departmentName,
              managerId: x.managerId,
              managerName: x.managerName
            };
            this.employees.push(employee);
          });
        }
        // console.log(res);
      },
      error: err => {
        console.log(err.message)
      }
    })
  }

  loadManagers(employeeId?: number) {
    this.managers = [
      { id: null, name: "Select Manager" },
    ];

    this._employeesService.getManagers(employeeId).subscribe({
      next: (res: any) => {
        if (res?.length > 0) {
          res.forEach((x: any) => {
            let manager: List = {
              id: x.id,
              name: x.name
            };
            this.managers.push(manager);
          });
        }
      },
      error: err => {
        console.log(err.message);
      }
    });
  }

  loadDepartments() {
    this.departments = [
      { id: null, name: "Select Department" },
    ];

    this._departmentsService.getDepartments().subscribe({
      next: (res: any) => {
        if (res?.length > 0) {
          this.departments = this.departments.concat(
            res.map((x: any) => ({ id: x.id, name: x.name } as List))
          );
        }
      },
      error: err => {
        console.log(err.message);
      }
    });
  }

  loadPositions() {
    this.positions = [
      { id: null, name: 'Select Position' },
    ]

    this._lookupsService.getBtMajorCode(LookupsMajorCodes.EmployeePositions).subscribe({
      next: (res: any) => {
        if (res?.length > 0) {
          this.positions = this.positions.concat(
            res.map((x: any) => ({ id: x.id, name: x.name } as List))
          );
        }
      },
      error: err => {
        console.log(err.message);
      }
    });
  }

  loadSaveDialog() {
    this.clearEmployeeForm();
    this.loadManagers();
    this.loadDepartments();
    this.loadPositions();
  }
}