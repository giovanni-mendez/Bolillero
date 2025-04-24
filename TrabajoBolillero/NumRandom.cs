namespace TrabajoBolillero
{
    public class LogicaAzar : ILogica
    {
        private Random random = new Random();

        public int SacarBolillas(Bolillero bolillero)
        {
            if (bolillero.BolillasAdentro.Count == 0)
            {
                throw new InvalidOperationException("No hay bolillas adentro para sacar.");
            }

            int indice = random.Next(bolillero.BolillasAdentro.Count);
            int bolilla = bolillero.BolillasAdentro[indice];
            return bolilla;
        }
    }
}
