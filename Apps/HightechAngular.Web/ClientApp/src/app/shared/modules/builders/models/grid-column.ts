export class GridColumn {
  cellRendererParams?: any;
  cellRenderer?: string;
  headerName: string
  field?: string
  sortable?: boolean
  hide?: boolean
  filter?: string
  menuTabs?: string[]
  filterParams?: {
    options?: any[]
  }
  children?: GridColumn[];
  width?: number;
}
