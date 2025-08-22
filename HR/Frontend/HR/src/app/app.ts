import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgIf, NgFor, NgClass, NgStyle, CommonModule } from '@angular/common';
import { RandomColor } from './directives/random-color';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReversePipe } from './pipes/reverse-pipe';

@Component({
  selector: 'app-root',
  imports: [
    // RouterOutlet, 
    // NgIf, 
    // NgFor, 
    // NgClass, 
    // NgStyle, 
    // RandomColor, 
    FormsModule, 
    ReactiveFormsModule,
    CommonModule,
    ReversePipe
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  title = "Welcome to Angular from Typescript";
  num: number = 52.165;
  bool: boolean = true;
  arr: string[] = ["one", "two", "three"]

  name = "Emp";
  currentIndex = 0;
  price = 2305.30;
  creationDate = new Date();

  students = [
    { name: "Bara", mark: 49 },
    { name: "Aya", mark: 56 },
    { name: "Omar", mark: 41 },
    { name: "Rula", mark: 95 },
    { name: "Ihab", mark: 68 },
    { name: "Anas", mark: 30 },
  ]

  images = [
    "https://i0.wp.com/picjumbo.com/wp-content/uploads/beautiful-fall-nature-scenery-free-image.jpeg?w=2210&quality=70",

    "https://t3.ftcdn.net/jpg/02/70/35/00/360_F_270350073_WO6yQAdptEnAhYKM5GuA9035wbRnVJSr.jpg",

    "https://images.pexels.com/photos/26151151/pexels-photo-26151151/free-photo-of-night-sky-filled-with-stars-reflecting-in-the-lake.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
  ];

  courses = [
    { id: 1, name: 'ASP.NET' },
    { id: 2, name: 'Angular' },
    { id: 3, name: 'Java' },
    { id: 4, name: 'Python' },
  ]

  form = new FormGroup({
    name: new FormControl(null, Validators.required),
    email: new FormControl(null, [Validators.required, Validators.email]),
    phone: new FormControl(null, [Validators.required, Validators.minLength(9), Validators.maxLength(10)]),
    course: new FormControl(1, Validators.required),
  });

  next() {
    if (this.currentIndex < this.images.length - 1) {
      this.currentIndex++;
    }
  }

  previous() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    }
  }

  reset() {
    this.form.reset({
      course: 1
    });
  }

  submit() {
    alert(`
      Welcome to the academy, ${this.form.value.name}!
      We will contact you shortly about the ${this.courses.find( x => x.id == this.form.value.course)?.name} Course.`);
  }
}
