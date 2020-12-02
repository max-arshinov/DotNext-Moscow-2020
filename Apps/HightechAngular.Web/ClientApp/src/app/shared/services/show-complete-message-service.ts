import {MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition} from "@angular/material/snack-bar";
import {Injectable} from "@angular/core";

@Injectable()
export class ShowCompleteMessageService{
  public horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  public verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  public duration: number = 5000;
  public action: string = 'Close';

  constructor(private _snackBar: MatSnackBar){

  }

  public show(message: string){
    this._snackBar.open(message, this.action, {
        duration: this.duration,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition
      }
    )
  }
}
