using System;

namespace Ej5
{
    public class CuentaBancaria
    {
        protected decimal saldo { get; set; }

        public CuentaBancaria()
        {

        }

        public void depositar(decimal plata)
        {
            if (plata <= 0)
            {
                Console.WriteLine("Error: no puede depositar un monto negativo o nulo");
            }
            else
            {
                saldo += plata;
                Console.WriteLine($"Ha depositado exitosamente ${plata}");
            }
        }

        public virtual bool extraer(decimal plata)
        {
            return false;
        }

        public void mostrarSaldo()
        {
            Console.WriteLine($"Saldo actual: {saldo}");
        }
    }
}
