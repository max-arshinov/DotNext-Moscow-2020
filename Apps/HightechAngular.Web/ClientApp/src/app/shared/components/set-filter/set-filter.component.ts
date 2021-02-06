import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { IDoesFilterPassParams, IFilterParams, RowNode } from '@ag-grid-community/all-modules';
import { IFilterAngularComp } from '@ag-grid-community/angular';
import { FormControl} from '@angular/forms';
import {ApiDropdownsService} from '../../modules/builders/providers/api-dropdowns-service';

@Component({
  selector: 'app-filter-cell',
  templateUrl: './set-filter.component.html',
  styleUrls: ['./set-filter.component.scss']
})
export class SetFilterComponent implements IFilterAngularComp {
  private params: IFilterParams;
  public selectedAll = true;
  public indeterminate = false;
  private valueGetter: (rowNode: RowNode) => any;
  dropdownsList: {value: string, label: string, checked: boolean}[];
  filtered: {value: string, label: string, checked: boolean}[];
  filter: FormControl;
  dropdownService: ApiDropdownsService;

  constructor(private dropdownsService: ApiDropdownsService) {
    this.filter = new FormControl('');
    this.dropdownService = dropdownsService;
  }

  @ViewChild('input', {static: false, read: ViewContainerRef }) public input;

  agInit(params: IFilterParams): void {
    this.params = params;
    this.valueGetter = params.valueGetter;
    const fieldOptions = params.colDef.filterParams.options.options;
    fieldOptions.forEach(y => y.checked = true);
    this.dropdownsList = fieldOptions;
    this.filtered = this.dropdownsList;
  }

  doesFilterPass(params: IDoesFilterPassParams): boolean {
    if (this.dropdownsList.length === 0) {
      return true;
    }
    const selected = this.dropdownsList.filter(y => y.checked).map(x => x.value);
    return selected.includes(this.valueGetter(params.node));
  }

  selectAll() {
    this.filtered.forEach(y => y.checked = this.selectedAll);
    this.onFilterChanged();
  }

  filterDropdowns() {
    this.filtered = this.dropdownsList.filter(x => x.label.toLowerCase().includes(this.filter.value.toLowerCase()));
    this.checkIfAllSelected();
  }

  checkIfAllSelected() {
    if (this.filtered.every(x => x.checked === true)) {
      this.selectedAll = true;
      this.indeterminate = false;
    } else if (this.filtered.filter(x => x.checked === true).length >= 1) {
      this.indeterminate = true;
    } else {
      this.selectedAll = false;
      this.indeterminate = false;
    }
    this.onFilterChanged();
  }

  onFilterChanged() {
    this.params.filterChangedCallback([{columnName: this.params.colDef.field, selected: this.dropdownsList.filter(x => x.checked)}]);
  }

  reset() {
    this.selectAll();
    this.selectedAll = true;
    this.indeterminate = false;
  }

  isFilterActive(): boolean {
    return true;
  }

  getModel(): any {
    //return { value: 'test' };
  }

  setModel(model: any): void {
  }
}
