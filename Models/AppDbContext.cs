using Microsoft.EntityFrameworkCore;

namespace md_apis_web_services_fuel_manager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<VeiculoUsuario> VeiculoUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VeiculoUsuario>()
                .HasKey(vu => new { vu.VeiculoId, vu.UsuarioId });

            modelBuilder.Entity<VeiculoUsuario>()
                .HasOne(vu => vu.Veiculo)
                .WithMany(v => v.VeiculoUsuarios)
                .HasForeignKey(vu => vu.VeiculoId);

            modelBuilder.Entity<VeiculoUsuario>()
                .HasOne(vu => vu.Usuario)
                .WithMany(u => u.VeiculoUsuarios)
                .HasForeignKey(vu => vu.UsuarioId);
        }
    }
}