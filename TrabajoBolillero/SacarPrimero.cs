namespace TrabajoBolillero;

public class SacarPrimero : ILogica
{
    public int SacarBolillas(Bolillero bolillero)
    {
        return bolillero.BolillasAdentro[0];
    }
}