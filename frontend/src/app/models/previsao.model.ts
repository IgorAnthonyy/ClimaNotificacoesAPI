export interface PrevisaoTempo {
  id?: number;
  cidadeId: number;
  data: Date;
  condicao: string;
  temperaturaMaxima: number;
  temperaturaMinima: number;
  umidade: number;
  velocidadeVento: number;
}
