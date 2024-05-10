namespace TarefasApplication.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        public string? TarefaNome { get; set; }
        public bool StatusTarefa { get; set; }
        public DateTime? InicioTarefa { get; set; } = DateTime.Now;
        public DateTime? ConclusaoTarefa { get; set; }
    }
}
