import {FormlyFieldConfig} from '@ngx-formly/core';

export function minlengthValidationMessage(err, field: FormlyFieldConfig) {
  return `Минимальная длинна этого поля ${field.templateOptions.minLength} символов`;
}

export function maxlengthValidationMessage(err, field: FormlyFieldConfig) {
  return `Максимальная длинна этого поля ${field.templateOptions.maxLength} символов`;
}

export function minValidationMessage(err, field: FormlyFieldConfig) {
  return `Это поле должно быть >= ${field.templateOptions.min}`;
}

export function maxValidationMessage(err, field: FormlyFieldConfig) {
  return `Это поле должно быть <= ${field.templateOptions.max}`;
}
