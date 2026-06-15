using System;

namespace Ej5
{
    class CuentaCorriente : CuentaBancaria
    {
        private decimal limite { get; set; }

        public CuentaCorriente(decimal limite)
        {
            saldo = 0;
            this.limite = limite;
        }

        public override bool extraer(decimal plata)
        {  
            if (plata <= 0)
            {
                Console.WriteLine("no puede extraer dinero no positvo");
                return false;
            }

            if ((saldo-plata) < limite)
            {
                Console.WriteLine("No tiene el dinero suficiente disponible");
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
