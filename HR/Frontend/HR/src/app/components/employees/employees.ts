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
import { ConfirmationDialog } from '../../shared-components/confirmation-dialog/confirmation-dialog';

@Component({
  selector: 'app-employees',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    ConfirmationDialog
  ],
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
    this.loadPositions();
  }

  ngOnDestroy(): void {
    console.log("Component Destroyed");
  }

  @ViewChild('closeButton') closeButton: ElementRef | undefined;
  @ViewChild('employeeImageInput') employeeImageInput!: ElementRef;

  paginationConfig = { itemsPerPage: 5, currentPage: 1 };

  deleteDialogTitle: string = "Delete Confirmation";
  deleteDialogBody: string = "Are you sure you want to delete this employee?";
  displayConfirmationDialog: boolean = false;
  employeeIdToBeDeleted: number | null = null;

  employees: Employee[] = [];
  departments: List[] = [];
  positions: List[] = [];
  managers: List[] = [];
  employeeStatusList: List[] = [
    { id: null, name: "Select Status" },
    { id: true, name: "Active" },
    { id: false, name: "Inactive" },
  ];

  employeesTableColumns: string[] = [
    "#",
    "Image",
    "Name",
    "Phone",
    "Birth Date",
    "Status",
    "Start Date",
    "Position",
    "Department",
    "Manager"
  ];

  employeeForm: FormGroup = new FormGroup({
    Id: new FormControl(null),
    Name: new FormControl(null, [Validators.required]),
    Phone: new FormControl(null),
    StartDate: new FormControl(null, [Validators.required]),
    BirthDate: new FormControl(null, [Validators.required]),
    Position: new FormControl(null, [Validators.required]),
    Department: new FormControl(null, [Validators.required]),
    Manager: new FormControl(null),
    Image: new FormControl(null),
    IsActive: new FormControl(true, [Validators.required]),
    HaveImage: new FormControl(false),
  });

  searchFilterForm: FormGroup = new FormGroup({
    Name: new FormControl(null),
    Position: new FormControl(null),
    Status: new FormControl(null),
  });

  uploadImage(event: any) {

    this.employeeForm.patchValue({
      Image: event.target.files[0]
    });
  }

  saveEmployee() {

    let employeeId = this.employeeForm.value.Id ?? 0;
    let newEmployee: Employee = {
      id: employeeId,
      name: this.employeeForm.value.Name,
      phone: this.employeeForm.value.Phone,
      startDate: this.employeeForm.value.StartDate,
      birthDate: this.employeeForm.value.BirthDate,
      positionId: this.employeeForm.value.Position,
      departmentId: this.employeeForm.value.Department,
      managerId: this.employeeForm.value.Manager,
      image: this.employeeForm.value.Image,
      isActive: this.employeeForm.value.IsActive,
      haveImage: this.employeeForm.value.HasImage
    };

    if (!this.employeeForm.value.Id) {

      this._employeesService.add(newEmployee).subscribe({
        next: res => {
          this.loadEmployees();
        },
        error: err => {
          console.log(err.error);
        }
      });
    }
    else {

      this._employeesService.update(newEmployee).subscribe({
        next: res => {
          this.loadEmployees();
        },
        error: err => {
          console.log(err.error);
        }
      });
    }

    this.closeButton?.nativeElement.click();
    this.clearEmployeeForm();
  }

  clearInputImage() {
    this.employeeImageInput.nativeElement.value = '';
  }

  clearEmployeeForm() {

    this.employeeForm.reset({
      IsActive: true,
      HasImage: false
    });

    this.clearInputImage();
  }

  editEmployee(id: number) {
    this.loadSaveDialog(id);
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
        HasImage: employee?.haveImage
      })
    }
  }

  removeEmployee() {
    if (!this.employeeIdToBeDeleted) return;

    return this._employeesService.delete(this.employeeIdToBeDeleted).subscribe({
      next: res => {
        this.loadEmployees();
      },
      error: err => {
        console.log(err.error);
      }
    })

    // let index = this.employees.findIndex(x => x.id === id);
    // this.employees.splice(index, 1);
  }

  changePage(pageNumber: number) {
    this.paginationConfig.currentPage = pageNumber;
  }

  loadEmployees() {
    this.employees = [];

    let searchObj = {
      name: this.searchFilterForm.value.Name,
      positionId: this.searchFilterForm.value.Position,
      isActive: this.searchFilterForm.value.Status,
    }

    this._employeesService.getAll(searchObj).subscribe({
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
              managerName: x.managerName,
              imagePath: x.imagePath ? x.imagePath.replaceAll("\\", "/") : "assets/images/emp-default-image.avif",
              haveImage: x.imagePath ? true : false,
            };
            this.employees.push(employee);
          });
        }
        // console.log(res);
      },
      error: err => {
        console.log(err.error)
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
        console.log(err.error);
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
        console.log(err.error);
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
        console.log
          (err.error);
      }
    });
  }

  loadSaveDialog(employeeId?: number) {
    this.clearEmployeeForm();
    this.loadManagers(employeeId);
    this.loadDepartments();
    this.loadPositions();
  }

  showConfirmDialog(id: number) {
    this.employeeIdToBeDeleted = id;
    this.displayConfirmationDialog = true;
  }

  confirmEmployeeDelete(confirmDelete: boolean) {
    if (confirmDelete) this.removeEmployee();

    this.employeeIdToBeDeleted = null;
    this.displayConfirmationDialog = false;
  }

  removeImage() {

    this.employeeForm.patchValue({
      HasImage: false,
    })
  }
}