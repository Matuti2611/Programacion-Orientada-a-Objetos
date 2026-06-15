using System;

namespace Ej5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- PRUEBA 1: CAJA DE AHORRO ---");
            CajaDeAhorro ahorro = new CajaDeAhorro();
            ahorro.depositar(1000);
            ahorro.extraer(400);
            ahorro.extraer(800);
            ahorro.mostrarSaldo();

            Console.WriteLine("\n--- PRUEBA 2: CUENTA CORRIENTE ---");
            CuentaCorriente corriente = new CuentaCorriente(-500);
            corriente.depositar(200);
            corriente.extraer(600);
            corriente.extraer(200);
            corriente.mostrarSaldo();

            Console.WriteLine("\n--- PRUEBA 3: BANCO Y TRANSFERENCIAS ---");
            Banco banco = new Banco();
            
            CajaDeAhorro ahorroBanco = new CajaDeAhorro();
            CuentaCorriente corrienteBanco = new CuentaCorriente(-500);
            
            banco.AgregarCuenta(ahorroBanco);
            banco.AgregarCuenta(corrienteBanco);
            
            ahorroBanco.depositar(1000);
            banco.Transferir(ahorroBanco, corrienteBanco, 300);
            banco.Transferir(ahorroBanco, corrienteBanco, 900);
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
