import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlbumDetailsComponent } from './albums/album-details/album-details.component';
import { AlbumListComponent } from './albums/album-list/album-list.component';
import { CreateAlbumComponent } from './albums/create-album/create-album.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { AddToPlaylistComponent } from './playlists/add-to-playlist/add-to-playlist.component';
import { CreatePlaylistComponent } from './playlists/create-playlist/create-playlist.component';
import { PlaylistDetailsComponent } from './playlists/playlist-details/playlist-details.component';
import { PlaylistListComponent } from './playlists/playlist-list/playlist-list.component';
import { RegisterComponent } from './register/register.component';
import { CreateTrackComponent } from './tracks/create-track/create-track.component';
import { TrackListComponent } from './tracks/track-list/track-list.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'albums/create', component: CreateAlbumComponent },
      { path: 'albums/:id', component: AlbumDetailsComponent },
      { path: 'albums', component: AlbumListComponent },
      { path: 'playlists/create', component: CreatePlaylistComponent },
      { path: 'playlists', component: PlaylistListComponent },
      { path: 'playlists/:id', component: PlaylistDetailsComponent },
      { path: 'tracks', component: TrackListComponent },
      { path: 'tracks/create/:id', component: CreateTrackComponent },
      { path: 'playlists/addtrack/:id', component: AddToPlaylistComponent },
    ]
  },
  { path: 'register', component: RegisterComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
