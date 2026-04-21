namespace SimuladorDeportes
{
    public class Profesional : IJugador
    {
        private int minutosAcumulados = 0;
        private bool estadoCansado = false;
        private const int LIMITE_MINUTOS = 40;

        public bool correr(int minutos)
        {
            if (estadoCansado || minutosAcumulados + minutos > LIMITE_MINUTOS)
            {
                estadoCansado = true;
                return false;
            }
            minutosAcumulados += minutos;
            if (minutosAcumulados == LIMITE_MINUTOS) estadoCansado = true;
            return true;
        }

        public bool cansado() => estadoCansado;

        public void descansar(int minutos)
        {
            minutosAcumulados = Math.Max(0, minutosAcumulados - minutos);
            if (minutosAcumulados < LIMITE_MINUTOS) estadoCansado = false;
        }
    }
}