import { Component } from '@angular/core';
import { User } from './models/User';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.setCurrentUSer();
  }

  setCurrentUSer(){
    const user: User = JSON.parse(localStorage.getItem('user')!);
    this.authService.setCurrentUser(user);
  }
}
