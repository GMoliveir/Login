using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Academia.Models;


namespace Academia.Models
{
    public class Context:DbContext
    {
        /*o método contrutor usa o objeto options da superclasse
         para buscar as configurações de conexão com o BD */
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        /*Para que os modelos sejam mapeados como tabelas no BD,
         declare-os como DbSet*/
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Personal> Personais { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da relação muitos para um entre Aluno e Personal
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Personal)
                .WithMany(p => p.Alunos)
                .HasForeignKey(a => a.PersonalId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Academia.Models.Usuario>? Usuario { get; set; }
    }
}
    