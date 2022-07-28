import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Type } from 'src/app/models/Type';
import { TrackService } from 'src/app/services/track.service';

@Component({
  selector: 'app-create-track',
  templateUrl: './create-track.component.html',
  styleUrls: ['./create-track.component.css']
})
export class CreateTrackComponent implements OnInit {
  trackForm!: FormGroup;
  trackTypes = Object.values(Type);

  constructor(private fb: FormBuilder, private trackService: TrackService,
    private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.trackForm = this.fb.group({
      name: ['', Validators.required],
      writtenBy: ['', [Validators.required]],
      performedBy: ['', Validators.required],
      arrangedBy: ['', [Validators.required]],
      duration: ['', Validators.required],
      type: ['', [Validators.required]],
    })
  }

  create() {
    this.trackService.create(this.trackForm.value, this.activatedRoute.snapshot.paramMap.get('id')!).subscribe({
      next: track => console.log(track),
      error: error => console.log(error),
      complete: () => this.router.navigateByUrl('/tracks')
    });
  }
}
