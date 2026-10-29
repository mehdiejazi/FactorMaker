import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BlogPostService } from '../../../../services/blog-post.service';
import { SettingsService } from '../../../../services/settings.service';
import { BlogPost } from '../../../../interfaces/blog-post/blog-post';
import { ConfirmationModalComponent } from '../../../modals/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-slide-list',
  templateUrl: './slide-list.component.html',
  styleUrl: './slide-list.component.css'
})
export class SlideListComponent implements OnInit {
  public blogPosts: BlogPost[] = [];
  public error: Error;

  public constructor(
    private _blogPostService: BlogPostService,
    private _bsModalService: BsModalService,
    private _settingsService: SettingsService,
    private _router: Router) { }

  public ngOnInit(): void {
    this.fillTable();
  }

  public fillTable(): void {
    this._blogPostService.getAllAsync().subscribe(result => {
      const posts = result.data || [];
      this.blogPosts = posts
        .filter(post => post.isHot)
        .map(post => this.normalizePost(post));
    }, error => {
      this.error = error;
      console.error(error);
    });
  }

  public editSlide(blogPost: BlogPost): void {
    this._router.navigate(['/slides/form'], { queryParams: { id: blogPost.id } });
  }

  public toggleIsPublished(blogPost: BlogPost): void {
    const action = blogPost.isPublished ? 'عدم انتشار' : 'انتشار';
    const modal = this._bsModalService.show(ConfirmationModalComponent);
    (<ConfirmationModalComponent>modal.content).showConfirmationModal(action, `آیا مایل به ${action} این اسلاید هستید؟`);

    (<ConfirmationModalComponent>modal.content).onClose.subscribe(result => {
      if (result !== true) {
        return;
      }

      blogPost.isPublished = !blogPost.isPublished;
      this._blogPostService.updateAsync(blogPost).subscribe(() => this.fillTable(), error => {
        this.error = error;
        console.error(error);
      });
    });
  }

  public removeFromSlides(blogPost: BlogPost): void {
    const modal = this._bsModalService.show(ConfirmationModalComponent);
    (<ConfirmationModalComponent>modal.content).showConfirmationModal('حذف از اسلایدها', 'آیا این مطلب از اسلایدها حذف شود؟');

    (<ConfirmationModalComponent>modal.content).onClose.subscribe(result => {
      if (result !== true) {
        return;
      }

      blogPost.isHot = false;
      this._blogPostService.updateAsync(blogPost).subscribe(() => this.fillTable(), error => {
        this.error = error;
        console.error(error);
      });
    });
  }

  private normalizePost(post: BlogPost): BlogPost {
    if (post.coverImage?.url) {
      post.coverImage.url = this.getAbsoluteUrl(post.coverImage.url);
    }

    if (post.images) {
      post.images.forEach(image => {
        image.url = this.getAbsoluteUrl(image.url);
      });
    }

    return post;
  }

  private getAbsoluteUrl(url: string): string {
    if (!url || url.startsWith('http://') || url.startsWith('https://')) {
      return url;
    }

    return `${this._settingsService.baseUrl}${url.startsWith('/') ? '' : '/'}${url}`;
  }
}
