namespace Formas
{
    public class Triangulo : Figura
    {
        public double lado { get; set; }
        public double altura { get; set; }

        public Triangulo(double lado, double altura)
        {
            this.lado = lado;
            this.altura = altura;
            nombre = "Triangulo";
        }

        public override double calcularArea()
        {
            return (lado * altura) / 2.0;
        }

        public override double calcularPerimetro()
        {
            return 3 * lado;
        }
    }
}
