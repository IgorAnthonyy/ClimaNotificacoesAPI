export interface Usuario {
  id?: number;
  nome: string;
  email: string;
  senha?: string;
  telefone: string;
}

export interface UsuarioRequest {
  nome: string;
  email: string;
  senha: string;
  telefone: string;
}

export interface LoginRequest {
  email: string;
  senha: string;
}

export interface LoginResponse {
  usuario: Usuario;
  token: string;
}
