using InvestControl.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace InvestControl.Infra.Context
{
    public class InvestControlContext : DbContext
    {
        public InvestControlContext(DbContextOptions<InvestControlContext> options) : base(options)
        {
        }

        DbSet<Corretora> Corretoras { get; set; }
        DbSet<Transacao> Transacoes { get; set; }
        DbSet<Evento> Eventos { get; set; }
        DbSet<Rendimento> Rendimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Corretora>().ToTable("Corretora");
            modelBuilder.Entity<Corretora>().Property(e => e.NomeFantasia).IsRequired(false);
            modelBuilder.Entity<Corretora>().Property(e => e.RazaoSocial).IsRequired();
            modelBuilder.Entity<Corretora>().Property(e => e.CNPJ).IsRequired(false);


            modelBuilder.Entity<Transacao>().ToTable("Transacao");
            modelBuilder.Entity<Transacao>().Property(e => e.DataOperacao).IsRequired();
            modelBuilder.Entity<Transacao>().Property(e => e.TipoCategoria).IsRequired();
            modelBuilder.Entity<Transacao>().Property(e => e.CodigoAtivo).IsRequired();
            modelBuilder.Entity<Transacao>().Property(e => e.TipoOperacao).IsRequired();
            modelBuilder.Entity<Transacao>().Property(e => e.Quantidade)
                .HasPrecision(19, 4)
                .IsRequired();
            modelBuilder.Entity<Transacao>().Property(e => e.PrecoUnitario)
                .HasPrecision(19,6)
                .IsRequired();
            modelBuilder.Entity<Transacao>()
                .HasOne(e => e.Corretora)
                .WithMany()
                .HasForeignKey(e => e.CorretoraId);
            
            modelBuilder.Entity<Evento>().ToTable("Evento");
            modelBuilder.Entity<Evento>().HasKey(e => e.Id);
            modelBuilder.Entity<Evento>().Property(e => e.CodigoOrigem).IsRequired();
            modelBuilder.Entity<Evento>().Property(e => e.CodigoDestino).IsRequired();
            modelBuilder.Entity<Evento>().Property(e => e.FatorBase).IsRequired();
            modelBuilder.Entity<Evento>().Property(e => e.FatorGanho).IsRequired();
            modelBuilder.Entity<Evento>().Property(e => e.Valor).IsRequired();
            modelBuilder.Entity<Evento>().Property(e => e.Data).IsRequired();
            modelBuilder.Entity<Evento>().Property(e => e.TipoEvento).IsRequired();

            modelBuilder.Entity<Rendimento>().ToTable("Rendimento");
            modelBuilder.Entity<Rendimento>().HasKey(e => e.Id);
            modelBuilder.Entity<Rendimento>().Property(e => e.CodigoAtivo).IsRequired();
            modelBuilder.Entity<Rendimento>().Property(e => e.TipoCategoria).IsRequired();
            modelBuilder.Entity<Rendimento>().Property(e => e.Data).IsRequired();
            modelBuilder.Entity<Rendimento>().Property(e => e.Valor).IsRequired();
        }
    }
}
