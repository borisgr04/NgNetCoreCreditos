import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

    constructor() {
        
    }

    login(user: string, password: string) {
        //validar en el servidor si el usuario y password son válidos.
        //en el caso que sean válidos se deberian retornar los roles que tiene asociado dicho usuario
        //se podría encriptar el nombre de la variable
        sessionStorage.setItem('user', user);
        sessionStorage.setItem('roles', JSON.stringify(['admin']));
        alert('registrado');
    }

    logout() {
        sessionStorage.removeItem('user');
        sessionStorage.removeItem('roles');
    }

    isAuthenticated(): boolean {

        return sessionStorage.getItem('user')!=null;
    }

    hasRole(rol: string): boolean {
        if (!this.isAuthenticated()) return false;
        let roles: string[] = JSON.parse(sessionStorage.getItem('roles'));
        return roles.indexOf(rol) >= 0;
    }

    getUserName(): string {
        return localStorage.getItem('user') != null ? localStorage.getItem('user'):'Anonimo';
    }
}
