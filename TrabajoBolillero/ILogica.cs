namespace TrabajoBolillero;
public interface ILogica
{
    int SacarBolillas(Bolillero bolillero);
    int JugarNVeces(List<int> jugada, int cantidad);
    bool Jugar(IList<int> jugada);
    void ReIngresarBolillas();
}