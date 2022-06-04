export interface User {
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

export interface UserList {
    page: number;
    totalPages: number;
    pageSize: number;
    results: User[]
}