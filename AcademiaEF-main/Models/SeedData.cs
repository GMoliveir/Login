using Microsoft.EntityFrameworkCore;

namespace Academia.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            // Obtém o contexto do serviço fornecido.
            using (var context = new Context(
                serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
            {
                // Aplica as migrações pendentes para garantir que o banco de dados esteja atualizado.
                context.Database.Migrate();

                // Se não houver pessoais no banco de dados, insere dados iniciais.
                if (!context.Personais.Any())
                {
                    context.Personais.AddRange(
                        new Personal { Nome = "Fernando", Especialidade = "Musculação" },
                        new Personal { Nome = "Juliana", Especialidade = "Funcional" }
                    );
                    context.SaveChanges();
                }
                // Se não houver alunos no banco de dados, insere dados iniciais.
                if (!context.Alunos.Any())
                {
                    var personalIds = context.Personais.Select(p => p.PersonalId).ToList();
                    context.Alunos.AddRange(
                        new Aluno
                        {
                            Nome = "Maria",
                            DataNascimento = new DateTime(1990, 1, 15),
                            Email = "maria@example.com",
                            Instagram = "maria_insta",
                            Telefone = "987654321",
                            PersonalId = personalIds.FirstOrDefault(),
                            Observacoes = "Aluna dedicada e comprometida"
                        },
                        new Aluno
                        {
                            Nome = "Carlos",
                            DataNascimento = new DateTime(1985, 5, 20),
                            Email = "carlos@example.com",
                            Instagram = "carlos_insta",
                            Telefone = "123456789",
                            PersonalId = personalIds.LastOrDefault(),
                            Observacoes = "Aluno esforçado e motivado"
                        },
                        new Aluno
                        {
                            Nome = "Ana",
                            DataNascimento = new DateTime(1992, 8, 10),
                            Email = "ana@example.com",
                            Instagram = "ana_insta",
                            Telefone = "999888777",
                            PersonalId = personalIds.FirstOrDefault(),
                            Observacoes = "Aluna em busca de condicionamento físico"
                        },
                        new Aluno
                        {
                            Nome = "Lucas",
                            DataNascimento = new DateTime(1986, 12, 5),
                            Email = "lucas@example.com",
                            Instagram = "lucas_insta",
                            Telefone = "444555666",
                            PersonalId = personalIds.LastOrDefault(),
                            Observacoes = "Aluno focado em ganho de massa muscular"
                        },
                        new Aluno
                        {
                            Nome = "Fernanda",
                            DataNascimento = new DateTime(1990, 3, 25),
                            Email = "fernanda@example.com",
                            Instagram = "fernanda_insta",
                            Telefone = "111222333",
                            PersonalId = personalIds.FirstOrDefault(),
                            Observacoes = "Aluna determinada a perder peso"
                        }
                    );
                    context.SaveChanges();
                }
                
                // Se não houver exercícios no banco de dados, insere dados iniciais.
                if (!context.Exercicios.Any())
                {
                    context.Exercicios.AddRange(
                        new Exercicio { Nome = "Flexão de Braço", Categoria = "Peitoral", Descricao = "Exercício para o peitoral e tríceps" },
                        new Exercicio { Nome = "Leg Press", Categoria = "Pernas", Descricao = "Exercício para os quadríceps e glúteos" }
                        // Adicione mais exercícios conforme necessário
                    );
                    context.SaveChanges();
                }
                
                // Se não houver treinos no banco de dados, insere dados iniciais.
                if (!context.Treinos.Any())
                {
                    var personalIds = context.Personais.Select(p => p.PersonalId).ToList();
                    var alunoIds = context.Alunos.Select(p => p.AlunoId).ToList();
                    context.Treinos.AddRange(
                        new Treino { PersonalId = personalIds.FirstOrDefault(), AlunoId = alunoIds.FirstOrDefault(), Data = new DateTime(2024, 4, 1, 12, 24, 44) }
                    );
                    context.SaveChanges();
                }

                // Salva as alterações no banco de dados.
                context.SaveChanges();
            }
        }
    }
}
