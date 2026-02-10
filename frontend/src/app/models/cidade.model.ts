export interface Cidade {
  id?: number;
  nome: string;
  usuarioId: number;
}

export interface CidadeRequest {
  nome: string;
  usuarioId: number;
}
