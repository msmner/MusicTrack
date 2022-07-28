import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlbumService } from 'src/app/services/album.service';

@Component({
  selector: 'app-create-album',
  templateUrl: './create-album.component.html',
  styleUrls: ['./create-album.component.css']
})
export class CreateAlbumComponent implements OnInit {
  albumForm!: FormGroup;

  constructor(private fb: FormBuilder, private albumService: AlbumService, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.albumForm = this.fb.group({
      name: ['', Validators.required],
      publishingYear: ['', [Validators.required]],
    })
  }

  create() {
    this.albumService.create(this.albumForm.value).subscribe({
      complete: () => this.router.navigateByUrl('/albums')
    });
  }
}
