import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Track } from 'src/app/models/Track';
import { TrackService } from 'src/app/services/track.service';

@Component({
  selector: 'app-album-details',
  templateUrl: './album-details.component.html',
  styleUrls: ['./album-details.component.css']
})
export class AlbumDetailsComponent implements OnInit {
  tracks: Track[] = [];
  constructor(private trackService: TrackService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadTracks();
  }

  loadTracks() {
    this.trackService.getTracksByAlbum(this.activatedRoute.snapshot.paramMap.get('id')!).subscribe((tracks: Track[]) => {
      this.tracks = tracks;
    })
  }
}
