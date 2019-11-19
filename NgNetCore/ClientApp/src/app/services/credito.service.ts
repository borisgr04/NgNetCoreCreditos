import { Injectable, Inject } from '@angular/core';
import { HandleErrorService } from '../@base/services/handle-error.service';
import { HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CreditoRegisterViewModel } from '../creditos/credito-register/credito-register.component';


@Injectable({
  providedIn: 'root'
})
export class CreditoService {
    baseUrl: string;
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') baseUrl: string,
        private handleErrorService: HandleErrorService)
    {
        this.baseUrl = baseUrl;
    }

    post(credito: CreditoRegisterViewModel): Observable<CreditoRegisterViewModel> {
        return this.http.post<CreditoRegisterViewModel>(this.baseUrl + 'api/Credito', credito)
            .pipe(
                tap(_ => this.handleErrorService.log('datos enviados')),
                catchError(this.handleErrorService.handleError<CreditoRegisterViewModel>('REGISTRAR RUBRO', null))
            );
    }
}
