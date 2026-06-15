using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinica_Medica
{
    public class GestorTurnos
    {
        public void cancelarTurno(ClinicaContext context, Paciente paciente)
        {
            Console.WriteLine("Dia del Turno: ");
            string dia = Console.ReadLine() ?? "";
            Console.WriteLine("Hora de Turno: ");
            string hora = Console.ReadLine() ?? "";

            Turno? turnoACancelar = context.turnos.FirstOrDefault(t =>
             t.paciente.dni == paciente.dni &&
             t.fecha == dia &&
             t.hora == hora);

            if (turnoACancelar == null)
            {
                Console.WriteLine("No se pudo cancelar el turno");
            }
            else
            {
                turnoACancelar.cancelarTurno(context);
                Console.WriteLine("Turno cancelado exitosamente.");
            }
        }

        public Especialidad EleccionEspecialidad(ClinicaContext context)
        {
            Console.WriteLine("Especialidades disponibles: ");
            foreach (Especialidad especialidad in context.especialidades)
            {
                Console.WriteLine($"{especialidad.nombre}");
            }
            Console.WriteLine("Ingrese la especialidad deseada: ");
            string especialidadDeseada = (Console.ReadLine() ?? "").ToLower();
            Especialidad? especialidadElegida = context.especialidades.FirstOrDefault(e => e.nombre.ToLower() == especialidadDeseada);
            while (especialidadElegida == null)
            {
                Console.WriteLine("Especialidad no encontrada, ingresela nuevamente: ");
                especialidadDeseada = (Console.ReadLine() ?? "").ToLower();
                especialidadElegida = context.especialidades.FirstOrDefault(e => e.nombre.ToLower() == especialidadDeseada);
            }
            return especialidadElegida;
        }

        public Medico EleccionMedico(ClinicaContext context, int idEspecialidad)
        {
            var medicosDisponibles = context.medicoEspecialidades
                .Where(me => me.idEspecialidad == idEspecialidad)
                .Select(me => me.medico)
                .ToList();

            Console.WriteLine("Medicos disponibles para la especialidad seleccionada: ");
            foreach (var medico in medicosDisponibles) 
            {
                Console.WriteLine($"{medico.nombre} {medico.apellido}");
            }
            Console.WriteLine("Ingrese el nombre del medico deseado: ");
            string nombreMedico = (Console.ReadLine() ?? "").ToLower();
            Console.WriteLine("Ingrese el apellido del medico deseado: ");
            string apellidoMedico = (Console.ReadLine() ?? "").ToLower();
            Medico? medicoElegido = context.medicos.FirstOrDefault(m => m.nombre.ToLower() == nombreMedico && m.apellido.ToLower() == apellidoMedico);
            while (medicoElegido == null)
            {
                Console.WriteLine("Medico no encontrado, ingreselo nuevamente: ");
                Console.WriteLine("Ingrese el nombre del medico: ");
                nombreMedico = (Console.ReadLine() ?? "").ToLower();
                Console.WriteLine("Ingrese el apellido del medico: ");
                apellidoMedico = (Console.ReadLine() ?? "").ToLower();
                medicoElegido = context.medicos.FirstOrDefault(m => m.nombre.ToLower() == nombreMedico && m.apellido.ToLower() == apellidoMedico);
            }
            return medicoElegido;
        }

        public (string Fecha, string Hora) EleccionFechaHora(ClinicaContext context, int idEspecialidad, int matriculaMedico)
        {
            Console.WriteLine("A continuacion se diran los horarios de atencion del medico en la especialidad elegida: ");
            string[] nombresDias = { "", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };

            foreach (Disponibilidad disponibilidad in context.disponibilidades)
            {
                if (disponibilidad.matricula == matriculaMedico && disponibilidad.idEspecialidad == idEspecialidad)
                {
                    string diaEnLetras = nombresDias[disponibilidad.diaSemana];
                    Console.WriteLine($"Dia de atencion: {diaEnLetras} Horario:{disponibilidad.horaInicio} a {disponibilidad.horaFin}");
                }
            }
            Console.WriteLine("Ingrese Fecha del Turno deseada (ej: 2026-06-20): ");
            string fechaString = Console.ReadLine() ?? "";

            DateTime fechaParseada;

            while (!DateTime.TryParse(fechaString, out fechaParseada))
            {
                Console.WriteLine("Error: Formato de fecha invalido. Ingrésela nuevamente (ej: 2026-10-25): ");
                fechaString = Console.ReadLine() ?? "";
            }

            int numeroDia = (int)fechaParseada.DayOfWeek;
            bool medicoAtiende = context.disponibilidades.Any(d => d.matricula == matriculaMedico && d.diaSemana == numeroDia);
            if (!medicoAtiende)
            {
                Console.WriteLine("El médico no atiende ese día de la semana. Vuelva a elegir la Fecha");
                return EleccionFechaHora(context, idEspecialidad, matriculaMedico);
            }

            Console.WriteLine("Ingrese la Hora del Turno (ej: 20:30): ");
            string horaString = Console.ReadLine() ?? "";
            TimeSpan horaParseada;

            while (!TimeSpan.TryParseExact(horaString, "h\\:mm", null, out horaParseada))
            {
                Console.WriteLine("Error: Formato de hora invalido. Ingrésela nuevamente (ej: 19:20): ");
                horaString = Console.ReadLine() ?? "";
            }
            var disponibilidadesDelDia = context.disponibilidades
            .Where(d => d.matricula == matriculaMedico && d.diaSemana == numeroDia)
            .ToList();
          
            bool horaValida = disponibilidadesDelDia.Any(d =>
                horaParseada >= TimeSpan.Parse(d.horaInicio) &&
                horaParseada <= TimeSpan.Parse(d.horaFin));

            if (!horaValida)
            {
                Console.WriteLine("El médico no atiende en ese horario. Vuelva a elegir la Fecha y Hora.");
                return EleccionFechaHora(context, idEspecialidad, matriculaMedico);
            }
            bool turnoOcupado = context.turnos.Any(t =>
            t.medico.matricula == matriculaMedico &&
            t.fecha == fechaString &&
            t.hora == horaString &&
            t.estado.descripcion != "cancelado");

            if (turnoOcupado)
            {
                Console.WriteLine("Ese horario ya está ocupado. Vuelva a elegir la Fecha y Hora.");
                return EleccionFechaHora(context, idEspecialidad, matriculaMedico);
            }
            return (fechaString, horaString);
        }

        public void ReservaTurno(ClinicaContext context, int dni)
        {
            Especialidad especialidadTurno = EleccionEspecialidad(context);
            int idEspecialidad = especialidadTurno.idEspecialidad;
            Medico medicoTurno = EleccionMedico(context, idEspecialidad);
            int matriculaMedico = medicoTurno.matricula;
            var (fechaElegida, horaElegida) = EleccionFechaHora(context, idEspecialidad, matriculaMedico);
            Console.WriteLine("A continuacion los datos del Turno: ");
            Console.WriteLine($"Medico: {medicoTurno.apellido} {medicoTurno.nombre} Especialidad: {especialidadTurno.nombre}");
            Console.WriteLine($"Fecha Turno:{fechaElegida} Hora Turno:{horaElegida}");

            Console.WriteLine("Desea confirmar el turno(Contestar con Si/No): ");
            string respuesta = (Console.ReadLine() ?? "").ToLower();
            if (respuesta == "si")
            {
                Turno nuevoTurno = new Turno(context, dni, matriculaMedico, idEspecialidad, fechaElegida, horaElegida);
                context.turnos.Add(nuevoTurno);
                context.SaveChanges();
                Console.WriteLine("Turno Creado con Exito");
            }
        }

        public Paciente RegistroPaciente(int dni, ClinicaContext context)
        {
            Console.WriteLine("Ingrese nombre:");
            string nombre = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese apellido:");
            string apellido = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese telefono:");
            string telefono = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese email:");
            string mail = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese fecha de nacimiento:");
            string fechaNacimiento = Console.ReadLine() ?? "";
            Paciente nuevoPaciente = new Paciente(dni, nombre, apellido, telefono, mail, fechaNacimiento);
            context.pacientes.Add(nuevoPaciente);
            context.SaveChanges();
            return nuevoPaciente;
        }
    }
}