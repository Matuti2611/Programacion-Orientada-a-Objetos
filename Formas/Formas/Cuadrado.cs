namespace Formas
{
    public class Cuadrado : Figura
    {
        public double lado { get; set; }

        public Cuadrado(double lado)
        {
            this.lado = lado;
            nombre = "Cuadrado";
        }

        public override double calcularArea()
        {
            return lado * lado;
        }

        public override double calcularPerimetro()
        {
            return 4 * lado;
        }
    }
}
