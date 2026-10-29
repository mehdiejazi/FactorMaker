import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { CategorySaleTotalPrice, CategorySaleTotalQuantity, CategoryService } from '../../../services/category.service';
import { CustomerGenderStat, CustomerSaleTotalPrice, CustomerSaleTotalQuantity, CustomerService } from '../../../services/customer.service';
import { FactorSaleHourly, FactorSaleMonthly, FactorSaleWeekDaily, FactorService } from '../../../services/factor.service';
import { ProductSaleTotalPrice, ProductSaleTotalQuantity, ProductService } from '../../../services/product.service';
import { SettingsService } from '../../../services/settings.service';
import { LanguageService } from '../../../i18n/language.service';
import { Factor } from '../../../interfaces/factor/factor';

interface DayRevenue {
  day: string;
  revenue: number;
  label: string;
}

interface MetricSummary {
  label: string;
  count: number;
  amount: number;
}

interface HourlyChartItem {
  label: string;
  revenue: number;
}

interface WeekdayChartItem {
  label: string;
  revenue: number;
}

interface PieChartItem {
  label: string;
  value: number;
  color: string;
}

@Component({
  selector: 'app-dashboard-section',
  templateUrl: './dashboard-section.component.html',
  styleUrl: './dashboard-section.component.css'
})
export class DashboardSectionComponent implements OnInit {
  public settings: SettingsService;
  public todayRevenue = 0;
  public todayFactorsCount = 0;
  public totalProductsCount = 0;
  public revenueData: DayRevenue[] = [];
  public hourlyRevenueData: HourlyChartItem[] = [];
  public weekdayRevenueData: WeekdayChartItem[] = [];
  public monthlyRevenueData: HourlyChartItem[] = [];
  public topProducts: ProductSaleTotalQuantity[] = [];
  public topProductsByPrice: ProductSaleTotalPrice[] = [];
  public categorySales: CategorySaleTotalQuantity[] = [];
  public categorySalesByPrice: CategorySaleTotalPrice[] = [];
  public productPieByQuantity: PieChartItem[] = [];
  public productPieByPrice: PieChartItem[] = [];
  public categoryPieByQuantity: PieChartItem[] = [];
  public categoryPieByPrice: PieChartItem[] = [];
  public topCustomersByQuantity: CustomerSaleTotalQuantity[] = [];
  public topCustomersByPrice: CustomerSaleTotalPrice[] = [];
  public customerGenderStats: PieChartItem[] = [];
  public recentFactors: Factor[] = [];
  public summaryCards: MetricSummary[] = [];
  public hasSelectedStore = false;
  public isLoading = true;

  private readonly pieColors = ['#4fc3f7', '#8bc34a', '#ffb74d', '#ce93d8', '#ef5350', '#26a69a', '#9575cd', '#ff8a65'];

  public constructor(
    _settings: SettingsService,
    private _factorService: FactorService,
    private _productService: ProductService,
    private _categoryService: CategoryService,
    private _customerService: CustomerService,
    private readonly languageService: LanguageService
  ) {
    this.settings = _settings;
  }

  public ngOnInit(): void {
    this.loadDashboardData();
  }

  private loadDashboardData(): void {
    this.isLoading = true;
    this.resetDashboardData();

    const storeId = this.settings.getStore()?.id ?? null;
    this.hasSelectedStore = !!storeId;

    if (!this.hasSelectedStore) {
      this.isLoading = false;
      return;
    }

    const now = new Date();
    const monthStart = new Date(now.getFullYear(), now.getMonth(), 1).toISOString();
    const dayStart = new Date(now.getFullYear(), now.getMonth(), now.getDate()).toISOString();
    const weekStart = new Date(now);
    weekStart.setDate(now.getDate() - 6);
    weekStart.setHours(0, 0, 0, 0);
    const persianYear = this.getPersianYear(now);

    forkJoin({
      factorsResult: this._factorService.getByStoreIdAsync(storeId),
      productsResult: this._productService.getByStoreIdAsync(storeId),
      topProductsResult: this._productService.getTop10SaleByQuantityAsync(storeId),
      topProductsByPriceResult: this._productService.getTop10SaleByPriceAsync(storeId),
      categoriesResult: this._categoryService.getSaleTotalQuantityAsync(storeId, monthStart, now.toISOString()),
      categoriesByPriceResult: this._categoryService.getSaleTotalByPriceAsync(storeId, monthStart, now.toISOString()),
      productPieQuantityResult: this._productService.getSaleTotalByQuantityAsync(storeId, monthStart, now.toISOString()),
      productPiePriceResult: this._productService.getSaleTotalByPriceAsync(storeId, monthStart, now.toISOString()),
      hourlyResult: this._factorService.getHourlyFactorSaleAsync(storeId, dayStart, now.toISOString()),
      weekdayResult: this._factorService.getWeekDailyFactorSaleAsync(storeId, weekStart.toISOString(), now.toISOString()),
      monthlyResult: this._factorService.getMonthlyFactorSaleAsync(storeId, persianYear),
      topCustomersQuantityResult: this._customerService.getTop10ByQuantityAsync(storeId),
      topCustomersPriceResult: this._customerService.getTop10ByPriceAsync(storeId),
      genderStatsResult: this._customerService.getGenderStatsByStoreIdAsync(storeId)
    }).subscribe({
      next: ({
        factorsResult,
        productsResult,
        topProductsResult,
        topProductsByPriceResult,
        categoriesResult,
        categoriesByPriceResult,
        productPieQuantityResult,
        productPiePriceResult,
        hourlyResult,
        weekdayResult,
        monthlyResult,
        topCustomersQuantityResult,
        topCustomersPriceResult,
        genderStatsResult
      }) => {
        const factors = factorsResult.isSuccessful && factorsResult.data ? factorsResult.data : [];
        const products = productsResult.isSuccessful && productsResult.data ? productsResult.data : [];

        this.totalProductsCount = products.length;
        this.topProducts = topProductsResult.isSuccessful && topProductsResult.data ? topProductsResult.data.slice(0, 5) : [];
        this.topProductsByPrice = topProductsByPriceResult.isSuccessful && topProductsByPriceResult.data ? topProductsByPriceResult.data.slice(0, 5) : [];
        this.categorySales = categoriesResult.isSuccessful && categoriesResult.data ? categoriesResult.data.slice(0, 5) : [];
        this.categorySalesByPrice = categoriesByPriceResult.isSuccessful && categoriesByPriceResult.data ? categoriesByPriceResult.data.slice(0, 5) : [];
        this.topCustomersByQuantity = topCustomersQuantityResult.isSuccessful && topCustomersQuantityResult.data ? topCustomersQuantityResult.data.slice(0, 5) : [];
        this.topCustomersByPrice = topCustomersPriceResult.isSuccessful && topCustomersPriceResult.data ? topCustomersPriceResult.data.slice(0, 5) : [];
        this.recentFactors = factors.slice(0, 5);

        this.productPieByQuantity = this.buildPieItems(
          productPieQuantityResult.data || [],
          item => item.product?.name || 'نامشخص',
          item => this.toNumber(item.totalQuantity)
        );
        this.productPieByPrice = this.buildPieItems(
          productPiePriceResult.data || [],
          item => item.product?.name || 'نامشخص',
          item => this.toNumber(item.totalPrice)
        );
        this.categoryPieByQuantity = this.buildPieItems(
          categoriesResult.data || [],
          item => item.category?.name || 'نامشخص',
          item => this.toNumber(item.totalQuantity)
        );
        this.categoryPieByPrice = this.buildPieItems(
          categoriesByPriceResult.data || [],
          item => item.category?.name || 'نامشخص',
          item => this.toNumber(item.totalPrice)
        );
        this.customerGenderStats = this.buildGenderPie(genderStatsResult.data || []);

        this.calculateTodayRevenue(factors);
        this.generateRevenueData(factors);
        this.buildSummaryCards(factors);
        this.hourlyRevenueData = this.buildHourlyRevenue(hourlyResult.data || []);
        this.weekdayRevenueData = this.buildWeekdayRevenue(weekdayResult.data || []);
        this.monthlyRevenueData = this.buildMonthlyRevenue(monthlyResult.data || []);
        this.isLoading = false;
      },
      error: error => {
        console.error('خطا در بارگذاری داشبورد', error);
        this.isLoading = false;
      }
    });
  }

  private resetDashboardData(): void {
    this.todayRevenue = 0;
    this.todayFactorsCount = 0;
    this.totalProductsCount = 0;
    this.revenueData = [];
    this.hourlyRevenueData = [];
    this.weekdayRevenueData = [];
    this.monthlyRevenueData = [];
    this.topProducts = [];
    this.topProductsByPrice = [];
    this.categorySales = [];
    this.categorySalesByPrice = [];
    this.productPieByQuantity = [];
    this.productPieByPrice = [];
    this.categoryPieByQuantity = [];
    this.categoryPieByPrice = [];
    this.topCustomersByQuantity = [];
    this.topCustomersByPrice = [];
    this.customerGenderStats = [];
    this.recentFactors = [];
    this.summaryCards = [];
  }

  private calculateTodayRevenue(factors: Factor[]): void {
    const today = new Date().toISOString().split('T')[0];
    this.todayRevenue = 0;
    this.todayFactorsCount = 0;

    factors.forEach(factor => {
      const factorDate = factor.sellDateTime ? factor.sellDateTime.split('T')[0] : null;
      if (factor.isClosed && factorDate === today) {
        this.todayRevenue += factor.totalPrice || 0;
        this.todayFactorsCount++;
      }
    });
  }

  private buildSummaryCards(factors: Factor[]): void {
    const now = new Date();
    const monthStart = new Date(now.getFullYear(), now.getMonth(), 1);
    const weekStart = new Date(now);
    weekStart.setDate(now.getDate() - 6);
    weekStart.setHours(0, 0, 0, 0);

    this.summaryCards = [
      this.createMetricSummary('این ماه', factors, factor => new Date(factor.sellDateTime) >= monthStart),
      this.createMetricSummary('۷ روز اخیر', factors, factor => new Date(factor.sellDateTime) >= weekStart),
      this.createMetricSummary('کل فروش بسته', factors, factor => factor.isClosed),
    ];
  }

  private createMetricSummary(label: string, factors: Factor[], predicate: (factor: Factor) => boolean): MetricSummary {
    const selected = factors.filter(factor => factor.isClosed && factor.sellDateTime && predicate(factor));
    return {
      label,
      count: selected.length,
      amount: selected.reduce((sum, factor) => sum + this.toNumber(factor.totalPrice), 0)
    };
  }

  private generateRevenueData(factors: Factor[]): void {
    const last7Days = this.getLast7Days();
    const revenueByDay: { [key: string]: number } = {};

    last7Days.forEach(day => {
      revenueByDay[day] = 0;
    });

    factors.filter(factor => factor.isClosed).forEach(factor => {
      const factorDate = factor.sellDateTime ? factor.sellDateTime.split('T')[0] : null;
      if (factorDate && Object.prototype.hasOwnProperty.call(revenueByDay, factorDate)) {
        revenueByDay[factorDate] += factor.totalPrice || 0;
      }
    });

    this.revenueData = last7Days.map(day => ({
      day,
      revenue: revenueByDay[day],
      label: this.formatDatePersian(day)
    }));
  }

  private buildHourlyRevenue(items: FactorSaleHourly[]): HourlyChartItem[] {
    const revenueByHour = new Map<number, number>();
    for (let hour = 0; hour < 24; hour++) {
      revenueByHour.set(hour, 0);
    }

    items.forEach(item => revenueByHour.set(item.hour, this.toNumber(item.totalPrice)));

    return Array.from(revenueByHour.entries())
      .filter(([hour]) => hour >= 6 && hour <= 23)
      .map(([hour, revenue]) => ({
        label: `${hour.toString().padStart(2, '0')}:00`,
        revenue,
      }));
  }

  private buildWeekdayRevenue(items: FactorSaleWeekDaily[]): WeekdayChartItem[] {
    const dayLabels = ['یکشنبه', 'دوشنبه', 'سه‌شنبه', 'چهارشنبه', 'پنجشنبه', 'جمعه', 'شنبه'];
    return dayLabels.map((label, index) => ({
      label,
      revenue: this.toNumber(items.find(item => item.dayOfWeek === index)?.totalPrice)
    }));
  }

  private buildMonthlyRevenue(items: FactorSaleMonthly[]): HourlyChartItem[] {
    return items.map(item => ({
      label: item.persianMonth,
      revenue: this.toNumber(item.totalPrice)
    }));
  }

  private buildPieItems<T>(items: T[], labelSelector: (item: T) => string, valueSelector: (item: T) => number): PieChartItem[] {
    return items
      .slice(0, 8)
      .map((item, index) => ({
        label: labelSelector(item),
        value: valueSelector(item),
        color: this.pieColors[index % this.pieColors.length]
      }))
      .filter(item => item.value > 0);
  }

  private buildGenderPie(stats: CustomerGenderStat[]): PieChartItem[] {
    return stats.map((stat, index) => ({
      label: this.getGenderLabel(stat.gender),
      value: stat.count,
      color: this.pieColors[index % this.pieColors.length]
    }));
  }

  private getGenderLabel(gender?: number | null): string {
    if (gender === 0) {
      return 'مرد';
    }
    if (gender === 1) {
      return 'زن';
    }
    return 'نامشخص';
  }

  private getPersianYear(date: Date): number {
    try {
      return Number(new Intl.DateTimeFormat('fa-IR-u-ca-persian', { year: 'numeric' }).format(date).replace(/[^\d]/g, ''));
    } catch {
      return date.getFullYear() - 621;
    }
  }

  private getLast7Days(): string[] {
    const days: string[] = [];
    const today = new Date();

    for (let i = 6; i >= 0; i--) {
      const date = new Date(today);
      date.setDate(date.getDate() - i);
      days.push(date.toISOString().split('T')[0]);
    }

    return days;
  }

  private formatDatePersian(dateStr: string): string {
    const parts = dateStr.split('-');
    return `${parts[2]}/${parts[1]}`;
  }

  public formatCurrency(value: number): string {
    const locale = this.languageService.currentLanguage === 'fa' ? 'fa-IR' : 'en-US';
    return new Intl.NumberFormat(locale, {
      style: 'currency',
      currency: 'IRR',
      minimumFractionDigits: 0
    }).format(value);
  }

  public getMaxValue(values: number[]): number {
    if (values.length === 0) {
      return 1;
    }

    return Math.max(...values, 1);
  }

  public getBarHeight(value: number, values: number[]): number {
    const max = this.getMaxValue(values);
    return max > 0 ? (value / max) * 100 : 0;
  }

  public revenueValues(items: { revenue: number }[]): number[] {
    return items.map(item => item.revenue);
  }

  public quantityValues(items: { totalQuantity: number }[]): number[] {
    return items.map(item => this.toNumber(item.totalQuantity));
  }

  public priceValues(items: { totalPrice: number }[]): number[] {
    return items.map(item => this.toNumber(item.totalPrice));
  }

  public pieTotal(items: PieChartItem[]): number {
    return items.reduce((sum, item) => sum + item.value, 0);
  }

  public pieSliceStyle(item: PieChartItem, items: PieChartItem[]): Record<string, string> {
    const total = this.pieTotal(items);
    const percent = total > 0 ? (item.value / total) * 100 : 0;
    return {
      width: `${percent}%`,
      backgroundColor: item.color
    };
  }

  private toNumber(value: unknown): number {
    const numberValue = Number(value);
    return Number.isFinite(numberValue) ? numberValue : 0;
  }
}
