using System;

namespace SimuladorDeportes
{
    class Program
    {
        static void Main(string[] args)
        {
            IJugador pibe = new Amateur();
            IJugador pro = new Profesional();

            Console.WriteLine($"Amateur corre 15m: {pibe.correr(15)}");
            Console.WriteLine($"Pro corre 35m: {pro.correr(35)}");

            Console.ReadLine();
        }
    }
}