import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoGameEditComponent } from './video-game-edit.component';

describe('VideoGameEditComponent', () => {
  let component: VideoGameEditComponent;
  let fixture: ComponentFixture<VideoGameEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VideoGameEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VideoGameEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
