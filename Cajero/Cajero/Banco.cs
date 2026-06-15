using System;
using System.Collections.Generic;

namespace Ej5
{
    public class Banco
    {
        private List<CuentaBancaria> _cuentas;

        public Banco()
        {
            _cuentas = new List<CuentaBancaria>();
        }

        public void AgregarCuenta(CuentaBancaria cuenta)
        {
            if (!_cuentas.Contains(cuenta))
            {
                _cuentas.Add(cuenta);
                Console.WriteLine("Cuenta registrada exitosamente en el Banco.");
            }
        }

        public void Transferir(CuentaBancaria origen, CuentaBancaria destino, decimal monto)
        {
            Console.WriteLine($"\nIntentando transferencia de ${monto}...");

            if (!_cuentas.Contains(origen) || !_cuentas.Contains(destino))
            {
                Console.WriteLine("Fallo de transferencia: Ambas cuentas deben estar registradas en el banco.");
                return;
            }

            if (monto <= 0)
            {
                Console.WriteLine("Fallo de transferencia: El monto debe ser mayor a cero.");
                return;
            }

            if (origen.extraer(monto))
            {
                destino.depositar(monto);
                Console.WriteLine("Transferencia completada con éxito.");
            }
            else
            {
                Console.WriteLine("Fallo de transferencia: La cuenta origen rechazó la operación.");
            }
        }
    }
}
