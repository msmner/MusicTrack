import { HttpClient, HttpParams } from '@angular/common/http';
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
  params = new HttpParams();

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

  search(name?: string, performer?: string, arranger?: string, startDuration?: Date, endDuration?: Date, type?: Type): Observable<Track[]> {
    if (name) {
      this.params = this.params.set('name', name);
    } else if (performer) {
      this.params = this.params.set('performer', performer);
    }else if (arranger) {
      this.params = this.params.set('arranger', arranger);
    }
    // else if (startDuration) {
    //   let params = new HttpParams().set('startDuration', startDuration);
    // }else if (endDuration) {
    //   let params = new HttpParams().set('endDuration', endDuration);
    // }
    else if (type) {
      this.params = this.params.set('type', type);
    }
    
    return this.httpClient.get<Track[]>(this.baseUrl + 'tracks', {params: this.params});
  }
}
