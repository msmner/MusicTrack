import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Album } from '../models/Album';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  baseUrl = environment.apiUrl;
  headers = { 'content-type': 'application/json' };

  constructor(private httpClient: HttpClient) { }
  getAll(): Observable<Album[]> {
    return this.httpClient.get<Album[]>(this.baseUrl + 'albums/all');
  }

  get(id: string) {
    return this.httpClient.get<Album>(this.baseUrl + 'albums/' + id);
  }

  create(album: Album) {
    return this.httpClient.post<Album>(this.baseUrl + 'albums', JSON.stringify(album), { 'headers': this.headers });
  }

  delete(id: string) {
    return this.httpClient.delete(this.baseUrl + 'albums/' + id, { 'headers': this.headers });
  }
}
