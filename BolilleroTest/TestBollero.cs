namespace BolilleroTest;

public class TestBolillero
{
        private Bolillero bolillero;

        public BolilleroTests()
        {
            bolillero = new Bolillero(10);
            bolillero = new SacarBolillas(); 
        }

        [Fact]
        public void SacarBolilla()
        {
            int bolilla = bolillero.SacarBolilla();
            Assert.Equal(0, bolilla); 

            Assert.Equal(9, bolillero.BolillasAdentro.Count);
            Assert.Single(bolillero.BolillasAfuera);
        }

        [Fact]
        public void ReIngresar()
        {
            bolillero.SacarBolilla();
            bolillero.ReIngresar();

            Assert.Equal(10, bolillero.BolillasAdentro.Count);
            Assert.Empty(bolillero.BolillasAfuera);
        }

        [Fact]
        public void JugarGana()
        {
            var jugada = new List<int> { 0, 1, 2, 3 };
            bool gano = bolillero.Jugar(jugada);
            Assert.True(gano);
        }

        [Fact]
        public void JugarPierde()
        {
            var jugada = new List<int> { 4, 2, 1 };
            bool gano = bolillero.Jugar(jugada);
            Assert.False(gano);
        }

        [Fact]
        public void GanarNVeces()
        {
            var jugada = new List<int> { 0, 1 };
            int ganadas = bolillero.GanarNVeces(jugada, 1);
            Assert.Equal(1, ganadas);
        }
}
