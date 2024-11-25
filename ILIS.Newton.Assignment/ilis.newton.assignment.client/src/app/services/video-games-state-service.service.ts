import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class VideoGameStateService {
  currentPage = signal<number>(1);


  setCurrentPage(page: number): void {
    this.currentPage.set(page);
  }

  getCurrentPage(): number {
    return this.currentPage();
  }
}
