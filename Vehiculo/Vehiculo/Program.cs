using System;

namespace Vehiculo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- PRUEBA BÁSICA ---");
            Auto fiat = new Auto(45);
            Bicicleta bici = new Bicicleta();
            Camion camion = new Camion();

            bici.mover(20);
            Console.WriteLine($"Posición bici tras 20s: {bici.posicion()}m");

            bici.mover(10);
            Console.WriteLine($"Posición bici tras 10s más: {bici.posicion()}m");


            Carrera pista = new Carrera();

            Console.WriteLine("\nCompetencia: Auto vs Camion");
            pista.competir(fiat, camion, 10);

            Auto autoEstandar = new Auto(); 
            Console.WriteLine("\nCompetencia: Bicicleta vs Auto Estándar");
            pista.competir(bici, autoEstandar, 30);

            Console.ReadLine();
        }
    }
}