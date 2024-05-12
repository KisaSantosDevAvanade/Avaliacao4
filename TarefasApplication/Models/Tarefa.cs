using System.ComponentModel.DataAnnotations;

namespace TarefasApplication.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }

        [Required]
        public string? TarefaNome { get; set; }
        public string? StatusTarefa { get; set; }
        public DateTime? InicioTarefa { get; set; } = DateTime.Now;
        public DateTime? ConclusaoTarefa { get; set; } = null;
    }
}
