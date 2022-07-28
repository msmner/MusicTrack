import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Track } from 'src/app/models/Track';
import { PlaylistService } from 'src/app/services/playlist.service';

@Component({
  selector: 'app-playlist-details',
  templateUrl: './playlist-details.component.html',
  styleUrls: ['./playlist-details.component.css']
})
export class PlaylistDetailsComponent implements OnInit {
  tracks!: Track[];
  constructor(private playlistService: PlaylistService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadTracks();
  }

  loadTracks() {
    this.playlistService.getTracks(this.activatedRoute.snapshot.paramMap.get('id')!).subscribe((tracks: Track[]) => {
      this.tracks = tracks;
    })
  }
}
