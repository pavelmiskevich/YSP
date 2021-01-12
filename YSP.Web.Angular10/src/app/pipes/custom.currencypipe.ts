//https://www.angularjswiki.com/angular/angular-currency-pipe-formatting-currency-in-angular/#:~:text=Angular%20Currency%20Pipe%20%26%20Format%20Currency%20In%20Angular%20with%20examples,-Learn%20how%20to&text=Angular%20Currency%20Pipe%20is%20one,currency%2Cdecimal%2Clocale%20information.

import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';
registerLocaleData(localeRu, 'ru');

import { Pipe, PipeTransform } from '@angular/core';
import { formatCurrency, getCurrencySymbol } from '@angular/common';
@Pipe({
    name: 'mycurrency',
  })
  export class MycurrencyPipe implements PipeTransform {
    transform(
        value: number,
        currencyCode: string = 'RUB',
        display:
            | 'code'
            | 'symbol'
            | 'symbol-narrow'
            | string
            | boolean = 'symbol',
        digitsInfo: string = '3.2-2',
        locale: string = 'ru',
    ): string | null {
        return formatCurrency(
          value,
          locale,
          getCurrencySymbol(currencyCode, 'wide'),
          currencyCode,
          digitsInfo,
        );
    }
}