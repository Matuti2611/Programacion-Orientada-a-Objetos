using System;

namespace SimuladorSemaforo
{
    public class Semaforo
    {
        // Atributos internos (estado)
        private string colorActual;
        private int segundosEnColor;
        private bool modoIntermitente;

        // Variables para guardar el estado normal cuando pasamos a intermitente
        private string colorGuardado;
        private int segundosGuardados;

        public Semaforo(string colorInicial)
        {
            this.colorActual = colorInicial;
            this.segundosEnColor = 0;
            this.modoIntermitente = false;

            // Inicializamos el estado guardado por seguridad
            this.colorGuardado = colorInicial;
            this.segundosGuardados = 0;
        }

        // Método requerido 1: Avanzar el tiempo
        public void pasoDelTiempo(int segundos)
        {
            for (int i = 0; i < segundos; i++)
            {
                AvanzarUnSegundo();
            }
        }

        // Método requerido 2: Mostrar el color
        public void mostrarColor()
        {
            Console.WriteLine($"Color actual: {colorActual}");
        }

        // Método requerido 3: Activar intermitente
        public void ponerEnIntermitente()
        {
            if (!modoIntermitente)
            {
                // Guardamos en qué punto exacto de la secuencia estábamos
                colorGuardado = colorActual;
                segundosGuardados = segundosEnColor;

                // Activamos el modo y forzamos el color inicial
                modoIntermitente = true;
                colorActual = "Amarillo";
            }
        }

        // Método requerido 4: Desactivar intermitente
        public void sacarDeIntermitente()
        {
            if (modoIntermitente)
            {
                // Restauramos la secuencia normal desde donde la dejamos
                modoIntermitente = false;
                colorActual = colorGuardado;
                segundosEnColor = segundosGuardados;
            }
        }

        // --- MÉTODOS PRIVADOS (Encapsulamiento de la lógica interna) ---

        private void AvanzarUnSegundo()
        {
            if (modoIntermitente)
            {
                // Lógica del modo intermitente (alterna cada segundo)
                if (colorActual == "Amarillo")
                {
                    colorActual = "Apagado";
                }
                else
                {
                    colorActual = "Amarillo";
                }
            }
            else
            {
                // Lógica de la secuencia normal
                segundosEnColor++;
                int limiteDeTiempo = ObtenerDuracion(colorActual);

                // Si ya cumplió su ciclo, cambiamos al siguiente color
                if (segundosEnColor >= limiteDeTiempo)
                {
                    CambiarSiguienteColor();
                    segundosEnColor = 0; // Reiniciamos el contador para el nuevo color
                }
            }
        }

        private int ObtenerDuracion(string color)
        {
            switch (color)
            {
                case "Rojo": return 30;
                case "Rojo + Amarillo": return 2;
                case "Verde": return 20;
                case "Amarillo": return 2;
                default: return 1;
            }
        }

        private void CambiarSiguienteColor()
        {
            switch (colorActual)
            {
                case "Rojo":
                    colorActual = "Rojo + Amarillo";
                    break;
                case "Rojo + Amarillo":
                    colorActual = "Verde";
                    break;
                case "Verde":
                    colorActual = "Amarillo";
                    break;
                case "Amarillo":
                    colorActual = "Rojo";
                    break;
            }
        }
    }

    // Clase principal para probar el código
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Iniciando Semáforo en Verde ---");
            Semaforo semaforo = new Semaforo("Verde");
            semaforo.mostrarColor(); // Verde

            Console.WriteLine("\n--- Avanzando 20 segundos ---");
            semaforo.pasoDelTiempo(20);
            semaforo.mostrarColor(); // Amarillo

            Console.WriteLine("\n--- Avanzando 2 segundos ---");
            semaforo.pasoDelTiempo(2);
            semaforo.mostrarColor(); // Rojo

            Console.WriteLine("\n--- Avanzando 15 segundos (mitad del rojo) ---");
            semaforo.pasoDelTiempo(15);
            semaforo.mostrarColor(); // Sigue en Rojo

            Console.WriteLine("\n--- Activando Intermitente por 3 segundos ---");
            semaforo.ponerEnIntermitente();
            semaforo.mostrarColor(); // Amarillo
            semaforo.pasoDelTiempo(1);
            semaforo.mostrarColor(); // Apagado
            semaforo.pasoDelTiempo(1);
            semaforo.mostrarColor(); // Amarillo
            semaforo.pasoDelTiempo(1);
            semaforo.mostrarColor(); // Apagado

            Console.WriteLine("\n--- Desactivando Intermitente ---");
            semaforo.sacarDeIntermitente();
            semaforo.mostrarColor(); // Debería volver a Rojo (habían pasado 15s)

            Console.WriteLine("\n--- Avanzando 15 segundos más ---");
            semaforo.pasoDelTiempo(15);
            semaforo.mostrarColor(); // Debería cambiar a Rojo + Amarillo (completó los 30s)

            Console.ReadLine(); // Para que la consola no se cierre al terminar
        }
    }
}