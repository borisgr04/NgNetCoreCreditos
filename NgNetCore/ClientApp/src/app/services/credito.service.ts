import { Injectable, Inject } from '@angular/core';
import { HandleErrorService } from '../@base/services/handle-error.service';
import { HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CreditoRegisterRequest } from '../creditos/models/credito-register-request';

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

    post(credito: CreditoRegisterRequest): Observable<CreditoRegisterRequest> {
        return this.http.post<CreditoRegisterRequest>(this.baseUrl + 'api/Credito', credito)
            .pipe(
                tap(_ => this.handleErrorService.log('datos enviados')),
                catchError(this.handleErrorService.handleError<CreditoRegisterRequest>('REGISTRAR RUBRO', null))
            );
    }
}
