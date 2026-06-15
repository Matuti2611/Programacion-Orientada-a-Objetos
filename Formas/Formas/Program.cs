using System;
using System.Collections.Generic;

namespace Formas
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Figura> formas = new List<Figura>();
            formas.Add(new Rectangulo(7, 3));
            formas.Add(new Cuadrado(4));
            formas.Add(new Rectangulo(6, 4));
            formas.Add(new Triangulo(2, 1));
            formas.Add(new Triangulo(6, 7));

            foreach (Figura forma in formas)
            {
                forma.mostrarInformacion();
            }
        }
    }
}
