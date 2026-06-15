namespace Clinica_Medica
{
    public class Disponibilidad
    {
        public int matricula { get; set; }
        public int idEspecialidad { get; set; }
        public int diaSemana { get; set; }
        public string horaInicio { get; set; } = "";
        public string horaFin { get; set; } = "";

        public Medico medico { get; set; } = null!;
        public Especialidad especialidad { get; set; } = null!;
    }
}
