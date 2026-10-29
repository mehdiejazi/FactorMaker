import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'gender' })
export class GenderPipe implements PipeTransform {
    public transform(gender: number | null | undefined): string {
        if (gender === 0 || gender === 0 as any && gender !== null && gender !== undefined && Number(gender) === 0) {
            return 'مرد';
        }

        if (Number(gender) === 0) {
            return 'مرد';
        }

        if (Number(gender) === 1) {
            return 'زن';
        }

        return 'نامشخص';
    }
}