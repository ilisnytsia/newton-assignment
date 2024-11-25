import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VideoGame } from '../models/video-game';
import { PagedResult } from '../models/paged-result';


@Injectable({
  providedIn: 'root',
})
export class VideoGameService {
  private apiUrl = 'https://localhost:7244/api/videogames'; 

  videoGames = signal<VideoGame[]>([]); 

  constructor(private http: HttpClient) {}

  getVideoGames(page: number, size: number): Observable<PagedResult<VideoGame>> {
    const params = new HttpParams()
      .set('pageNumber', page)
      .set('pageSize', size);
    return this.http.get<PagedResult<VideoGame>>(`${this.apiUrl}/paged`, { params });
  }

  getVideoGameById(id: number): Observable<VideoGame> {
    return this.http.get<VideoGame>(`${this.apiUrl}/${id}`);
  }

  updateVideoGame(id: number, patchDocument: any): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}`, patchDocument, {
      headers: { 'Content-Type': 'application/json-patch+json' },
    });
  }

  setVideoGames(games: VideoGame[]): void {
    this.videoGames.set(games); 
  }
}
