import { Component, inject, TemplateRef } from '@angular/core';
import { NgbModal, NgbNavModule, NgbTooltipModule, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { CategoriaModel } from './models/categoria.model';
import { IconAvatar } from '../../shared/components/icon-avatar/icon-avatar';
import { StatusBadge } from '../../shared/components/status-badge/status-badge';

@Component({
  selector: 'app-categorias',
  imports: [NgbNavModule, IconAvatar, StatusBadge, ReactiveFormsModule, NgbTooltipModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css',
})
export class Categorias {
  private modalService = inject(NgbModal);

  nome = new FormControl('');
  descricao = new FormControl('');
  cor = new FormControl('#000000');
  icone = new FormControl('');
  active = 1;

  editarCategoria: CategoriaModel | null = null;

  categorias_despesas: CategoriaModel[] = [
    {
      id: '1',
      nome: 'Alimentação',
      descricao: 'Gastos com comida',
      cor: '#dc3545',
      icone: 'ri-restaurant-line',
      tipo: 'despesa',
      ativo: true,
    },
    {
      id: '2',
      nome: 'Transporte',
      descricao: 'Uber, ônibus, etc',
      cor: '#fd7e14',
      icone: 'ri-bus-line',
      tipo: 'despesa',
      ativo: true,
    },
    {
    id: '3',
      nome: 'Lazer',
      descricao: 'Despesas com lazer',
      cor: '#ffc107',
      icone: 'ri-film-line',
      tipo: 'despesa',
      ativo: true,
    },
  ];

  categorias_receitas: CategoriaModel[] = [
    {
      id: '1',
      nome: 'Salário',
      descricao: 'Recebimento mensal',
      cor: '#28a745',
      icone: 'ri-bank-line',
      tipo: 'receita',
      ativo: true,
    },
    {
      id: '2',
      nome: 'Freelance',
      descricao: 'Trabalhos extras',
      cor: '#17a2b8',
      icone: 'ri-briefcase-line',
      tipo: 'receita',
      ativo: false,
    },
    {
      id: '3',
      nome: 'Investimentos',
      descricao: 'Rendimentos de investimentos',
      cor: '#ffc107',
      icone: 'ri-line-chart-line',
      tipo: 'receita',
      ativo: false,
    },
  ];

  open(content: TemplateRef<any>, categoria?: CategoriaModel) {
    if (categoria) { 
      this.editarCategoria = categoria;
      this.nome.setValue(categoria.nome);
      this.descricao.setValue(categoria.descricao);
      this.cor.setValue(categoria.cor);
      this.icone.setValue(categoria.icone);
      this.active = categoria.tipo === 'despesa' ? 1 : 2;
    } else {
      this.editarCategoria = null;
      this.nome.reset();
      this.descricao.reset();
      this.cor.setValue('#000000');
      this.icone.reset();
    }

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }


  cadastrarCategoria() {
    const novaCategoria: CategoriaModel = {
      id: this.editarCategoria ? this.editarCategoria.id : Date.now().toString(),
      nome: this.nome.value!,
      descricao: this.descricao.value!,
      cor: this.cor.value!,
      icone: this.icone.value!,
      tipo: this.active === 1 ? 'despesa' : 'receita',
      ativo: true,
    };

    if (this.editarCategoria) {
     
      if (novaCategoria.tipo === 'despesa') {
        this.categorias_despesas = this.categorias_despesas.map(c =>
          c.id === novaCategoria.id ? novaCategoria : c
        );
      } else {
        this.categorias_receitas = this.categorias_receitas.map(c =>
          c.id === novaCategoria.id ? novaCategoria : c
        );
      }
    } else {
      
      if (novaCategoria.tipo === 'despesa') {
        this.categorias_despesas.push(novaCategoria);
      } else {
        this.categorias_receitas.push(novaCategoria);
      }
    }

    this.modalService.dismissAll();
    this.editarCategoria = null;
  }

  
  excluirCategoriaDespesa(id: string) {
    this.categorias_despesas = this.categorias_despesas.filter(c => c.id !== id);
  }

  excluirCategoriaReceita(id: string) {
    this.categorias_receitas = this.categorias_receitas.filter(c => c.id !== id);
  }

  
}
