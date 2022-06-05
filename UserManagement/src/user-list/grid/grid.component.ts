import { Component, EventEmitter, Input, Output } from "@angular/core";
import { OrderDirection } from "src/shared/ordering/ordering.component";
import { Ordering, User } from "../../shared/models";

export type orderingField = null | 'firstName' | 'lastName' | 'username' | 'email' | 'status';

@Component({
    selector: 'app-grid',
    templateUrl: 'grid.component.html',
    styleUrls: ['grid.component.scss']
})
export class GridComponent {
    @Input() users: User[] = [];
    @Input() totalPages: number = 10;
    @Input() activePage: number = 1;

    @Output() pageToggle = new EventEmitter<number>();
    @Output() ordering = new EventEmitter<Ordering>();
    @Output() onDelete = new EventEmitter<number>();

    activeOrderingField: orderingField = null;

    onOrdering(ordering: OrderDirection, field: string) {
        this.activeOrderingField = field as any;
        this.ordering.emit({ key: field as any, direction: ordering });
    }
}