import { Component, OnInit } from '@angular/core';
import { CreditoService } from '../../services/credito.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ClienteConsultaModalComponent } from '../../clientes/modals/cliente-consulta-modal/cliente-consulta-modal.component';
import { AlertModalComponent } from '../../@base/modals/alert-modal/alert-modal.component';
import { ClienteService } from '../../services/cliente.service';
import { ClienteViewModel } from '../../clientes/models/cliente-view-model';
import { CreditoRegisterRequest } from '../models/credito-register-request';


@Component({
    selector: 'app-credito-register',
    templateUrl: './credito-register.component.html',
    styleUrls: ['./credito-register.component.css']
})
export class CreditoRegisterComponent implements OnInit {
    credito: CreditoRegisterRequest;
    registerForm: FormGroup;
    submitted = false;

    constructor(
        private creditoService: CreditoService,
        private clienteService: ClienteService,
        private formBuilder: FormBuilder,
        private modalService: NgbModal) { }

    ngOnInit() {
        this.credito = new CreditoRegisterRequest();
        let myDate = new Date();
        this.credito.fecha = myDate;
        this.credito.clienteId = "";
        this.credito.numeroCuotas = 0;
        this.credito.valorCredito = 0;
        this.registerForm = this.formBuilder.group({
            clienteId: [this.credito.clienteId, Validators.required],
            clienteNombre: [''],
            fecha: [this.credito.fecha, Validators.required],
            numeroCuotas: [this.credito.numeroCuotas, [Validators.required, , Validators.min(2), Validators.max(12), Validators.pattern("^[0-9]*$")]],
            valorCredito: [this.credito.valorCredito, [Validators.required, Validators.min(100000), Validators.pattern("^[0-9]*$")]],
            observacion: [this.credito.observacion],
            
        });
        
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }
  
    buscarCliente() {
        this.clienteService.getByIdentificacion(this.registerForm.value.clienteId).subscribe(cliente => {
            if (cliente != null) {
                this.f['clienteId'].setValue(cliente.identificacion);
                this.f['clienteNombre'].setValue(cliente.nombreCompleto);
            }
            else
            {
                this.openModalCliente();
            }
        });
    }

    //Manejo Modal
    openModalCliente()
    {
        this.modalService.open(ClienteConsultaModalComponent, { size: 'lg' }).result.then((cliente) => this.actualizar(cliente));
    }

    actualizar(cliente: ClienteViewModel) {
        
        this.registerForm.controls['clienteId'].setValue(cliente.identificacion);
        this.registerForm.controls['clienteNombre'].setValue(cliente.nombreCompleto);
    }
    //Fin Manejo Modal

    //Manejo Registrar
    onSubmit() {
        this.submitted = true;
        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }
        this.create();
    }

    create() {
        this.credito = this.registerForm.value;

        this.creditoService.post(this.credito).subscribe(c => {
            if (c != null) {
                const messageBox = this.modalService.open(AlertModalComponent)
                messageBox.componentInstance.title = "Resultado Operación";
                messageBox.componentInstance.message = 'SUCCESS!! :-)';
            }
        });
    }

    onReset() {
        this.submitted = false;
        this.registerForm.reset();
    }



}

