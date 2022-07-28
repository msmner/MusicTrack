import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Album } from 'src/app/models/Album';
import { AlbumService } from 'src/app/services/album.service';

@Component({
  selector: 'app-album-card',
  templateUrl: './album-card.component.html',
  styleUrls: ['./album-card.component.css']
})
export class AlbumCardComponent implements OnInit {
  @Input() album!: Album;
  constructor(private albumService: AlbumService, private router: Router) { }

  ngOnInit(): void {
  }

  delete() {
    this.albumService.delete(this.album.id).subscribe({
      complete: () => this.router.navigateByUrl('')
    });
  }
}
