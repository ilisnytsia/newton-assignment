import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoGameService } from '../../services/video-game.service';
import { VideoGame } from '../../models/video-game';
import { FormsModule } from '@angular/forms'
import { NgIf } from '@angular/common';

@Component({
    selector: 'app-video-game-edit',
    standalone: true,
    imports: [FormsModule, NgIf],
    templateUrl: './video-game-edit.component.html',
    styleUrls: ['./video-game-edit.component.css']
})
export class VideoGameEditComponent implements OnInit {
  videoGame: VideoGame | null = {
    id: 0,
    title: '',
    genre: '',
    price: 0,
    releaseDate: ''
  };

  showModal: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private videoGameService: VideoGameService
  ) {}

  ngOnInit(): void {
    const state = history.state; 
    if (state.game) {
      this.videoGame = state.game; 
    } else {
      const id = Number(this.route.snapshot.paramMap.get('id'));
      this.videoGameService.getVideoGameById(id).subscribe((game) => {
      });
    }
  }

  saveChanges(): void {
    if (!this.videoGame) return;

    const patchDoc = [
      { op: 'replace', path: '/title', value: this.videoGame.title },
      { op: 'replace', path: '/price', value: this.videoGame.price },
      { op: 'replace', path: '/genre', value: this.videoGame.genre },
      { op: 'replace', path: '/releaseDate', value: this.videoGame.releaseDate },
    ];

    this.videoGameService.updateVideoGame(this.videoGame.id, patchDoc).subscribe(() => {
      this.showModal = true;
    });
  }


  closeModal(): void {
    this.showModal = false;
    this.router.navigate(['/games']);
  }
}
