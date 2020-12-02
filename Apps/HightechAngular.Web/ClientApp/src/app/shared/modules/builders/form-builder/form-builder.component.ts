import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {FormlyFieldConfig, FormlyFormOptions} from '@ngx-formly/core';
import {FormBuilderSchemaService} from '../providers/form-builder-schema-service';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-form-builder',
  templateUrl: './form-builder.component.html'
})
export class FormBuilderComponent implements OnInit {
  @Input() formSchemaName: string;

  @Input() model: any = {};
  @Input() onSave: (value: any) => void;
  @Input() onError: (value: any) => void;
  @Input() onCancel: (model) => void;
  @Input() onSubmit: (form: FormGroup) => void;
  @Input() form = new FormGroup({});
  @Input() multipleEnums: string[] = [];
  @Input() isForUpdate = false;
  @Input() idParamName = 'id';
  @Input() editable = true;

  @Output() modelChange: EventEmitter<any> = new EventEmitter();
  @Output() submitted: EventEmitter<any> = new EventEmitter<any>();


  public options: FormlyFormOptions = {};
  public fields: FormlyFieldConfig[] = [];

  constructor(private formBuilderSchemaService: FormBuilderSchemaService,
              private http: HttpClient) {
  }

  ngOnInit(): void {
    this.formBuilderSchemaService.getFormSchema(this.formSchemaName).subscribe(result => {
     // this.multipleEnums = result[0].fieldGroup.filter(x => x.templateOptions.multiple).map(x => x.key);
      this.multipleEnums.forEach(x => this.model[x] = this.model[x]?.split(', '));
      if (this.model) {
        this.checkIfHasSubobject(this.model, result)
      }
      this.fields = result;
      if (!this.editable){
        this.fields.map(x => x.fieldGroup.map(y => y.templateOptions.readonly = true));
      }
    });
  }

  checkIfHasSubobject(model, result) {
    for (const prop in model) {
      if (typeof model[prop] != 'string' && Object.keys(model[prop]).length) {
        for (let item of [].concat(...result.map(x => x.fieldGroup)).filter(x => x.key == prop)) {
          const options = [].concat(item.templateOptions.options.map(x => x.value));
          this.model[prop] = options.filter(x => this.deepEqual(x, model[prop]))[0]
        }
      }
    }
  }

  deepEqual = (x, y) => {
    if (x === y) {
      return true;
    }
    else if ((typeof x == "object" && x != null) && (typeof y == "object" && y != null)) {
      if (Object.keys(x).length != Object.keys(y).length)
        return false;

      for (const prop in x) {
        if (y.hasOwnProperty(prop))
        {
          if (! this.deepEqual(x[prop], y[prop]))
            return false;
        }
        else
          return false;
      }

      return true;
    }
    else
      return false;
  }

  submit() {
    const formValue = this.form.value;
    this.multipleEnums.forEach(x => formValue[x] = formValue[x].join(', '));
    let finalPath = '';
    if(this.isForUpdate) {
      finalPath = 'Update';
      this.form.value.id = this.model.id;
    } else {
      finalPath = 'Create';
    }
    this.http
      .post(`${environment.httpDomain}/api/${this.formSchemaName}/${finalPath}`, formValue)
      .subscribe(result => {
        this.submitted.emit(result);
      });
  }

  change($event) {
    return this.modelChange.emit($event);
  }
}
