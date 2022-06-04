import { orderingField } from "src/user-list/grid/grid.component";
import { OrderDirection } from "./ordering/ordering.component";

export interface User {
    userId?: number;
    firstName: string;
    lastName: string;
    username: string;
    password: string;
    email: string;
    status: string;
}

export interface Permission {
    code: string;
    description: string;
}

// Request/Response models

export interface Ordering {
    key?: orderingField;
    direction?: OrderDirection;
}

export interface UserList extends Ordering {
    page: number;
    totalPages?: number;
    pageSize: number;
    results?: User[]
}