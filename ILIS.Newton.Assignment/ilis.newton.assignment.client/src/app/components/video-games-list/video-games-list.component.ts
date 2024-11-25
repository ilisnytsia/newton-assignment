import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { VideoGameService } from '../../services/video-game.service';
import { VideoGame } from '../../models/video-game';
import { VideoGameStateService } from '../../services/video-games-state-service.service';

@Component({
    selector: 'app-video-games-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './video-games-list.component.html',
    styleUrls: ['./video-games-list.component.css']
})
export class VideoGamesListComponent implements OnInit {
  page = 1;
  size = 6;
  totalCount = 0;

  videoGames: VideoGame[] = [];

  constructor(
    private videoGameService: VideoGameService, 
    private router: Router, 
    private stateService: VideoGameStateService
  ) {}

  ngOnInit(): void {
    this.page = this.stateService.getCurrentPage();
    this.loadGames();
  }

  loadGames(): void {
    this.videoGameService.getVideoGames(this.page, this.size).subscribe((result) => {
      this.videoGameService.setVideoGames(result.items);
      this.videoGames = this.videoGameService.videoGames();
      this.totalCount = result.totalCount;
    });
  }

  editGame(id: number): void {
    const selectedGame = this.videoGames.find((game) => game.id === id); 
    this.router.navigate([`/games`, id], { state: { game: selectedGame } }); 
  }

  onPageChange(newPage: number): void {
    this.page = newPage;
    this.stateService.setCurrentPage(newPage);
    this.loadGames();
  }
}
