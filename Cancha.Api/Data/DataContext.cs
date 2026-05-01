using Microsoft.EntityFrameworkCore;
using Cancha.Shared.Entities;
using CanchaEntidad = Cancha.Shared.Entities.Cancha; //Por conflicto con nombramiento

namespace Cancha.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CanchaEntidad> Canchas { get; set; }
        public DbSet<HorarioDisponible> HorariosDisponibles { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.HasIndex(c => c.Cedula).IsUnique();
                entity.HasIndex(e => e.Correo).IsUnique();
            });

            modelBuilder.Entity<CanchaEntidad>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.ToTable(tb => tb.HasCheckConstraint(
                    "CK_Cancha_Tipo",
                    "Tipo IN ('Futbol', 'Futbol Sala', 'Tennis', 'Baloncesto', 'Voleibol', 'Padel')"
                ));
                entity.Property(c => c.Activa).HasDefaultValue(true); //Valor por defecto en la base de datos
            });

            modelBuilder.Entity<HorarioDisponible>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => new { e.CanchaId, e.DiaSemana, e.HoraInicio })
                    .IsUnique(); //Restriccion en la base de datos para que los registros sean verdaderamente unicos
                entity.ToTable(tb => tb.HasCheckConstraint(
                    "CK_HorarioDisponible_DiaSemana",
                    "DiaSemana IN (0, 1, 2, 3, 4, 5, 6)"
                ));
                entity.Property(c => c.Activo).HasDefaultValue(true);
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();

                entity.HasIndex(r => new { r.CanchaId, r.Fecha, r.HoraInicio })
                    .IsUnique();

                entity.ToTable(tb => tb.HasCheckConstraint(
                    "CK_Reserva_Estado",
                    "Estado IN ('Pendiente', 'Confirmada', 'Completada', 'Cancelada')"
                ));

                entity.ToTable(tb => tb.HasCheckConstraint(
                    "CK_Reserva_HoraFin",
                    "HoraFin > HoraInicio"
                ));
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(p => p.FechaPago)
                    .HasDefaultValueSql("GETDATE()");  //Default fecha actual en bd

                entity.ToTable(tb => tb.HasCheckConstraint(
                    "CK_Pago_MetodoPago",
                    "MetodoPago IN ('Efectivo', 'Transferencia', 'Tarjeta')"
                ));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
