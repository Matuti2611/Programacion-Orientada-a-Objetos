using System;

namespace Formas
{
    public class Figura : IForma
    {
        protected string nombre { get; set; } = "";

        public Figura() { }

        public virtual double calcularArea()
        {
            return 0;
        }

        public virtual double calcularPerimetro()
        {
            return 0;
        }

        public void mostrarInformacion() 
        {
            Console.WriteLine($"{nombre}: Área = {calcularArea()}, Perímetro = {calcularPerimetro()}");
        }
    }
}
