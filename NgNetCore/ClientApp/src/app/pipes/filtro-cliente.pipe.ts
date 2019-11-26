import { Pipe, PipeTransform } from '@angular/core';
import { ClienteViewModel } from '../clientes/models/cliente-view-model';

@Pipe({
  name: 'filtroCliente'
})
export class FiltroClientePipe implements PipeTransform {
    transform(clientes: ClienteViewModel[], searchText: string) {
        if (searchText == null) return clientes;
        return clientes.filter(cliente =>
            cliente.nombreCompleto.toLocaleLowerCase().indexOf(searchText) !== -1
            ||
            cliente.telefono.toLocaleLowerCase().indexOf(searchText) !== -1
        );
    }
  

}
