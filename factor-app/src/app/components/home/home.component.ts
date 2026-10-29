import { Component, OnInit } from '@angular/core';
import { Store } from '../../interfaces/store/store';
import { HttpRequestResultT } from '../../models/http-request-result-t';
import { StoreService } from '../../services/store.service';

interface LandingStat {
  value: string;
  label: string;
}

interface LandingFeature {
  icon: string;
  title: string;
  description: string;
}

interface LandingStep {
  index: string;
  title: string;
  description: string;
}

interface LandingLink {
  label: string;
  href: string;
}

interface LandingStory {
  icon: string;
  title: string;
  lead: string;
  body: string;
}

interface LandingTestimonial {
  name: string;
  role: string;
  text: string;
  initial: string;
  tone: string;
}

interface LandingGalleryItem {
  title: string;
  description: string;
  icon: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  public activeStores: Store[] = [];
  public activeStoreCount = '۰';
  public isLoadingStores = true;
  public selectedStoryIndex = 0;

  readonly stats: LandingStat[] = [
    { value: '۹+', label: 'ماژول عملیاتی برای مدیریت فروشگاه' },
    { value: '۳', label: 'مسیر اصلی: فروشگاه، فاکتور، خروجی' },
    { value: '۲', label: 'خروجی آماده برای چاپ و PDF' }
  ];

  readonly features: LandingFeature[] = [
    {
      icon: 'storefront',
      title: 'مدیریت فروشگاه',
      description: 'تعریف فروشگاه، لوگو، لینک، توضیحات، شبکه‌های اجتماعی و مسیر عمومی برای هر برند یا شعبه.'
    },
    {
      icon: 'inventory_2',
      title: 'محصول و دسته‌بندی',
      description: 'محصولات، قیمت، تصویر، دسته‌بندی و ترتیب نمایش را برای فروش و ویترین عمومی مدیریت کنید.'
    },
    {
      icon: 'groups',
      title: 'مشتریان',
      description: 'اطلاعات مشتری را یک‌بار ثبت کنید و در تمام فاکتورها و گزارش‌ها دوباره از آن استفاده کنید.'
    },
    {
      icon: 'receipt_long',
      title: 'صدور فاکتور',
      description: 'فاکتور جدید ایجاد کنید، آیتم‌ها را اضافه کنید، تخفیف دهید و نسخه نهایی را ببندید.'
    },
    {
      icon: 'query_stats',
      title: 'داشبورد فروش',
      description: 'درآمد، تعداد فاکتور، محصولات پرفروش، مشتریان برتر و روند فروش را یکجا ببینید.'
    },
    {
      icon: 'public',
      title: 'صفحه عمومی فروشگاه',
      description: 'برای هر فروشگاه یک ویترین عمومی قابل اشتراک بسازید و محصولات و محتوای فعال را نمایش دهید.'
    }
  ];

  readonly workflow: LandingStep[] = [
    {
      index: '۱',
      title: 'فروشگاه را بسازید',
      description: 'نام، لوگو، توضیحات، لینک عمومی و اطلاعات پایه فروشگاه را ثبت کنید.'
    },
    {
      index: '۲',
      title: 'کالا و مشتری را اضافه کنید',
      description: 'محصولات، دسته‌بندی‌ها و مشتری‌ها را مرتب نگه دارید تا صدور فاکتور سریع شود.'
    },
    {
      index: '۳',
      title: 'فاکتور صادر کنید',
      description: 'آیتم‌ها، تعداد، تخفیف و مبلغ نهایی را کنترل کنید و فاکتور را ببندید.'
    },
    {
      index: '۴',
      title: 'چاپ یا PDF بگیرید',
      description: 'نسخه نهایی فاکتور را مشاهده کنید و همان‌جا چاپ یا دانلود PDF انجام دهید.'
    }
  ];

  readonly quickLinks: LandingLink[] = [
    { label: 'ورود به پنل', href: '/login' },
    { label: 'ثبت‌نام', href: '/signup' },
    { label: 'پیش‌نمایش فاکتور', href: '/invoice' },
    { label: 'فروشگاه‌های فعال', href: '/stores/active' }
  ];

  readonly stories: LandingStory[] = [
    {
      icon: 'storefront',
      title: 'شروع کار با فروشگاه',
      lead: 'فروشگاه، لوگو و اطلاعات پایه را ثبت کنید',
      body: 'هر فروشگاه می‌تواند نام انگلیسی، توضیح، لوگو، تصویر کاور و لینک اختصاصی خودش را داشته باشد و از مسیر عمومی جداگانه نمایش داده شود.'
    },
    {
      icon: 'receipt_long',
      title: 'گردش کامل ثبت سفارش',
      lead: 'از مشتری و محصول تا ردیف‌های فاکتور',
      body: 'بعد از تعریف داده‌های پایه، فرایند ساخت فاکتور به شکل مرحله‌ای پوشش داده می‌شود: انتخاب مشتری، افزودن آیتم، محاسبه مبلغ و خروجی نهایی.'
    },
    {
      icon: 'public',
      title: 'نمایش عمومی فروشگاه‌ها',
      lead: 'فروشگاه‌های فعال در یک صفحه عمومی نمایش داده می‌شوند',
      body: 'صفحه خانه، صفحه فروشگاه‌های فعال و صفحه اختصاصی هر فروشگاه از داده واقعی API استفاده می‌کنند و کاربر را به مسیر مناسب هدایت می‌کنند.'
    }
  ];

  readonly testimonials: LandingTestimonial[] = [
    {
      name: 'مدیر فروشگاه پوشاک',
      role: 'کاربر پنل فروش',
      text: 'بعد از تعریف محصولات و مشتری‌ها، صدور فاکتور خیلی سریع‌تر شد و خروجی چاپی مرتب‌تری داریم.',
      initial: 'پ',
      tone: '#2563eb'
    },
    {
      name: 'فروشگاه ابزار',
      role: 'مدیریت چند دسته محصول',
      text: 'دسته‌بندی کالا و گزارش‌های داشبورد کمک می‌کند بفهمیم کدام محصول بیشتر فروش دارد.',
      initial: 'ا',
      tone: '#14b8a6'
    },
    {
      name: 'کسب‌وکار خانگی',
      role: 'صفحه عمومی فروشگاه',
      text: 'لینک عمومی فروشگاه برای معرفی محصولات و محتوا کاربردی است و نیاز به سایت جدا را کم می‌کند.',
      initial: 'خ',
      tone: '#f59e0b'
    }
  ];

  readonly galleryItems: LandingGalleryItem[] = [
    {
      title: 'داشبورد فروش',
      description: 'نمای کلی درآمد، فاکتورها، محصولات پرفروش و مشتریان برتر.',
      icon: 'query_stats'
    },
    {
      title: 'صدور فاکتور',
      description: 'انتخاب مشتری، افزودن آیتم‌ها، محاسبه مبلغ و ثبت نسخه نهایی.',
      icon: 'receipt_long'
    },
    {
      title: 'مدیریت محصول',
      description: 'قیمت، تصویر، دسته‌بندی و نمایش در صفحه عمومی فروشگاه.',
      icon: 'inventory_2'
    },
    {
      title: 'ویترین عمومی',
      description: 'صفحه قابل اشتراک برای فروشگاه‌های فعال و محتوای منتشرشده.',
      icon: 'storefront'
    }
  ];

  public constructor(private _storeService: StoreService) { }

  public ngOnInit(): void {
    this.loadActiveStores();
  }

  public trackByStore(index: number, store: Store): string {
    return store.id || store.storeEnglishName || `${index}`;
  }

  public getStoreLink(store: Store): string {
    return `/s/${store.storeEnglishName}`;
  }

  public getStoreExternalUrl(url: string): string | null {
    const rawUrl = (url || '').trim();

    if (!rawUrl) {
      return null;
    }

    const normalizedUrl = /^https?:\/\//i.test(rawUrl) ? rawUrl : `https://${rawUrl}`;

    try {
      const parsedUrl = new URL(normalizedUrl);
      if (parsedUrl.protocol !== 'http:' && parsedUrl.protocol !== 'https:') {
        return null;
      }
      return parsedUrl.href;
    } catch {
      return null;
    }
  }

  public selectStory(index: number): void {
    this.selectedStoryIndex = index;
  }

  private loadActiveStores(): void {
    this.isLoadingStores = true;

    this._storeService.getActiveAsync().subscribe({
      next: (result: HttpRequestResultT<Store[]>) => {
        const stores = result?.isSuccessful ? (result.data || []) : [];
        this.activeStores = stores.slice(0, 6);
        this.activeStoreCount = this.toPersianDigits(`${stores.length}`);
        this.isLoadingStores = false;
      },
      error: () => {
        this.activeStores = [];
        this.activeStoreCount = '۰';
        this.isLoadingStores = false;
      }
    });
  }

  private toPersianDigits(value: string): string {
    const digits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    return value.replace(/\d/g, digit => digits[Number(digit)]);
  }
}
