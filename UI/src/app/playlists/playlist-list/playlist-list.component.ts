import { Component, OnInit } from '@angular/core';
import { Playlist } from 'src/app/models/Playlist';
import { PlaylistService } from 'src/app/services/playlist.service';

@Component({
  selector: 'app-playlist-list',
  templateUrl: './playlist-list.component.html',
  styleUrls: ['./playlist-list.component.css']
})
export class PlaylistListComponent implements OnInit {
  playlists: Playlist[] = [];
  constructor(private playlistService: PlaylistService) { }

  ngOnInit(): void {
    this.loadTracks();
  }

  loadTracks() {
    this.playlistService.getAll().subscribe((playlists: Playlist[]) => {
      this.playlists = playlists;
    })
  }
}
