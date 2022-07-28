import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Playlist } from '../models/Playlist';
import { environment } from 'src/environments/environment';
import { Track } from '../models/Track';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {
  baseUrl = environment.apiUrl;
  headers = { 'content-type': 'application/json' };

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<Playlist[]> {
    return this.httpClient.get<Playlist[]>(this.baseUrl + 'playlists/all');
  }

  get(id: string): Observable<Playlist> {
    return this.httpClient.get<Playlist>(this.baseUrl + 'playlists/' + id);
  }

  getByName(name: string): Observable<Playlist> {
    return this.httpClient.get<Playlist>(this.baseUrl + 'playlists/get/' + name);
  }

  create(playlist: Playlist) {
    return this.httpClient.post<Playlist>(this.baseUrl + 'playlists', JSON.stringify(playlist), { 'headers': this.headers });
  }

  delete(id: string) {
    return this.httpClient.delete(this.baseUrl + 'playlists/' + id, { 'headers': this.headers });
  }

  getTracks(id: string): Observable<Track[]> {
    return this.httpClient.get<Track[]>(this.baseUrl + 'playlists/' + id + '/tracks');
  }

  addTrack(playlistId: string, trackId: string, position?: number) {
    if (position) {
      return this.httpClient.put(this.baseUrl + 'playlists/' + playlistId + '/add/' + trackId + '/' + position, { 'headers': this.headers });
    } else {
      return this.httpClient.put(this.baseUrl + 'playlists/' + playlistId + '/add/' + trackId, { 'headers': this.headers });
    }
  }
}
