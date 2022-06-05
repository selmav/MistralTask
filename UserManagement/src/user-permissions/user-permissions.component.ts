import { Component, OnInit } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { firstValueFrom, Observable, take } from "rxjs";
import { PermissionService } from "src/services/permission.service";
import { UserService } from "src/services/user.service";
import { Permission, User } from "src/shared/models";

@Component({
    selector: 'app-user-permissions',
    templateUrl: 'user-permissions.component.html',
    styleUrls: ['user-permissions.component.scss']
})
export class UserPermissionsComponent implements OnInit {
    allPermissions$: Observable<Permission[]> = this.permissionService.allPermissions$
    userPermissions$: Observable<Permission[]> = this.permissionService.userPermissions$;
    user!: User;
    form: FormGroup = this.fb.group({ permissions: new FormArray([]) });

    constructor(
        private readonly permissionService: PermissionService,
        private readonly userService: UserService,
        private readonly activatedRoute: ActivatedRoute,
        private readonly fb: FormBuilder,
        private readonly router: Router) {

    }

    ngOnInit(): void {
        const userId = this.activatedRoute.snapshot.params?.['userId'];
        this.permissionService.getUserPermissions(userId);

        this.userService.getUser(userId).pipe(take(1)).subscribe(user => {
            this.user = user;

            const selectedPermissions = this.form.get('permissions') as FormArray;
            this.user.permissionIds?.map(p => selectedPermissions.push(new FormControl(p)));
        });
    }

    isChecked(permissionId: number) {
        return this.user.permissionIds?.includes(permissionId);
    }

    onCheckboxChange(event: any) {
        const selectedPermissions = this.form.get('permissions') as FormArray;
        this.form.markAsTouched();

        if (event.target.checked) {
            selectedPermissions.push(new FormControl(Number(event.target.value)));
        } else {
            const index = selectedPermissions.controls.findIndex(c => c.value === Number(event.target.value));
            selectedPermissions.removeAt(index);
        }
    }

    onSubmit() {
        firstValueFrom(this.permissionService.setUserPermissions({
            userId: this.user?.userId,
            permissionIds: this.form.value.permissions.map((p: string) => Number(p))
        })).then(_ => this.router.navigate(['/user-list']));
    }
}