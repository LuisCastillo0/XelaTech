using XelaTech.Modelos;
namespace XelaTech.Servicios;

public class Datos
{
    public List<Cliente> Clientes { get; } = new();
    public List<Tecnico> Tecnicos { get; } = new();
    public List<Equipo> Equipos { get; } = new();
    public List<Repuesto> Repuestos { get; } = new();
    public List<Solicitud> Solicitudes { get; } = new();
    public Queue<int> Urgentes { get; } = new();
    public Queue<int> Normales { get; } = new();
    public int[] Calificaciones { get; } = new int[5];
    public const int LimiteTrabajos = 3;
    public Cliente? Cliente(int codigo) => Clientes.Find(x => x.Codigo == codigo);
    public Tecnico? Tecnico(int codigo) => Tecnicos.Find(x => x.Codigo == codigo);
    public Equipo? Equipo(int codigo) => Equipos.Find(x => x.Codigo == codigo);
    public Repuesto? Repuesto(int codigo) => Repuestos.Find(x => x.Codigo == codigo);
    public Solicitud? Solicitud(int codigo) => Solicitudes.Find(x => x.Codigo == codigo);
    public bool Activa(Solicitud s) => s.Estado != "Entregada" && s.Estado != "Cancelada";
    public int[,] MatrizSemanal()
    {
        int[,] matriz = new int[Tecnicos.Count, 7];
        for (int i = 0; i < Tecnicos.Count; i++)
            foreach (var s in Solicitudes)
                if (s.TecnicoCodigo == Tecnicos[i].Codigo)
                {
                    int dia = ((int)(s.FechaAsignacion ?? s.Fecha).DayOfWeek + 6) % 7;
                    matriz[i, dia]++;
                }
        return matriz;
    }
}
