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

    public long SimularConHilos(Bolillero original, List<int> jugada, long cantidadSimulaciones, int cantidadHilos)
    {
        long aciertos = 0;

        var contador = new object();

        void SimularEnHilo(long inicio, long fin)
        {
            for (long i = inicio; i < fin; i++)
            {
                Bolillero clon = (Bolillero)original.Clon(); 
                if (clon.Jugar(jugada)) 
                {
                    lock (contador)
                    {
                        aciertos++;
                    }
                }
            }
        }

        long simulacionesPorHilo = cantidadSimulaciones / cantidadHilos;
        long sobrantes = cantidadSimulaciones % cantidadHilos; 
        var tareas = new List<Task>();

        
        for (int i = 0; i < cantidadHilos; i++)
        {
            long inicio = i * simulacionesPorHilo;
            long fin = inicio + simulacionesPorHilo + (i < sobrantes ? 1 : 0);

            var tarea = Task.Run(() => SimularEnHilo(inicio, fin));
            tareas.Add(tarea);
        }

        Task.WhenAll(tareas).Wait();

        return aciertos;
    }
}