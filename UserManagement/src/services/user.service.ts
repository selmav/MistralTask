import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { switchMap } from "rxjs/operators";
import { User, UserList } from "src/shared/models";

@Injectable({ providedIn: 'root' })
export class UserService {
    private pageSubject = new BehaviorSubject<number>(1);
    pageChangeAction$ = this.pageSubject.asObservable();
    userList$ = this.pageChangeAction$.pipe(
        switchMap((page) => this.getUsers(page)
        ));

    constructor(private readonly http: HttpClient) {

    }

    changePage(page: number = 1) {
        this.pageSubject.next(page);
    }

    getUsers(page: number = 1, pageSize: number = 10): Observable<UserList> {
        return this.http.get<UserList>(`User?page=${page}&pageSize=${pageSize}`);
    }
}