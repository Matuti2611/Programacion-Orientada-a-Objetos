namespace Mazo_Cartas
{
    public class Carta
    {
        public Palo palo { get; }
        public string valor { get; }

        public Carta(Palo palo, string valor)
        {
            this.palo = palo;
            this.valor = valor;
        }

        public override string ToString()
        {
            return $"{valor} de {palo}";
        }
    }
}
