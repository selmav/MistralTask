import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { BehaviorSubject, firstValueFrom } from "rxjs";
import { UserService } from "src/services/user.service";
import { User } from "src/shared/models";

@Component({
    selector: 'app-user-details',
    templateUrl: 'user-details.component.html',
    styleUrls: ['user-details.component.scss']
})
export class UserDetailsComponent {
    form: FormGroup = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        username: ['', Validators.required],
        password: ['', Validators.required],
        email: ['', Validators.required],
        // status: ['', Validators.required]
    });

    loading$ = new BehaviorSubject<boolean>(false);

    constructor(private readonly fb: FormBuilder, private readonly userService: UserService, private readonly router: Router) {

    }

    onSubmit() {
        if (this.form.valid) {
            const user = this.form.value as User;
            this.loading$.next(true);
            firstValueFrom(this.userService.addUser(user))
                .then((_) => {
                    this.router.navigate(['user-list']);
                })
                .finally(() => this.loading$.next(false));
        }
    }
}