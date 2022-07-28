import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Playlist } from 'src/app/models/Playlist';
import { PlaylistService } from 'src/app/services/playlist.service';

@Component({
  selector: 'app-add-to-playlist',
  templateUrl: './add-to-playlist.component.html',
  styleUrls: ['./add-to-playlist.component.css']
})
export class AddToPlaylistComponent implements OnInit {
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
      position: [''],
    })
  }

  add() {
    this.playlistService.getByName(this.playlistForm.get('name')!.value).subscribe(
      (playlist: Playlist) => {
        this.playlist = playlist;
        this.position = this.playlistForm.get('position')!.value;

        if (this.position == null) {
          this.playlistService.addTrack(this.playlist.id, this.activatedRoute.snapshot.paramMap.get('id')!).subscribe(() => {
            this.router.navigateByUrl('/playlists/' + this.playlist.id)
          });
        } else {
          this.playlistService.addTrack(this.playlist.id, this.activatedRoute.snapshot.paramMap.get('id')!, this.position).subscribe(() => {
            this.router.navigateByUrl('/playlists/' + this.playlist.id)
          });
        }
      }
    )

  }
}
