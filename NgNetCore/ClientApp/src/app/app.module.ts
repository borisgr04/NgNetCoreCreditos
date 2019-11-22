import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AlertModalComponent } from './@base/modals/alert-modal/alert-modal.component';
import { CreditoRegisterComponent } from './creditos/credito-register/credito-register.component';
import { ClienteConsultaComponent } from './clientes/consulta/cliente-consulta.component';
import { ClienteConsultaModalComponent } from './clientes/modals/cliente-consulta-modal/cliente-consulta-modal.component';
import { FilterPipe } from './filter.pipe';
import { UploadComponent } from './upload/upload.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AlertModalComponent,
    CreditoRegisterComponent,
    ClienteConsultaComponent,
    ClienteConsultaModalComponent,
    FilterPipe,
    UploadComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
        { path: '', component: HomeComponent, pathMatch: 'full' },
        { path: 'counter', component: CounterComponent },
        { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
        { path: 'credito-register', component: CreditoRegisterComponent, canActivate: [AuthorizeGuard], data: { role: ['RegistrarCreditosX'] } },
        { path: 'clientes-consulta', component: ClienteConsultaComponent, canActivate: [AuthorizeGuard] },
        { path: 'upload-file', component: UploadComponent },
    ]),
      NgbModule,
      ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
    bootstrap: [AppComponent],
    entryComponents: [
        AlertModalComponent,
        ClienteConsultaComponent,
        ClienteConsultaModalComponent
    ]
})
export class AppModule { }
