import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormSearch,JourneyData } from '../interfaces/Flights';
import { throwError, map, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class FlightService {

  private url = 'https://localhost:7078/api';
  private pathSearch='/Flight/trip-route?origin=';
  constructor(private http:HttpClient) {
  }
  //obtiene la lista de usuarios de la busqueda
  getTripRoute(form:FormSearch) {
    const url=`${this.url}${this.pathSearch}${form.origin}&destination=${form.destination}&typeFlight=${form.typeFlight}`;

    return this.http.get<JourneyData>(url)
                    .pipe(
                      catchError(err=>of(err.error.message))
                    );
  }
}
