namespace SimuladorVehiculos
{
    public interface IVehiculo
    {
        void mover(int tiempo);
        int posicion();
        void reiniciarPosicion();
    }
}