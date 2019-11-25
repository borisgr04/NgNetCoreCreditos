import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ClienteService } from '../../services/cliente.service';
import { ClienteViewModel } from '../models/cliente-view-model';

@Component({
    selector: 'app-cliente-consulta',
    templateUrl: './cliente-consulta.component.html',
    styleUrls: ['./cliente-consulta.component.css']
})
export class ClienteConsultaComponent implements OnInit {

    filteredClients: ClienteViewModel[] = [];
    clientes: ClienteViewModel[];

    @Output() seleccionado = new EventEmitter<ClienteViewModel>();

    constructor(private clienteService: ClienteService) { }

    ngOnInit() {
        this.clienteService.get().subscribe(result => {
            this.clientes = result;
            this.filteredClients = result;
            this.listFilter = '';
        });
    }

    _listFilter = '';
    get listFilter(): string {
        return this._listFilter;
    }
    set listFilter(value: string) {
        this._listFilter = value;
        this.filteredClients = this.listFilter ? this.doFilter(this.listFilter) : this.clientes;
    }

    doFilter(filterBy: string): ClienteViewModel[] {
        filterBy = filterBy.toLocaleLowerCase();
        return this.clientes.filter(cliente =>
            cliente.nombreCompleto.toLocaleLowerCase().indexOf(filterBy) !== -1
            ||
            cliente.telefono.toLocaleLowerCase().indexOf(filterBy) !== -1
        );
    }

    seleccionar(cliente: ClienteViewModel) {
        this.seleccionado.emit(cliente);
    }
}
