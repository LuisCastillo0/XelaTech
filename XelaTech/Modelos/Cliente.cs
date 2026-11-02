namespace XelaTech.Modelos;

public class Cliente
{
    public int Codigo { get; private set; }
    public string NombreCompleto { get; private set; }
    public string Telefono { get; private set; }
    public string Correo { get; private set; }
    public string TipoCliente { get; private set; }
    public int CantidadSolicitudes { get; private set; }

    public Cliente(int codigo, string nombreCompleto, string telefono,
                   string correo, string tipoCliente)
    {
        Codigo = codigo;
        NombreCompleto = nombreCompleto;
        Telefono = telefono;
        Correo = correo;
        TipoCliente = tipoCliente;
        CantidadSolicitudes = 0;
    }

    public void RegistrarSolicitud()
    {
        CantidadSolicitudes++;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"Código: {Codigo} | Nombre: {NombreCompleto}");
        Console.WriteLine($"Teléfono: {Telefono} | Correo: {Correo}");
        Console.WriteLine($"Tipo: {TipoCliente} | Solicitudes: {CantidadSolicitudes}");
        Console.WriteLine(new string('-', 45));
    }
}

