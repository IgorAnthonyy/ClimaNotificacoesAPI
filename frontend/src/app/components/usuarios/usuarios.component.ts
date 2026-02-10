import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { Usuario, UsuarioRequest } from '../../models/usuario.model';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './usuarios.component.html',
  styleUrl: './usuarios.component.scss'
})
export class UsuariosComponent implements OnInit {
  usuarios: Usuario[] = [];
  loading = false;
  showForm = false;
  editMode = false;
  selectedUsuario: UsuarioRequest = this.getEmptyUsuario();
  selectedId?: number;

  constructor(private usuarioService: UsuarioService) { }

  ngOnInit(): void {
    this.loadUsuarios();
  }

  loadUsuarios(): void {
    this.loading = true;
    this.usuarioService.getAll().subscribe({
      next: (data) => {
        this.usuarios = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar usuários:', error);
        alert('Erro ao carregar usuários. Verifique se a API está rodando.');
        this.loading = false;
      }
    });
  }

  getEmptyUsuario(): UsuarioRequest {
    return {
      nome: '',
      email: '',
      senha: '',
      telefone: ''
    };
  }

  openCreateForm(): void {
    this.editMode = false;
    this.selectedUsuario = this.getEmptyUsuario();
    this.showForm = true;
  }

  openEditForm(usuario: Usuario): void {
    this.editMode = true;
    this.selectedId = usuario.id;
    this.selectedUsuario = {
      nome: usuario.nome,
      email: usuario.email,
      senha: '',
      telefone: usuario.telefone
    };
    this.showForm = true;
  }

  cancelForm(): void {
    this.showForm = false;
    this.selectedUsuario = this.getEmptyUsuario();
  }

  saveUsuario(): void {
    if (this.editMode && this.selectedId) {
      this.usuarioService.update(this.selectedId, this.selectedUsuario).subscribe({
        next: () => {
          alert('Usuário atualizado com sucesso!');
          this.loadUsuarios();
          this.cancelForm();
        },
        error: (error) => {
          console.error('Erro ao atualizar usuário:', error);
          alert('Erro ao atualizar usuário: ' + (error.error?.message || error.message));
        }
      });
    } else {
      this.usuarioService.create(this.selectedUsuario).subscribe({
        next: () => {
          alert('Usuário criado com sucesso!');
          this.loadUsuarios();
          this.cancelForm();
        },
        error: (error) => {
          console.error('Erro ao criar usuário:', error);
          alert('Erro ao criar usuário: ' + (error.error?.message || error.message));
        }
      });
    }
  }

  deleteUsuario(id: number): void {
    if (confirm('Deseja realmente excluir este usuário?')) {
      this.usuarioService.delete(id).subscribe({
        next: () => {
          alert('Usuário excluído com sucesso!');
          this.loadUsuarios();
        },
        error: (error) => {
          console.error('Erro ao excluir usuário:', error);
          alert('Erro ao excluir usuário: ' + (error.error?.message || error.message));
        }
      });
    }
  }
}
