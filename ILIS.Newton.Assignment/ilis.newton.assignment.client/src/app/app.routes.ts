import { Routes } from '@angular/router';
import { VideoGamesListComponent } from './components/video-games-list/video-games-list.component';
import { VideoGameEditComponent } from './components/video-game-edit/video-game-edit.component';

export const routes: Routes = [
  { path: '', redirectTo: '/games', pathMatch: 'full' },
  { path: 'games', component: VideoGamesListComponent },
  { path: 'games/:id', component: VideoGameEditComponent },
];