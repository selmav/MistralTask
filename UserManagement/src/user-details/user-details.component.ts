import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { BehaviorSubject, firstValueFrom, take } from "rxjs";
import { UserService } from "src/services/user.service";
import { User } from "src/shared/models";

@Component({
    selector: 'app-user-details',
    templateUrl: 'user-details.component.html',
    styleUrls: ['user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {
    form: FormGroup = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        username: ['', Validators.required],
        password: ['', Validators.required],
        email: ['', Validators.required],
        // status: ['', Validators.required]
    });

    userId?: number = undefined;
    loading$ = new BehaviorSubject<boolean>(false);


    constructor(
        private readonly fb: FormBuilder,
        private readonly userService: UserService,
        private readonly router: Router,
        private readonly activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {
        this.userId = this.activatedRoute.snapshot.params?.['userId'];

        if (this.userId) {
            this.loading$.next(true);
            this.userService.getUser(this.userId).pipe(take(1)).subscribe(user => {
                const { userId, status, permissionIds, ...formValue } = user;
                this.form.setValue(formValue);
                this.form.controls['username'].disable();
                this.form.controls['password'].disable();
                this.loading$.next(false);
            });
        }
    }

    onSubmit() {
        if (this.form.valid) {
            const user = this.form.value as User;
            this.loading$.next(true);
            firstValueFrom(!!this.userId ?
                this.userService.updateUser({ ...user, userId: this.userId }) :
                this.userService.addUser(user))
                .then((_) => {
                    this.router.navigate(['user-list']);
                })
                .finally(() => this.loading$.next(false));
        }
    }
}