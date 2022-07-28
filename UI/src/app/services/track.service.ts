import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Track } from '../models/Track';
import { Type } from '../models/Type';

@Injectable({
  providedIn: 'root'
})
export class TrackService {
  baseUrl = environment.apiUrl;
  headers = { 'content-type': 'application/json' };

  constructor(private httpClient: HttpClient) { }
  getAll(): Observable<Track[]> {
    return this.httpClient.get<Track[]>(this.baseUrl + 'tracks/all');
  }

  get(id: string) {
    return this.httpClient.get<Track>(this.baseUrl + 'tracks/' + id);
  }

  create(track: Track, albumId: string) {
    return this.httpClient.post<Track>(this.baseUrl + 'tracks/' + albumId, JSON.stringify(track), { 'headers': this.headers });
  }

  delete(id: string) {
    return this.httpClient.delete(this.baseUrl + 'tracks/' + id, { 'headers': this.headers });
  }

  getTracksByAlbum(albumId: string) {
    return this.httpClient.get<Track[]>(this.baseUrl + 'tracks/album/' + albumId);
  }

  search(name?: string, duration?: Date, performer?: string, arranger?: string, type?: Type): Observable<Track[]> {
    return this.httpClient.get<Track[]>(this.baseUrl + 'tracks');
  }
}
