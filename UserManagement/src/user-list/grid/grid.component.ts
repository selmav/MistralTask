import { Component, EventEmitter, Input, Output } from "@angular/core";
import { User } from "../../shared/models";

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
}