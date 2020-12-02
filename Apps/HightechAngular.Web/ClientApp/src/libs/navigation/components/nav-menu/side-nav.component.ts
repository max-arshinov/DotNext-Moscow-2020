import {Component, Input} from '@angular/core';
import {MenuHeader} from '../../models/menuHeader';

@Component({
  selector: 'app-side-nav',
  template: `
    <div *ngFor="let menuItem of menuHeaders">
      <mat-toolbar>
        {{menuItem.name}}
      </mat-toolbar>
      <mat-nav-list *ngFor="let menuElement of menuItem.elements">
        <a mat-list-item [routerLink]="[menuElement.path]">
          {{menuElement.name}}
        </a>
      </mat-nav-list>
    </div>
  `
})
export class SideNavComponent {

  @Input() menuHeaders: MenuHeader[];

  constructor() { }


}
