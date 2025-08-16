import { ElementRef } from '@angular/core';
import { RandomColor } from './random-color';

describe('RandomColor', () => {
  it('should create an instance', (element: ElementRef) => {
    const directive = new RandomColor();
    expect(directive).toBeTruthy();
  });
});
