import {Observable} from "rxjs";
import {IAuthenticationResult, IUser} from "../../../api-authorization/authorize.service";

export interface IAuthorizeService {
  isAuthenticated(): Observable<boolean>,
  getAccessToken(): Observable<string>,
  signIn(state: any): Promise<IAuthenticationResult>,
  completeSignIn(url: string),
  signOut(state: any): Promise<IAuthenticationResult>,
  completeSignOut(url: string),
  getUser(): Observable<IUser | null>
}
