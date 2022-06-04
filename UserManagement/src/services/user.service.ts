import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, combineLatest, Observable } from "rxjs";
import { switchMap } from "rxjs/operators";
import { Ordering, User, UserList } from "src/shared/models";

@Injectable({ providedIn: 'root' })
export class UserService {
    private changeRequestSubject = new BehaviorSubject<UserList>({ page: 1, pageSize: 10 });
    changeRequestAction$ = this.changeRequestSubject.asObservable();

    // private pageSubject = new BehaviorSubject<number>(1);
    // pageChangeAction$ = this.pageSubject.asObservable();

    // private orderingSubject = new BehaviorSubject<Ordering | null>(null);
    // orderingChangeAction$ = this.orderingSubject.asObservable();

    userList$ = this.changeRequestAction$.pipe(
        switchMap((request) => this.getUsers(request))
    );

    constructor(private readonly http: HttpClient) {

    }

    changePage(page: number = 1) {
        this.changeRequestSubject.next({ ...this.changeRequestSubject.value, page });
    }

    // changeOrdering(ordering: Ordering) {
    //     this.orderingSubject.next(ordering);
    // }

    changeRequest(request: UserList) {
        this.changeRequestSubject.next(request);
    }

    getUsers(request: UserList): Observable<UserList> {
        return this.http.get<UserList>('User', { params: this.getQuery(request) });
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

        // if (request.page) {
        //     query['page'] = request.page;
        // }

        // if (request.pageSize) {
        //     query['pageSize'] = request.pageSize;
        // }

        // if (!!request.key) {
        //     query['key'] = request.key;
        // }

        // if (!!request.direction) {
        //     query['direction'] = request.direction;
        // }

        // return query;
    }
}