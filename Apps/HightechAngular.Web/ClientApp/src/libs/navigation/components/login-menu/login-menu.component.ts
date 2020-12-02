import {Component, Inject, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import {IAuthorizeService} from "../../models/iauthorize-service";
import {tap} from "rxjs/operators";

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;

  constructor(@Inject('IAuthorizeService') private authorizeService: IAuthorizeService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }
}
