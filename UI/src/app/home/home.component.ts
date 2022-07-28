import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

  toggleRegister() {
    this.registerMode = !this.registerMode;
  }
  
  cancelRegister(event: boolean) {
    this.registerMode = event;
  }
}
