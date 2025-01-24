using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string? Descricao { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? PrazoConclusao { get; set; }

        [Required]
        public string Status { get; set; } = "Pendente";

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}