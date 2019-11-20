import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { map } from 'rxjs/operators';

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

    public isAuthenticated: Observable<boolean>;
    public userName: Observable<string>;
    public role: any;

    constructor(private authorizeService: AuthorizeService) { }

    ngOnInit() {
        this.isAuthenticated = this.authorizeService.isAuthenticated();
        this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
        this.authorizeService.getUser().subscribe(user => {
            if (user) {
                this.role = user.role;
            }
        }
        );
    }

    isAuthenticatedRole(role: string): boolean {
        if (role != null && this.role != null) {
            return this.role.indexOf(role) >= 0;
        }
    }

}
