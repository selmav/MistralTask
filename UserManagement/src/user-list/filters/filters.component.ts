import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { Filters } from "src/shared/models";

@Component({
    selector: 'app-filters',
    templateUrl: 'filters.component.html',
    styleUrls: ['filters.component.scss']
})
export class FiltersComponent implements OnInit {
    form: FormGroup = this.fb.group({
        firstName: (''),
        lastName: (''),
        username: (''),
        email: ('')
    });

    @Input() currentFilters: Filters = {};
    @Output() apply = new EventEmitter<Filters>();

    constructor(private readonly fb: FormBuilder) {

    }

    ngOnInit(): void {
        !!this.currentFilters && this.form.patchValue(this.currentFilters);
    }

    onClear() {
        this.form.reset();
        this.apply.emit(this.form.value);
    }
}