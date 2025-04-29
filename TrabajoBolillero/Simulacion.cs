namespace TrabajoBolillero;

public static class Simulacion
{
    public static long SimularSinHilos(Bolillero bolilleroClon, List<int> jugada, int cantidad)
    {
        int aciertos = 0;

        for (int i = 0; i < cantidad; i++)
        {
            Bolillero clon = (Bolillero)bolilleroClon.Clone();

            if (clon.Jugar(jugada))
                aciertos++;
        }

        return aciertos;
    }
}
