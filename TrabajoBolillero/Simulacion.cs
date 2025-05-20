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

    public async Task<long> SimularConHilosAsync(Bolillero bolillero, List<int> jugada, long cantidadSimulaciones, int cantidadHilos)
    {
        long resutado = await Task<long>.Run(() => SimularConHilos(bolillero, jugada, cantidadSimulaciones, cantidadHilos));
        return resutado;
    }

    public async Task<long> SimularParallelAsync(Bolillero bolillero, List<int> jugada, long cantidadSimulaciones, int cantidadHilos)
    {
        var aciertos = new long[cantidadHilos];

        int simulacionesPorHilo = (int)(cantidadSimulaciones / cantidadHilos);
        long sobrantes = cantidadSimulaciones % cantidadHilos;

        var opciones = new ParallelOptions()
        {
            //Cantidad maxima de tareas en paralelo
            MaxDegreeOfParallelism = cantidadHilos
        };

        await Task<long>.Run(() =>
            Parallel.For(0, cantidadHilos, opciones,
            (i) =>
            {
                int simsParaEsteHilo = simulacionesPorHilo + (i < sobrantes ? 1 : 0);
                Bolillero clon = bolillero.Clon();
                aciertos[i] = clon.JugarNVeces(jugada, simsParaEsteHilo);
            })
        );

        return aciertos.Sum();
    }

}