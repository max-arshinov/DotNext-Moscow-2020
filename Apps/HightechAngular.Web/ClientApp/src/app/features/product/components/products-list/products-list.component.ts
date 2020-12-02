import { Component, OnInit } from '@angular/core';
import {RowNode} from 'ag-grid-community';
import {Router} from '@angular/router';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onDoubleClicked(row: RowNode) {
    this.router.navigateByUrl(`/products/update/${row.data.id}`);
  }
}
