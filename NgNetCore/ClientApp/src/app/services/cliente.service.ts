import { Injectable, Inject } from '@angular/core';
import { HandleErrorService } from '../@base/services/handle-error.service';
import { HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ClienteViewModel } from '../clientes/consulta/cliente-consulta.component';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

    baseUrl: string;
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') baseUrl: string,
        private handleErrorService: HandleErrorService) {
        this.baseUrl = baseUrl;
    }

    get(): Observable<ClienteViewModel[]> {
        return this.http.get<ClienteViewModel[]>(this.baseUrl + 'api/Cliente')
            .pipe(
                tap(_ => this.handleErrorService.log('datos enviados')),
                catchError(this.handleErrorService.handleError<ClienteViewModel[]>('Consulta Clientes', null))
            );
    }
}
