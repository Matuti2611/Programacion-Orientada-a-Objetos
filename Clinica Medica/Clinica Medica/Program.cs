using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Clinica_Medica
{
    class Program
    {
        static void Main(string[] args)
        {
            ClinicaContext context = new ClinicaContext();
            var gestorTurnos = new GestorTurnos();

            var turnos = context.turnos
                .Include(t => t.paciente)
                .Include(t => t.medico)
                .Include(t => t.especialidad)
                .Include(t => t.estado)
                .OrderBy(t => t.fecha)
                .ThenBy(t => t.hora)
                .ToList();

            foreach (var t in turnos)
            {
                Console.WriteLine($"{t.fecha} {t.hora} | {t.paciente.nombre} {t.paciente.apellido} | {t.medico.nombre} {t.medico.apellido} | {t.especialidad.nombre} | {t.estado.descripcion}");
            }

            var turnos2 = context.turnos.Where(t => t.estado.descripcion == "cancelado");
            foreach (var turno in turnos2)
            {
                Console.WriteLine($"{turno.estado.descripcion} | {turno.especialidad.nombre} | {turno.paciente.nombre}");
            }

            Paciente? paciente = context.pacientes.FirstOrDefault(p => p.dni == 27888111);
            if (paciente != null)
            {
                Console.WriteLine($"{paciente.nombre} {paciente.apellido}");
            }

            Console.WriteLine("Ingrese Dni del paciente a buscar:");
            int dni = int.Parse(Console.ReadLine() ?? "0");

            Paciente? paciente2 = context.pacientes.FirstOrDefault(p => p.dni == dni);

            if (paciente2 == null)
            {
                Console.WriteLine("No esta en el sistema registrese");
                paciente2 = gestorTurnos.RegistroPaciente(dni, context);
            }
            else
            {
                paciente2.mostrarDatos();
                var turnosDelPaciente = turnos.Where(t => t.paciente.dni == dni);
                   
                Console.WriteLine("Turnos del Paciente:");
                foreach (var turno in turnosDelPaciente)
                {
                    Console.WriteLine($"{turno.fecha} {turno.hora} | {turno.medico.nombre} {turno.medico.apellido} | {turno.especialidad.nombre} | {turno.estado.descripcion}");
                }
                Console.WriteLine("Desea Cancelar un Turno(Responder con Si/No): ");
                string respuesta = (Console.ReadLine() ?? "").ToLower();
                if (respuesta == "si")
                {
                    gestorTurnos.cancelarTurno(context, paciente2);
                }
            }
            Console.WriteLine("Desea Reservar un Nuevo Turno(Responder con Si/No): ");
            string respuesta2 = (Console.ReadLine() ?? "").ToLower();
            if (respuesta2 == "si")
            {
                gestorTurnos.ReservaTurno(context, dni);
            }
        }
    }
}
