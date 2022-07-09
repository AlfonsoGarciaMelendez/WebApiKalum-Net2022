using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum
{
    public class KalumDbContext : DbContext
    {
        public DbSet<CarreraTecnica> CarreraTecnica { get; set; }
        public DbSet<Jornada> Jornada { get; set; }
        public DbSet<Aspirante> Aspirante { get; set; }
        public DbSet<ExamenAdmision> ExamenAdmision { get; set; }
        public DbSet<Inscripcion> Inscripcion { get; set; }
        public DbSet<Alumno> Alumno { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CuentasXCobrar> CuentasXCobrar { get; set; }
        public DbSet<InversionCarreraTecnica> InversionCarreraTecnica { get; set; }
        public DbSet<InscripcionPago> InscripcionPago { get; set; }
        public DbSet<ResultadosExamenAdmision> ResultadosExamenAdmision { get; set; }
        public KalumDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraTecnica>().ToTable("CarreraTecnica").HasKey(ct => new{ct.CarreraId});
            modelBuilder.Entity<Jornada>().ToTable("Jornada").HasKey(j => new{j.JornadaId});
            modelBuilder.Entity<ExamenAdmision>().ToTable("ExamenAdmision").HasKey(ex => new {ex.ExamenId});
            modelBuilder.Entity<Aspirante>().ToTable("Aspirante").HasKey(a => new{a.NoExpediente});
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion").HasKey(i => new {i.InscripcionId});
            modelBuilder.Entity<Alumno>().ToTable("Alumno").HasKey(a => new {a.Carne});
            modelBuilder.Entity<Cargo>().ToTable("Cargo").HasKey(c => c.CargoId);
            modelBuilder.Entity<CuentasXCobrar>().ToTable("CuentasXCobrar").HasKey(ca => new{ca.Cargo});
            modelBuilder.Entity<InversionCarreraTecnica>().ToTable("InversionCarreraTecnica").HasKey(i =>new{i.InversionId});
            modelBuilder.Entity<InscripcionPago>().ToTable("InscripcionPago").HasKey(ip => new{ip.BoletaPago});
            modelBuilder.Entity<ResultadosExamenAdmision>().ToTable("ResultadosExamenAdmision").HasKey(r => new{r.NoExpediente});

            modelBuilder.Entity<Aspirante>()
                .HasOne<CarreraTecnica>( a => a.CarreraTecnica)
                .WithMany(ct => ct.Aspirantes)
                .HasForeignKey(c => c.CarreraId);
                
            modelBuilder.Entity<Aspirante>()
                .HasOne<Jornada>( a => a.Jornada)
                .WithMany(j => j.Aspirantes)
                .HasForeignKey(a =>a.JornadaId);
            modelBuilder.Entity<Aspirante>()
                .HasOne<ExamenAdmision>(a => a.ExamenAdmision)
                .WithMany(ex => ex.Aspirantes)
                .HasForeignKey(a =>a.ExamenId);
            modelBuilder.Entity<Inscripcion>()
                .HasOne<CarreraTecnica>( i => i.CarreraTecnica)
                .WithMany(ct => ct.Inscripciones)
                .HasForeignKey(i => i.CarreraId);
            modelBuilder.Entity<Inscripcion>()
                .HasOne<Jornada>( i => i.Jornada)
                .WithMany(j => j.Inscripciones)
                .HasForeignKey( i => i.JornadaId);
            modelBuilder.Entity<Inscripcion>()
                .HasOne<Alumno>( i => i.Alumno)
                .WithMany(a => a.Inscripciones)
                .HasForeignKey(i => i.Carne);
            modelBuilder.Entity<CuentasXCobrar>()
                .HasOne<Alumno>(ca => ca.Alumnos)
                .WithMany(a => a.CuentasXCobrar)
                .HasForeignKey(ca => ca.Carne);
             modelBuilder.Entity<CuentasXCobrar>()
                .HasOne<Cargo>(ca => ca.Cargos)
                .WithMany(c => c.CuentasXCobrar)
                .HasForeignKey(ca => ca.CargoId);
             modelBuilder.Entity<InversionCarreraTecnica>()
                .HasOne<CarreraTecnica>(i => i.CarreraTecnica)
                .WithMany(ct => ct.InversionCarreraTecnica)
                .HasForeignKey(i => i.CarreraId);
             modelBuilder.Entity<InscripcionPago>()
                .HasOne<Aspirante>(ip => ip.Aspirantes)
                .WithMany(a => a.InscripcionPagos)
                .HasForeignKey(ip => ip.NoExpediente);
             modelBuilder.Entity<ResultadosExamenAdmision>()
                .HasOne<Aspirante>(r => r.Aspirantes)
                .WithMany(a => a.ResultadosExamenAdmision)
                .HasForeignKey(r => r.NoExpediente);
                
            
        }

    }
}