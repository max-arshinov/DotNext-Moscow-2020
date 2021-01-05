import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {GridBuilderSchemaService} from '../providers/grid-builder-schema-service';
import {HttpClient, HttpParams} from '@angular/common/http';
import {SetFilterComponent} from '../../../components/set-filter/set-filter.component';
import {environment} from '../../../../../environments/environment';
import {GridColumn} from '../models/grid-column';
import {RowNode} from 'ag-grid-community';
import {BehaviorSubject, Subscription} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-grid-builder',
  templateUrl: './grid-builder.component.html',
  styleUrls: ['./grid-builder.css']
})
export class GridBuilderComponent implements OnInit {

  @Input() schemaName: string;
  @Input() path: string;
  @Input() height = 500
  @Input() additionalFrameWorkComponents = {};
  @Input() additionalColumns: GridColumn[];
  @Output() doubleClicked = new EventEmitter();

  constructor(private gridBuilderSchemaService: GridBuilderSchemaService,
              private http: HttpClient,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
    this.defaultColDef = {
      editable: false,
      sortable: true,
      flex: 1,
      minWidth: 50,
      maxWidth: 400,
      filter: true,
      comparator: () => 0
    };
  }

  public get getHeight(): string{
    return `height: ${this.height}px`;
  }

  onGridReady(params: any) {
    params.api.update = this.update.bind(this);
    this.gridApi = params.api;
  }

  columnDefs: GridColumn[] = [];
  rowData = [];
  defaultColDef;
  frameworkComponents = {setFilter: SetFilterComponent};
  filterParams: any = {};
  gridApi: any;
  isLoaded: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  onFilterChanged(filterParams: { columnName: string, selected: any[] }[]) {
    const params = filterParams[0];
    if (params){
      const data = params.selected.map(x => x.value);
      this.filterParams[params.columnName] = data.length === 0 ? "-1" : data;

      this.update();
    }
  }

  onSortChanged(){
    const sortModel = this.gridApi.getSortModel()[0];
    if (sortModel) {
      this.filterParams.asc = sortModel.sort === 'asc';
      this.filterParams.order = sortModel.colId.replace("_1", "");
    } else {
      delete this.filterParams.asc;
      delete this.filterParams.order;
    }
    this.update();
  }

  onCellDoubleClicked(row: RowNode) {
    return this.doubleClicked.emit(row);
  }

  ngOnInit(): void {
    this.frameworkComponents = {
      setFilter: SetFilterComponent,
      ...this.additionalFrameWorkComponents
    };

    this.update();
    this.reloadSchema()
  }

  isSchemaLoaded = false;
  public reloadSchema(params = null) {

    this.gridBuilderSchemaService.getGridSchema(this.schemaName, params).subscribe(res => {
      res.map(x => {
        if (!x.filterParams.options)
          x.filter = "";
      });
      if (this.additionalColumns){
        this.columnDefs = [...this.additionalColumns, ...res]
      }
      else{
        this.columnDefs = res;
      }
      this.isSchemaLoaded = true;
    });
  }

  public update() {
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: this.filterParams
    }).then(_ => this.getData());
  }

  private hasEmptyFilters(): boolean{
    const values = Object.values(this.filterParams);
    return values.includes('-1') || values.includes(-1);
  }

  getData(): void{
    if (!this.hasEmptyFilters()) {
      this.http.get(
          `${environment.httpDomain}${this.path}`,
          {params: new HttpParams({fromObject: this.filterParams as any})})
        .subscribe(res => {
          this.rowData = res as any[];
          this.isLoaded.next(true);
        });
    }
    else{
      this.rowData = [];
    }
  }
}
