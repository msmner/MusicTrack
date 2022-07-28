import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { TrackService } from 'src/app/services/track.service';
import { Type } from 'src/app/models/Type';
import { Track } from 'src/app/models/Track';

@Component({
  selector: 'app-search-tracks',
  templateUrl: './search-tracks.component.html',
  styleUrls: ['./search-tracks.component.css']
})
export class SearchTracksComponent implements OnInit {
  trackForm!: FormGroup;
  trackTypes = Object.values(Type);
  tracks!: Track[];

  constructor(private fb: FormBuilder, private trackService: TrackService,
    private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.trackForm = this.fb.group({
      name: [''],
      performedBy: [''],
      arrangedBy: ['',],
      startDuration: [''],
      endDuration: [''],
      type: [''],
    })
  }

  search() {
    this.trackService.search(this.trackForm.value).subscribe((tracks: Track[]) => {
      this.tracks = tracks;
    })
  }
}
