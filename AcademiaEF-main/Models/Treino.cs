namespace Academia.Models
{
    public class Treino
    {
        public int TreinoId { get; set; }
        public int PersonalId { get; set; }
        public int AlunoId { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public Aluno Aluno { get; set; }
        public ICollection<Exercicio> Exercicios { get; set; }
    }
}
