using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}