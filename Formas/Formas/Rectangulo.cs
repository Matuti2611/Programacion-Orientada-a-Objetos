namespace Formas
{
    public class Rectangulo : Figura
    {
        public double baseRectangulo { get; set; }
        public double alturaRectangulo { get; set; }

        public Rectangulo(double baseRectangulo, double alturaRectangulo)
        {
            this.baseRectangulo = baseRectangulo;
            this.alturaRectangulo = alturaRectangulo;
            nombre = "Rectangulo";
        }

        public override double calcularArea()
        {
            return baseRectangulo * alturaRectangulo;
        }

        public override double calcularPerimetro()
        {
            return 2 * (baseRectangulo + alturaRectangulo);
        }
    }
}
