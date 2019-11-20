import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ClienteService } from '../../services/cliente.service';

@Component({
    selector: 'app-cliente-consulta',
    templateUrl: './cliente-consulta.component.html',
    styleUrls: ['./cliente-consulta.component.css']
})
export class ClienteConsultaComponent implements OnInit {

    @Output() seleccionado = new EventEmitter<ClienteViewModel>();

    seleccionar(cliente: ClienteViewModel)
    {
        this.seleccionado.emit(cliente);
    }

    _listFilter = '';
    get listFilter(): string {
        return this._listFilter;
    }
    set listFilter(value: string) {
        this._listFilter = value;
        this.filteredClients = this.listFilter ? this.doFilter(this.listFilter) : this.clientes;

    }
    filteredClients: ClienteViewModel[] = [];
    
    clientes: ClienteViewModel[];

    constructor(private clienteService: ClienteService) {
        
    }
   

    ngOnInit() {
        this.clienteService.get().subscribe(result =>
        {
            this.clientes = result;
            this.filteredClients = result;
            this.listFilter = '';
        });
    }

    doFilter(filterBy: string): ClienteViewModel[] {
        filterBy = filterBy.toLocaleLowerCase();
        return this.clientes.filter(cliente =>
            cliente.nombreCompleto.toLocaleLowerCase().indexOf(filterBy) !== -1
            ||
            cliente.telefono.toLocaleLowerCase().indexOf(filterBy) !== -1
        );
    }
}

export class ClienteViewModel {
    identificacion: string;
    email: string;
    telefono: string;
    nombreCompleto: string;
}
