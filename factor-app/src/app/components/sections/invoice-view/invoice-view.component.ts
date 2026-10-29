import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FactorService } from '../../../services/factor.service';
import { Factor } from '../../../interfaces/factor/factor';
import * as jsPDFLib from 'jspdf';
import html2canvas from 'html2canvas';
import { SettingsService } from '../../../services/settings.service';
import { StoreService } from '../../../services/store.service';
import { HttpRequestResultT } from '../../../models/http-request-result-t';
import { Store } from '../../../interfaces/store/store';

const jsPDF = jsPDFLib.jsPDF;

@Component({
  selector: 'app-invoice-view',
  templateUrl: './invoice-view.component.html',
  styleUrl: './invoice-view.component.css',
  encapsulation: ViewEncapsulation.None
})
export class InvoiceViewComponent implements OnInit {
  public factor: Factor;
  public loading: boolean = true;
  public error: string = '';
  public isSample = false;
  public storeName: string = '';
  public storeLogoUrl: string = '';
  constructor(
    private route: ActivatedRoute,
    private factorService: FactorService,
    private storeService: StoreService,
    private settingsService: SettingsService
  ) {

  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const factorId = params['factor_id'];
      if (factorId) {
        this.isSample = false;
        this.loadFactor(factorId);
      } else {
        this.showSample();
      }
    });
  }

  private loadFactor(factorId: string): void {
    this.factorService.getFactorWithItemsByIdAsync(factorId).subscribe(
      (response: HttpRequestResultT<Factor>) => {
        if (response.isSuccessful && response.data) {
          this.factor = response.data;
          this.applyStore(this.factor.store);

          if (this.factor.store?.name || !this.factor.storeId) {
            this.loading = false;
            return;
          }

          this.storeService.getByIdAsync(this.factor.storeId).subscribe({
            next: (storeResp: HttpRequestResultT<Store>) => {
              if (storeResp?.isSuccessful && storeResp.data) {
                this.applyStore(storeResp.data);
              }
              this.loading = false;
            },
            error: () => {
              this.loading = false;
            }
          });
        } else {
          this.error = response.errorMessages?.[0] || 'خطا در دریافت فاکتور';
          this.loading = false;
        }
      },
      (error: any) => {
        console.error('خطا در دریافت فاکتور:', error);
        this.error = 'خطا در دریافت فاکتور';
        this.loading = false;
      }
    );
  }

  public get customerName(): string {
    const owner = this.factor?.owner;
    const name = `${owner?.firstName || ''} ${owner?.lastName || ''}`.trim();
    return name || 'مشتری ثبت نشده';
  }

  private showSample(): void {
    this.isSample = true;
    this.error = '';
    this.storeName = 'فروشگاه نمونه فکتور میکر';
    this.storeLogoUrl = '';
    this.factor = {
      description: 'پیش‌نمایش چیدمان فاکتور با داده نمونه. برای فاکتور واقعی از داخل پنل، دکمه مشاهده فاکتور را بزنید.',
      isClosed: true,
      sellDateTime: new Date().toISOString(),
      totalPrice: 339200,
      owner: { firstName: 'سارا', lastName: 'قاسمی' },
      factorItems: [
        { product: { name: 'قهوه ترک' }, price: 95000, quantity: 2, offPercent: 0 },
        { product: { name: 'عسل طبیعی' }, price: 168000, quantity: 1, offPercent: 10 }
      ]
    } as Factor;
    this.loading = false;
  }

  private applyStore(store: Store): void {
    if (!store) {
      this.storeName = this.storeName || 'فروشگاه';
      return;
    }

    this.storeName = store.name || 'فروشگاه';
    this.storeLogoUrl = this.absoluteUrl(store.logo?.url);
    if (this.factor) {
      this.factor.store = store;
    }
  }

  private absoluteUrl(url: string): string {
    if (!url) {
      return '';
    }

    if (url.startsWith('http://') || url.startsWith('https://')) {
      return url;
    }

    return `${this.settingsService.baseUrl}${url.startsWith('/') ? '' : '/'}${url}`;
  }

  public printInvoice(): void {
    window.print();
  }

  public async downloadPDF(): Promise<void> {
    try {
      const element = document.getElementById('invoiceContent');
      if (!element) {
        alert('محتوای فاکتور یافت نشد');
        return;
      }

      const canvas = await html2canvas(element, {
        scale: 2,
        useCORS: true,
        logging: false,
        backgroundColor: '#ffffff'
      });

      const imgData = canvas.toDataURL('image/png');
      const pdf = new jsPDF({
        orientation: 'p',
        unit: 'mm',
        format: 'a4'
      });

      const imgWidth = 210; // عرض A4 به میلی‌متر
      const pageHeight = 297; // ارتفاع A4 به میلی‌متر
      let heightLeft = canvas.height * imgWidth / canvas.width;
      let position = 0;

      pdf.addImage(imgData, 'PNG', 0, position, imgWidth, Math.min(heightLeft, pageHeight));
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - pageHeight;
        pdf.addPage();
        pdf.addImage(imgData, 'PNG', 0, position, imgWidth, heightLeft);
        heightLeft -= pageHeight;
      }

      pdf.save(`فاکتور-${this.factor?.id || 'بدون-شناسه'}.pdf`);
    } catch (error) {
      console.error('خطا در دانلود PDF:', error);
      alert('خطا در تهیه PDF');
    }
  }

  public getTotal(): number {
    if (!this.factor?.factorItems) return 0;
    return this.factor.factorItems.reduce((sum: number, item: any) => {
      const itemTotal = (item.price * item.quantity);
      const discount = itemTotal * (item.offPercent / 100);
      return sum + (itemTotal - discount);
    }, 0);
  }

  public getTaxAmount(): number {
    const total = this.getTotal();
    return total * (this.taxPercent / 100);
  }

  public get taxPercent(): number {
    const value = Number(this.factor?.store?.taxPercent);
    return Number.isFinite(value) && value >= 0 ? value : 9;
  }

  public getFinalTotal(): number {
    return this.getTotal() + this.getTaxAmount();
  }
}
