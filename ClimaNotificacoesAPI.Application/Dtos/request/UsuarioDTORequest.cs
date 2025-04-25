using System.ComponentModel.DataAnnotations;

namespace ClimaNotificacoesAPI.Application.Dtos;

public class UsuarioDTORequest
{   
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de e-mail válido.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Senha deve ter no máximo 100 caracteres.")]
    public string Senha { get; set; }
    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [StringLength(15, ErrorMessage = "O campo Telefone deve ter no máximo 15 caracteres.")]
    public string Telefone { get; set; }
}