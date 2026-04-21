namespace SimuladorVehiculos
{
    public class Camion : IVehiculo
    {
        private int posicionActual = 0;
        private const int VELOCIDAD = 30;

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