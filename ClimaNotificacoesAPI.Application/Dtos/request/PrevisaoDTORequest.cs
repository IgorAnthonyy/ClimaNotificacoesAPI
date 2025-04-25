using System.ComponentModel.DataAnnotations;

namespace ClimaNotificacoesAPI.Application.Dtos;

    public class PrevisaoDTORequest
    {
        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo Data deve ser uma data válida.")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "O campo Condição é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Condição deve ter no máximo 100 caracteres.")]
        public string Condicao { get; set; }
        [Required(ErrorMessage = "O campo Temperatura Máxima é obrigatório.")]
        [Range(-100, 100, ErrorMessage = "O campo Temperatura Máxima deve estar entre -100 e 100.")]
        public double TemperaturaMaxima { get; set; }
        [Required(ErrorMessage = "O campo Temperatura Mínima é obrigatório.")]
        [Range(-100, 100, ErrorMessage = "O campo Temperatura Mínima deve estar entre -100 e 100.")]
        public double TemperaturaMinima { get; set; }
        [Required(ErrorMessage = "O campo Umidade é obrigatório.")]
        [Range(0, 100, ErrorMessage = "O campo Umidade deve estar entre 0 e 100.")]
        public double Umidade { get; set; }
        [Required(ErrorMessage = "O campo Velocidade do Vento é obrigatório.")]
        [Range(0, 100, ErrorMessage = "O campo Velocidade do Vento deve estar entre 0 e 100.")]
        public double VelocidadeVento { get; set; }
        [Required(ErrorMessage = "O campo CidadeId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo CidadeId deve ser um número positivo.")]
        public int CidadeId { get; set; }
        public CidadeDTORequest Cidade { get; set; }

}
