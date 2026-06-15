using System;
using System.Linq;

namespace Clinica_Medica
{
    public class Turno
    {
        public int dni { get; set; }
        public int matricula { get; set; }
        public int idEspecialidad { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public int idEstado { get; set; }
        public string? observaciones { get; set; }

        public void cancelarTurno(ClinicaContext context)
        {
            var estadoCancelado = context.estados.FirstOrDefault(e => e.descripcion == "cancelado");
            if (estadoCancelado != null)
            {
                idEstado = estadoCancelado.idEstado;
                context.SaveChanges();
            }
        }

        public Turno(ClinicaContext context, int dni, int matricula, int idEspecialidad, string fecha, string hora)
        {
            this.dni = dni;
            this.matricula = matricula;
            this.idEspecialidad = idEspecialidad;
            this.fecha = fecha;
            this.hora = hora;
            
            var estadoReservado = context.estados.FirstOrDefault(e => e.descripcion == "reservado");
            if (estadoReservado != null)
            {
                idEstado = estadoReservado.idEstado;
            }
        }

        public Paciente paciente { get; set; } = null!;
        public Medico medico { get; set; } = null!;
        public Especialidad especialidad { get; set; } = null!;
        public Estado estado { get; set; } = null!;
    }
}
