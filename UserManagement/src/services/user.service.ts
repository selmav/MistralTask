import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { switchMap } from "rxjs/operators";
import { Filters, Status, User, UserList } from "src/shared/models";

@Injectable({ providedIn: 'root' })
export class UserService {
    private readonly defaultRequest = { page: 1, pageSize: 10 };
    private changeRequestSubject = new BehaviorSubject<UserList & Filters>(this.defaultRequest);
    private changeRequestAction$ = this.changeRequestSubject.asObservable();

    userList$ = this.changeRequestAction$.pipe(
        switchMap((request) => this.http.get<UserList>('User', { params: this.getQuery(request) }))
    );

    allStatuses$ = this.http.get<Status[]>('User/statuses');

    constructor(private readonly http: HttpClient) {

    }

    resetSearch() {
        this.changeRequestSubject.next(this.defaultRequest);
    }

    changePage(page: number = 1) {
        this.changeRequestSubject.next({ ...this.changeRequestSubject.value, page });
    }

    changeRequest(request?: UserList) {
        this.changeRequestSubject.next(request ?? this.changeRequestSubject.value);
    }

    applyFilters(filters: Filters) {
        const request = { ...this.changeRequestSubject.value, ...filters, page: 1, pageSize: 10 };
        this.changeRequestSubject.next(request);
    }

    addUser(user: User | null): Observable<void> {
        return this.http.post<void>('User', user);
    }

    updateUser(user: User | null): Observable<void> {
        return this.http.put<void>('User', user);
    }

    getUser(userId: number): Observable<User> {
        return this.http.get<User>(`User/${userId}`);
    }

    deleteUser(userId: number): Observable<void> {
        return this.http.delete<void>(`User/${userId}`);
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