import { Injectable } from '@angular/core';
import { Http, HttpModule, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';


export class LookupType {
    id: number;
    typeDescription: string;
    isActive: boolean;
}

export class Lookup {
    id: number;
    value: string;
    lookupTypeId: number;
    lookupType: LookupType;
    isActive: boolean;
}

@Injectable()
export class LookupService {
    private baseURL = 'http://localhost:50720/api';
    lookupTypes;
    lookups;
    showInactive = false;

    constructor(private http: Http) {
    }

    private getHeaders(userId) {
        const headers = new Headers({ 'Accept': 'application/json' });
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        headers.append('userid', userId);
        headers.append('showInactive', this.showInactive.toString());
        return headers;
    }

    loadLookupType(userId): Observable<any> {
        return this.http.get(this.baseURL + '/LookupType', {headers: this.getHeaders(userId)})
        .map(res => {
            return res.json();
        });
    }

    loadLookupData(userId):  Observable<any> {
        return this.http.get(this.baseURL + '/Lookup', {headers: this.getHeaders(userId)})
        .map(res => {
            return res.json();
        });
    }
    createLookupValue(data: Lookup, userId: string) {
        console.log('In createLookupValue');
        console.log(data);
        return this.http.post(this.baseURL + '/Lookup', JSON.stringify(data),
            {headers: this.getHeaders(userId)});
    }

    createLookupTypeValue(data: LookupType, userId: string): Observable<Response> {
        console.log(data);
        return this.http.post(this.baseURL + '/LookupType', JSON.stringify(data),
            {headers: this.getHeaders(userId)});
    }

    saveLookupValues(data: Lookup, userId: string) {
        console.log('In saveLookupValue');
        console.log(data);
        return this.http.put(this.baseURL + '/Lookup', JSON.stringify(data),
            {headers: this.getHeaders(userId)});
    }

    saveLookupTypeValues(data: LookupType, userId: string) {
        console.log(data);
        return this.http.put(this.baseURL + '/LookupType', JSON.stringify(data),
            {headers: this.getHeaders(userId)});
    }

    deactivateLookup(id: number, userId: string) {
        console.log('Deactivating Lookup');
        console.log(id);
        return this.http.delete(this.baseURL + '/Lookup/' + id, {headers: this.getHeaders(userId)});
    }

    deactivateLookupType(id: number, userId: string) {
        console.log('Deactivating LookupType');
        console.log(id);
        return this.http.delete(this.baseURL + '/LookupType/' + id, {headers: this.getHeaders(userId)});
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
