namespace TrabajoBolillero
{
    public class LogicaAzar : ILogica
    {
        private Random random = new Random();

        public int SacarBolillas(Bolillero bolillero)
        {
            int indice = random.Next(0,bolillero.BolillasAdentro.Count);
            return bolillero.BolillasAdentro[indice];
        }
    }
}
