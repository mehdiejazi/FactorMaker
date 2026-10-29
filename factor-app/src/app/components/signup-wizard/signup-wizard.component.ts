import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LanguageService } from '../../i18n/language.service';
import { LoginRequest } from '../../models/auth/login-request';
import { UserRegister } from '../../models/user/user-register';
import { AuthService } from '../../services/auth.service';
import { SettingsService } from '../../services/settings.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-signup-wizard',
  templateUrl: './signup-wizard.component.html',
  styleUrls: ['./signup-wizard.component.css']
})
export class SignupWizardComponent {
  public currentStep: number = 1;
  public isLoading: boolean = false;
  public isSuccess: boolean = false;
  public passwordConfirmation: string = '';
  public registerRequest: UserRegister = new UserRegister();
  public errorMessages: string[] = [];
  public informationMessages: string[] = [];
  public warningMessages: string[] = [];

  public constructor(
    private _userService: UserService,
    private _authService: AuthService,
    private _settingsService: SettingsService,
    private _languageService: LanguageService,
    private _router: Router) {
  }

  public get selectedProvider(): string {
    return this.registerRequest.registrationProvider;
  }

  public selectProvider(provider: string): void {
    this.registerRequest.registrationProvider = provider;
    this.currentStep = 2;
    this.errorMessages = [];
    this.informationMessages = [];
    this.warningMessages = [];
  }

  public get providerTitle(): string {
    if (this.selectedProvider === 'google') return 'ثبت‌نام با Gmail';
    if (this.selectedProvider === 'instagram') return 'ثبت‌نام با اینستاگرام';
    return 'ثبت‌نام با ایمیل';
  }

  public get providerDescription(): string {
    if (this.selectedProvider === 'google') return 'ایمیل کاری Gmail و مشخصات مدیر فروشگاه را وارد کنید تا حساب شما سریع‌تر آماده شود.';
    if (this.selectedProvider === 'instagram') return 'آیدی یا لینک اینستاگرام را وارد کنید تا اطلاعات برند شما در ویزارد تکمیل شود.';
    return 'اطلاعات پایه حساب را وارد کنید تا ویزارد ثبت‌نام شروع شود.';
  }

  public goToStep(step: number): void {
    if (step < this.currentStep) {
      this.currentStep = step;
    }
  }

  public nextStep(): void {
    this.errorMessages = [];

    if (this.currentStep === 2 && this.validateAccountStep() === false) {
      return;
    }

    if (this.currentStep === 3 && this.validateStoreStep() === false) {
      return;
    }

    this.currentStep += 1;
  }

  public previousStep(): void {
    if (this.currentStep > 1) {
      this.currentStep -= 1;
      this.errorMessages = [];
    }
  }

  public createSlugFromName(): void {
    if (!this.registerRequest.storeEnglishName && this.registerRequest.storeName) {
      this.registerRequest.storeEnglishName = this.registerRequest.storeName
        .trim()
        .toLowerCase()
        .replace(/[^a-zA-Z0-9\s-]/g, '')
        .replace(/\s+/g, '-')
        .replace(/-+/g, '-');
    }
  }

  public submit(): void {
    this.errorMessages = [];
    this.informationMessages = [];
    this.warningMessages = [];

    if (this.validateAccountStep() === false || this.validateStoreStep() === false) {
      return;
    }

    this.isLoading = true;

    this._userService.registerAsync(this.registerRequest).subscribe(result => {
      if (result.isSuccessful === false) {
        this.isLoading = false;
        this.errorMessages = result.errorMessages || [];
        this.warningMessages = result.warningMessages || [];
        this.informationMessages = result.informationMessages || [];
        return;
      }

      const loginRequest: LoginRequest = new LoginRequest();
      loginRequest.userName = this.registerRequest.userName;
      loginRequest.password = this.registerRequest.password;

      this._authService.loginAsync(loginRequest).subscribe(loginResult => {
        this.isLoading = false;

        if (loginResult.isSuccessful === false) {
          this.isSuccess = false;
          this.currentStep = 4;
          this.informationMessages = ['حساب شما ایجاد شد. برای ورود از صفحه لاگین استفاده کنید.'];
          this.errorMessages = loginResult.errorMessages || [];
          return;
        }

        this._settingsService.setUser(loginResult.data.user);
        this._settingsService.setToken(loginResult.data.token);
        this._settingsService.setRefreshToken(loginResult.data.refreshToken);
        this._languageService.syncLanguageWithCurrentUser();
        this.isSuccess = true;
        this.currentStep = 4;
        this.informationMessages = ['حساب و فروشگاه شما با موفقیت ایجاد شد.'];
      }, error => {
        this.isLoading = false;
        this.isSuccess = false;
        this.currentStep = 4;
        this.errorMessages = ['حساب ساخته شد اما ورود خودکار انجام نشد.'];
        console.error(error);
      });
    }, error => {
      this.isLoading = false;
      this.errorMessages = ['خطا در ارتباط با سرور'];
      console.error(error);
    });
  }

  public goToDashboard(): void {
    this._router.navigate(['/dashboard']);
  }

  public goToLogin(): void {
    this._router.navigate(['/login']);
  }

  private validateAccountStep(): boolean {
    if (!this.registerRequest.firstName || !this.registerRequest.lastName || !this.registerRequest.userName || !this.registerRequest.email || !this.registerRequest.password) {
      this.errorMessages = ['همه فیلدهای اطلاعات حساب را تکمیل کنید.'];
      this.currentStep = 2;
      return false;
    }

    if (this.registerRequest.password.length < 6) {
      this.errorMessages = ['رمز عبور باید حداقل 6 کاراکتر باشد.'];
      this.currentStep = 2;
      return false;
    }

    if (this.registerRequest.password !== this.passwordConfirmation) {
      this.errorMessages = ['تکرار رمز عبور با رمز عبور یکسان نیست.'];
      this.currentStep = 2;
      return false;
    }

    if (this.selectedProvider === 'instagram' && !this.registerRequest.instagramPage) {
      this.errorMessages = ['برای مسیر اینستاگرام، آیدی یا لینک پیج را وارد کنید.'];
      this.currentStep = 2;
      return false;
    }

    return true;
  }

  private validateStoreStep(): boolean {
    if (!this.registerRequest.storeName || !this.registerRequest.storeEnglishName) {
      this.errorMessages = ['نام فروشگاه و نام انگلیسی فروشگاه الزامی است.'];
      this.currentStep = 3;
      return false;
    }

    return true;
  }
}
