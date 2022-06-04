import { Component, EventEmitter, Output } from "@angular/core";

export enum OrderDirection {
    Asc = 'asc',
    Desc = 'desc'
}

@Component({
    selector: 'app-ordering',
    template: `
        <span (click)="onClick(OrderDirection.Desc)" [class.active]="activeOrdering === OrderDirection.Desc">&darr;</span>
        <span (click)="onClick(OrderDirection.Asc)" [class.active]="activeOrdering === OrderDirection.Asc">&uarr;</span>
    `,
    styles: [`
        :host-context(.active-ordering) {
            .active {
                color: red;
            }
        }

        span {
            font-weight: 600;
            cursor: pointer;

            &:first-child {
                margin-left: 0.5rem;
            }
        }

    `]
})
export class OrderingComponent {
    @Output() ordering = new EventEmitter<OrderDirection>();

    activeOrdering!: OrderDirection | null;
    OrderDirection = OrderDirection;

    onClick(ordering: OrderDirection) {
        this.activeOrdering = ordering;
        this.ordering.emit(ordering);
    }
}