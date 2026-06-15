namespace Vehiculo
{
    public interface IVehiculo
    {
        void mover(int tiempo);
        int posicion();
        void reiniciarPosicion();
    }
}