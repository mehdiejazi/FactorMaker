import { Component, OnInit } from '@angular/core';
import { Store } from '../../interfaces/store/store';
import { HttpRequestResultT } from '../../models/http-request-result-t';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-active-stores',
  templateUrl: './active-stores.component.html',
  styleUrl: './active-stores.component.css'
})
export class ActiveStoresComponent implements OnInit {
  public stores: Store[] = [];
  public isLoading = true;
  public errorMessages: string[] = [];

  public constructor(private _storeService: StoreService) { }

  public ngOnInit(): void {
    this.loadStores();
  }

  public trackByStore(index: number, store: Store): string {
    return store.id || store.storeEnglishName || `${index}`;
  }

  public getStoreLink(store: Store): string {
    return `/s/${store.storeEnglishName}`;
  }

  public getExternalUrl(url: string): string {
    const rawUrl = (url || '').trim();
    if (!rawUrl) {
      return '';
    }

    return /^https?:\/\//i.test(rawUrl) ? rawUrl : `https://${rawUrl}`;
  }

  private loadStores(): void {
    this.isLoading = true;
    this.errorMessages = [];

    this._storeService.getActiveAsync().subscribe({
      next: (result: HttpRequestResultT<Store[]>) => {
        if (!result.isSuccessful) {
          this.errorMessages = result.errorMessages?.length
            ? result.errorMessages
            : ['در دریافت فروشگاه‌های فعال خطایی رخ داد.'];
          this.stores = [];
          this.isLoading = false;
          return;
        }

        this.stores = result.data || [];
        this.isLoading = false;
      },
      error: () => {
        this.errorMessages = ['در دریافت فروشگاه‌های فعال خطایی رخ داد.'];
        this.stores = [];
        this.isLoading = false;
      }
    });
  }
}
