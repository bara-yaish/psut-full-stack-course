import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReversePipe } from './pipes/reverse-pipe';
import { Employees } from './components/employees/employees';
import { Departments } from './components/departments/departments';
import { RouterOutlet, RouterLink, RouterLinkActive  } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [
    // NgIf, 
    // NgFor, 
    // NgClass, 
    // NgStyle, 
    // RandomColor, 
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    ReversePipe,
    Employees,
    Departments,
],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {}

function testingStuff() {
  let title = "Welcome to Angular from Typescript";
  let num: number = 52.165;
  let bool: boolean = true;
  let arr: string[] = ["one", "two", "three"]

  let name = "Emp";
  let currentIndex = 0;
  let price = 2305.30;
  let creationDate = new Date();

  let students = [
    { name: "Bara", mark: 49 },
    { name: "Aya", mark: 56 },
    { name: "Omar", mark: 41 },
    { name: "Rula", mark: 95 },
    { name: "Ihab", mark: 68 },
    { name: "Anas", mark: 30 },
  ]

  let images = [
    "https://i0.wp.com/picjumbo.com/wp-content/uploads/beautiful-fall-nature-scenery-free-image.jpeg?w=2210&quality=70",

    "https://t3.ftcdn.net/jpg/02/70/35/00/360_F_270350073_WO6yQAdptEnAhYKM5GuA9035wbRnVJSr.jpg",

    "https://images.pexels.com/photos/26151151/pexels-photo-26151151/free-photo-of-night-sky-filled-with-stars-reflecting-in-the-lake.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
  ];

  let courses = [
    { id: 1, name: 'ASP.NET' },
    { id: 2, name: 'Angular' },
    { id: 3, name: 'Java' },
    { id: 4, name: 'Python' },
  ]

  let form = new FormGroup({
    name: new FormControl(null, Validators.required),
    email: new FormControl(null, [Validators.required, Validators.email]),
    phone: new FormControl(null, [Validators.required, Validators.minLength(9), Validators.maxLength(10)]),
    course: new FormControl(1, Validators.required),
  });

  function next() {
    if (currentIndex < images.length - 1) {
      currentIndex++;
    }
  }

  function previous() {
    if (currentIndex > 0) {
      currentIndex--;
    }
  }

  function reset() {
    form.reset({
      course: 1
    });
  }

  function submit() {
    alert(`
      Welcome to the academy, ${form.value.name}!
      We will contact you shortly about the ${courses.find( x => x.id == form.value.course)?.name} Course.`);
  }
}
