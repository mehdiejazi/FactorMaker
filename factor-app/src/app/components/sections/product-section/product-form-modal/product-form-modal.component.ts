import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from '../../../../models/product/product';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CategorySelectModalComponent } from '../../category-section/category-select-modal/category-select-modal.component';
import { ImageAssetSelectModalComponent } from '../../image-asset-section/image-asset-select-modal/image-asset-select-modal.component';
import { SettingsService } from '../../../../services/settings.service';

@Component({
  selector: 'app-product-form-modal',
  templateUrl: './product-form-modal.component.html',
  styleUrl: './product-form-modal.component.css'
})
export class ProductFormModalComponent {
  public active: boolean = false;
  public title: string;
  public onClose: Subject<boolean>;
  public error: Error;
  public product:Product;

  public constructor(
    private _bsModalRef: BsModalRef,
    private _bsModalService: BsModalService,
    private _settingsService: SettingsService) { }

  public ngOnInit(): void {

    this.onClose = new Subject();

  }

  public onConfirm(): void {

    if (this.isValidForm() == false)
      return;

    this.active = false;
    this.onClose.next(true);
    this._bsModalRef.hide();

  }

  public selectCategory() {

    const modal = this._bsModalService.show(CategorySelectModalComponent,
      { class: 'modal-xl modal-dialog-centered modal-dialog' });

    (<CategorySelectModalComponent>modal.content).showConfirmationModal();

    (<CategorySelectModalComponent>modal.content).onClose.subscribe(result => {

      if (result == true) {

        this.product.categoryId = modal.content.selectedCategory.id;
        this.product.category = modal.content.selectedCategory;
        
      }

    });

  }

  public selectImage(): void {
    const modal = this._bsModalService.show(ImageAssetSelectModalComponent,
      { class: 'modal-xl modal-dialog-centered modal-dialog' });
    (<ImageAssetSelectModalComponent>modal.content).showConfirmationModal(this._settingsService.getUser().id);
    (<ImageAssetSelectModalComponent>modal.content).onClose.subscribe(result => {
      if (result === true) {
        this.product.image = modal.content.selectedImageAsset;
        this.product.imageId = modal.content.selectedImageAsset.id;
      }
    });
  }

  public removeImage(): void {
    this.product.image = null;
    this.product.imageId = null;
  }

  public isValidForm(): boolean {

    var result = true;

    if (!this.product.name) {

      result = false;

    }

    if (!this.product.price) {

      result = false;

    }

    if (!this.product.categoryId) {

      result = false;

    }
    
    return result;
  }

  public showConfirmationModal(product:Product): void {

    this.product = product;
    this.product.showOnLanding = this.product.id ? this.product.showOnLanding : true;
    this.product.sortOrder = this.product.sortOrder || 0;

    if (product?.id)

      this.title = 'ویرایش';

    else {

      this.title = 'جدید';

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
