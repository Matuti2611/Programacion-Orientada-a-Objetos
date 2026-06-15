using Microsoft.EntityFrameworkCore;

namespace Clinica_Medica
{
    public class ClinicaContext : DbContext
    {
        public DbSet<Especialidad> especialidades { get; set; }
        public DbSet<Medico> medicos { get; set; }
        public DbSet<MedicoEspecialidad> medicoEspecialidades { get; set; }
        public DbSet<Paciente> pacientes { get; set; }
        public DbSet<Estado> estados { get; set; }
        public DbSet<Turno> turnos { get; set; }
        public DbSet<Disponibilidad> disponibilidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ClinicaMedica.db");
            options.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidad>()
                .ToTable("especialidad")
                .HasKey(e => e.idEspecialidad);
            modelBuilder.Entity<Especialidad>()
                .Property(e => e.idEspecialidad).HasColumnName("id_especialidad");
            modelBuilder.Entity<Especialidad>()
                .Property(e => e.duracionTurnoMin).HasColumnName("duracion_turno_min");

            modelBuilder.Entity<Medico>()
                .ToTable("medico")
                .HasKey(m => m.matricula);
            modelBuilder.Entity<Medico>()
                .Property(m => m.activo).HasColumnName("activo");

            modelBuilder.Entity<Paciente>()
                .ToTable("paciente")
                .HasKey(p => p.dni);
            modelBuilder.Entity<Paciente>()
                .Property(p => p.fechaNacimiento).HasColumnName("fecha_nacimiento");

            modelBuilder.Entity<Estado>()
                .ToTable("estado")
                .HasKey(e => e.idEstado);
            modelBuilder.Entity<Estado>()
                .Property(e => e.idEstado).HasColumnName("id_estado");

            modelBuilder.Entity<Disponibilidad>()
                .ToTable("disponibilidad")
                .HasKey(d => new { d.matricula, d.idEspecialidad, d.diaSemana });
            modelBuilder.Entity<Disponibilidad>()
                .Property(d => d.idEspecialidad).HasColumnName("id_especialidad");
            modelBuilder.Entity<Disponibilidad>()
                .Property(d => d.diaSemana).HasColumnName("dia_semana");
            modelBuilder.Entity<Disponibilidad>()
                .Property(d => d.horaInicio).HasColumnName("hora_inicio");
            modelBuilder.Entity<Disponibilidad>()
                .Property(d => d.horaFin).HasColumnName("hora_fin");
            modelBuilder.Entity<Disponibilidad>()
                .HasOne(d => d.medico)
                .WithMany()
                .HasForeignKey(d => d.matricula);
            modelBuilder.Entity<Disponibilidad>()
                .HasOne(d => d.especialidad)
                .WithMany()
                .HasForeignKey(d => d.idEspecialidad);

            modelBuilder.Entity<Turno>()
                .ToTable("turno")
                .HasKey(t => new { t.dni, t.matricula, t.idEspecialidad });
            modelBuilder.Entity<Turno>()
                .Property(t => t.idEspecialidad).HasColumnName("id_especialidad");
            modelBuilder.Entity<Turno>()
                .Property(t => t.idEstado).HasColumnName("id_estado");
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.paciente)
                .WithMany()
                .HasForeignKey(t => t.dni);
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.medico)
                .WithMany()
                .HasForeignKey(t => t.matricula);
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.especialidad)
                .WithMany()
                .HasForeignKey(t => t.idEspecialidad);
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.estado)
                .WithMany()
                .HasForeignKey(t => t.idEstado);
                
            modelBuilder.Entity<MedicoEspecialidad>()
                .ToTable("medico_especialidad") 
                .HasKey(me => new { me.matricula, me.idEspecialidad });
            modelBuilder.Entity<MedicoEspecialidad>()
                .Property(me => me.idEspecialidad).HasColumnName("id_especialidad");
            modelBuilder.Entity<MedicoEspecialidad>()
                .HasOne(me => me.medico)
                .WithMany()
                .HasForeignKey(me => me.matricula);
            modelBuilder.Entity<MedicoEspecialidad>()
                .HasOne(me => me.especialidad)
                .WithMany()
                .HasForeignKey(me => me.idEspecialidad);
        }
    }
}