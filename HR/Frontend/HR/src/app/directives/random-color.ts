import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appRandomColor]'
})
export class RandomColor {

  constructor(private element: ElementRef) {
    const colors = ['red', 'blue', 'green', 'yellow', 'purple', 'orange'];
    let color = colors[Math.floor(Math.random() * colors.length)];

    element.nativeElement.style.backgroundColor = color;
  }

}
