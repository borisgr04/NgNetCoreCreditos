import { Component, OnInit } from '@angular/core';
import { CreditoService } from '../../services/credito.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ClienteConsultaModalComponent } from '../../clientes/modals/cliente-consulta-modal/cliente-consulta-modal.component';
import { ClienteViewModel } from '../../clientes/consulta/cliente-consulta.component';
import { AlertModalComponent } from '../../@base/modals/alert-modal/alert-modal.component';


@Component({
    selector: 'app-credito-register',
    templateUrl: './credito-register.component.html',
    styleUrls: ['./credito-register.component.css']
})
export class CreditoRegisterComponent implements OnInit {
    credito: CreditoRegisterViewModel;
    registerForm: FormGroup;
    submitted = false;

    constructor(
        private creditoService: CreditoService,
        private formBuilder: FormBuilder,
        private modalService: NgbModal) { }

    //, Validators.email

    ngOnInit() {
        this.credito = new CreditoRegisterViewModel();
        let myDate = new Date();
        this.credito.fecha = myDate;
        this.credito.clienteId = '';
        this.credito.numeroCuotas = 0;
        this.credito.valorCredito = 0;
        this.registerForm = this.formBuilder.group({
            clienteId: [this.credito.clienteId, Validators.required],
            fecha: [this.credito.fecha, Validators.required],
            numeroCuotas: [this.credito.numeroCuotas, [Validators.required, , Validators.min(2), Validators.max(12)], Validators.pattern("^[0-9]*$")],
            valorCredito: [this.credito.valorCredito, [Validators.required, Validators.min(100000)], Validators.pattern("^[0-9]*$")],
            observacion: [this.credito.observacion, Validators.required],
        });
        
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }
    
    onSubmit() {
        this.submitted = true;
        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }
        this.create();
    }

    create()
    {
        this.credito = this.registerForm.value;

        this.creditoService.post(this.credito).subscribe(c =>
        {
            const messageBox = this.modalService.open(AlertModalComponent)
            messageBox.componentInstance.title = "Resultado Operación";
            if (c != null)
                messageBox.componentInstance.message = 'SUCCESS!! :-)';
            else
                messageBox.componentInstance.message = 'ERROR!! : -(';
        });
    }

    onReset() {
        this.submitted = false;
        this.registerForm.reset();
    }
    
    openModalCliente()
    {
        this.modalService.open(ClienteConsultaModalComponent, { size: 'lg' })
            .result.then((cliente) => this.actualizar(cliente));
    }

    actualizar(cliente: ClienteViewModel) {
        
        this.registerForm.controls['clienteId'].setValue(cliente.nombreCompleto);


    }

}

export class CreditoRegisterViewModel {
    clienteId: string;
    fecha: Date;
    numeroCuotas: number;
    valor: number;
    valorCredito: number;
    observacion: string;
    cuotas: CuotaRegisterViewModel[];
}

export class CuotaRegisterViewModel {
    valor: number;
    fecha: Date;
}
