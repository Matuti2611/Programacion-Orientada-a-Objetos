using System;

namespace SimuladorVehiculos
{
    public class Carrera
    {
        public void Competir(IVehiculo v1, IVehiculo v2, int segundos)
        {
            v1.reiniciarPosicion();
            v2.reiniciarPosicion();

            v1.mover(segundos);
            v2.mover(segundos);

            int posV1 = v1.posicion();
            int posV2 = v2.posicion();

            Console.WriteLine($"\n--- RESULTADOS DE LA CARRERA ({segundos} segundos) ---");
            Console.WriteLine($"Distancia Vehículo 1: {posV1} metros");
            Console.WriteLine($"Distancia Vehículo 2: {posV2} metros");

            Console.ForegroundColor = ConsoleColor.Magenta;

            if (posV1 > posV2)
            {
                Console.WriteLine("¡El Vehículo 1 es el GANADOR!");
            }
            else if (posV2 > posV1)
            {
                Console.WriteLine("¡El Vehículo 2 es el GANADOR!");
            }
            else
            {
                Console.WriteLine("¡Es un EMPATE exacto!");
            }

            Console.ResetColor();
        }
    }
}