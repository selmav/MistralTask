import { orderingField } from "src/user-list/grid/grid.component";
import { OrderDirection } from "./ordering/ordering.component";

export interface User {
    userId?: number;
    firstName: string;
    lastName: string;
    username: string;
    password: string;
    email: string;
    status: Status;
    permissionIds?: number[];
}

export interface Permission {
    permissionId: number;
    code: string;
    description: string;
}

export interface Status {
    statusId: number;
    name: string;
}

// Request/Response models

export interface Ordering {
    key?: orderingField;
    direction?: OrderDirection;
}

export interface Filters {
    firstName?: string;
    lastName?: string;
    username?: string;
    email?: string;
    status?: string;
}

export interface UserList extends Ordering {
    page: number;
    totalPages?: number;
    pageSize: number;
    results?: User[]
}

export interface UserPermissions {
    userId?: number;
    permissionIds: number[]
}