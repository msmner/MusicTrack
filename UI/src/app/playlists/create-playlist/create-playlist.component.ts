import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PlaylistService } from 'src/app/services/playlist.service';

@Component({
  selector: 'app-create-playlist',
  templateUrl: './create-playlist.component.html',
  styleUrls: ['./create-playlist.component.css']
})
export class CreatePlaylistComponent implements OnInit {
  playlistForm!: FormGroup;

  constructor(private fb: FormBuilder, private playlistService: PlaylistService,
    private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.playlistForm = this.fb.group({
      name: ['', Validators.required],
      isPublic: ['', Validators.required],
    })
  }

  create() {
    this.playlistService.create(this.playlistForm.value).subscribe({
      complete: () => this.router.navigateByUrl('/playlists')
    });
  }
}
