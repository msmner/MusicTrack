import { Component, OnInit } from '@angular/core';
import { Album } from 'src/app/models/Album';
import { AlbumService } from 'src/app/services/album.service';

@Component({
  selector: 'app-album-list',
  templateUrl: './album-list.component.html',
  styleUrls: ['./album-list.component.css']
})
export class AlbumListComponent implements OnInit {
  albums: Album[] = [];
  constructor(private albumService: AlbumService) { }

  ngOnInit(): void {
    this.loadAlbums();
  }

  loadAlbums() {
    this.albumService.getAll().subscribe((albums: Album[]) => {
      this.albums = albums;
    })
  }
}
