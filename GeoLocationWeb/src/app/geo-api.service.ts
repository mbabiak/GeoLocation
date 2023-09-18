import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GeoApiService {
  constructor(private http: HttpClient) { }

  searchLocations(data: any, file: File): Observable<any> {
    const formData: FormData = new FormData();

    formData.append('centerLongitude', data.centerLongitude);
    formData.append('centerLatitude', data.centerLatitude);
    formData.append('borderLongitude', data.borderLongitude);
    formData.append('borderLatitude', data.borderLatitude);
    formData.append('hubConnectionId', data.hubConnectionId);
    formData.append('file', file, file.name);

    return this.http.post(`${environment.apiUrl}/api/locations/search`, formData);
  }
}