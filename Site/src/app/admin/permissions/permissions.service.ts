import { Injectable } from '@angular/core';
import { Http, HttpModule, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

export class Permission {
    id: number;
    permission: string;
    permissionModuleKey: number;
    canAccess: boolean;
    canUpdate: boolean;
    canDelete: boolean;
    isActive: boolean;
    created: string;
    createdBy: string;
    lastUpdated: string;
    lastUpdatebBy: string;
}

@Injectable()
export class PermissionService {
    private baseURL = 'http://localhost:50720/api';
    permissions;
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

    loadPermissionData(userId): Observable<any> {
        return this.http.get(this.baseURL + '/Permission', {headers: this.getHeaders(userId)})
        .map(res => {
            return res.json();
        });
    }

    createPermission(data: Permission, userId: string) {
        return this.http.post(this.baseURL + '/Permission', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    savePermission(data: Permission, userId: string) {
        return this.http.put(this.baseURL + '/Permission', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    deactivatePermission(id: number, userId: string) {
        return this.http.delete(this.baseURL + '/Permission/' + id, {headers: this.getHeaders(userId)});
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
