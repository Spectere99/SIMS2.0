import { Injectable } from '@angular/core';
import { Http, HttpModule, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

export class UserRole {
    id: number;
    userId: number;
    roleId: number;
    isActive: boolean;
    created: string;
    createdBy: string;
    lastUpdated: string;
    lastUpdatedBy: string;
}

@Injectable()
export class UserRoleService {
    private baseURL = 'http://localhost:50720/api';
    users;
    showInactive = false;

    constructor (private http: Http) {
    }

    private getHeaders(userId) {
        const headers = new Headers({ 'Accept': 'application/json' });
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        headers.append('userid', userId);
        headers.append('showInactive', this.showInactive.toString());
        return headers;
    }

    loadUserRoleData(userId): Observable<any> {
        return this.http.get(this.baseURL + '/UserRole', {headers: this.getHeaders(userId)})
        .map(res => {
            return res.json();
        });
    }

    createUserRole(data: UserRole, userId: string) {
        return this.http.post(this.baseURL + '/UserRole', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    saveUserRole(data: UserRole, userId: string) {
        return this.http.put(this.baseURL + '/UserRole', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    deleteUserRole(id: number, userId: string) {

        return this.http.delete(this.baseURL + '/UserRole/' + id, {headers: this.getHeaders(userId)});
    }

    private extractData(res: Response) {
        const body = res.json();
        return body.data || {};
    }

    private handleErrorObservable (error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
}
