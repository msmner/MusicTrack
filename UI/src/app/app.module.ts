import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component'; 
import { TextInputComponent } from './forms/text-input/text-input.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlbumListComponent } from './albums/album-list/album-list.component';
import { AlbumDetailsComponent } from './albums/album-details/album-details.component';
import { PlaylistListComponent } from './playlists/playlist-list/playlist-list.component';
import { PlaylistDetailsComponent } from './playlists/playlist-details/playlist-details.component';
import { TrackListComponent } from './tracks/track-list/track-list.component';
import { SharedModule } from './modules/shared.module';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { HasRoleDirective } from './directives/has-role.directive';
import { CreateAlbumComponent } from './albums/create-album/create-album.component';
import { AlbumCardComponent } from './albums/album-card/album-card.component';
import { CreateTrackComponent } from './tracks/create-track/create-track.component';
import { TrackCardComponent } from './tracks/track-card/track-card.component';
import { PlaylistCardComponent } from './playlists/playlist-card/playlist-card.component';
import { CreatePlaylistComponent } from './playlists/create-playlist/create-playlist.component';
import { AddToPlaylistComponent } from './playlists/add-to-playlist/add-to-playlist.component';
import { RemoveTrackComponent } from './playlists/remove-track/remove-track.component';
import { SearchTracksComponent } from './tracks/search-tracks/search-tracks.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    NavComponent,
    TextInputComponent,
    HomeComponent,
    AlbumListComponent,
    AlbumDetailsComponent,
    PlaylistListComponent,
    PlaylistDetailsComponent,
    TrackListComponent,
    NotFoundComponent,
    ServerErrorComponent,
    HasRoleDirective,
    CreateAlbumComponent,
    AlbumCardComponent,
    CreateTrackComponent,
    TrackCardComponent,
    PlaylistCardComponent,
    CreatePlaylistComponent,
    AddToPlaylistComponent,
    RemoveTrackComponent,
    SearchTracksComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    BrowserAnimationsModule,
    SharedModule
  ],
  providers: [ 
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
