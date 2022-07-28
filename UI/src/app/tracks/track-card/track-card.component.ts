import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Track } from 'src/app/models/Track';
import { TrackService } from 'src/app/services/track.service';

@Component({
  selector: 'app-track-card',
  templateUrl: './track-card.component.html',
  styleUrls: ['./track-card.component.css']
})
export class TrackCardComponent implements OnInit {
  @Input() track!: Track
  constructor(private trackService: TrackService, private router: Router) { }

  ngOnInit(): void {
  }

  delete() {
    this.trackService.delete(this.track.id).subscribe({
      complete: () => this.router.navigateByUrl('')
    });
  }
}
