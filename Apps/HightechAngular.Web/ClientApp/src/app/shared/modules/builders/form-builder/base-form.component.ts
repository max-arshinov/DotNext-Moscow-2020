import {
  AfterViewChecked,
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ViewChild
} from '@angular/core';
import {FormGroup} from "@angular/forms"
import {environment} from "../../../../../environments/environment"
import {HttpClient} from "@angular/common/http"
import {FormBuilderComponent} from "./form-builder.component"

@Component({
  template: ``
})
export abstract class BaseFormComponent{
  public form = new FormGroup({});
  public multipleEnums: string[] = [];
  protected constructor(protected http: HttpClient) { }

}
