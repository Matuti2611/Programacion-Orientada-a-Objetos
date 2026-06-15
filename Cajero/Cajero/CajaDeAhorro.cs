using System;

namespace Ej5
{
    class CajaDeAhorro : CuentaBancaria
    {
        public CajaDeAhorro()
        {
            saldo = 0;
        }

        public override bool extraer(decimal plata)
        {
            if (plata <= 0 || (saldo-plata) < 0)
            {
                Console.WriteLine("ERROR");
                return false;
            }

            else
            {
                saldo -= plata;
                Console.WriteLine($"Extrajo con exito ${plata}");
               
                return true;
            }
        }
    }
}
