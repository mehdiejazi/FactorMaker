import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, finalize, forkJoin, Observable, of, timeout } from 'rxjs';
import { BlogPost } from '../../interfaces/blog-post/blog-post';
import { Category } from '../../interfaces/category/category';
import { Product } from '../../interfaces/product/product';
import { Store, StoreSocialLink } from '../../interfaces/store/store';
import { BlogPostService } from '../../services/blog-post.service';
import { CategoryService } from '../../services/category.service';
import { ProductService } from '../../services/product.service';
import { SettingsService } from '../../services/settings.service';
import { StoreService } from '../../services/store.service';
import { HttpRequestResultT } from '../../models/http-request-result-t';

interface ProductGroup {
  id: string;
  name: string;
  products: Product[];
  visibleProducts: Product[];
  visibleCount: number;
}

const storeRequestTimeoutMs = 15000;

@Component({
  selector: 'app-store-public',
  templateUrl: './store-public.component.html',
  styleUrl: './store-public.component.css'
})
export class StorePublicComponent implements OnInit {
  public store: Store | null = null;
  public storeExternalUrl: string | null = null;
  public slides: BlogPost[] = [];
  public blogPosts: BlogPost[] = [];
  public categories: Category[] = [];
  public products: Product[] = [];
  public productGroups: ProductGroup[] = [];
  public errorMessages: string[] = [];
  public isLoading = true;
  public readonly productGroupPageSize = 4;

  public constructor(
    private _route: ActivatedRoute,
    private _storeService: StoreService,
    private _blogPostService: BlogPostService,
    private _productService: ProductService,
    private _categoryService: CategoryService,
    private _settingsService: SettingsService
  ) { }

  public ngOnInit(): void {
    this._route.paramMap.subscribe(params => {
      const storeEnglishName = (params.get('storeEnglishName') || '').trim();
      this.loadPage(storeEnglishName);
    });
  }

  public hasMoreGroupProducts(group: ProductGroup): boolean {
    return group.visibleCount < group.products.length;
  }

  public showMoreGroupProducts(group: ProductGroup): void {
    group.visibleCount = Math.min(group.visibleCount + this.productGroupPageSize, group.products.length);
    group.visibleProducts = group.products.slice(0, group.visibleCount);
  }

  public getPostViewLink(_post: BlogPost): string[] {
    return ['/post/view'];
  }

  public getPostViewQuery(post: BlogPost): { id: string } {
    return { id: post.id };
  }

  public get paletteClass(): string {
    const palette = (this.store?.themePalette || 'forest').toLowerCase();
    return `palette-${palette}`;
  }

  public get primarySocialLink(): StoreSocialLink | null {
    const links = this.store?.socialLinks || [];
    return links.find(link => link.isPrimary && !!link.url) || links.find(link => !!link.url) || null;
  }

  public get primaryActionHref(): string | null {
    if (this.primarySocialLink?.url) {
      return this.getValidExternalUrl(this.primarySocialLink.url);
    }

    return this.storeExternalUrl;
  }

  public get primaryActionLabel(): string {
    if (!this.primarySocialLink) {
      return 'ورود به لینک فروشگاه';
    }

    return this.primarySocialLink.label?.trim() || this.platformLabel(this.primarySocialLink.platform);
  }

  public platformLabel(platform: string): string {
    switch ((platform || '').toLowerCase()) {
      case 'instagram':
        return 'اینستاگرام';
      case 'telegram':
        return 'تلگرام';
      case 'whatsapp':
        return 'واتساپ';
      case 'website':
        return 'وب‌سایت';
      default:
        return 'لینک';
    }
  }

  public getSocialHref(url: string): string | null {
    return this.getValidExternalUrl(url);
  }

  public socialLabel(link: StoreSocialLink): string {
    return link.label?.trim() || this.platformLabel(link.platform);
  }

  public formatPublicDate(value: string): string {
    if (!value || value.startsWith('0001-01-01')) {
      return '';
    }

    const parsed = new Date(value);
    if (Number.isNaN(parsed.getTime())) {
      return '';
    }

    return parsed.toLocaleDateString('fa-IR', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  }

  private loadPage(storeEnglishName: string): void {
    this.isLoading = true;
    this.errorMessages = [];
    this.store = null;
    this.storeExternalUrl = null;
    this.slides = [];
    this.blogPosts = [];
    this.categories = [];
    this.products = [];
    this.productGroups = [];

    if (!storeEnglishName) {
      this.errorMessages = ['نام انگلیسی فروشگاه مشخص نشده است.'];
      this.isLoading = false;
      return;
    }

    this._storeService.getByStoreEnglishNameAsync(storeEnglishName).pipe(
      timeout(storeRequestTimeoutMs),
      catchError(() => {
        this.errorMessages = ['خطا در دریافت اطلاعات فروشگاه.'];
        this.isLoading = false;
        return of(null);
      })
    ).subscribe({
      next: (storeResult: HttpRequestResultT<Store>) => {
        if (!storeResult) {
          return;
        }

        if (!storeResult.isSuccessful || !storeResult.data) {
          this.errorMessages = storeResult.errorMessages?.length
            ? storeResult.errorMessages
            : ['فروشگاه موردنظر پیدا نشد.'];
          this.isLoading = false;
          return;
        }

        this.store = storeResult.data;
        this.normalizeStore();

        if (!this.store.id) {
          this.errorMessages = ['شناسه فروشگاه برای واکشی محتوا در دسترس نیست.'];
          this.isLoading = false;
          return;
        }

        const ownerId = this.store.ownerId;
        const storeId = this.store.id;

        forkJoin({
          slidesResult: ownerId
            ? this.getSafeResult(this._blogPostService.getByOwnerPublishedHotAsync(ownerId), [])
            : this.getSafeResult(this._blogPostService.getPublishedHotAsync(), []),
          postsResult: ownerId
            ? this.getSafeResult(this._blogPostService.getByOwnerPublishedAsync(ownerId), [])
            : this.getSafeResult(this._blogPostService.getPublishedAsync(), []),
          categoriesResult: this.getSafeResult(this._categoryService.getByStoreIdAsync(storeId), []),
          productsResult: this.getSafeResult(this._productService.getByStoreIdAsync(storeId), [])
        }).pipe(
          finalize(() => {
            this.isLoading = false;
          })
        ).subscribe({
          next: ({ slidesResult, postsResult, categoriesResult, productsResult }) => {
            const slides = slidesResult?.isSuccessful ? (slidesResult.data || []) : [];
            const posts = postsResult?.isSuccessful ? (postsResult.data || []) : [];

            this.slides = this.normalizePosts(slides);
            const slideIds = new Set(this.slides.map(post => post.id));
            this.blogPosts = this.normalizePosts(posts).filter(post => !slideIds.has(post.id));

            this.categories = categoriesResult?.isSuccessful ? (categoriesResult.data || []) : [];
            this.products = productsResult?.isSuccessful
              ? this.normalizeProducts(productsResult.data || []).filter(product => product.showOnLanding !== false)
              : [];
            this.buildProductGroups();

            this.errorMessages = [
              ...(slidesResult?.errorMessages || []),
              ...(postsResult?.errorMessages || []),
              ...(categoriesResult?.errorMessages || []),
              ...(productsResult?.errorMessages || [])
            ];

          },
          error: () => {
            this.errorMessages = ['خطا در دریافت محتوای فروشگاه.'];
          }
        });
      },
      error: () => {
        this.errorMessages = ['خطا در دریافت اطلاعات فروشگاه.'];
        this.isLoading = false;
      }
    });
  }

  private getSafeResult<T>(
    request: Observable<HttpRequestResultT<T>>,
    fallbackData: T
  ): Observable<HttpRequestResultT<T>> {
    return request.pipe(
      timeout(storeRequestTimeoutMs),
      catchError(() => of({
        data: fallbackData,
        isSuccessful: true,
        errorMessages: [],
        warningMessages: [],
        informationMessages: []
      } as HttpRequestResultT<T>))
    );
  }

  private buildProductGroups(): void {
    const productsByCategory = new Map<string, Product[]>();

    this.products.forEach(product => {
      const groupId = product.categoryId || product.category?.id || 'uncategorized';
      const groupProducts = productsByCategory.get(groupId) || [];
      groupProducts.push(product);
      productsByCategory.set(groupId, groupProducts);
    });

    const groups: ProductGroup[] = [];

    this.categories.forEach(category => {
      const groupProducts = productsByCategory.get(category.id) || [];
      if (!groupProducts.length) {
        return;
      }

      groups.push({
        id: category.id,
        name: category.name,
        products: groupProducts,
        visibleProducts: groupProducts.slice(0, this.productGroupPageSize),
        visibleCount: this.productGroupPageSize
      });

      productsByCategory.delete(category.id);
    });

    productsByCategory.forEach((groupProducts, groupId) => {
      const firstProduct = groupProducts[0];
      groups.push({
        id: groupId,
        name: firstProduct?.category?.name || 'بدون دسته',
        products: groupProducts,
        visibleProducts: groupProducts.slice(0, this.productGroupPageSize),
        visibleCount: this.productGroupPageSize
      });
    });

    this.productGroups = groups;
  }

  private normalizeStore(): void {
    if (!this.store) {
      return;
    }

    if (this.store.logo?.url) {
      this.store.logo.url = this.getAbsoluteUrl(this.store.logo.url);
    }

    if (this.store.coverImage?.url) {
      this.store.coverImage.url = this.getAbsoluteUrl(this.store.coverImage.url);
    }

    this.storeExternalUrl = this.getValidExternalUrl(this.store.url);
  }

  private normalizeProducts(products: Product[]): Product[] {
    return [...products]
      .map(product => {
        if (product.image?.url) {
          product.image.url = this.getAbsoluteUrl(product.image.url);
        }
        return product;
      })
      .sort((a, b) => (a.sortOrder || 0) - (b.sortOrder || 0));
  }

  private normalizePosts(posts: BlogPost[]): BlogPost[] {
    return [...posts]
      .map(post => {
        if (post.coverImage?.url) {
          post.coverImage.url = this.getAbsoluteUrl(post.coverImage.url);
        }
        return post;
      })
      .sort((a, b) => this.toTime(b.publishDateTime || b.insertDateTime) - this.toTime(a.publishDateTime || a.insertDateTime));
  }

  private getAbsoluteUrl(url: string): string {
    if (!url) {
      return '';
    }

    if (url.startsWith('http://') || url.startsWith('https://')) {
      return url;
    }

    if (url.startsWith('/')) {
      return `${this._settingsService.baseUrl}${url}`;
    }

    return `${this._settingsService.baseUrl}/${url}`;
  }

  private toTime(value: string): number {
    const parsed = Date.parse(value || '');
    return Number.isNaN(parsed) ? 0 : parsed;
  }

  private getValidExternalUrl(url: string): string | null {
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
}
