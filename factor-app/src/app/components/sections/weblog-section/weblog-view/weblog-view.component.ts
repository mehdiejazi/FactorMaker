import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { SettingsService } from '../../../../services/settings.service';
import { BlogPost } from '../../../../models/blog-post/blog-post';
import { BlogPostService } from '../../../../services/blog-post.service';


@Component({
  selector: 'app-weblog-view',
  templateUrl: './weblog-view.component.html',
  styleUrls: ['./weblog-view.component.css']
})
export class WeblogViewComponent implements OnInit {

  // public customOptions: OwlOptions;

  public error: Error;

  public errorMessages: string[];
  public warningMessages: string[];
  public informationMessages: string[];

  public blogPost: BlogPost;
  public isLoading: boolean = true;

  public constructor(private _bsModalService: BsModalService,
    private _blogPostService: BlogPostService,
    private _settingsService: SettingsService,
    private _route: ActivatedRoute) { }

  public ngOnInit(): void {

    this._route.queryParams.subscribe(params => {

      if (params['id']) {
        this._blogPostService.getByIdAsync(params['id']).subscribe(
          result => {
            this.blogPost = result.data;
            this.normalizePostAssets(this.blogPost);
            this.isLoading = false;
          },
          error => {

            this.error = error;
            console.error(error);
            this.isLoading = false;

          }
        );
      }
      else {

        this.blogPost = new BlogPost();
        this.blogPost.postCategories = [];
        this.isLoading = false;
      }

    });

    // this.initOwlCarousel();
  }

  public get displayDate(): string {
    const date = this.getValidDate(this.blogPost?.publishDateTime) || this.getValidDate(this.blogPost?.insertDateTime);

    if (!date) {
      return 'بدون تاریخ انتشار';
    }

    return date.toLocaleDateString('fa-IR', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  }

  public get readingTime(): string {
    const text = `${this.blogPost?.body || ''} ${this.blogPost?.extendedBody || ''}`.trim();
    const wordCount = text ? text.split(/\s+/).length : 0;
    const minutes = Math.max(1, Math.ceil(wordCount / 180));

    return `${minutes.toLocaleString('fa-IR')} دقیقه مطالعه`;
  }

  public get hasBody(): boolean {
    return !!this.blogPost?.body?.trim();
  }

  public get hasExtendedBody(): boolean {
    return !!this.blogPost?.extendedBody?.trim();
  }

  public get hasAuthorAvatar(): boolean {
    return !!this.blogPost?.owner?.avatar?.url;
  }

  private getValidDate(rawDate: string): Date {
    if (!rawDate || rawDate.startsWith('0001-01-01')) {
      return undefined;
    }

    const parsed = new Date(rawDate);
    return Number.isNaN(parsed.getTime()) ? undefined : parsed;
  }

  private normalizePostAssets(post: BlogPost): void {
    if (!post) {
      return;
    }

    if (post.coverImage?.url) {
      post.coverImage.url = this.normalizeUrl(post.coverImage.url);
    }

    if (post.owner?.avatar?.url) {
      post.owner.avatar.url = this.normalizeUrl(post.owner.avatar.url);
    }

    if (post.images?.length) {
      post.images = post.images.map(image => ({
        ...image,
        url: image?.url ? this.normalizeUrl(image.url) : image?.url
      }));
    }
    else {
      post.images = [];
    }
  }

  private normalizeUrl(url: string): string {
    if (!url) {
      return url;
    }

    if (url.startsWith('http://') || url.startsWith('https://')) {
      return url;
    }

    return `${this._settingsService.baseUrl}${url}`;
  }
}
