import { Injectable } from '@angular/core';
import { CanActivate, } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { map } from 'rxjs/operators';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private authService: AuthService, private toastr: ToastrService) {
  }
  canActivate(): Observable<boolean> {
    return this.authService.currentUser$.pipe(
      map((user : User) => {
        if (user.roles.includes('admin')){
          return true;
        }
        this.toastr.error("You cannot enter this area")
        return false;
      })
    )
  }

}
