using TrabajoBolillero;

namespace BolilleroTest;

public class TestBolillero
{
    private Bolillero bolillero;

    public TestBolillero()
    {
        bolillero = new Bolillero(10, new SacarPrimero());
    }

    [Fact]
    public void SacarBolilla()
    {

        int bolilla = bolillero.SacarBolillas();

        Assert.Equal(0, bolilla);

        Assert.Equal(9, bolillero.BolillasAdentro.Count);
        Assert.Single(bolillero.BolillasAfuera);
    }

    [Fact]
    public void ReIngresar()
    {
        bolillero.SacarBolillas();

        bolillero.ReIngresarBolillas();

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

        int ganadas = bolillero.JugarNVeces(jugada, 1);

        Assert.Equal(1, ganadas);
    }

    [Fact]
    public void SimularSinHilos_DeberiaDarAciertosEsperados()
    {
        var bolillero = new Bolillero(10, new SacarPrimero());
        Simulacion sim = new Simulacion();

        var jugada = new List<int> { 0, 1, 2 };

        // Solo se acierta si siempre se sacan 0,1,2 (con SacarPrimero esto ocurre siempre)
        long cantidad = 10;
        long aciertos = sim.SimularSinHilos(bolillero, jugada, cantidad);

        Assert.Equal(cantidad, aciertos);
    }

    [Fact]
    public void SimularConHilos_DeberiaDarAciertosEsperados()
    {
        var bolillero = new Bolillero(10, new SacarPrimero());
        Simulacion sim = new Simulacion();

        var jugada = new List<int> { 0, 1, 2 };

        long cantidad = 10;
        int hilos = 2;
        long aciertos = sim.SimularConHilos(bolillero, jugada, cantidad, hilos);

        Assert.Equal(cantidad, aciertos);
    }

    [Fact]
    public async Task SimularConHilosAsync_DeberiaDevolverUnResultadoEsperado()
    {
        
        Bolillero bolillero = new Bolillero(10, new SacarPrimero()); 
        List<int> jugada = new List<int> { 1, 2, 3 }; 
        long cantidadSimulaciones = 1000;
        int cantidadHilos = 4;

        Simulacion simulacion = new Simulacion();

        
        long resultado = await simulacion.SimularConHilosAsync(bolillero, jugada, cantidadSimulaciones, cantidadHilos);

        
        Assert.InRange(resultado, 0, cantidadSimulaciones);
    }
}
