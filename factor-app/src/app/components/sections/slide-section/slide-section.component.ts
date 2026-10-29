import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SettingsService } from '../../../services/settings.service';

@Component({
  selector: 'app-slide-section',
  templateUrl: './slide-section.component.html',
  styleUrl: './slide-section.component.css'
})
export class SlideSectionComponent {
  public settingsService: SettingsService;

  public constructor(private _router: Router, private _settingsService: SettingsService) {
    this.settingsService = _settingsService;
  }

  public addNewSlide(): void {
    this._router.navigate(['/slides/form']);
  }

  public viewList(): void {
    this._router.navigate(['/slides/list']);
  }
}
