namespace SimuladorVehiculos
{
    public class Auto : IVehiculo
    {
        private int posicionActual = 0;
        private int velocidad;
        
        public Auto(int velocidadConfigurada = 40)
        {
            velocidad = velocidadConfigurada;
        }

        public void mover(int tiempo)
        {
            posicionActual += velocidad * tiempo;
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