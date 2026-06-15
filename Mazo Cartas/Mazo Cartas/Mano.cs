using System;
using System.Collections.Generic;

namespace Mazo_Cartas
{
    public class Mano
    {
        private readonly List<Carta> _cartas = new();

        public void recibirCarta(Carta? carta)
        {
            if (carta != null)
            {
                _cartas.Add(carta);
            }
        }

        public void mostrarMano()
        {
            foreach (var carta in _cartas)
            {
                Console.WriteLine($"- {carta}");
            }
        }

        public int cantidadDeCartas()
        {
            return _cartas.Count;
        }
    }
}
