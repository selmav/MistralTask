import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, switchMap } from "rxjs";
import { Permission, UserPermissions } from "src/shared/models";

@Injectable({ providedIn: 'root' })
export class PermissionService {
    private userPermissionsSubject = new BehaviorSubject<number>(0);
    private userPermissionsAction$ = this.userPermissionsSubject.asObservable();

    constructor(private readonly http: HttpClient) {

    }

    allPermissions$: Observable<Permission[]> = this.http.get<Permission[]>('Permission');

    userPermissions$ = this.userPermissionsAction$.pipe(
        switchMap((userId) => this.http.get<Permission[]>(`Permission/user-permissions?userId=${userId}`))
    );

    getUserPermissions(userId: number): void {
        this.userPermissionsSubject.next(userId);
    }

    setUserPermissions(request: UserPermissions): Observable<void> {
        return this.http.put<void>('Permission/user-permissions', request);
    }
}