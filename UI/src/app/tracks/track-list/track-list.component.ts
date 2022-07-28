import { Component, OnInit } from '@angular/core';
import { Track } from 'src/app/models/Track';
import { TrackService } from 'src/app/services/track.service';

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css']
})
export class TrackListComponent implements OnInit {
  tracks: Track[] = [];
  constructor(private trackService: TrackService) { }

  ngOnInit(): void {
    this.loadTracks();
  }

  loadTracks() {
    this.trackService.getAll().subscribe((tracks: Track[]) => {
      this.tracks = tracks;
    })
  }
}
