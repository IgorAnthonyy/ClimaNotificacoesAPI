using System.ComponentModel.DataAnnotations;

namespace ClimaNotificacoesAPI.Application.Dtos;

public class CidadeDTORequest
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo UsuarioId é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo UsuarioId deve ser um número positivo.")]
    public int UsuarioId { get; set; }
}
