import { Component, inject } from '@angular/core';
import { ListaService } from '../lista.service';
import { Observable, Subject, debounceTime, distinctUntilChanged, switchMap, startWith } from 'rxjs';
import { Film } from '../../models/film';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-lista',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './lista.component.html',
  styleUrl: './lista.component.css'
})
export class ListaComponent {
  private readonly listaService = inject(ListaService);
  public dane$: Observable<Film[]>;
  
  public frazaWyszukiwania = '';
  private searchSubject = new Subject<string>();

  constructor() {
    this.dane$ = this.searchSubject.pipe(
      startWith(''), 
      debounceTime(300), 
      distinctUntilChanged(), 
      switchMap(fraza => this.listaService.get(fraza)) 
    );
  }

  onSearch(): void {
    this.searchSubject.next(this.frazaWyszukiwania);
  }

  onClearSearch(): void {
    this.frazaWyszukiwania = '';
    this.searchSubject.next('');
  }
}