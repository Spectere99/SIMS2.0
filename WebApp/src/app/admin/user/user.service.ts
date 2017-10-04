import { Injectable } from '@angular/core';
import { Http, HttpModule, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

export class User {
    id: number;
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
    contactNumber: string;
    isActive: boolean;
    created: string;
    createdBy: string;
    lastUpdated: string;
    lastUpdatebBy: string;
}

@Injectable()
export class UserService {
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

    loadUserData(userId): Observable<any> {
        return this.http.get(this.baseURL + '/User', {headers: this.getHeaders(userId)})
        .map(res => {
            return res.json();
        });
    }

    createUser(data: User, userId: string) {
        return this.http.post(this.baseURL + '/User', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    saveUser(data: User, userId: string) {
        return this.http.put(this.baseURL + '/User', JSON.stringify(data),
        {headers: this.getHeaders(userId)});
    }

    deactivateUser(id: number, userId: string) {
        return this.http.delete(this.baseURL + '/User/' + id, {headers: this.getHeaders(userId)});
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
