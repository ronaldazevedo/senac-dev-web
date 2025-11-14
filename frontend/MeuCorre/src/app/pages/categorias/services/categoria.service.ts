import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoriaModel } from '../models/categoria.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {
  private apiUrl = 'https://localhost:7160/Categoria';

  private http = inject(HttpClient);

  private temporaria = "?UsuarioId=da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b";

  obterTodasPorUsuarios(): Observable<CategoriaModel[]> {
    return this.http.get<CategoriaModel[]>(this.apiUrl + "?UsuarioId=da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b");
  }
  
}
