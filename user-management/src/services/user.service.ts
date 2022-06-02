import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of, Subject } from "rxjs";
import { switchMap } from "rxjs/operators";
import { UserList } from "src/shared/models";

@Injectable({ providedIn: 'root' })
export class UserService {
    private pageSubject = new BehaviorSubject<number>(1);
    pageChangeAction$ = this.pageSubject.asObservable();
    userList$ = this.pageChangeAction$.pipe(
        switchMap((page) => this.getUsers(page)
        ));

    changePage(page: number = 1) {
        this.pageSubject.next(page);
    }

    getUsers(page: number = 1): Observable<UserList> {
        const res: UserList = {
            page,
            totalPages: 3,
            results: [{
                firstName: `Selma ${page}`,
                lastName: 'Vucijak',
                username: 'vselma',
                password: 'selma',
                email: 'selma@selma',
                status: '?'
            },
            {
                firstName: 'Selma afasffa',
                lastName: 'Vucijak',
                username: 'vselma ajfjjasjfa',
                password: 'selma',
                email: 'selma@selma',
                status: '?'
            }]
        }
        return of(res);
    }
}