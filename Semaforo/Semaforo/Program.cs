using System;
using System.Threading;

namespace SimuladorSemaforo
{
    public class Semaforo
    {
        private string colorActual;
        private int segundosEnColor;
        private bool modoIntermitente;

        private string colorGuardado;
        private int segundosGuardados;

        public Semaforo(string colorInicial)
        {
            colorActual = colorInicial;
            segundosEnColor = 0;
            modoIntermitente = false;

            colorGuardado = colorInicial;
            segundosGuardados = 0;
        }

        public void pasoDelTiempo(int segundos)
        {
            for (int i = 0; i < segundos; i++)
            {
                AvanzarUnSegundo();
            }
        }

        public void mostrarColor()
        {
            switch (colorActual)
            {
                case "Rojo": Console.ForegroundColor = ConsoleColor.Red; break;
                case "Verde": Console.ForegroundColor = ConsoleColor.Green; break;
                case "Amarillo": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Rojo + Amarillo": Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                default: Console.ResetColor(); break;
            }

            Console.WriteLine($"LUZ ACTUAL: {colorActual.ToUpper()}");
            Console.ResetColor();

            Console.WriteLine($"Tiempo transcurrido en este color: {segundosEnColor}s");
        }

        public void ponerEnIntermitente()
        {
            if (!modoIntermitente)
            {
                colorGuardado = colorActual;
                segundosGuardados = segundosEnColor;

                modoIntermitente = true;
                colorActual = "Amarillo";
            }
        }

        public void sacarDeIntermitente()
        {
            if (modoIntermitente)
            {
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
                segundosEnColor++;
                int limiteDeTiempo = ObtenerDuracion(colorActual);

                if (segundosEnColor >= limiteDeTiempo)
                {
                    CambiarSiguienteColor();
                    segundosEnColor = 0;
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
}

namespace SimuladorSemaforo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=== CONFIGURACIÓN DEL SEMÁFORO ===");
            Console.WriteLine("Escribe el color de inicio (Rojo, Verde, Amarillo):");
            string colorInicial = Console.ReadLine();

            if (string.IsNullOrEmpty(colorInicial)) colorInicial = "Rojo";

            Semaforo miSemaforo = new Semaforo(colorInicial);
            bool continuar = true;

            Console.Clear();
            Console.WriteLine("Simulación lista. Controles:");
            Console.WriteLine("- Presiona 'I' para modo INTERMITENTE");
            Console.WriteLine("- Presiona 'N' para modo NORMAL");
            Console.WriteLine("- Presiona 'ESC' para SALIR");
            Thread.Sleep(3000);

            while (continuar)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.I:
                            miSemaforo.ponerEnIntermitente();
                            break;
                        case ConsoleKey.N:
                            miSemaforo.sacarDeIntermitente();
                            break;
                        case ConsoleKey.Escape:
                            continuar = false;
                            break;
                    }
                }

                Console.Clear();
                Console.WriteLine("=== SEMÁFORO EN EJECUCIÓN ===");
                Console.WriteLine("[I] Intermitente | [N] Normal | [ESC] Salir");
                Console.WriteLine("------------------------------------------");

                miSemaforo.mostrarColor();

                miSemaforo.pasoDelTiempo(1);
                Thread.Sleep(1000);
            }

            Console.WriteLine("\nPrograma finalizado. Presiona Enter para cerrar.");
            Console.ReadLine();
        }
    }
}