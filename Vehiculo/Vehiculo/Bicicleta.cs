namespace SimuladorVehiculos
{
    public class Bicicleta : IVehiculo
    {
        private int posicionActual = 0;
        private const int VELOCIDAD = 10;

        public void mover(int tiempo)
        {
            posicionActual += VELOCIDAD * tiempo;
        }

        public int posicion()
        {
            return posicionActual;
        }

        public void reiniciarPosicion()
        {
            posicionActual = 0;
        }
    }
}