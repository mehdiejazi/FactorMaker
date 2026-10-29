
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { Store, StoreSocialLink } from '../../../../models/store/store';
import { ImageAssetSelectModalComponent } from '../../image-asset-section/image-asset-select-modal/image-asset-select-modal.component';
import { SettingsService } from '../../../../services/settings.service';

@Component({
  selector: 'app-store-form-modal',
  templateUrl: './store-form-modal.component.html',
  styleUrls: ['./store-form-modal.component.css']
})
export class StoreFormModalComponent implements OnInit {

  public active: boolean = false;
  public title: string;
  public onClose: Subject<boolean>;
  public error: Error;
  public store: Store;
  public activeTab: 'intro' | 'social' | 'appearance' = 'intro';
  public readonly palettes = [
    { id: 'forest', name: 'سبز جنگلی', primary: '#214d3c', surface: '#dce8de', accent: '#f58220' },
    { id: 'ocean', name: 'سرمه‌ای و فیروزه‌ای', primary: '#173044', surface: '#deeeeb', accent: '#247c78' },
    { id: 'burgundy', name: 'زرشکی و رز', primary: '#672c3c', surface: '#f2e3e5', accent: '#c88891' },
    { id: 'indigo', name: 'نیلی و طلایی', primary: '#29315a', surface: '#e9e9f1', accent: '#c69a43' },
    { id: 'graphite', name: 'ذغالی و سبز', primary: '#262d2a', surface: '#e7ecd9', accent: '#809a3e' }
  ];

  public constructor(
    private _bsModalRef: BsModalRef,
    private _bsModalService: BsModalService,
    private _settingsService:SettingsService) { }

  public ngOnInit(): void {

    this.onClose = new Subject();

  }

  public selectImage(target: 'logo' | 'cover') {

    const modal = this._bsModalService.show(ImageAssetSelectModalComponent,
      { class: 'modal-xl modal-dialog-centered modal-dialog' });
    (<ImageAssetSelectModalComponent>modal.content).showConfirmationModal(this._settingsService.getUser().id);
    (<ImageAssetSelectModalComponent>modal.content).onClose.subscribe(result => {

      if (result === true) {

        if (target === 'logo') {
          this.store.logo = modal.content.selectedImageAsset;
          this.store.logoId = modal.content.selectedImageAsset.id;
        } else {
          this.store.coverImage = modal.content.selectedImageAsset;
          this.store.coverImageId = modal.content.selectedImageAsset.id;
        }

      } else if (result === false) {

      }

    });

  }

  public addSocialLink(): void {
    const link = new StoreSocialLink();
    link.storeId = this.store.id;
    link.isPrimary = this.store.socialLinks.length === 0;
    this.store.socialLinks.push(link);
  }

  public removeSocialLink(index: number): void {
    const wasPrimary = this.store.socialLinks[index]?.isPrimary;
    this.store.socialLinks.splice(index, 1);
    if (wasPrimary && this.store.socialLinks.length) {
      this.store.socialLinks[0].isPrimary = true;
    }
  }

  public setPrimarySocialLink(index: number): void {
    this.store.socialLinks.forEach((link, linkIndex) => link.isPrimary = linkIndex === index);
  }

  public get selectedPalette() {
    return this.palettes.find(palette => palette.id === this.store.themePalette) || this.palettes[0];
  }

  public onConfirm(): void {

    if (this.isValidForm() == false)
      return;

    this.active = false;
    this.onClose.next(true);
    this._bsModalRef.hide();

  }


  public isValidURL(str: string) {

    if (str == '') return true;

    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
      '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
      '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
      '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
      '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
      '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator

    return !!pattern.test(str);
    
  }

  public isValidForm(): boolean {

    var result = true;

    if (!this.store.name) {

      result = false;

    }

    if (!this.store.storeEnglishName) {

      result = false;

    }

    if (!this.isValidURL(this.store.url || '')) {

      result = false;

    }

    return result;
  }

  public showConfirmationModal(store: Store): void {

    this.store = store;
    this.store.socialLinks = this.store.socialLinks || [];
    if (!this.store.socialLinks.length && this.store.url) {
      const legacyLink = new StoreSocialLink();
      legacyLink.storeId = this.store.id;
      legacyLink.platform = 'website';
      legacyLink.url = this.store.url;
      legacyLink.label = 'مشاهده و سفارش';
      legacyLink.isPrimary = true;
      this.store.socialLinks.push(legacyLink);
    }
    this.store.themePalette = this.store.themePalette || 'forest';
    this.store.isPublic = this.store.id ? this.store.isPublic : true;
    this.activeTab = 'intro';

    if (store?.id)

      this.title = 'ویرایش';

    else {

      this.title = 'گزینه جدید';

    }

    this.active = true;

  }



  public hideConfirmationModal(): void {

    this.active = false;
    this.onClose.next(false);
    this._bsModalRef.hide();

  }

  public onCancel(): void {

    this.hideConfirmationModal();

  }


}
