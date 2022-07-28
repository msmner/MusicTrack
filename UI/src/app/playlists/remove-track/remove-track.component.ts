import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Playlist } from 'src/app/models/Playlist';
import { PlaylistService } from 'src/app/services/playlist.service';

@Component({
  selector: 'app-remove-track',
  templateUrl: './remove-track.component.html',
  styleUrls: ['./remove-track.component.css']
})
export class RemoveTrackComponent implements OnInit {
  playlistForm!: FormGroup;
  position!: number;
  playlist!: Playlist;

  constructor(private fb: FormBuilder, private playlistService: PlaylistService,
    private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.playlistForm = this.fb.group({
      name: ['', Validators.required],
    })
  }

  remove() {
    this.playlistService.getByName(this.playlistForm.get('name')!.value).subscribe(
      (playlist: Playlist) => {
        this.playlist = playlist;

        this.playlistService.removeTrack(this.playlist.id, this.activatedRoute.snapshot.paramMap.get('id')!).subscribe(() => {
          this.router.navigateByUrl('/playlists/' + this.playlist.id)
        })
      }
  )}
}
