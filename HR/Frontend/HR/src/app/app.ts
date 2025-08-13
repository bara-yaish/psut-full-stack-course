import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgIf, NgFor, NgClass, NgStyle } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgIf, NgFor, NgClass, NgStyle],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  title = "Welcome to Angular from Typescript";
  num: number = 52.165;
  bool: boolean = true;
  arr: string[] = ["one", "two", "three"]

  students = [
    { name: "Bara", mark: 49 },
    { name: "Aya", mark: 56 },
    { name: "Omar", mark: 41 },
    { name: "Rula", mark: 95 },
    { name: "Ihab", mark: 68 },
    { name: "Anas", mark: 30 },
  ]
}
