namespace TrabajoBolillero;

public class Simulacion
{
    public long SimularSinHilos(Bolillero original, List<int> jugada, long cantidad)
    {
        long aciertos = 0;

        for (long i = 0; i < cantidad; i++)
        {
            Bolillero clon = (Bolillero)original.Clon();

            if (clon.Jugar(jugada))
                aciertos++;
        }

        return aciertos;
    }

    public long SimularConHilos(Bolillero bolillero, List<int> jugada, long cantidadSimulaciones, int cantidadHilos)
{      
    //
    long simulacionesPorHilo = cantidadSimulaciones / cantidadHilos;
    long sobrantes = cantidadSimulaciones % cantidadHilos;

    Task<long>[] tareas = new Task<long>[cantidadHilos];

    for (int i = 0; i < cantidadHilos; i++)
    {
        long simulacionesParaEsteHilo = simulacionesPorHilo + (i < sobrantes ? 1 : 0);

        tareas[i] = Task.Run(() =>
        {
            long aciertos = 0;
            Bolillero clon = (Bolillero)bolillero.Clon();

            for (long j = 0; j < simulacionesParaEsteHilo; j++)
            {
                if (clon.Jugar(jugada))
                    aciertos++;
            }

            return aciertos;
        });
    }

    Task.WaitAll(tareas);

    long totalAciertos = tareas.Sum(t => t.Result);
    return totalAciertos;
}
}