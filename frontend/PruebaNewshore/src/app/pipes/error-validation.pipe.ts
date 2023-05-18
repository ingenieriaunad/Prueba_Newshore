import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'errorValidation'
})
export class ErrorValidationPipe implements PipeTransform {
  transform(form: any, field: string): { error: boolean, errorMessage: string } {
    if (field === 'origin') {
      if (form.origin === '') {
        return { error: true, errorMessage: 'El origen es requerido' };
      }
      if (form.origin.length !== 3) {
        return { error: true, errorMessage: 'El origen debe tener al menos 3 caracteres' };
      }
    }

    if (field === 'destination') {
      if (form.destination === '') {
        return { error: true, errorMessage: 'El destino es requerido' };
      }
      if (form.destination.length !== 3) {
        return { error: true, errorMessage: 'El destino debe tener al menos 3 caracteres' };
      }
      if (form.origin === form.destination) {
        return { error: true, errorMessage: 'El destino debe ser diferente al origen' };
      }
    }

    return { error: false, errorMessage: '' };
  }
}
