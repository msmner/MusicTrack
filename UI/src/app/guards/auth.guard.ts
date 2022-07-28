import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { User } from '../models/User';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService, private toastr: ToastrService) { }
  canActivate(): Observable<boolean> {
    return this.authService.currentUser$.pipe(
      map((user: User) => {
        if (user) return true;
        this.toastr.error("You need to login first!")
        return false;
      })
    )
  }
}