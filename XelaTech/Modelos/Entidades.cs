namespace XelaTech.Modelos;

public class Cliente
{
    public int Codigo { get; set; }
    public string Nombre { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Correo { get; set; } = "";
    public string Tipo { get; set; } = "";
    public int CantidadSolicitudes { get; set; }
}

public class Tecnico
{
    public int Codigo { get; set; }
    public string Nombre { get; set; } = "";
    public string Especialidad { get; set; } = "";
    public bool Disponible { get; set; } = true;
    public int TrabajosActivos { get; set; }
    public int Finalizados { get; set; }
    public int SumaCalificaciones { get; set; }
    public int NumeroCalificaciones { get; set; }
    public double Promedio => NumeroCalificaciones == 0 ? 0 : (double)SumaCalificaciones / NumeroCalificaciones;
}

public abstract class Equipo
{
    public int Codigo { get; set; }
    public string Serie { get; set; } = "";
    public string Marca { get; set; } = "";
    public string Modelo { get; set; } = "";
    public int ClienteCodigo { get; set; }
    public string EstadoFisico { get; set; } = "";
    public string Accesorios { get; set; } = "";
    public abstract string Tipo { get; }
    public virtual decimal CostoRevision() => 50;
}

public class Portatil : Equipo { public override string Tipo => "Portatil"; public override decimal CostoRevision() => 65; }
public class Escritorio : Equipo { public override string Tipo => "Escritorio"; public override decimal CostoRevision() => 55; }
public class Impresora : Equipo { public override string Tipo => "Impresora"; public override decimal CostoRevision() => 45; }

public class Repuesto
{
    public int Codigo { get; set; }
    public string Nombre { get; set; } = "";
    public string Categoria { get; set; } = "";
    public int Existencia { get; set; }
    public int Minima { get; set; }
    public decimal CostoUnitario { get; set; }
    public decimal PrecioVenta { get; set; }
    public int Utilizados { get; set; }
}

public class Solicitud
{
    public int Codigo { get; set; }
    public int ClienteCodigo { get; set; }
    public int EquipoCodigo { get; set; }
    public string Problema { get; set; } = "";
    public DateTime Fecha { get; set; } = DateTime.Now;
    public bool Urgente { get; set; }
    public string Estado { get; set; } = "Recibida";
    public int? TecnicoCodigo { get; set; }
    public DateTime? FechaAsignacion { get; set; }
    public int? Calificacion { get; set; }
    public string Diagnostico { get; set; } = "";
    public decimal ManoObra { get; set; }
    public decimal CostoRepuestos { get; set; }
    public int TiempoEstimado { get; set; }
    public List<string> RepuestosUsados { get; } = new();
    public Stack<string> Historial { get; } = new();
    public decimal Total => ManoObra + CostoRepuestos;
}
