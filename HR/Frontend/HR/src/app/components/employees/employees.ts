import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-employees',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employees.html',
  styleUrl: './employees.css'
})
export class Employees {

  @ViewChild ('closeButton') closeButton: ElementRef | undefined;

  employees : Employee[] = [
     { id: 1, name: "Emp1", isActive: true, startDate: new Date(2025, 11, 21), phone: "+96255895155", positionId: 1, positionName: "Manager", birthDate: new Date(1995,5,1), departmentId: 1, departmentName: "HR", managerId: null, managerName : null },

    { id: 2, name: "Emp2", isActive: true, startDate: new Date(2025, 6, 21), phone: "+962546645668", positionId: 1, positionName: "Manager", birthDate: new Date(1994,5,1), departmentId: 2, departmentName: "IT", managerId: null, managerName : null },

    { id: 3, name: "Emp3", isActive: false, startDate: new Date(2025, 5, 21), phone: "+962771235452", positionId: 2, positionName: "Developer", birthDate: new Date(2000,5,1), departmentId: 2, departmentName: "IT", managerId: 2, managerName : "Emp2" },

    { id: 4, name: "Emp4", isActive: true, startDate: new Date(2025, 1, 11), phone: "+964534534534", positionId: 2, positionName: "Developer", birthDate: new Date(2001,5,1), departmentId: 2, departmentName: "IT", managerId: 2, managerName : "Emp2" },

    { id: 5, name: "Emp5", isActive: false, startDate: new Date(2025, 2, 25), phone: "+962224455259", positionId: 3, positionName: "HR", birthDate: new Date(1999,5,1), departmentId: 1, departmentName: "HR", managerId: 1, managerName : "Emp1" },
  ]

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

  departments = [
    { id: null, name: "Select Department" },
    { id: 1, name: "HR" },
    { id: 2, name: "IT" },
  ];

  positions = [
    { id: null, name: "Select Position" },
    { id: 1, name: "Manager" },
    { id: 2, name: "Developer" },
    { id: 3, name: "HR" },
  ];

  managers = [
    { id: null, name: "Select Manager" },
    { id: 1, name: "Emp1" },
    { id: 2, name: "Emp2" },
  ]

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

    if (id != null) {
      this.employeeForm.patchValue({
        Id: employee?.id,
        Name: employee?.name,
        Phone: employee?.phone,
        StartDate: employee?.startDate,
        BirthDate: employee?.birthDate,
        Position: employee?.positionId,
        Department: employee?.departmentId,
        Manager: employee?.managerId,
        IsActive: employee?.isActive,
      })
    }
  }
}

export interface Employee {
  id: number;
  name: string;
  positionId?: number;
  positionName?: string;
  birthDate?: Date;
  isActive: boolean;
  startDate: Date;
  phone?: string;
  managerId?: any | null;
  managerName?: string | null;
  departmentId?: number;
  departmentName?: string;
}