using System;
using System.Threading;

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