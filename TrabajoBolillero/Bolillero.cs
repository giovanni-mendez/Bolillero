namespace TrabajoBolillero
{
    public class Bolillero
    {
        public List<int> BolillasAdentro { get; set; } = new List<int>();
        public List<int> BolillasAfuera { get; set; } = new List<int>();
        ILogica Logica;

        public int SacarBolillas()
        {
            int bolilla = Logica.SacarBolillas(this);
            BolillasAfuera.Add(bolilla);
            BolillasAdentro.Remove(bolilla);

            return bolilla;
        }

        public int JugarNVeces(List<int> jugada, int cantidad)
        {
            if (cantidad < 0)
            {
                throw new ArgumentException("La cantidad de veces a jugar debe ser no negativa.", nameof(cantidad));
            }

            int aciertos = 0;
            for (int i = 0; i < cantidad; i++)
            {
                ReIngresarBolillas();
                if (Jugar(jugada))
                {
                    aciertos++;
                }
            }
            return aciertos;
        }

        public bool Jugar(IList<int> jugada)
        {
            foreach (var objetivo in jugada)
            {
                int numero = SacarBolillas();
                if (numero != objetivo)
                {
                    ReIngresarBolillas();
                    return false;
                }
            }

            ReIngresarBolillas();

            return true;
        }


        public void ReIngresarBolillas()
        {
            BolillasAdentro.AddRange(BolillasAfuera);
            BolillasAfuera.Clear();
        }
    }

}