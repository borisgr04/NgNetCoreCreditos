import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClienteViewModel } from '../../models/cliente-view-model';

@Component({
    selector: 'app-cliente-consulta-modal',
    templateUrl: './cliente-consulta-modal.component.html',
    styleUrls: ['./cliente-consulta-modal.component.css']
})
export class ClienteConsultaModalComponent  {

    constructor(public activeModal: NgbActiveModal) { }
    
    actualizar(cliente: ClienteViewModel) {
        this.activeModal.close(cliente);
    }
}
