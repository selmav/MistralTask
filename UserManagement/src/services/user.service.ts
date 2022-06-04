import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, combineLatest, Observable } from "rxjs";
import { switchMap } from "rxjs/operators";
import { Ordering, User, UserList } from "src/shared/models";

@Injectable({ providedIn: 'root' })
export class UserService {
    private changeRequestSubject = new BehaviorSubject<UserList>({ page: 1, pageSize: 10 });
    private changeRequestAction$ = this.changeRequestSubject.asObservable();

    // private addUserSubject = new BehaviorSubject<User | null>(null);
    // private addUserAction$ = this.addUserSubject.asObservable();

    userList$ = this.changeRequestAction$.pipe(
        switchMap((request) => this.getUsers(request))
    );

    // addUser$ = this.addUserAction$.pipe(
    //     switchMap((request) => this.addUser(request))
    // );

    constructor(private readonly http: HttpClient) {

    }

    changePage(page: number = 1) {
        this.changeRequestSubject.next({ ...this.changeRequestSubject.value, page });
    }

    changeRequest(request: UserList) {
        this.changeRequestSubject.next(request);
    }

    // onAddUser(user: User) {
    //     this.addUserSubject.next(user);
    // }
    
    private getUsers(request: UserList): Observable<UserList> {
        return this.http.get<UserList>('User', { params: this.getQuery(request) });
    }
    
    addUser(user: User | null): Observable<void> {
        return this.http.post<void>('User', user);
    }


    private getQuery(request: UserList) {
        const query: { [key: string]: any } = {};

        const keys = Object.keys(request) as Array<keyof UserList>;
        keys.forEach(key => {
            if (!!request[key]) {
                query[key as string] = request[key];
            }
        });

        return query;
    }
}