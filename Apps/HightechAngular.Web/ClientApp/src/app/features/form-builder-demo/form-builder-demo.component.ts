import {Component, OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';

@Component({
  templateUrl: './form-builder-demo.component.html',
})
export class FormBuilderDemoComponent implements OnInit {
  public formExample = new FormGroup({});

  public onSubmit = (form: FormGroup) => {
  }

  ngOnInit(): void {

  }
}
