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

    
    constructor(private authorizeService: AuthService) { }

    userName(): string {
        return this.authorizeService.getUserName();
    }

    public isAuthenticated(): boolean
    {
        return this.authorizeService.isAuthenticated();
    }

    isAuthenticatedRole(role: string): boolean {
        
        if (this.isAuthenticated && role != null ) {
            return this.authorizeService.hasRole(role);
        }
    }

}
