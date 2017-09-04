import { Injectable } from '@angular/core';
import { Http, HttpModule, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

export class Role {
    id: number;
    role: string;
    permissionId: string;
    isActive: boolean;
    created: string;
    createdBy: string;
    lastUpdated: string;
    lastUpdatebBy: string;
}

@Injectable()
export class RoleService {
    private baseURL = 'http://localhost:50720/api';
    roles;
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

    loadRoleData(userId): Observable<any> {
        return this.http.get(this.baseURL + '/Role', {headers: this.getHeaders(userId)})
        .map(res => {
            return res.json();
        });
    }

    createRole(data: Role, userId: string) {
        return this.http.post(this.baseURL + '/Role', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    saveRole(data: Role, userId: string) {
        return this.http.put(this.baseURL + '/Role', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    deactivateRole(id: number, userId: string) {
        return this.http.delete(this.baseURL + '/Role/' + id, {headers: this.getHeaders(userId)});
    }
    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
    }

    private handleErrorObservable (error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
}
