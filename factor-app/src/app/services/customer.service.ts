import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingsService } from './settings.service';
import { LogManagerService } from './log-manager.service';
import { Customer } from '../interfaces/customer/customer';
import { Observable, map } from 'rxjs';
import { HttpRequestResultT } from '../models/http-request-result-t';
import { HttpRequestResult } from '../models/http-request-result';

export interface CustomerSaleTotalQuantity {
  customer: Customer;
  totalQuantity: number;
}

export interface CustomerSaleTotalPrice {
  customer: Customer;
  totalPrice: number;
}

export interface CustomerGenderStat {
  gender?: number | null;
  count: number;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  public constructor(
    private _httpClient: HttpClient,
    private _settingsService: SettingsService,
    private _logManagerService: LogManagerService) {

    this._baseUrl = `${_settingsService.baseUrl}/Customer`;

  }

  private _baseUrl: string;

  public insertAsync(customer:Customer): Observable<HttpRequestResultT<Customer>> {

    let url = `${this._baseUrl}/InsertAsync`;


    let response = this._httpClient.post<HttpRequestResultT<Customer>>(url, customer,
      this._settingsService.httpOptionsForm).pipe(map(result => {


        for (let i = 0; i < result.errorMessages.length; i++) {

          this._logManagerService.logError(result.errorMessages[i])

        }

        if (result.warningMessages !== undefined)
          for (let i = 0; i < result.warningMessages.length; i++) {

            this._logManagerService.logWarning(result.warningMessages[i])

          }

        if (result.informationMessages !== undefined)
          for (let i = 0; i < result.informationMessages.length; i++) {

            this._logManagerService.logInformation(result.informationMessages[i])

          }

        return result;

      })
      );

    return response;


  }

  public updateAsync(customer:Customer): Observable<HttpRequestResultT<Customer>> {

    let url = `${this._baseUrl}/UpdateAsync`;

    let response = this._httpClient.post<HttpRequestResultT<Customer>>
      (url, customer, this._settingsService.httpOptions).pipe(map(result => {

        if (result.errorMessages !== undefined)
          for (let i = 0; i < result.errorMessages.length; i++) {

            this._logManagerService.logError(result.errorMessages[i])

          }

        if (result.warningMessages !== undefined)
          for (let i = 0; i < result.warningMessages.length; i++) {

            this._logManagerService.logWarning(result.warningMessages[i])

          }

        if (result.informationMessages !== undefined)
          for (let i = 0; i < result.informationMessages.length; i++) {

            this._logManagerService.logInformation(result.informationMessages[i])

          }

        return result;

      })
      );

    return response;
  }

  public deleteByIdAsync(customer:Customer): Observable<HttpRequestResult> {

    let url = `${this._baseUrl}/DeleteByIdAsync?id=${customer.id}`;

    let response = this._httpClient.get<HttpRequestResult>(url).pipe(map(result => {
      if (result.errorMessages !== undefined)
        for (let i = 0; i < result.errorMessages.length; i++) {

          this._logManagerService.logError(result.errorMessages[i])

        }

      if (result.warningMessages !== undefined)
        for (let i = 0; i < result.warningMessages.length; i++) {

          this._logManagerService.logWarning(result.warningMessages[i])

        }

      if (result.informationMessages !== undefined)
        for (let i = 0; i < result.informationMessages.length; i++) {

          this._logManagerService.logInformation(result.informationMessages[i])

        }

      return result;

    }));

    return response;
  }

  public getByIdAsync(id:string): Observable<HttpRequestResultT<Customer>> {
    let url = `${this._baseUrl}/GetByIdAsync?id=${id}`;

    let response = this._httpClient.get<HttpRequestResultT<Customer>>(url).pipe(map(result => {
      if (result.errorMessages !== undefined)
        for (let i = 0; i < result.errorMessages.length; i++) {

          this._logManagerService.logError(result.errorMessages[i])

        }

      if (result.warningMessages !== undefined)
        for (let i = 0; i < result.warningMessages.length; i++) {

          this._logManagerService.logWarning(result.warningMessages[i])

        }

      if (result.informationMessages !== undefined)
        for (let i = 0; i < result.informationMessages.length; i++) {

          this._logManagerService.logInformation(result.informationMessages[i])

        }

      return result;

    }));

    return response;
  }

  public getByStoreIdAsync(storeId:string): Observable<HttpRequestResultT<Customer[]>> {

    let url = `${this._baseUrl}/GetByStoreIdAsync?storeId=${storeId}`;

    let response = this._httpClient.get<HttpRequestResultT<Customer[]>>(url).pipe(map(result => {
      if (result.errorMessages !== undefined)
        for (let i = 0; i < result.errorMessages.length; i++) {

          this._logManagerService.logError(result.errorMessages[i])

        }

      if (result.warningMessages !== undefined)
        for (let i = 0; i < result.warningMessages.length; i++) {

          this._logManagerService.logWarning(result.warningMessages[i])

        }

      if (result.informationMessages !== undefined)
        for (let i = 0; i < result.informationMessages.length; i++) {

          this._logManagerService.logInformation(result.informationMessages[i])

        }

      return result;
    })
    );

    return response;
  }

  public getTop10ByQuantityAsync(storeId: string): Observable<HttpRequestResultT<CustomerSaleTotalQuantity[]>> {
    return this._httpClient.post<HttpRequestResultT<CustomerSaleTotalQuantity[]>>(
      `${this._baseUrl}/GetTop10ByQuantityAsync?storeId=${storeId}`,
      null,
      this._settingsService.httpOptions
    ).pipe(map(result => this.handleResult(result)));
  }

  public getTop10ByPriceAsync(storeId: string): Observable<HttpRequestResultT<CustomerSaleTotalPrice[]>> {
    return this._httpClient.post<HttpRequestResultT<CustomerSaleTotalPrice[]>>(
      `${this._baseUrl}/GetTop10ByPriceAsync?storeId=${storeId}`,
      null,
      this._settingsService.httpOptions
    ).pipe(map(result => this.handleResult(result)));
  }

  public getGenderStatsByStoreIdAsync(storeId: string): Observable<HttpRequestResultT<CustomerGenderStat[]>> {
    return this._httpClient.get<HttpRequestResultT<CustomerGenderStat[]>>(
      `${this._baseUrl}/GetGenderStatsByStoreIdAsync?storeId=${storeId}`
    ).pipe(map(result => this.handleResult(result)));
  }

  private handleResult<T extends HttpRequestResult | HttpRequestResultT<any>>(result: T): T {
    if (result.errorMessages !== undefined) {
      for (const error of result.errorMessages) {
        this._logManagerService.logError(error);
      }
    }

    if (result.warningMessages !== undefined) {
      for (const warning of result.warningMessages) {
        this._logManagerService.logWarning(warning);
      }
    }

    if (result.informationMessages !== undefined) {
      for (const info of result.informationMessages) {
        this._logManagerService.logInformation(info);
      }
    }

    return result;
  }


}
