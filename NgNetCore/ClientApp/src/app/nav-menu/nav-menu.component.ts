import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

    isExpanded = false;

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }

    public isAuthenticated: boolean;
    public userName: string;
    
    constructor(private authorizeService: AuthService) { }

    ngOnInit() {
        this.isAuthenticated = this.authorizeService.isAuthenticated();
        this.userName = this.authorizeService.getUserName();
    }

    isAuthenticatedRole(role: string): boolean {
        
        if (this.isAuthenticated && role != null ) {
            return this.authorizeService.hasRole(role);
        }
    }

}
