import {Component, OnInit} from '@angular/core';
import {RowNode} from 'ag-grid-community';

@Component({
  templateUrl: './grid-builder-demo.component.html',
})
export class GridBuilderDemoComponent implements OnInit {
  ngOnInit(): void {
  }

  onDoubleClicked(row: RowNode) {
  }
}
