import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CidadeService } from '../../services/cidade.service';
import { UsuarioService } from '../../services/usuario.service';
import { PrevisaoService } from '../../services/previsao.service';
import { Cidade, CidadeRequest } from '../../models/cidade.model';
import { Usuario } from '../../models/usuario.model';
import { PrevisaoTempo } from '../../models/previsao.model';

@Component({
  selector: 'app-cidades',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cidades.component.html',
  styleUrl: './cidades.component.scss'
})
export class CidadesComponent implements OnInit {
  cidades: Cidade[] = [];
  usuarios: Usuario[] = [];
  loading = false;
  showForm = false;
  selectedCidade: CidadeRequest = this.getEmptyCidade();
  previsao?: PrevisaoTempo;
  showPrevisao = false;

  constructor(
    private cidadeService: CidadeService,
    private usuarioService: UsuarioService,
    private previsaoService: PrevisaoService
  ) { }

  ngOnInit(): void {
    this.loadCidades();
    this.loadUsuarios();
  }

  loadCidades(): void {
    this.loading = true;
    this.cidadeService.getAll().subscribe({
      next: (data) => {
        this.cidades = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar cidades:', error);
        alert('Erro ao carregar cidades. Verifique se a API está rodando.');
        this.loading = false;
      }
    });
  }

  loadUsuarios(): void {
    this.usuarioService.getAll().subscribe({
      next: (data) => {
        this.usuarios = data;
      },
      error: (error) => {
        console.error('Erro ao carregar usuários:', error);
      }
    });
  }

  getEmptyCidade(): CidadeRequest {
    return {
      nome: '',
      usuarioId: 0
    };
  }

  openCreateForm(): void {
    this.selectedCidade = this.getEmptyCidade();
    this.showForm = true;
  }

  cancelForm(): void {
    this.showForm = false;
    this.selectedCidade = this.getEmptyCidade();
  }

  saveCidade(): void {
    this.cidadeService.create(this.selectedCidade).subscribe({
      next: () => {
        alert('Cidade adicionada com sucesso!');
        this.loadCidades();
        this.cancelForm();
      },
      error: (error) => {
        console.error('Erro ao adicionar cidade:', error);
        alert('Erro ao adicionar cidade: ' + (error.error?.message || error.message));
      }
    });
  }

  deleteCidade(id: number): void {
    if (confirm('Deseja realmente excluir esta cidade?')) {
      this.cidadeService.delete(id).subscribe({
        next: () => {
          alert('Cidade excluída com sucesso!');
          this.loadCidades();
        },
        error: (error) => {
          console.error('Erro ao excluir cidade:', error);
          alert('Erro ao excluir cidade: ' + (error.error?.message || error.message));
        }
      });
    }
  }

  getPrevisao(cidadeId: number): void {
    this.showPrevisao = false;
    this.previsaoService.getPrevisao(cidadeId).subscribe({
      next: (data) => {
        this.previsao = data;
        this.showPrevisao = true;
      },
      error: (error) => {
        console.error('Erro ao buscar previsão:', error);
        alert('Erro ao buscar previsão. Verifique se a API Key do OpenWeatherMap está configurada.');
      }
    });
  }

  getUsuarioNome(usuarioId: number): string {
    const usuario = this.usuarios.find(u => u.id === usuarioId);
    return usuario ? usuario.nome : 'N/A';
  }
}
