import { Component, OnInit } from "@angular/core";
import { BehaviorSubject, firstValueFrom, Observable } from "rxjs";
import { UserService } from "src/services/user.service";
import { Filters, Ordering, UserList } from "src/shared/models";

@Component({
    selector: 'app-user-list',
    templateUrl: 'user-list.component.html',
    styleUrls: ['user-list.component.scss']
})
export class UserListComponent implements OnInit {
    userList$!: Observable<UserList>;
    deleteUser$ = new BehaviorSubject<number | boolean>(false);
    showFilters$ = new BehaviorSubject<boolean>(false);
    filters$ = new BehaviorSubject<Filters>({});

    constructor(private readonly userService: UserService) {

    }

    ngOnInit(): void {
        this.userService.resetSearch();
        this.userList$ = this.userService.userList$;
    }

    onPageClick(pageToggle: number, currentPage: number) {
        this.userService.changePage(currentPage + pageToggle);
    }

    onOrdering(ordering: Ordering) {
        this.userService.changeRequest({ ...ordering, page: 1, pageSize: 10 });
    }

    onDelete() {
        const userId = Number(this.deleteUser$.value);
        firstValueFrom(this.userService.deleteUser(userId))
            .then(() => this.userService.changeRequest())
            .finally(() => this.deleteUser$.next(false));
    }

    onApplyFilters(filters: Filters) {
        this.filters$.next(filters);
        this.showFilters$.next(false);
        this.userService.applyFilters(filters);
    }
}