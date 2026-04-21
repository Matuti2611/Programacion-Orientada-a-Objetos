using System;

namespace SimuladorCronometro
{
    public class Cronometro
    {
        private int minutos;
        private int segundos;

        public Cronometro()
        {
            minutos = 0;
            segundos = 0;
        }

        public void reiniciar()
        {
            minutos = 0;
            segundos = 0;
        }

        public void incrementarTiempo()
        {
            segundos++;

            if (segundos > 59)
            {
                minutos++;
                segundos = 0;
            }
        }

        public string mostrarTiempo()
        {
            return $"{minutos} minutos, {segundos} segundos";
        }
    }
}