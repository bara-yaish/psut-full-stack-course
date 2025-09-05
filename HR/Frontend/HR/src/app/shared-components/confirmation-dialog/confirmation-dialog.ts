import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-confirmation-dialog',
  imports: [],
  templateUrl: './confirmation-dialog.html',
  styleUrl: './confirmation-dialog.css'
})
export class ConfirmationDialog {

  @Input() title: string = "";
  @Input() body: string = "";

  @Output() confirm = new EventEmitter<boolean>(); 

  confirmDelete(isConfirmed: boolean) {
    this.confirm.emit(isConfirmed);
  }
}
