import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';
import { FormSearch, JourneyData, Journey } from './interfaces/Flights';
import { FlightService } from './services/flight.service';
import { catchError } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  errorOrigin            :boolean = false;
  errorDestination       :boolean = false;
  isLoading              :boolean = false;
  showNoResults          :boolean = true;
  noResultsMessage       :string  = 'No hay resultados para mostrar';
  errorOriginMessage     :string  = '';
  errorDestinationMessage:string  = '';

  data:JourneyData={};
  form:FormSearch = {
    origin: '',
    destination: '',
    typeFlight: 0
  };
  constructor(private titleService: Title, private flightService: FlightService) {
    this.titleService.setTitle('Buscador de vuelos');
  }
  searchFlights():void {
    this.form.origin = this.form.origin!.toUpperCase();
    this.form.destination = this.form.destination!.toUpperCase();
    this.errorOrigin = false;
    this.errorDestination = false;
    this.errorOriginMessage = '';
    this.errorDestinationMessage = '';
    if (this.form.origin === '') {
      this.errorOrigin = true;
      this.errorOriginMessage = 'El origen es requerido';
      return;
    }
    if(this.form.origin!.length!=3){
      this.errorOrigin = true;
      this.errorOriginMessage = 'El origen debe tener al menos 3 caracteres';
      return;
    }
    if (this.form.destination === '') {
      this.errorDestination = true;
      this.errorDestinationMessage = 'El destino es requerido';
      return;
    }

    if(this.form.destination!.length!=3){
      this.errorDestination = true;
      this.errorDestinationMessage = 'El destino debe tener al menos 3 caracteres';
      return;
    }
    if(this.form.origin===this.form.destination){
      this.errorDestination = true;
      this.errorDestinationMessage = 'El destino debe ser diferente al origen';
      return;
    }
    this.isLoading = true;
    this.flightService
        .getTripRoute(this.form)
        .subscribe((data) => {
          this.isLoading = false;
          if(data===undefined){
            return;
          }
          console.log("se muestra desde aqui",data);
          if (data.journey === undefined) {
            this.showNoResults = true;
            this.noResultsMessage = data;
            this.data = {};
            return;
          }
          this.showNoResults =false;
          this.data = data;
        });
  }
  validateInput(value:string,type:string):void {
    value = value.trim();
    if (value.length < 3 && type === 'origin') {
      this.errorOrigin = true;
      this.errorOriginMessage = 'El origen debe tener al menos 3 caracteres';
      return;
    }
  }

}
