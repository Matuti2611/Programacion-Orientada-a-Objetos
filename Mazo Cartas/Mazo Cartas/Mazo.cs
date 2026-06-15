using System;
using System.Collections.Generic;

namespace Mazo_Cartas
{
    public class Mazo
    {
        private readonly List<Carta> _cartas;
        private readonly Random _random = new();

        public Mazo()
        {
            _cartas = new List<Carta>();
            string[] valores = { "1", "2", "3", "4", "5", "6", "7", "10", "11", "12" };

            foreach (Palo palo in Enum.GetValues(typeof(Palo)))
            {
                foreach (string valor in valores)
                {
                    _cartas.Add(new Carta(palo, valor));
                }
            }
        }

        public void barajar()
        {
            int n = _cartas.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                Carta valor = _cartas[k];
                _cartas[k] = _cartas[n];
                _cartas[n] = valor;
            }
        }

        public Carta? robarCarta()
        {
            if (_cartas.Count == 0)
            {
                Console.WriteLine("Error: el mazo esta vacio");
                return null;
            }

            Carta cartaRobada = _cartas[^1]; 
            _cartas.RemoveAt(_cartas.Count - 1);
            
            return cartaRobada;
        }

        public int cuantasCartasQuedan()
        {
            return _cartas.Count;
        }
    }
}
