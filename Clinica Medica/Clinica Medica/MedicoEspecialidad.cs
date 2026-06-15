namespace Clinica_Medica
{
    public class MedicoEspecialidad
    {
        public int matricula { get; set; }
        public int idEspecialidad { get; set; }
        public Medico medico { get; set; } = null!;
        public Especialidad especialidad { get; set; } = null!;
    }
}