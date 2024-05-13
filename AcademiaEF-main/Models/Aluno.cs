namespace Academia.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Instagram { get; set; }
        public string Telefone { get; set; }

        public int PersonalId { get; set; }

        public string Observacoes { get; set; }

        public Personal Personal { get; set; }
        public ICollection<Treino> Treinos { get; set; }
    }
}
