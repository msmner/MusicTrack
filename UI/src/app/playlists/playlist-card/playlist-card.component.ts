import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Playlist } from 'src/app/models/Playlist';
import { PlaylistService } from 'src/app/services/playlist.service';

@Component({
  selector: 'app-playlist-card',
  templateUrl: './playlist-card.component.html',
  styleUrls: ['./playlist-card.component.css']
})
export class PlaylistCardComponent implements OnInit {
  @Input() playlist!: Playlist;
  constructor(private playlistService: PlaylistService, private router: Router) { }

  ngOnInit(): void {
  }

  delete() {
    this.playlistService.delete(this.playlist.id).subscribe({
      complete: () => this.router.navigateByUrl('')
    });
  }
}
