import { Component, OnInit } from "@angular/core";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { map, switchMap } from "rxjs/operators";
import { UserService } from "src/services/user.service";
import { User, UserList } from "src/shared/models";

@Component({
    selector: 'app-user-list',
    templateUrl: 'user-list.component.html',
    styleUrls: ['user-list.component.scss']
})
export class UserListComponent implements OnInit {
    userList$: Observable<UserList>

    constructor(private readonly userService: UserService) {

    }

    ngOnInit(): void {
        this.userList$ = this.userService.userList$;
    }

    onPageClick(pageToggle: number, currentPage: number) {
        this.userService.changePage(currentPage + pageToggle);
    }
}