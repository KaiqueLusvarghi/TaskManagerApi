using System.ComponentModel.DataAnnotations;
using TaskManagerApi.DTOS;

public class TarefaDTO
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; }

    public string Descricao { get; set; }

    [Required]
    public DateTime DataCriacao { get; set; }

    public DateTime? PrazoConclusao { get; set; }

    [Required]
    public string Status { get; set; }

    public int CategoriaId { get; set; }

    // Novo campo para a categoria como um objeto
    public CategoriaDTO Categoria { get; set; }
}
